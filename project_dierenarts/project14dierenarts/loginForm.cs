using _15_1_FirmaBruinsma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project14dierenarts
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        // close button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            // open db connection
            OleDbDataReader dbread = null;
            OleDbConnection connection = new OleDbConnection();
            Globaal.Connection.Open();

            // query
            string sql = @"SELECT * FROM medewerker WHERE gebruikerid = @gebruikerid AND wachtwoord = @wachtwoord";

            OleDbCommand dbcom = new OleDbCommand(sql, Globaal.Connection);

            // username parameter
            OleDbParameter paramUsername = new OleDbParameter();
            paramUsername.ParameterName = "@gebruikerid";
            paramUsername.Value = loginUsername.Text;

            // password parameter
            OleDbParameter paramPassword = new OleDbParameter();
            paramPassword.ParameterName = "@wachtwoord";
            paramPassword.Value = loginPassword.Text;

            // add parameters
            dbcom.Parameters.Add(paramUsername);
            dbcom.Parameters.Add(paramPassword);

            // execute
            dbread = dbcom.ExecuteReader();

            if (dbread.Read()) // if credentials correct
            {
                // db close
                dbread.Close();
                dbcom.Dispose();
                Globaal.Connection.Close();

                loginUsername.ForeColor = Color.Green;
                loginPassword.ForeColor = Color.Green;
                loginButton.BackColor = Color.Green;

                await Task.Delay(1000);

                // to overzichtForm
                this.Hide();
                overzichtForm frm = new overzichtForm();
                frm.ShowDialog();
            }
            else // if not correct
            {
                incorrectLabel.Visible = true;
                loginPassword.ForeColor = Color.Red;
                loginUsername.ForeColor = Color.Red;

                // db close
                dbread.Close();
                dbcom.Dispose();
                Globaal.Connection.Close();
            }
        }











        // styling & functional (not db related)
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            loginUsername.Focus();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            loginPassword.Focus();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            loginUsername.Focus();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            loginPassword.Focus();
        }

        private void loginUsername_Enter(object sender, EventArgs e)
        {
            userPlaceholder.Visible = false;
            loginUsername.Text = "";
        }

        private void loginPassword_Enter(object sender, EventArgs e)
        {
            passPlaceholder.Visible = false;
            loginPassword.Text = "";
        }

        private void userPlaceholder_Click(object sender, EventArgs e)
        {
            loginUsername.Focus();
        }

        private void passPlaceholder_Click(object sender, EventArgs e)
        {
            loginPassword.Focus();
        }

        private void loginUsername_Leave(object sender, EventArgs e)
        {
            if (loginUsername.Text == "")
            {
                userPlaceholder.Visible = true;
            }
        }
        private void loginPassword_Leave(object sender, EventArgs e)
        {
            if (loginPassword.Text == "")
            {
                passPlaceholder.Visible = true;
            }
        }
        private void loginUsername_TextChanged(object sender, EventArgs e)
        {
            incorrectLabel.Visible = false;
            loginUsername.ForeColor = Color.FromArgb(1, 58, 75, 115);
            loginPassword.ForeColor = Color.FromArgb(1, 58, 75, 115);
        }

        private void loginPassword_TextChanged(object sender, EventArgs e)
        {
            incorrectLabel.Visible = false;
            loginUsername.ForeColor = Color.FromArgb(1, 58, 75, 115);
            loginPassword.ForeColor = Color.FromArgb(1, 58, 75, 115);
        }


        // draggable (totaal niet van stackoverflow gekopieerd)
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void loginForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void loginForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}