namespace LUS
{
    partial class Frm_LUS_Report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.cboProgram = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.Location = new System.Drawing.Point(9, 93);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(776, 348);
            this.reportViewer1.TabIndex = 0;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(13, 56);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker.TabIndex = 1;
            // 
            // cboProgram
            // 
            this.cboProgram.FormattingEnabled = true;
            this.cboProgram.Items.AddRange(new object[] {
            "All Programs",
            "Bachelor of Science in Computer Engineering",
            "Bachelor of Science in Information Technology",
            "Bachelor of Science in Computer Science ",
            "Associate in Information Technology",
            "Associate in Computer Science",
            "Bachelor of Arts ",
            "Bachelor of Science in Business Administration",
            "Bachelor of Elementary Education ",
            "BACHELOR OF SECONDARY EDUCATION",
            "Diploma in Fisheries Technology",
            "Bachelor of Agricultural Technology",
            "Senior High ",
            "Laboratory High School",
            "Bachelor of Science in Agriculture",
            "Bachelor of Science in Nursing ",
            "Master of Arts in Language Teaching Filipino",
            "Doctor of Education",
            "Doctor of Public Administration",
            "Master of Public Administration",
            "Master of Business Administration ",
            "Master of Arts in Education ",
            "Master of Arts in Teaching Mathematics",
            "Master of Art in Language Teaching English",
            "Master of Science in Agriculture ",
            "Master of Arts in Social Science ",
            "Master of Arts in Language Teaching Filipino",
            "BS BIO",
            "BACHELOR OF ARTS IN HISTORY",
            "Bachelor of Science in Information System",
            "Master of Arts in Nursing",
            "BACHELOR OF SCIENCE IN BIOLOGY",
            "BACHELOR OF EARLY CHILDHOOD EDUCATION",
            "BACHELOR OF TECHNOLOGY AND LIVELIHOOD EDUCATION",
            "BACHELOR OF ARTS IN ENGLISH LANGUAGES STUDIES",
            "BACHELOR OF ARTS IN ISLAMIC STUDIES",
            "BACHELOR OF SCIENCE IN BUSINESS ADMINISTRATION MAJOR IN MARKETING",
            "BACHELOR OF SCIENCE IN BUSINESS ADMINISTRATION",
            "BS CRIMINOLOGY",
            "BS Social Work",
            "Bachelor of Public Administration",
            "Bachelor of Science in Tourism Management",
            "Bachelor of Science in Hospitality Management",
            "Bachelor of Science in Food Technology",
            "Doctor of Philosophy In Language Teaching English",
            "Master in Information Technology",
            "BACHELOR OF SCIENCE IN COMPUTER ENGINEERING",
            "Bachelor of Arts in English Language (2024-2025)",
            "PHYSICAL EDUCATION"});
            this.cboProgram.Location = new System.Drawing.Point(219, 56);
            this.cboProgram.Name = "cboProgram";
            this.cboProgram.Size = new System.Drawing.Size(485, 21);
            this.cboProgram.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(710, 55);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(216, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Program";
            // 
            // Frm_LUS_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cboProgram);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Frm_LUS_Report";
            this.Text = "Stalwart Library Utilization System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_LUS_Report_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.ComboBox cboProgram;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}