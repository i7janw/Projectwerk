using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// usings ivm. mySQL
using MySql.Data.MySqlClient;
using MySql.Data;

namespace K810a
{
    public partial class Form1 : Form
    {
        private static string server = "localhost";
        private static string database = "laagspanningsnet";
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
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("MyTable");

            ds.Tables.Add(dt);
            dgvAansluitingen.DataSource = ds.Tables[0];


            dt.Columns.Add(new DataColumn("Kring", typeof(string)));
            dt.Columns.Add(new DataColumn("Nummer", typeof(string)));
            dt.Columns.Add(new DataColumn("Omschrijving", typeof(string)));
            dt.Columns.Add(new DataColumn("Kabeltype", typeof(string)));
            dt.Columns.Add(new DataColumn("Kabel sectie", typeof(string)));
            dt.Columns.Add(new DataColumn("Amp", typeof(string)));
            dt.Columns.Add(new DataColumn("Aant.polen", typeof(string)));

            
            


            connectie = new MySqlConnection(connectiestring);
            try
            {
                // Connectie met database maken
                connectie.Open();
                Console.WriteLine("Geconnecteerd");

                // Gegevens van K810a opvragen als test
                string query = "SELECT * FROM Aansluitingen WHERE AP_id = 'K810a';";
                MySqlCommand cmd = new MySqlCommand(query, connectie);
                var mdr = cmd.ExecuteReader();  // computer bekijk zelf maar wat voor gegevens het zijn.
                int count = 0;
                while (mdr.Read())
                {
                    string db_APid = mdr.GetString("AP_id");
                    string db_Aid = mdr.GetString("A_id");

                    // db_naar = combinatie van ...
                    string db_naar = "";

                    // naar APid kan NULL zijn
                    string db_naarAPid;
                    if (Convert.IsDBNull(mdr["Naar_AP_id"]))
                    {
                        db_naarAPid = "NULL";
                    }
                    else
                    {
                        db_naarAPid = mdr.GetString("Naar_AP_id");
                        db_naar = db_naarAPid;
                    }

                    // naar Mid kan NULL zijn
                    string db_naarMid = "NULL"; 
                    if (Convert.IsDBNull(mdr["Naar_M_id"]))
                    {
                        db_naarMid = "NULL";
                    }
                    else
                    {
                        db_naarMid = mdr.GetString("Naar_M_id");
                        db_naar = db_naarMid;
                    }
                    
                    // omschrijving kan NULL zijn + machineomschrijving wordt ergens anders van gehaald
                    string db_omschrijving;
                    if (Convert.IsDBNull(mdr["Omschrijving"]))
                    {
                        db_omschrijving = "";
                    }
                    else
                    {
                        db_omschrijving = mdr.GetString("Omschrijving");
                    }
                    if (db_naarMid != "NULL")
                    {
                        // Niet werkende code!!!!
                        // query = "SELECT MOmschrijving FROM laagspanningsnet.machines WHERE Mid = '" + db_naarMid + "';";
                        // cmd = new MySqlCommand(query, connectie);
                        // var mm = cmd.ExecuteReader();
                        db_omschrijving = "OPHALEN UIT MACHINE DATABASE!"; // mm.GetString("MOmschrijving");
                    }

                    // string db_omschrijving = mdr.GetString("omschrijving");
                    // string db_omschrijving = "NULLomschrijving";
                    string db_KabelType = mdr.GetString("KabelType");
                    string db_KabelSectie = mdr.GetString("KabelSectie");
                    string db_Stroom = mdr.GetString("Stroom");
                    string db_Polen = mdr.GetString("Polen");



                   
                    // Rijen met de juiste waarden vullen
                    DataRow dr = dt.NewRow();
                    dr["Kring"] = db_Aid;
                    dr["Nummer"] = db_naar;
                    dr["Omschrijving"] = db_omschrijving;
                    dr["Kabeltype"] = db_KabelType;
                    dr["Kabel sectie"] = db_KabelSectie;
                    dr["Amp"] = db_Stroom;
                    dr["Aant.polen"] = db_Polen;

                    
                    

                    dt.Rows.Add(dr);
                    if (db_naar != "")
                    {
                        dgvAansluitingen.Rows[count].Cells[1] = new DataGridViewButtonCell();
                    }
                    count++;
                }
                connectie.Close();


                

                // dgvAansluitingen.Rows[0].Cells[0] = new DataGridViewButtonCell();

                // DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                // dgvAansluitingen.Columns.Insert(2, btn);
                // btn.HeaderText = "Click Data";
                // btn.Text = "Click Here";
                // btn.Name = "btn";
                // btn.UseColumnTextForButtonValue = true;
                






            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Geen verbinding met de server.");
                        break;
                    case 1045:
                        Console.WriteLine("Gebruikersnaam of Wachtwoord fouttief.");
                        break;
                }
            }

        }



        private void dgvAansluitingen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
        }

        //private void dgvAansluitingen_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
        //}
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

            Uitlezen();

        }

        private void Uitlezen()
        {
            string query = "SELECT * FROM klant;";
            MySqlCommand cmd = new MySqlCommand(query, connectie);
            var mdr = cmd.ExecuteReader();  // computer bekijk zelf maar wat voor gegevens het zijn.
            while (mdr.Read())
            {
                lblStatusInhoud.Text = mdr.GetString("voornaam");
            }

        }

        private bool SluitenConnectie()
        {
            try
            {
                connectie.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        private bool OpenenConnectie()
        {
            connectie = new MySqlConnection(connectiestring);
            try
            {
                connectie.Open();
                return true;
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
                return false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Sluiten van de connectie
            if (SluitenConnectie())
            {
                lblStatusInhoud.Text = "Connectie afgesloten";
                lblStatusInhoud.ForeColor = Color.Orange;
            }
            else
            {
                lblStatusInhoud.Text = "Afsluiten mislukt";
                lblStatusInhoud.ForeColor = Color.Red;
            }

        }
    }
}

    */