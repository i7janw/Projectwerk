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

namespace _2transfo
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

        DataSet ds = new DataSet();
        DataTable dt = new DataTable("MyTable");

        public Form1()
        {
            
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetData("SELECT * FROM laagspanningsnet.aansluitingen WHERE AP_id = 'T8';");
            //GetData("SELECT AP_id AS Transfo, AP_locatie AS Locatie FROM laagspanningsnet.aansluitpunten WHERE AP_id LIKE 'T%';");
        }

        private void GetData(string mysql)
        {

            /////////////////////
            ds = new DataSet();
            dt = new DataTable("MyTable");

            ds.Tables.Add(dt);
            dataGridView1.DataSource = ds.Tables[0];

            dt.Columns.Add(new DataColumn("+", typeof(string)));
            dt.Columns.Add(new DataColumn("A", typeof(string)));
            dt.Columns.Add(new DataColumn("Kring", typeof(string)));
            dt.Columns.Add(new DataColumn("Nummer", typeof(string)));
            dt.Columns.Add(new DataColumn("Omschrijving", typeof(string)));
            dt.Columns.Add(new DataColumn("Kabeltype", typeof(string)));
            dt.Columns.Add(new DataColumn("Kabel sectie", typeof(string)));
            dt.Columns.Add(new DataColumn("Amp", typeof(string)));
            dt.Columns.Add(new DataColumn("Aant.polen", typeof(string)));
            dt.Columns.Add(new DataColumn("Locatie", typeof(string)));





            connectie = new MySqlConnection(connectiestring);
            try
            {
                // Connectie met database maken
                connectie.Open();
                Console.WriteLine("Geconnecteerd");

                // Gegevens van K810a opvragen als test
                string query = mysql;
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

                    // Wat als het een machine is???
                    // OmSchrijving & Locatie ophalen
                    string db_Locatie = "";
                    if (db_naarMid != "NULL")
                    {
                        // Ophalen M_omschrijving
                        MySqlConnection connectie2 = new MySqlConnection(connectiestring);
                        connectie2.Open();
                        string query2 = "SELECT M_omschrijving FROM laagspanningsnet.machines WHERE M_id = '" + db_naarMid + "';";
                        Console.WriteLine(query2);
                        MySqlCommand cmd2 = new MySqlCommand(query2, connectie2);
                        var mm= cmd2.ExecuteReader();
                        while (mm.Read())
                        {
                            db_omschrijving = mm.GetString("M_omschrijving");
                        }
                        connectie2.Close();
                        // Ophalen M_locatie
                        connectie2 = new MySqlConnection(connectiestring);
                        connectie2.Open();
                        query2 = "SELECT M_locatie FROM laagspanningsnet.machines WHERE M_id = '" + db_naarMid + "';";
                        Console.WriteLine(query2);
                        cmd2 = new MySqlCommand(query2, connectie2);
                        var mmm = cmd2.ExecuteReader();
                        while (mmm.Read())
                        {
                            db_Locatie = mmm.GetString("M_locatie");
                        }
                        connectie2.Close();
                    }

                    // Wat als het een aansluitpunt is???
                    // Locatie ophalen
                    if (db_naarAPid != "NULL")
                    {
                        // Ophalen AP_locatie
                        MySqlConnection connectie2 = new MySqlConnection(connectiestring);
                        connectie2.Open();
                        string query2 = "SELECT AP_locatie FROM laagspanningsnet.aansluitpunten WHERE AP_id = '" + db_naarAPid + "';";
                        Console.WriteLine(query2);
                        MySqlCommand cmd2 = new MySqlCommand(query2, connectie2);
                        var mmm = cmd2.ExecuteReader();
                        while (mmm.Read())
                        {
                            db_Locatie = mmm.GetString("AP_locatie");
                        }
                        connectie2.Close();


                        

                    }



                    // string db_omschrijving = mdr.GetString("omschrijving");
                    // string db_omschrijving = "NULLomschrijving";
                    string db_KabelType = mdr.GetString("KabelType");
                    string db_KabelSectie = mdr.GetString("KabelSectie");
                    string db_Stroom = mdr.GetString("Stroom");
                    string db_Polen = mdr.GetString("Polen");
                    



                    // Rijen met de juiste waarden vullen
                    DataRow dr = dt.NewRow();
                    dr[0] = "+";
                    dr[1] = "-";
                    dr["Kring"] = db_Aid;
                    dr["Nummer"] = db_naar;
                    dr["Omschrijving"] = db_omschrijving;
                    dr["Kabeltype"] = db_KabelType;
                    dr["Kabel sectie"] = db_KabelSectie;
                    dr["Amp"] = db_Stroom;
                    dr["Aant.polen"] = db_Polen;
                    dr["Locatie"] = db_Locatie;
                      
                    dt.Rows.Add(dr);

                    dataGridView1.Rows[count].Cells[0] = new DataGridViewButtonCell();
                    dataGridView1.Columns[0].Width = 25;
                    dataGridView1.Rows[count].Cells[1] = new DataGridViewButtonCell();
                    dataGridView1.Columns[1].Width = 25;
                    if (db_naar != "")
                    {
                        dataGridView1.Rows[count].Cells[3] = new DataGridViewButtonCell();
                    }
                    count++;
                }
                connectie.Close();



                ////////////////////////
                // Een extra veld toevoegen omwille van +
                DataRow ddr = dt.NewRow();
                ddr[0] = "+";
                //ddr[1] = "A";
                dt.Rows.InsertAt(ddr, count);
                dataGridView1.Rows[count].Cells[0] = new DataGridViewButtonCell();
                //dataGridView1.Rows[count].Cells[1] = new DataGridViewButtonCell();
                for (int y=1; y<10; y++ ) { 
                    dataGridView1.Rows[count].Cells[y].Style.BackColor = Color.DarkGray;
                }

                dataGridView1.Columns[0].HeaderText = "";
                dataGridView1.Columns[1].HeaderText = "";

            }
            catch { Console.WriteLine("Failed"); }
        }

        /*private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString();
            //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
            Console.WriteLine(dataGridView1.CurrentCell.Value.ToString());

            if (s == "+")
            {
                DataRow ddr = dt.NewRow();
                ddr[0] = "+";
                ddr[1] = "-";
                dt.Rows.InsertAt(ddr, e.RowIndex);
                dataGridView1.Rows[e.RowIndex].Cells[0] = new DataGridViewButtonCell();
                dataGridView1.Rows[e.RowIndex].Cells[1] = new DataGridViewButtonCell();
                return;
            }
            if (s== "-")
            {
                DataGridViewCell cell = dataGridView1.Rows[0].Cells[2];
                cell.ReadOnly = false;
                dataGridView1.CurrentCell = cell;
                cell.ReadOnly = false;
                dataGridView1.BeginEdit(true);
                return;
            }
            GetData("SELECT * FROM laagspanningsnet.aansluitingen WHERE AP_id = '" + dataGridView1.CurrentCell.Value.ToString() + "';");
        }*/
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

