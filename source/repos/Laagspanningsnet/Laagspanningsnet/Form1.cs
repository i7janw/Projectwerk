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
        private Database database;      // Alle communicatie met de database verloopt via de database klasse
        private DataTable dtDisplay;    // Inhoud van deze DataTable wordt via een DataGridView op het scherm getoond
        private bool unsaved;           // Staan er niet bewaarde gegevens op het scherm?
        private string aansluitpunt;    // Het aansluitpunt dat momenteel wordt getoond
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            database = new Database();
            showAansluitpunt("Transfos");   // We starten met een overzicht van de Transfos
        }
        
        /* Tonen van het aansluitpunt.
         * 
         * Als parameter wordt een string meegegeven dat het 'nummer' van het aansluitpunt is
         * Speciaal geval is 'Transfos' als parameter, in dat geval wordt een overzicht van de tranformatoren gegeven
         */
        private void showAansluitpunt(String ap)
        {
            this.aansluitpunt = ap;

            // Maak een nieuwe (lege) dataset aan waarin de gegevens komen zoals ze op het sherm getoond worden
            // en koppel die aan dgvLaagspanningsnet
            DataSet dsDisplay = new DataSet();
            dtDisplay = new DataTable("Display");
            dsDisplay.Tables.Add(dtDisplay);
            dgvLaagspanningsnet.DataSource = dsDisplay.Tables[0];

            // Haal de gegevens uit de database.
            DataSet dsDatabase;
            if (aansluitpunt == "Transfos")     // Overzicht transfos tonen, of de aansluitingen van een aansluitpunt?
            {
                dsDatabase = database.getTransfos();

                // Maak de kolommen aan die getoond moeten worden
                dtDisplay.Columns.Add(new DataColumn("+", typeof(string)));
                dtDisplay.Columns.Add(new DataColumn("-", typeof(string)));
                dtDisplay.Columns.Add(new DataColumn("A", typeof(string)));
                dtDisplay.Columns.Add(new DataColumn("Transfo", typeof(string)));
                dtDisplay.Columns.Add(new DataColumn("Locatie", typeof(string)));

                // Loop over de Database gegevens om ze te analyseren
                foreach (DataRow row in dsDatabase.Tables[0].Rows)
                {
                    var db_AP_id = row["AP_id"];
                    var db_AP_Locatie = row["AP_locatie"];

                    // Rij (velden) met de juiste waarden vullen
                    DataRow dr = dtDisplay.NewRow();
                    dr[0] = "+";
                    dr[1] = "-";
                    dr[2] = "A";
                    dr["Transfo"] = db_AP_id;
                    dr["Locatie"] = db_AP_Locatie;
                    dtDisplay.Rows.Add(dr);

                    // Bepaalde cellen moeten buttons worden.
                    dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells["Transfo"] = new DataGridViewButtonCell();
                    dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells["-"] = new DataGridViewButtonCell();
                    dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells["+"] = new DataGridViewButtonCell();
                    dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells["A"] = new DataGridViewButtonCell();
                }
            }
            else
            {
                dsDatabase = database.getAansluitingen(aansluitpunt);

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
                dtDisplay.Columns.Add(new DataColumn("Type", typeof(string)));          // Normaal ; Aansluitpunt ; Machine

                // de column "Type" is enkel voor intern gebruik en wordt dus niet getoond
                dgvLaagspanningsnet.Columns["Type"].Visible = false;

                // Loop over de Database gegevens om ze te analyseren
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
                    string db_Type = "N";   // zet standaard op Normaal.

                    // Gaat deze aansluiting naar een ander aansluitpunt?
                    var db_Naar_AP_id = row["Naar_AP_id"];
                    if (db_Naar_AP_id != DBNull.Value)
                    {
                        db_Nummer = (String)db_Naar_AP_id;
                        db_Type = "A";      // type = Aansluitpunt
                        db_Locatie = database.getAansluitpuntLocatie(db_Nummer);                    // Locatie van aansluitpunt ophalen
                    }

                    // Gaat deze aansluiting naar een machine?
                    var db_Naar_M_id = row["Naar_M_id"];
                    if (db_Naar_M_id != DBNull.Value)
                    {
                        db_Nummer = (String)db_Naar_M_id;
                        db_Type = "M";      // type = Machine
                        db_Locatie = database.getMachineLocatie(db_Nummer);                         // Locatie van machine ophalen
                        db_Omschrijving = database.getMachineOmschrijving((String)db_Naar_M_id);    // Bij een machine komt de omschrijving uit de machine DB
                    }

                    // Rij (velden) met de juiste waarden vullen
                    DataRow dr = dtDisplay.NewRow();
                    dr["+"] = "+";
                    dr["-"] = "-";
                    dr["A"] = "A";
                    dr["Kring"] = db_A_id;
                    dr["Nummer"] = db_Nummer;
                    dr["Omschrijving"] = db_Omschrijving;
                    dr["Kabeltype"] = db_Kabeltype;
                    dr["Kabelsectie"] = db_Kabelsectie;
                    dr["Stroom (A)"] = db_Stroom;
                    dr["Aantal polen"] = db_Polen;
                    dr["Locatie"] = db_Locatie;
                    dr["Type"] = db_Type;
                    dtDisplay.Rows.Add(dr);

                    // Bepaalde cellen moeten buttons worden.
                    if (db_Type == "A")    // T/VB/K-nummer --> ook een button
                    {
                        dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells["Nummer"] = new DataGridViewButtonCell();
                    }
                    dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells["-"] = new DataGridViewButtonCell();
                    dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells["+"] = new DataGridViewButtonCell();
                    dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells["A"] = new DataGridViewButtonCell();
                }

            }

            // En nog een extra lijn bijvoegen voor de extra "+" knop.
            DataRow extraDataRow = dtDisplay.NewRow();
            extraDataRow[0] = "+";
            dtDisplay.Rows.Add(extraDataRow);
            dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount-1].Cells[0] = new DataGridViewButtonCell();
            for (int y = 1; y < dgvLaagspanningsnet.ColumnCount; y++) // Op deze rij alles grijs maken, behalve de +knop.
            {
                dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount-1].Cells[y].Style.BackColor = Color.DarkGray;
            }

            // eerste 3 columns zijn de +/-A knoppen --> width op 25 zetten en HeaderText verbergen
            dgvLaagspanningsnet.Columns["+"].Width = 25;
            dgvLaagspanningsnet.Columns["+"].HeaderText = "";
            dgvLaagspanningsnet.Columns["-"].Width = 25;
            dgvLaagspanningsnet.Columns["-"].HeaderText = "";
            dgvLaagspanningsnet.Columns["A"].Width = 25;
            dgvLaagspanningsnet.Columns["A"].HeaderText = "";

            // Niet sorteren van columns
            foreach (DataGridViewColumn column in dgvLaagspanningsnet.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            // Text in button voeding aanpassen
            btnDynVoeding.Text = database.getVoeding(aansluitpunt);

            // Text Layout aanpassen
            if (aansluitpunt == "Transfos")
            {
                lblLayout.Text = "Overzicht transfos";
            }
            else
            { 
                lblLayout.Text = "Layout van " + aansluitpunt;
            }

            // Text Locatie aanpassen
            lblDynLocatie.Text = database.getAansluitpuntLocatie(aansluitpunt);

            // Text kabel aanpassen
            lblDynKabel.Text = database.getKabel(aansluitpunt);

            // Text stroom aanpassen
            lblDynStroom.Text = database.getStroom(aansluitpunt);

            // Alle data staat op het scherm --> unsaved=false
            setUnsaved(false);
        }

        private void dgvLaagspanningsnet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridView dgv = sender as DataGridView;

            // Is er op een cell met een button gedrukt?
            if (dgv[e.ColumnIndex, e.RowIndex].GetType() != typeof(DataGridViewButtonCell))
            {
                return; // Geen button --> negeren
            }

            // Afhandelen van drukken op +/-/A
            if (e.ColumnIndex == 0) // +
            {
                return;
            }
            if (e.ColumnIndex == 1) // -
            {
                dtDisplay.Rows.RemoveAt(e.RowIndex);
                setUnsaved(true);
                return;
            }
            if (e.ColumnIndex == 2) // A
            {
                return;
            }

            // Doorbladeren naar een ander aansluitpunt
            showAansluitpunt(dtDisplay.Rows[e.RowIndex][e.ColumnIndex].ToString());
        }

        /* Als er op de knop van de voeding wordt geklikt, ga we naar het scherm van dit aansluitpunt.
         */
        private void btnDynVoeding_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String ap = button.Text.Split(' ').First();
            if(ap == "-")
            {
                ap = "Transfos";
            }
            showAansluitpunt(ap);
        }

        /* Bijhouden of data reeds in database is opgeslagen
         */
        private void setUnsaved(bool status)
        {
            unsaved = status;
            if (unsaved)
            {
                btnSave.BackColor = Color.Red;
            }
            else
            {
                btnSave.BackColor = Color.Green;
            }
        }

        /* De blauwe selectie-balk hebben we in dit programma niet nodig.
         * --> disable
         * 
         * Info:
         * <https://stackoverflow.com/questions/11330147/how-to-disable-the-ability-to-select-in-a-datagridview>
         */
        private void dgvLaagspanningsnet_SelectionChanged(object sender, EventArgs e)
        {
            dgvLaagspanningsnet.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            // OPGEPAST andere save routine voor TRANSFOS en AANSLUITPUNT!!!!!

            // 1. DELETE FROM laagspanningsnet.aansluitingen WHERE AP_id = 'VB810';
            // 2. LOOP INSERT : INSERT INTO `Laagspanningsnet`.`Aansluitingen` 
            // (`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , 
            // `Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`) 
            // VALUES('T8', 'H810', 'VB810', NULL, NULL, 'XVB', '4G95', '250', '3');
            
            // !!!!! TODO !!!!!

            // Volledige dataSet doorlopen, behalve de laatste lijn, want dat is lege lijn met +
            // foreach (DataRow row in dtDisplay.Rows)
            for (int count=0; count < dtDisplay.Select().Length - 1; count++)
           {

                // voorlopig gewoon alle gegevens afdrukken... 
                // code om echte te bewaren nog te programmeren

                DataRow row = dtDisplay.Rows[count];
                Console.WriteLine("--" + count + "---------");

                Console.WriteLine(row["+"]);
                Console.WriteLine(row["-"]);
                Console.WriteLine(row["A"]);
                Console.WriteLine(row["Kring"]);
                Console.WriteLine(row["Nummer"]);
                Console.WriteLine(row["Omschrijving"]);
                Console.WriteLine(row["Kabeltype"]);
                Console.WriteLine(row["Kabelsectie"]);
                Console.WriteLine(row["Stroom (A)"]);
                Console.WriteLine(row["Aantal polen"]);
                Console.WriteLine(row["Locatie"]);
                Console.WriteLine(row["Type"]);
                               
            }

            // Gegevens terug inladen zodat hetgene op het scherm staat zeker hetzelfde is als inde database is opgeslagen
            showAansluitpunt(aansluitpunt);
        }

        /* UNDO : Aanpassingen ongedaan maken = database terug inlezen en tonen
         */
        private void btnUndo_Click(object sender, EventArgs e)
        {
            showAansluitpunt(aansluitpunt);
        }
    }
}
