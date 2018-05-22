/* LaagspanningGridView : Dit is een DataGridView waar extra's aan toegevoegd zijn voor het tonen Laagspanningsnet informatie.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - width omschrijving aangepast
 */
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class LaagspanningGridView : DataGridView
    {
        public const int Transfos = 1;
        public const int Aansluitpunt = 2;
        public const int Search = 3;

        private DataTable _dtDisplay;               // Inhoud van deze DataTable wordt op het scherm getoond
        private readonly Database _database;        // Alle communicatie met de database verloopt via de database klasse
        private string _aansluitpunt;
        private int _mode;                          // 1 = transfos  2 = aansluitpunt  3 = zoekresultaten

        public LaagspanningGridView()
        {
            _database = new Database();
            _aansluitpunt = "";
            _mode = 0;
        }

        /* Tonen van het overzicht van de Transfos
         */
        public void ShowTransfos()
        {
            Reload("", Transfos);
        }

        /* Tonen van het _aansluitpunt.
         * 
         * Als parameter wordt een string meegegeven dat het 'nummer' van het _aansluitpunt is
         */
        public void ShowAansluitpunt(string ap)
        {
            Reload(ap, Aansluitpunt);
        }

        /* Tonen van zoekresultaten.
         * 
         * Als parameter wordt een string meegegeven met zoekterm
         */
        public void ShowSearch(string search)
        {
            Reload(search, Search);
        }

        /* Tonen van Zoekresultaten of Aansluitpunt
         * 
         * Eerste parameter = string = zoekterm of _aansluitpunt
         * Tweede Parameter = int    = 1. Transfos (eerste parameter wordt genegeerd)
         *                             2. Aansluitpunt
         *                             3. Search
         */
        public void Reload(string ap, int mode)
        {
            _aansluitpunt = ap;
            _mode = mode;
            
            // DataSet definieren waar de database gegevens in geladen worden
            DataSet dsDatabase;

            // Maak een nieuwe (lege) dataset aan waarin de gegevens komen zoals ze op het scherm getoond worden
            // en koppel die aan dgvLaagspanningsnet
            DataSet dsDisplay = new DataSet();
            _dtDisplay = new DataTable("Display");
            dsDisplay.Tables.Add(_dtDisplay);
            DataSource = dsDisplay.Tables[0];

            // De +/-/A Columns toevoegen
            _dtDisplay.Columns.Add(new DataColumn("+", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("-", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("A", typeof(string)));
            // De andere Columns toevoegen
            _dtDisplay.Columns.Add(new DataColumn("T/VB/K", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("Kring", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("Nummer", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("Omschrijving", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("Kabeltype", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("Kabelsectie", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("Stroom (A)", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("Aantal polen", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("Locatie", typeof(string)));
            _dtDisplay.Columns.Add(new DataColumn("Type", typeof(string)));          // Normaal ; Aansluitpunt ; Machine

            Columns["Type"].Visible = false;        // de column "Type" is enkel voor intern gebruik en wordt dus niet getoond

            // Andere gegevens uit database halen + op het scherm zetten naar gelang de modus
            switch (mode)
            {
                case Transfos:  
                {
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
                    dsDatabase = _database.GetTransfos();
                    break;
                }
                case Search:    
                {
                    // Zichtbaarheid instellen
                    Columns["+"].Visible = false;
                    Columns["-"].Visible = false;
                    Columns["A"].Visible = false;
                    // database gegevens ophalen
                    dsDatabase = _database.GetSearch(ap);
                    break;
                }
                default:    // case Aansluitpunt = default
                {
                    // Zichtbaarheid instellen
                    Columns["T/VB/K"].Visible = false;
                    // database gegevens ophalen
                    dsDatabase = _database.GetAansluitingen(ap);
                    break;
                }
            }

            // Zitten er wel gegevens in database DataSet?
            if (dsDatabase.Tables.Count == 0)
            {
                return; // Als er niks in zit, valt er niks te doen...
            }

            // Loop over de Database gegevens om ze te analyseren
            foreach (DataRow row in dsDatabase.Tables[0].Rows)
            {
                var dbApId = row["AP_id"];
                var dbAId = row["A_id"];
                var dbKabeltype = row["Kabeltype"];
                var dbKabelsectie = row["Kabelsectie"];
                var dbStroom = row["Stroom"];
                var dbPolen = row["Polen"];
                var dbOmschrijving = row["Omschrijving"];
                string dbLocatie = "";
                string dbNummer = "";
                string dbType = "N";    // zet standaard op Normaal.
                
                // Gaat deze aansluiting naar een ander _aansluitpunt?
                var dbNaarApId = row["Naar_AP_id"];
                if (dbNaarApId != DBNull.Value)
                {
                    dbNummer = (String)dbNaarApId;
                    dbType = "A";      // type = Aansluitpunt
                    dbLocatie = _database.GetAansluitpuntLocatie(dbNummer);                    // Locatie van _aansluitpunt ophalen
                }

                // Gaat deze aansluiting naar een machine?
                var dbNaarMId = row["Naar_M_id"];
                if (dbNaarMId != DBNull.Value)
                {
                    dbNummer = (String)dbNaarMId;
                    dbType = "M";      // type = Machine
                    dbLocatie = _database.GetMachineLocatie(dbNummer);                       // Locatie van machine ophalen
                    dbOmschrijving = _database.GetMachineOmschrijving((string)dbNaarMId);    // Bij een machine komt de omschrijving uit de machine DB
                }

                // Rij (velden) met de juiste waarden vullen
                DataRow dr = _dtDisplay.NewRow();
                dr["+"] = "+";
                dr["-"] = "-";
                dr["A"] = "A";
                dr["T/VB/K"] = dbApId;
                dr["Kring"] = dbAId;
                dr["Nummer"] = dbNummer;
                dr["Omschrijving"] = dbOmschrijving;
                dr["Kabeltype"] = dbKabeltype;
                dr["Kabelsectie"] = dbKabelsectie;
                dr["Stroom (A)"] = dbStroom;
                dr["Aantal polen"] = dbPolen;
                dr["Locatie"] = dbLocatie;
                dr["Type"] = dbType;
                _dtDisplay.Rows.Add(dr);
            }

            // En nog een extra lijn bijvoegen voor de extra "+" knop.
            if (mode == Aansluitpunt)
            {    // enkel bij modus Aansluitpunt
                DataRow extraDataRow = _dtDisplay.NewRow();
                extraDataRow[0] = "+";
                _dtDisplay.Rows.Add(extraDataRow);
            }

            // eerste 3 columns zijn de +/-A knoppen --> width op 25 zetten en HeaderText verbergen
            Columns["+"].Width = 25;
            Columns["+"].HeaderText = "";
            Columns["-"].Width = 25;
            Columns["-"].HeaderText = "";
            Columns["A"].Width = 25;
            Columns["A"].HeaderText = "";

            // width omschrijving instellen
            Columns["Omschrijving"].Width = Width / 3;

            // Niet sorteren van columns
            foreach (DataGridViewColumn column in Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Knoppen in de dgv aanmaken.
            foreach (DataGridViewRow row in Rows)
            {
                MakeButtons(row.Index);
            }
        }

        /* Maak in een bepaalde row van de DataGridView de knoppen aan voor +/-/A en Aansluitpunten
         * Maak op de laatste row alles grijs als het over de laatste lege lijn met een + gaat (bij weergave _aansluitpunt).
         * 
         * count = de index van de row
         */
        public void MakeButtons(int count)
        {
            // Maak van de +  een knop.
            Rows[count].Cells["+"] = new DataGridViewButtonCell();
            
            if (Rows[count].Cells["Type"].Value != DBNull.Value)    // op de laatste lege lijn met enkel een + heeft "Type" geen waarde.
            {
                // Maak van -/A en Aansluitpunten een knop.
                if ((string)Rows[count].Cells["Type"].Value == "A")
                {
                    Rows[count].Cells["Nummer"] = new DataGridViewButtonCell();
                }
                Rows[count].Cells["-"] = new DataGridViewButtonCell();
                Rows[count].Cells["A"] = new DataGridViewButtonCell();
                if(Rows[count].Cells["T/VB/K"].Value != DBNull.Value)
                { 
                    Rows[count].Cells["T/VB/K"] = new DataGridViewButtonCell();
                }
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
            return _dtDisplay;
        }

        public string GetAansluitpunt()
        {
            return _aansluitpunt;
        }

        public int GetMode()
        {
            return _mode;
        }

        public void Reload()
        {
            switch (_mode)
            {
                case Transfos:
                    ShowTransfos();
                    break;
                case Aansluitpunt:
                    // Stel, we staan op een aansluitpunt dat we hernoemd hebben en dus niet bestaat.
                    // In dat geval gaan we terug naar overzicht van de transfo's
                    if (_database.IsAansluitpunt(_aansluitpunt))
                    { 
                        ShowAansluitpunt(_aansluitpunt);
                    }
                    else
                    {
                        ShowTransfos();
                    }
                    break;
                case Search:
                    ShowSearch(_aansluitpunt);
                    break;
            }
        }
    }
}
