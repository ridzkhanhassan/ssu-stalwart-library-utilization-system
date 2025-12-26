using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace LUS
{
    public partial class Frm_LUS_Report : Form
    {
        public Frm_LUS_Report()
        {
            InitializeComponent();
        }

        private void Frm_LUS_Report_Load(object sender, EventArgs e)
        {
            reportViewer1.RefreshReport();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string date = dateTimePicker.Value.ToString("yyyy-MM-dd");
                string program = cboProgram.Text.ToString();

                DataTable dt = new DataTable();

                using (MySqlConnection conn = Connection.GetConnection())
                {
                    conn.Open();

                    if (program == "All Programs")
                    {
                        program = "%"; // Use wildcard for all programs
                    }

                    string query = @"
                                    SELECT 
                                        s.id_number,
                                        CONCAT(
                                            UPPER(LEFT(s.lastname, 1)),
                                            LOWER(SUBSTRING(s.lastname, 2))
                                        ) AS lastname,
                                        CONCAT(
                                            UPPER(LEFT(s.firstname, 1)),
                                            LOWER(SUBSTRING(s.firstname, 2))
                                        ) AS firstname,
                                        CONCAT(
                                            UPPER(LEFT(s.middleinitial, 1)),
                                            LOWER(SUBSTRING(s.middleinitial, 2))
                                        ) AS middleinitial,
                                        s.gender,
                                        s.program,
                                        al.time_in,
                                        al.time_out,
                                        al.log_date
                                    FROM students s
                                    INNER JOIN attendance_logs al 
                                        ON s.id_number = al.id_number
                                    WHERE al.log_date = @date
                                      AND s.program LIKE @program
                                    ORDER BY al.time_in ASC ";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@date", date);
                    da.SelectCommand.Parameters.AddWithValue("@program", program);
                    da.Fill(dt);
                }

                reportViewer1.Reset();
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Daily.rdlc";


                ReportDataSource rds = new ReportDataSource("LibraryDataSet", dt);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Print layout
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show("No Record Found" + ex);
            }
        }
    }
}
