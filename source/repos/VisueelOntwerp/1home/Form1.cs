using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Database
using MySql.Data.MySqlClient;
using MySql.Data;

namespace _1home
{
    public partial class Form1 : Form
    {
        private static string server = "localhost";
        private static string database = "Laagspanningsnet";
        private static string user = "root";
        private static string password = "root";
        private static MySqlConnection connectie = new MySqlConnection(connectiestring);
        private static string connectiestring = "SERVER=" + server +
            ";DATABASE=" + database + ";UID=" + user +
            ";PASSWORD=" + password + ";";

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //GetData("SELECT * FROM laagspanningsnet.aansluitingen WHERE AP_id = 'T8';");
            GetData("SELECT AP_id AS Transfo, AP_locatie AS Locatie FROM laagspanningsnet.aansluitpunten WHERE AP_id LIKE 'T%';");
        }

        private void GetData(string mysql)
        {
            connectie = new MySqlConnection(connectiestring);
            try
            {
                connectie.Open();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Geen verbinding met de server.");
                        break;
                    case 1045:
                        MessageBox.Show("Gebruikersnaam of Wachtwoord fouttief.");
                        break;
                }
                Application.Exit();
            }

            string query = mysql;// "SELECT AP_id AS Transfo, AP_locatie AS Locatie FROM laagspanningsnet.aansluitpunten WHERE AP_id LIKE 'T%';";
            // MySqlCommand cmd = new MySqlCommand(query, connectie);
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connectie);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            // BindingSource bSource = new BindingSource();
            // DataTable data = new DataTable();
            // dataGridView.DataSource = bSource;
            dataGridView1.DataSource = ds.Tables[0];
            connectie.Close();

            int c = 0;
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    dataGridView1.Rows[c].Cells[0] = new DataGridViewButtonCell();
                    c++;
                }
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
            Console.WriteLine(dataGridView1.CurrentCell.Value.ToString());
            GetData("SELECT * FROM laagspanningsnet.aansluitingen WHERE AP_id = '" + dataGridView1.CurrentCell.Value.ToString() + "';");
        }
    }
}

/*
using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private static string server = "localhost";
        private static string database = "hardwareshop";
        private static string user = "root";
        private static string password = "root";
        private static MySqlConnection connectie = new MySqlConnection(connectiestring);
        private static string connectiestring = "SERVER=" + server +
            ";DATABASE=" + database + ";UID=" + user +
            ";PASSWORD=" + password + ";";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            // Openen van de connectie
            if (OpenenConnectie())
            {
                lblStatusInhoud.Text = "Geconnecteerd";
                lblStatusInhoud.ForeColor = Color.Green;
            }
            else
            {
                lblStatusInhoud.Text = "Geen connectie";
                lblStatusInhoud.ForeColor = Color.Red;
            }

            UitlezenVoorDatagrid();
            
        }

        private void UitlezenVoorDatagrid()
        {
            string query = "SELECT * FROM klant;";
            // MySqlCommand cmd = new MySqlCommand(query, connectie);
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connectie);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            // BindingSource bSource = new BindingSource();
            // DataTable data = new DataTable();
            // dataGridView.DataSource = bSource;
            dataGridView.DataSource = ds.Tables[0];
            connectie.Close();
        }


        private void Uitlezen()
        {
            string query = "SELECT * FROM klant;";
            MySqlCommand cmd = new MySqlCommand(query, connectie);
            var mdr = cmd.ExecuteReader();  // computer bekijk zelf maar wat voor gegevens het zijn.
            while (mdr.Read())
            {
                listBox.Items.Add(mdr.GetString("voornaam"));
                //  lblStatusInhoud.Text = mdr.GetString("voornaam");
                // listView.Items.Add(mdr.GetString("voornaam"));



            }
            
        }

        private bool SluitenConnectie()
        {
            try
            {
                connectie.Close();
                return true;
            } catch (MySqlException ex)
            {
                return false;
            }
        }

        private bool OpenenConnectie()
        { 
            connectie = new MySqlConnection(connectiestring);
            try {
                connectie.Open();
                return true;
            } catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Geen verbinding met de server.");
                        break;
                    case 1045:
                        MessageBox.Show("Gebruikersnaam of Wachtwoord fouttief.");
                        break;
                }
                return false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Sluiten van de connectie
            if (SluitenConnectie()) {
                lblStatusInhoud.Text = "Connectie afgesloten";
                lblStatusInhoud.ForeColor = Color.Orange;
            } else
            {
                lblStatusInhoud.Text = "Afsluiten mislukt";
                lblStatusInhoud.ForeColor = Color.Red;
            }

        }
    }
}

 */

