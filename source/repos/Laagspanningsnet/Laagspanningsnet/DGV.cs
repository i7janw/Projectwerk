using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public class DGV : DataGridView
    {
        private DataTable dtDisplay;    // Inhoud van deze DataTable wordt via een DataGridView op het scherm getoond
        private Database database;      // Alle communicatie met de database verloopt via de database klasse
        private String aansluitpunt;
        private int mode;

        public DGV()
        {
            database = new Database();
            aansluitpunt = "";
            mode = 0;
        }

        /* Tonen van het overzicht van de Transfos
         */
        public void ShowTransfos()
        {
            Reload("", 1);
        }

        /* Tonen van het aansluitpunt.
         * 
         * Als parameter wordt een string meegegeven dat het 'nummer' van het aansluitpunt is
         */
        public void ShowAansluitpunt(String _ap)
        {
            Reload(_ap, 2);
        }

        /* Tonen van zoekresultaten.
         * 
         * Als parameter wordt een string meegegeven met zoekterm
         */
        public void ShowSearch(String _search)
        {
            Reload(_search, 3);
        }

        /* Tonen van Zoekresultaten of Aansluitpunt
         * 
         * Eerste parameter = string = zoekterm of aansluitpunt
         * Tweede Parameter = int    = 1. Transfos (eerste parameter wordt genegeerd)
         *                             2. Aansluitpunt
         *                             3. Search
         */
        public void Reload(String _ap, int _mode)
        {
            aansluitpunt = _ap;
            mode = _mode;
            
            // DataSet definieren waar de database gegevens in geladen worden
            DataSet dsDatabase;

            // Maak een nieuwe (lege) dataset aan waarin de gegevens komen zoals ze op het sherm getoond worden
            // en koppel die aan dgvLaagspanningsnet
            DataSet dsDisplay = new DataSet();
            dtDisplay = new DataTable("Display");
            dsDisplay.Tables.Add(dtDisplay);
            DataSource = dsDisplay.Tables[0];

            // De +/-/A Columns toevoegen
            dtDisplay.Columns.Add(new DataColumn("+", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("-", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("A", typeof(string)));
            // De andere Columns toevoegen
            dtDisplay.Columns.Add(new DataColumn("T/VB/K", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Kring", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Nummer", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Omschrijving", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Kabeltype", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Kabelsectie", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Stroom (A)", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Aantal polen", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Locatie", typeof(string)));
            dtDisplay.Columns.Add(new DataColumn("Type", typeof(string)));          // Normaal ; Aansluitpunt ; Machine

            // Zichtbaarheid van de Column Type instellen
            Columns["Type"].Visible = false;                    // de column "Type" is enkel voor intern gebruik en wordt dus niet getoond

            // Andere gegevens uit database halen + op het scherm zetten naar gelang de modus
            switch (_mode)
            {
                case 1:     // transfos
                    // Zichtbaarheid instellen
                    Columns["+"].Visible = false;
                    Columns["-"].Visible = false;
                    Columns["A"].Visible = false;
                    Columns["T/VB/K"].Visible = false;
                    //dgvLaagspanningsnet.Columns["Kring"].Visible = false;
                    Columns["Omschrijving"].Visible = false;
                    Columns["Kabeltype"].Visible = false;
                    Columns["Kabelsectie"].Visible = false;
                    Columns["Stroom (A)"].Visible = false;
                    // Columns["Aantal polen"].Visible = false;
                    // database gegevens ophalen
                    dsDatabase = database.GetTransfos();
                    break;
                case 3:     // search
                    // Zichtbaarheid instellen
                    Columns["+"].Visible = false;
                    Columns["-"].Visible = false;
                    Columns["A"].Visible = false;
                    // database gegevens ophalen
                    dsDatabase = database.GetSearch(_ap);
                    break;
                default:    // aansluitpunt // case 2 = default
                    // Zichtbaarheid instellen
                    Columns["T/VB/K"].Visible = false;
                    // database gegevens ophalen
                    dsDatabase = database.GetAansluitingen(_ap);
                    break;
            }

            // Zitten er wel gegevens in database DataSet?
            if (dsDatabase.Tables.Count == 0)
            {
                return; // Als er niks in zit, valt er niks te doen...
            }

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
                    db_Locatie = database.GetAansluitpuntLocatie(db_Nummer);                    // Locatie van aansluitpunt ophalen
                }

                // Gaat deze aansluiting naar een machine?
                var db_Naar_M_id = row["Naar_M_id"];
                if (db_Naar_M_id != DBNull.Value)
                {
                    db_Nummer = (String)db_Naar_M_id;
                    db_Type = "M";      // type = Machine
                    db_Locatie = database.GetMachineLocatie(db_Nummer);                         // Locatie van machine ophalen
                    db_Omschrijving = database.GetMachineOmschrijving((String)db_Naar_M_id);    // Bij een machine komt de omschrijving uit de machine DB
                }

                // Rij (velden) met de juiste waarden vullen
                DataRow dr = dtDisplay.NewRow();
                dr["+"] = "+";
                dr["-"] = "-";
                dr["A"] = "A";
                dr["T/VB/K"] = db_AP_id;
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
            }

            // En nog een extra lijn bijvoegen voor de extra "+" knop.
            if (_mode == 2)
            {    // enkel bij modus (2)aansluitpunt
                DataRow extraDataRow = dtDisplay.NewRow();
                extraDataRow[0] = "+";
                dtDisplay.Rows.Add(extraDataRow);
            }

            // eerste 3 columns zijn de +/-A knoppen --> width op 25 zetten en HeaderText verbergen
            Columns["+"].Width = 25;
            Columns["+"].HeaderText = "";
            Columns["-"].Width = 25;
            Columns["-"].HeaderText = "";
            Columns["A"].Width = 25;
            Columns["A"].HeaderText = "";

            // Niet sorteren van columns
            foreach (DataGridViewColumn column in Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Knoppen in de dgv aanmaken.
            int count = 0;
            foreach (DataGridViewRow row in Rows)
            {
                MakeButtons(count);
                count++;
            }
        }

        /* Maak in een bepaalde row van de DataGridView de knoppen aan voor +/-/A en Aansluitpunten
         * Maak op de laatste row alles grijs als het over de laatste lege lijn met een + gaat (bij weergave aansluitpunt).
         * 
         * _count = de index van de row
         */
        public void MakeButtons(int _count)
        {
            // Maak van de +  een knop.
            Rows[_count].Cells["+"] = new DataGridViewButtonCell();

            if (Rows[_count].Cells["Type"].Value != DBNull.Value)    // op de laatste lege lijn met enkel een + heeft "Type" geen waarde.
            {
                // Maak van -/A en Aansluitpunten een knop.
                if ((String)Rows[_count].Cells["Type"].Value == "A")
                {
                    Rows[_count].Cells["Nummer"] = new DataGridViewButtonCell();
                }
                Rows[_count].Cells["-"] = new DataGridViewButtonCell();
                Rows[_count].Cells["A"] = new DataGridViewButtonCell();
            }
            else // deze code wordt uitgevoerd als we op de laatste lege lijn met enkel een + zijn 
            {
                // Op deze rij alles grijs maken, behalve de +knop.
                for (int y = 1; y < ColumnCount; y++)
                {
                    Rows[RowCount - 1].Cells[y].Style.BackColor = Color.DarkGray;
                }
            }
        }

        public DataTable GetDataTable()
        {
            return dtDisplay;
        }

        public String GetAansluitpunt()
        {
            return aansluitpunt;
        }

        public int GetMode()
        {
            return mode;
        }

        public void Reload()
        {
            switch (mode)
            {
                case 1:
                    ShowTransfos();
                    break;
                case 2:
                    ShowAansluitpunt(aansluitpunt);
                    break;
                case 3:
                    ShowSearch(aansluitpunt);
                    break;
            }
        }
    }
}
