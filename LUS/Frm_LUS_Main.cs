using AutoUpdaterDotNET;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.BC;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LUS
{
    public partial class Frm_LUS_Main : Form
    {
        // ======== Double Scan Protection Variables ========
        private bool scanLocked = false;
        private Timer scanTimer;
        private int scanCountdown = 3;

        // 🔥 Track last scanned ID
        private string lastScannedID = "";

        public Frm_LUS_Main()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        // ============================================================
        //   SETUP COUNTDOWN TIMER (3 seconds)
        // ============================================================
        private void SetupScanTimer()
        {
            scanTimer = new Timer();
            scanTimer.Interval = 1000;

            scanTimer.Tick += (s, e) =>
            {
                scanCountdown--;

                lblRecord.Visible = true;
                lblRecord.Text = $"Please wait... {scanCountdown} sec";

                if (scanCountdown <= 0)
                {
                    scanLocked = false;
                    scanCountdown = 3;
                    scanTimer.Stop();
                    lblRecord.Visible = false;
                    lastScannedID = ""; // reset last scanned ID after unlock
                }
            };
        }

        private void LoadCurrentCount()
        {
            try
            {
                using (MySqlConnection conn = Connection.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT COUNT(*)
                FROM attendance_logs
                WHERE log_date = CURDATE()
                  AND time_in IS NOT NULL
                  AND time_out IS NULL";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        lblCount.Text = count.ToString("D2");   // currently inside
                        lblTotal.Text = count.ToString("D2");   // if total == inside
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading count: " + ex.Message);
            }
        }

        // ============================================================
        //   PROCESS SCAN (MAIN LOGIC)
        // ============================================================
        private void ProcessScan()
        {
            string currentID = txtID.Text.Trim();

            // SAME ID tapped again within countdown → show wait message
            if (scanLocked && currentID == lastScannedID)
            {
                lblRecord.Visible = true;
                lblRecord.Text = $"Please wait... {scanCountdown} sec";
                txtID.Clear();
                return;
            }

            // DIFFERENT ID during countdown → ALLOW scanning normally
            if (scanLocked && currentID != lastScannedID)
            {
                // Unlock immediately for new student
                scanLocked = false;
                scanTimer.Stop();
                lblRecord.Visible = false;
                scanCountdown = 3;
            }

            // FIRST scan or different student → lock AFTER this scan
            scanLocked = true;
            scanCountdown = 3;
            scanTimer.Start();

            lblRecord.Visible = false;

            if (currentID.Length < 3) return;

            // Save last scanned ID
            lastScannedID = currentID;

            using (MySqlConnection conn = Connection.GetConnection())
            {
                conn.Open();

                // Check student
                string checkStudent = @"
                    SELECT lastname, firstname, middleinitial
                    FROM students
                    WHERE id_number = @id";

                MySqlCommand cmd = new MySqlCommand(checkStudent, conn);
                cmd.Parameters.AddWithValue("@id", currentID);

                MySqlDataReader dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    dr.Close();
                    lblRecord.Visible = true;
                    lblRecord.Text = "ID NOT FOUND";

                    this.BeginInvoke(new Action(() =>
                    {
                        txtID.Clear();
                        txtID.Focus();
                    }));

                    return;
                }

                string lastname = dr["lastname"].ToString();
                string firstname = dr["firstname"].ToString();
                dr.Close();

                // Check last log today
                string checkLastLog = @"
                    SELECT log_id, time_in, time_out
                    FROM attendance_logs
                    WHERE id_number = @id AND log_date = CURDATE()
                    ORDER BY log_id DESC
                    LIMIT 1";

                MySqlCommand cmd2 = new MySqlCommand(checkLastLog, conn);
                cmd2.Parameters.AddWithValue("@id", currentID);

                MySqlDataReader dr2 = cmd2.ExecuteReader();
                bool hasLog = dr2.Read();
                int lastLogId = hasLog ? Convert.ToInt32(dr2["log_id"]) : 0;
                bool hasTimeIn = hasLog && dr2["time_in"] != DBNull.Value;
                bool hasTimeOut = hasLog && dr2["time_out"] != DBNull.Value;
                dr2.Close();

                // TIME-IN (first scan today)
                if (!hasLog)
                {
                    string insertQuery = @"
                        INSERT INTO attendance_logs (id_number, time_in, log_date)
                        VALUES (@id, NOW(), CURDATE())";

                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@id", currentID);
                    insertCmd.ExecuteNonQuery();

                    lblCount.Text = (int.Parse(lblCount.Text) + 1).ToString("D2");
                    lblTotal.Text = (int.Parse(lblTotal.Text) + 1).ToString("D2");
                    tssMonitor.Text = $"TIME-IN: {firstname} {lastname}";
                }
                else
                {
                    // TIME-OUT
                    if (hasTimeIn && !hasTimeOut)
                    {
                        string updateQuery = @"
                            UPDATE attendance_logs
                            SET time_out = NOW()
                            WHERE log_id = @log_id";

                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@log_id", lastLogId);
                        updateCmd.ExecuteNonQuery();

                        lblCount.Text = Math.Max(0, int.Parse(lblCount.Text) - 1).ToString("D2");
                        //lblTotal.Text = (int.Parse(lblTotal.Text) + 1).ToString("D2");
                        tssMonitor.Text = $"TIME-OUT: {firstname} {lastname}";
                    }
                    else
                    {
                        // DUPLICATE ENTRY → NEW TIME-IN
                        string insertAgainQuery = @"
                            INSERT INTO attendance_logs (id_number, time_in, log_date)
                            VALUES (@id, NOW(), CURDATE())";

                        MySqlCommand insertAgainCmd = new MySqlCommand(insertAgainQuery, conn);
                        insertAgainCmd.Parameters.AddWithValue("@id", currentID);
                        insertAgainCmd.ExecuteNonQuery();

                        lblCount.Text = (int.Parse(lblCount.Text) + 1).ToString("D2");
                        lblTotal.Text = (int.Parse(lblTotal.Text) + 1).ToString("D2");
                        tssMonitor.Text = $"NEW TIME-IN: {firstname} {lastname}";
                    }
                }

                this.BeginInvoke(new Action(() =>
                {
                    txtID.Clear();
                    txtID.Focus();
                }));
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                HandleEscape();
                return true; // mark as handled
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void HandleEscape()
        {
            ExitConfirmation();
        }

        private void ExitConfirmation()
        {
            string msg = "Are you sure you want to exit the application?";
            DialogResult result = MessageBox.Show(msg, "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void Frm_LUS_Main_Load_1(object sender, EventArgs e)
        {
            using (MySqlConnection conn = Connection.GetConnection())
            {
                try
                {
                    conn.Open();
                    tssConnecion.Text = "Database connected";
                }
                catch (Exception ex)
                {
                    tssConnecion.Text = "Connection failed: " + ex.Message;
                }
            }

            LoadCurrentCount();
            SetupScanTimer();
        }

        private void Frm_LUS_Main_Activated(object sender, EventArgs e)
        {
            txtID.Focus();
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                ProcessScan();
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start("https://raw.githubusercontent.com/ridzhassan/Library-Utilization-System/main/update.xml");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitConfirmation();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LUS_Report frm_report = new Frm_LUS_Report();
            frm_report.ShowDialog();
        }
    }
}