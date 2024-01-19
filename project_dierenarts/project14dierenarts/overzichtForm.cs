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
    public partial class overzichtForm : Form
    {
        public overzichtForm()
        {
            InitializeComponent();

            ophalenKlanten();

            berekenBehandelingen();
        }





        // berekenen van behandelingen
        private void berekenBehandelingen()
        {
            OleDbDataReader dbread = null;
            OleDbConnection connection = new OleDbConnection();
            Globaal.Connection.Open();

            string sql = "SELECT COUNT(*) FROM behandelregel";

            OleDbCommand dbcom = new OleDbCommand(sql, Globaal.Connection);

            dbread = dbcom.ExecuteReader();

            while (dbread.Read())
            {
                behandelingenBox.Text = dbread.GetValue(0).ToString();
            }

            dbread.Close();
            dbcom.Dispose();
            Globaal.Connection.Close();
        }






        // klantgegevens ophalen/verversen
        private void ophalenKlanten()
        {
            // datagrid krijg ik niet werkend :(

            OleDbDataReader dbread = null;
            OleDbConnection connection = new OleDbConnection();
            Globaal.Connection.Open();

            ListBox[] listBoxes = new ListBox[]
            {
                listBox1,
                listBox2,
                listBox3,
                listBox4,
                listBox5
            };

            for (int i = 0; i <= 4; i++)
            {
                string sql = "SELECT * FROM klanten";

                OleDbCommand dbcom = new OleDbCommand(sql, Globaal.Connection);
                dbread = dbcom.ExecuteReader();

                listBoxes[i].Items.Clear();
                while (dbread.Read())
                {
                    listBoxes[i].Items.Add(dbread.GetValue(i).ToString());
                }

                dbread.Close();
                dbcom.Dispose();
            }

            Globaal.Connection.Close();
        }





        // invullen informatie van klant in aanpassen groupbox
        private void klantAanpassen(int klantid)
        {
            OleDbDataReader dbread = null;
            OleDbConnection connection = new OleDbConnection();
            Globaal.Connection.Open();

            string sql = "SELECT * FROM klanten WHERE klantid = @klantid";

            OleDbCommand dbcom = new OleDbCommand(sql, Globaal.Connection);

            OleDbParameter paramKlantid = new OleDbParameter();
            paramKlantid.ParameterName = "@klantid";
            paramKlantid.Value = klantid;
            dbcom.Parameters.Add(paramKlantid);

            dbread = dbcom.ExecuteReader();

            while (dbread.Read())
            {
                string klantvoorletters = dbread.GetValue(1).ToString();
                string klantachternaam = dbread.GetValue(2).ToString();
                string klantadres = dbread.GetValue(3).ToString();
                string klantwoonplaats = dbread.GetValue(4).ToString();

                klantidBox1.Text = klantid.ToString();
                klantvoorlettersBox1.Text = klantvoorletters;
                klantachternaamBox1.Text = klantachternaam;
                klantadresBox1.Text = klantadres;
                klantwoonplaatsBox1.Text = klantwoonplaats;
            }

            dbread.Close();
            dbcom.Dispose();
            Globaal.Connection.Close();
        }






        // wijziging van klantgegevens doorvoeren
        private void klantWijzigen()
        {
            // niet echt nodig maar toch voor duidelijkheid
            string klantid = klantidBox1.Text;
            string klantvoorletters = klantvoorlettersBox1.Text;
            string klantachternaam = klantachternaamBox1.Text;
            string klantadres = klantadresBox1.Text;
            string klantwoonplaats = klantwoonplaatsBox1.Text;

            OleDbConnection connection = new OleDbConnection();
            Globaal.Connection.Open();

            // update/edit query
            string sql = "UPDATE klanten SET " +
                "klantvoorletters = @klantvoorletters, " +
                "klantachternaam = @klantachternaam, " +
                "klantadres = @klantadres, " +
                "klantwoonplaats = @klantwoonplaats " +
                "WHERE klantid = @klantid";

            OleDbCommand dbcom = new OleDbCommand(sql, Globaal.Connection);

            // parameters toevoegen
            dbcom.Parameters.AddWithValue("@klantvoorletters", klantvoorletters);
            dbcom.Parameters.AddWithValue("@klantachternaam", klantachternaam);
            dbcom.Parameters.AddWithValue("@Wklantadres", klantadres);
            dbcom.Parameters.AddWithValue("@klantwoonplaats", klantwoonplaats);
            dbcom.Parameters.AddWithValue("@klantid", klantid);

            dbcom.ExecuteNonQuery();

            dbcom.Dispose();
            Globaal.Connection.Close();

            // klantendata verversen
            ophalenKlanten();
        }





        // het verwijderen van klantgegevens doorvoeren
        private void klantVerwijderen()
        {
            string klantid = klantidBox1.Text;

            OleDbConnection connection = new OleDbConnection();
            Globaal.Connection.Open();

            // verwijderen query
            string sql = "DELETE FROM klanten WHERE klantid = @klantid";

            OleDbCommand dbcom = new OleDbCommand(sql, Globaal.Connection);

            dbcom.Parameters.AddWithValue("@klantid", klantid);

            dbcom.ExecuteNonQuery();

            dbcom.Dispose();
            Globaal.Connection.Close();

            // klantendata verversen
            ophalenKlanten();
        }





        // nieuwe klantgegevens verwerken
        private void klantToevoegen()
        {
            string klantvoorletters = klantvoorlettersBox2.Text;
            string klantachternaam = klantachternaamBox2.Text;
            string klantadres = klantadresBox2.Text;
            string klantwoonplaats = klantwoonplaatsBox2.Text;

            OleDbConnection connection = new OleDbConnection();
            Globaal.Connection.Open();

            // update/edit query
            string sql = "INSERT INTO klanten (" +
                "klantvoorletters, klantachternaam, klantadres, klantwoonplaats" +
                ") VALUES (" +
                "@klantvoorletters, @klantachternaam, @klantadres, @klantwoonplaats" +
                ")";

            OleDbCommand dbcom = new OleDbCommand(sql, Globaal.Connection);

            // parameters toevoegen
            dbcom.Parameters.AddWithValue("@klantvoorletters", klantvoorletters);
            dbcom.Parameters.AddWithValue("@klantachternaam", klantachternaam);
            dbcom.Parameters.AddWithValue("@Wklantadres", klantadres);
            dbcom.Parameters.AddWithValue("@klantwoonplaats", klantwoonplaats);

            dbcom.ExecuteNonQuery();

            dbcom.Dispose();
            Globaal.Connection.Close();

            // klantendata verversen
            ophalenKlanten();
        }










        // hieronder is geen db code meer

        // listbox sync
        public bool bezig = false;
        private void regelAanpassen(int i)
        {
            if (bezig || i == -1)
            {
                return;
            }

            bezig = true;
            ListBox[] listBoxes = new ListBox[]
            {
                listBox1,
                listBox2,
                listBox3,
                listBox4,
                listBox5,
            };

            for (int index = 0; index < listBoxes.Length; index++)
            {
                listBoxes[index].SelectedIndex = i;

            }

            int klantid = int.Parse(listBox1.Items[i].ToString());
            klantAanpassen(klantid);

            bezig = false;
        }
       


        private void buttonWijzig_Click(object sender, EventArgs e)
        {
            klantWijzigen();
        }

        private void buttonVerwijder_Click(object sender, EventArgs e)
        {
            klantVerwijderen();
        }

        private void buttonVoegToe_Click(object sender, EventArgs e)
        {
            klantToevoegen();
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            regelAanpassen(listBox5.SelectedIndex);
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            regelAanpassen(listBox4.SelectedIndex);
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            regelAanpassen(listBox3.SelectedIndex);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            regelAanpassen(listBox2.SelectedIndex);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            regelAanpassen(listBox1.SelectedIndex);
        }
            
        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void closePanel_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void helpPanel_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Klantoverzicht:\n" +
                "- Klik op een record om hem te kunnen aanpassen\n" +
                "\n" +
                "Klant aanpassen:\n" +
                "- Wijzig klantdata door op wijzigen de klikken\n" +
                "- Verwijder klantdata door op verwijderen te klikken\n" +
                "\n" +
                "Klant toevoegen:\n" +
                "- Klik op toevoegen om een klant toe te voegen");
        }






        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void overzichtForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void overzichtForm_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void overzichtForm_Load(object sender, EventArgs e)
        {

        }
    }
}
