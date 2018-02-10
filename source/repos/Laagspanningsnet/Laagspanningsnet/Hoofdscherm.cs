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
    public partial class Hoofdscherm : Form
    {
        private Database database;      // Alle communicatie met de database verloopt via de database klasse
        private DataTable dtDisplay;    // Inhoud van deze DataTable wordt via een DataGridView op het scherm getoond
        private bool unsaved;           // Staan er niet bewaarde gegevens op het scherm?
        private string aansluitpunt;    // Het aansluitpunt dat momenteel wordt getoond
        
        public Hoofdscherm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            database = new Database();
            showTransfos();             // We starten met een overzicht van de Transfos
        }

        /* Tonen van het overzicht van de Transfos
         */
        private void showTransfos()
        {
            showCommon("", 1);
         
        }
        
        /* Tonen van het aansluitpunt.
         * 
         * Als parameter wordt een string meegegeven dat het 'nummer' van het aansluitpunt is
         */
        private void showAansluitpunt(String _ap)
        {
            showCommon(_ap, 2);
        }

        /* Tonen van zoekresultaten.
         * 
         * Als parameter wordt een string meegegeven met zoekterm
         */
        private void showSearch(String _search)
        {
            showCommon(_search, 3);
        }
        
        /* Tonen van Zoekresultaten of Aansluitpunt
         * 
         * Eerste parameter = string = zoekterm of aansluitpunt
         * Tweede Parameter = int    = 1. Transfos (eerste parameter wordt genegeerd)
         *                             2. Aansluitpunt
         *                             3. Search
         */
        private void showCommon(String _ap, int _mode)
        {
            // Maak een nieuwe (lege) dataset aan waarin de gegevens komen zoals ze op het sherm getoond worden
            // en koppel die aan dgvLaagspanningsnet
            DataSet dsDisplay = new DataSet();
            dtDisplay = new DataTable("Display");
            dsDisplay.Tables.Add(dtDisplay);
            dgvLaagspanningsnet.DataSource = dsDisplay.Tables[0];
            
            // DataSet definieren waar de database gegevens in geladen worden
            DataSet dsDatabase;

            // Andere gegevens uit database halen + op het scherm zetten naar gelang de modus
            switch (_mode)
            {
                case 1:
                    this.aansluitpunt = "";
                    lblLayout.Text = "Overzicht transfos";
                    dsDatabase = database.getTransfos();
                    break;
                case 3:
                    this.aansluitpunt = "";
                    lblLayout.Text = "Zoeken : " + _ap;
                    dsDatabase = database.getSearch(_ap);
                    break;
                default:    // case 2 = default
                    this.aansluitpunt = _ap;
                    lblLayout.Text = "Layout van " + aansluitpunt;
                    dsDatabase = database.getAansluitingen(aansluitpunt);
                    break;
            }

            // Transfo() <> aansluitpunt/search andere gegevens worden in DataGridView getoond
            switch (_mode)
            {
                case 1:
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
                    break;
                default:    // (2)aansluitpunt of (3)search
                    // Maak de kolommen aan die getoond moeten worden
                    dtDisplay.Columns.Add(new DataColumn("+", typeof(string)));
                    dtDisplay.Columns.Add(new DataColumn("-", typeof(string)));
                    dtDisplay.Columns.Add(new DataColumn("A", typeof(string)));
                    if (_mode == 3)
                    {
                        dtDisplay.Columns.Add(new DataColumn("T/VB/K", typeof(string)));
                    }
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
                        var db_AP_id = row["AP_id"];
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
                        if (_mode == 3)
                        {
                            dr["T/VB/K"] = db_AP_id;
                        }
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
                    break;
            }

            // En nog een extra lijn bijvoegen voor de extra "+" knop.
            DataRow extraDataRow = dtDisplay.NewRow();
            extraDataRow[0] = "+";
            dtDisplay.Rows.Add(extraDataRow);
            dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells[0] = new DataGridViewButtonCell();
            for (int y = 1; y < dgvLaagspanningsnet.ColumnCount; y++) // Op deze rij alles grijs maken, behalve de +knop.
            {
                dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells[y].Style.BackColor = Color.DarkGray;
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

            // Text Locatie aanpassen
            lblDynLocatie.Text = database.getAansluitpuntLocatie(aansluitpunt);

            // Text kabel aanpassen
            lblDynKabel.Text = database.getKabel(aansluitpunt);

            // Text stroom aanpassen
            lblDynStroom.Text = database.getStroom(aansluitpunt);

            // Alle data staat op het scherm --> unsaved=false
            setUnsaved(false);
        }

        /* Is er op een cell van het dataGrid geklikt?
         */
        private void dgvLaagspanningsnet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Klikken op de bovenste rij (column-text) negeren.
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
                setUnsaved(true);
                DataRow row = dtDisplay.NewRow();
                row["+"] = "+";
                row["-"] = "-";
                row["A"] = "A";
                row["Kring"] = "TODO!!!!";
                row["Type"] = "N";
                dtDisplay.Rows.InsertAt(row, e.RowIndex);
                dgvLaagspanningsnet.Rows[e.RowIndex].Cells["-"] = new DataGridViewButtonCell();
                dgvLaagspanningsnet.Rows[e.RowIndex].Cells["+"] = new DataGridViewButtonCell();
                dgvLaagspanningsnet.Rows[e.RowIndex].Cells["A"] = new DataGridViewButtonCell();
                AansluitingAanpassen aa = new AansluitingAanpassen(aansluitpunt, dtDisplay.Rows[e.RowIndex], false);    // false = invoegen
                aa.ShowDialog();    // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
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
                setUnsaved(true);
                // Open het venster om aanpassingen te doen
                AansluitingAanpassen aa = new AansluitingAanpassen(aansluitpunt, dtDisplay.Rows[e.RowIndex], true);     // true = aanpassen
                aa.ShowDialog();    // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
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
                showTransfos();
                return;
            }
            showAansluitpunt(ap);
            return;
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

        /* Er is op de knop save geklikt.
         */
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Als er geen unsaved data is, moet er niks gebeuren.
            if (!unsaved)
            {
                return;
            }
            
            // !!!! TODO : bekijken wat we doen bij overzicht transfos.
            if (aansluitpunt=="")
            {
                MessageBox.Show("Onder constructie, nog niet geprogrammeerd!!! TODO");
                return;
            }
                        
            // Maak een nieuwe Dataset aan met de nodige columns, waar de gegevens voor de database in komen
            DataSet dsDatabase = new DataSet();
            DataTable dtDatabase = new DataTable("aansluitingen");
            dsDatabase.Tables.Add(dtDatabase);
            dtDatabase.Columns.Add(new DataColumn("A_id", typeof(string)));
            dtDatabase.Columns.Add(new DataColumn("AP_id", typeof(string)));
            dtDatabase.Columns.Add(new DataColumn("Naar_AP_id", typeof(string)));
            dtDatabase.Columns.Add(new DataColumn("Naar_M_id", typeof(string)));
            dtDatabase.Columns.Add(new DataColumn("Omschrijving", typeof(string)));
            dtDatabase.Columns.Add(new DataColumn("Kabeltype", typeof(string)));
            dtDatabase.Columns.Add(new DataColumn("Kabelsectie", typeof(string)));
            dtDatabase.Columns.Add(new DataColumn("Stroom", typeof(int)));
            dtDatabase.Columns.Add(new DataColumn("Polen", typeof(int)));

            // Doorloop de datatable die op het scherm staat.
            // Opm. de laatste lijn is de lege lijn met een plus dus die lezen we niet.
            for (int count = 0; count < dtDisplay.Select().Length - 1; count++)
            {
                Console.WriteLine(count);
                DataRow rowDatabase = dtDatabase.NewRow();       // nieuwe row dtDatabase 
                DataRow rowDisplay = dtDisplay.Rows[count];      // lees een row dtDisplay

                rowDatabase["AP_id"] = aansluitpunt;
                rowDatabase["A_id"] = rowDisplay["Kring"];
                if ((string)rowDisplay["Type"] == "N")
                {
                    rowDatabase["Naar_AP_id"] = null;
                    rowDatabase["Naar_M_id"] = null;
                    rowDatabase["Omschrijving"] = rowDisplay["Omschrijving"];
                }
                if ((string)rowDisplay["Type"] == "M")
                {
                    rowDatabase["Naar_AP_id"] = null;
                    rowDatabase["Naar_M_id"] = rowDisplay["Nummer"];
                    rowDatabase["Omschrijving"] = null;
                }
                if ((string)rowDisplay["Type"] == "A")
                {
                    rowDatabase["Naar_AP_id"] = rowDisplay["Nummer"];
                    rowDatabase["Naar_M_id"] = null;
                    rowDatabase["Omschrijving"] = null;
                }
                rowDatabase["Kabeltype"] = rowDisplay["Kabeltype"];
                rowDatabase["Kabelsectie"] = rowDisplay["Kabelsectie"];
                
                // stroom & polen = int --> via Int32.TryParse
                int convert;
                if (rowDisplay["Stroom (A)"] == DBNull.Value) rowDisplay["Stroom (A)"] = "";
                if (rowDisplay["Aantal polen"] == DBNull.Value) rowDisplay["Aantal polen"] = "";
                if (Int32.TryParse((String)rowDisplay["Stroom (A)"], out convert))
                {
                    rowDatabase["Stroom"] = convert;
                }
                else
                {
                    rowDatabase["Stroom"] = DBNull.Value;
                }
                if (Int32.TryParse((String)rowDisplay["Aantal polen"], out convert))
                {
                    rowDatabase["Polen"] = convert;
                }
                else
                {
                    rowDatabase["Polen"] = 3;                       // Standaard is het aantal polen 3 : R S T + Vaste PEN aansluiting 
                }
                
                // Deze row toevoegen aan de dataSet
                dtDatabase.Rows.Add(rowDatabase);
            }
            
            // De database dataset sturen we naar de database, die de gegevens op de mySQL-server zal opslaan
            database.setAansluitingen(dsDatabase);
            
            // Gegevens terug inladen zodat hetgene op het scherm staat zeker hetzelfde is als in de database is opgeslagen
            showAansluitpunt(aansluitpunt);
        }

        /* UNDO : Aanpassingen ongedaan maken = database terug inlezen en tonen
         */
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (aansluitpunt != "")
            {
                showAansluitpunt(aansluitpunt);
            }
        }

        /* Op de zoekknop klikken --> start zoeken
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            showSearch(txtbxSearch.Text);
        }

        /* Op Enter drukken in de search box = op de zoekknop klikken
         */
        private void txtbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(this, new EventArgs());

                // Toegevoegd omdat anders een ding-sound wordt afgespeeld na het drukken op enter.
                // <https://stackoverflow.com/questions/6290967/stop-the-ding-when-pressing-enter>
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
