using System;
using System.Windows.Forms;

namespace LUS
{
    public partial class Frm_Loading : Form
    {
        public Frm_Loading()
        {
            InitializeComponent();
        }

        public Frm_Loading(string message) : this()
        {
            lblMessage.Text = message;
        }

        public void SetMessage(string message)
        {
            if (lblMessage.InvokeRequired)
            {
                lblMessage.Invoke(new Action(() => lblMessage.Text = message));
            }
            else
            {
                lblMessage.Text = message;
            }
        }
    }
}

