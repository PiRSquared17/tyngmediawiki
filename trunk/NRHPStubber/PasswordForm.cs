using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NRHPStubber
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        public string PasswordText
        {
            get
            {
                return txtPassword.Text;
            }
            set
            {
                txtPassword.Text = value;
            }
        }

        public static DialogResult Show(out string passwordText)
        {
            PasswordForm f = new PasswordForm();
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
                passwordText = f.PasswordText;
            else
                passwordText = null;
            return result;
        }

        private void PasswordForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}