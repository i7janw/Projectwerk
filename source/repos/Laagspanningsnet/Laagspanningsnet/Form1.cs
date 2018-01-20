using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class Form1 : Form
    {
        private Database database;
        private DataTable dtDisplay;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            database = new Database();
            showAansluitpunt("T8");
        }

        private void showAansluitpunt(String aansluitpunt)
        { 
            // Haal de gegevens uit de database
            DataSet dsDatabase = database.getAansluitingen(aansluitpunt);

            // Maak een nieuwe (lege) dataset aan waarin de gegevens komen zoals ze op het sherm getoond worden
            // en koppel die aan dgvLaagspanningsnet
            DataSet dsDisplay = new DataSet();
            dtDisplay = new DataTable("Display");
            dsDisplay.Tables.Add(dtDisplay);
            dgvLaagspanningsnet.DataSource = dsDisplay.Tables[0];

            // Maak de kolommen aan die getoond moeten worden
            dtDisplay.Columns.Add(new DataColumn("+", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("-", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("A", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Kring", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Nummer", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Omschrijving", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Kabeltype", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Kabelsectie", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Stroom (A)", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Aantal polen", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Locatie", typeof(string)));

            // eerste 3 columns zijn de +/-A knoppen --> width op 25 zetten en HeaderText verbergen
            dgvLaagspanningsnet.Columns[0].Width = 25;
            dgvLaagspanningsnet.Columns[0].HeaderText = "";
            dgvLaagspanningsnet.Columns[1].Width = 25;
            dgvLaagspanningsnet.Columns[1].HeaderText = "";
            dgvLaagspanningsnet.Columns[2].Width = 25;
            dgvLaagspanningsnet.Columns[2].HeaderText = "";

            // Loop over de Database gegevens om ze te analyseren
            int count = 0;
            foreach (DataRow row in dsDatabase.Tables[0].Rows)
            {
                var db_A_id = row["A_id"];
                var db_Kabeltype = row["Kabeltype"];
                var db_Kabelsectie = row["Kabelsectie"];
                var db_Stroom = row["Stroom"];
                var db_Polen = row["Polen"];
                var db_Omschrijving = row["Omschrijving"];
                string db_Locatie = "";
                string db_Nummer = "";

                // Gaat deze aansluiting naar een ander aansluitpunt?
                var db_Naar_AP_id = row["Naar_AP_id"];
                if (db_Naar_AP_id != DBNull.Value)
                {
                    db_Nummer = (String)db_Naar_AP_id;
                    db_Locatie = database.getAansluitpuntLocatie(db_Nummer);                    // Locatie van aansluitpunt ophalen
                }

                // Gaat deze aansluiting naar een machine?
                var db_Naar_M_id = row["Naar_M_id"];
                if (db_Naar_M_id != DBNull.Value)
                {
                    db_Nummer = (String)db_Naar_M_id;
                    db_Locatie = database.getMachineLocatie(db_Nummer);                         // Locatie van machine ophalen
                    db_Omschrijving = database.getMachineOmschrijving((String)db_Naar_M_id);    // Bij een machine komt de omschrijving uit de machine DB
                }

                // Rij (velden) met de juiste waarden vullen
                DataRow dr = dtDisplay.NewRow();
                dr[0] = "+";
                dr[1] = "-";
                dr[2] = "A";
                dr["Kring"] = db_A_id;
                dr["Nummer"] = db_Nummer;
                dr["Omschrijving"] = db_Omschrijving;
                dr["Kabeltype"] = db_Kabeltype;
                dr["Kabelsectie"] = db_Kabelsectie;
                dr["Stroom (A)"] = db_Stroom;
                dr["Aantal polen"] = db_Polen;
                dr["Locatie"] = db_Locatie;
                dtDisplay.Rows.Add(dr);

                // Bepaalde cellen moeten buttons worden.
                dgvLaagspanningsnet.Rows[count].Cells[0] = new DataGridViewButtonCell();
                dgvLaagspanningsnet.Rows[count].Cells[1] = new DataGridViewButtonCell();
                dgvLaagspanningsnet.Rows[count].Cells[2] = new DataGridViewButtonCell();
                if (db_Naar_AP_id != DBNull.Value)    // T/VB/K-nummer --> ook een button
                {
                    dgvLaagspanningsnet.Rows[count].Cells["Nummer"] = new DataGridViewButtonCell();
                }

                count++;
            }

            // En nog een extra lijn bijvoegen voor de extra "+" knop.
            DataRow extraDataRow = dtDisplay.NewRow();
            extraDataRow[0] = "+";
            dtDisplay.Rows.Add(extraDataRow);
            dgvLaagspanningsnet.Rows[count].Cells[0] = new DataGridViewButtonCell();
            for (int y = 1; y < dgvLaagspanningsnet.ColumnCount; y++) // Op deze rij alles grijs maken, behalve de +knop.
            {
                dgvLaagspanningsnet.Rows[count].Cells[y].Style.BackColor = Color.DarkGray;
            }
        }

        private void dgvLaagspanningsnet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv[e.ColumnIndex, e.RowIndex].GetType() != typeof(DataGridViewButtonCell))
            {
                return;
            }
            if (e.ColumnIndex == 0) // +
            {
                return;
            }
            if (e.ColumnIndex == 1) // -
            {
                return;
            }
            if (e.ColumnIndex == 2) // A
            {
                return;
            }

            //var dgv = (DataGridView)sender;

            Console.Write(e.ToString());

            //if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
            //  e.RowIndex >= 0)
            //{
            //TODO - Button Clicked - Execute Code Here
            //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");

            // MessageBox.Show(dtDisplay.Rows[e.RowIndex][e.ColumnIndex].ToString());
            showAansluitpunt(dtDisplay.Rows[e.RowIndex][e.ColumnIndex].ToString());


            //}
        }
    }
}
