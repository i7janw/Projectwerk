using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Deze verwijderen als afdrukken in een aparte klasse steekt.
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

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
            ShowTransfos();             // We starten met een overzicht van de Transfos
        }

        /* Tonen van het overzicht van de Transfos
         */
        private void ShowTransfos()
        {
            ShowCommon("", 1);

        }

        /* Tonen van het aansluitpunt.
         * 
         * Als parameter wordt een string meegegeven dat het 'nummer' van het aansluitpunt is
         */
        private void ShowAansluitpunt(String _ap)
        {
            ShowCommon(_ap, 2);
        }

        /* Tonen van zoekresultaten.
         * 
         * Als parameter wordt een string meegegeven met zoekterm
         */
        private void ShowSearch(String _search)
        {
            ShowCommon(_search, 3);
        }

        /* Tonen van Zoekresultaten of Aansluitpunt
         * 
         * Eerste parameter = string = zoekterm of aansluitpunt
         * Tweede Parameter = int    = 1. Transfos (eerste parameter wordt genegeerd)
         *                             2. Aansluitpunt
         *                             3. Search
         */
        private void ShowCommon(String _ap, int _mode)
        {
            // DataSet definieren waar de database gegevens in geladen worden
            DataSet dsDatabase;

            // Maak een nieuwe (lege) dataset aan waarin de gegevens komen zoals ze op het sherm getoond worden
            // en koppel die aan dgvLaagspanningsnet
            DataSet dsDisplay = new DataSet();
            dtDisplay = new DataTable("Display");
            dsDisplay.Tables.Add(dtDisplay);
            dgvLaagspanningsnet.DataSource = dsDisplay.Tables[0];

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
            dgvLaagspanningsnet.Columns["Type"].Visible = false;                    // de column "Type" is enkel voor intern gebruik en wordt dus niet getoond

            // Andere gegevens uit database halen + op het scherm zetten naar gelang de modus
            switch (_mode)
            {
                case 1:     // transfos
                    // Zichtbaarheid instellen
                    dgvLaagspanningsnet.Columns["+"].Visible = false;
                    dgvLaagspanningsnet.Columns["-"].Visible = false;
                    dgvLaagspanningsnet.Columns["A"].Visible = false;
                    dgvLaagspanningsnet.Columns["T/VB/K"].Visible = false;
                    //dgvLaagspanningsnet.Columns["Kring"].Visible = false;
                    dgvLaagspanningsnet.Columns["Omschrijving"].Visible = false;
                    dgvLaagspanningsnet.Columns["Kabeltype"].Visible = false;
                    dgvLaagspanningsnet.Columns["Kabelsectie"].Visible = false;
                    dgvLaagspanningsnet.Columns["Stroom (A)"].Visible = false;
                    //dgvLaagspanningsnet.Columns["Aantal polen"].Visible = false;
                    // aansluitpunt + titel aanpassen
                    this.aansluitpunt = "";
                    lblLayout.Text = "Overzicht transfos";
                    // database gegevens ophalen
                    dsDatabase = database.GetTransfos();
                    break;
                case 3:     // search
                    // Zichtbaarheid instellen
                    dgvLaagspanningsnet.Columns["+"].Visible = false;
                    dgvLaagspanningsnet.Columns["-"].Visible = false;
                    dgvLaagspanningsnet.Columns["A"].Visible = false;
                    // aansluitpunt + titel aanpassen
                    this.aansluitpunt = "";
                    lblLayout.Text = "Zoeken : " + _ap;
                    // database gegevens ophalen
                    dsDatabase = database.GetSearch(_ap);
                    break;
                default:    // aansluitpunt // case 2 = default
                    // Zichtbaarheid instellen
                    dgvLaagspanningsnet.Columns["T/VB/K"].Visible = false;
                    // aansluitpunt + titel aanpassen
                    this.aansluitpunt = _ap;
                    lblLayout.Text = "Layout van " + aansluitpunt;
                    // database gegevens ophalen
                    dsDatabase = database.GetAansluitingen(aansluitpunt);
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
            if(_mode == 2) {    // enkel bij modus (2)aansluitpunt
                DataRow extraDataRow = dtDisplay.NewRow();
                extraDataRow[0] = "+";
                dtDisplay.Rows.Add(extraDataRow);
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
            btnDynVoeding.Text = database.GetVoeding(aansluitpunt);

            // Text Locatie aanpassen
            lblDynLocatie.Text = database.GetAansluitpuntLocatie(aansluitpunt);

            // Text kabel aanpassen
            lblDynKabel.Text = database.GetKabel(aansluitpunt);

            // Text stroom aanpassen
            lblDynStroom.Text = database.GetStroom(aansluitpunt);

            // Alle data staat op het scherm --> unsaved=false
            SetUnsaved(false);

            // Knoppen in de dgv aanmaken.
            int count = 0;
            foreach (DataGridViewRow row in dgvLaagspanningsnet.Rows)
            {
                MakeButtons(count);
                count++;
            }
        }

        /* Is er op een cell van het dataGrid geklikt?
         */
        private void DgvLaagspanningsnet_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                SetUnsaved(true);
                // Maak een nieuwe dataRow aan en vul deze met default gegevens
                DataRow row = dtDisplay.NewRow();
                row["+"] = "+";
                row["-"] = "-";
                row["A"] = "A";
                row["Kring"] = "Nieuw";
                row["Type"] = "N";
                row["T/VB/K"] = aansluitpunt;
                dtDisplay.Rows.InsertAt(row, e.RowIndex);
                // maak van de +/-/A velden reeds knoppen
                MakeButtons(e.RowIndex);
                AansluitingAanpassen aa = new AansluitingAanpassen(dtDisplay, e.RowIndex);
                if (aa.ShowDialog() == DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
                {
                    dtDisplay.Rows.Remove(row);                 // Verwijder de toegevoegde rij als er op cancel is gedrukt.
                    return;
                }
                // knoppen updaten
                MakeButtons(e.RowIndex);
                return;
            }
            if (e.ColumnIndex == 1) // -
            {
                dtDisplay.Rows.RemoveAt(e.RowIndex);
                SetUnsaved(true);
                return;
            }
            if (e.ColumnIndex == 2) // A
            {
                SetUnsaved(true);
                // Open het venster om aanpassingen te doen
                AansluitingAanpassen aa = new AansluitingAanpassen(dtDisplay, e.RowIndex);
                aa.ShowDialog();    // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
                // knoppen updaten
                MakeButtons(e.RowIndex);
                return;
            }

            // Doorbladeren naar een ander aansluitpunt
            ShowAansluitpunt(dtDisplay.Rows[e.RowIndex][e.ColumnIndex].ToString());
        }

        /* Als er op de knop van de voeding wordt geklikt, gaan we naar het scherm van dit aansluitpunt.
         */
        private void BtnDynVoeding_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String ap = button.Text.Split(' ').First();
            if (ap == "-")
            {
                ShowTransfos();
                return;
            }
            ShowAansluitpunt(ap);
        }

        /* Bijhouden of data reeds in database is opgeslagen
         */
        private void SetUnsaved(bool _status)
        {
            unsaved = _status;
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
        private void DgvLaagspanningsnet_SelectionChanged(object sender, EventArgs e)
        {
            dgvLaagspanningsnet.ClearSelection();
        }

        /* Er is op de knop save geklikt.
         */
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Als er geen unsaved data is, moet er niks gebeuren.
            // Als er geen aansluitpunt getoond wordt, kan er niets veranderd zijn --> niks te doen.
            if (!unsaved || aansluitpunt == "")
            {
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
            database.SetAansluitingen(dsDatabase);

            // Gegevens terug inladen zodat hetgene op het scherm staat zeker hetzelfde is als in de database is opgeslagen
            ShowAansluitpunt(aansluitpunt);
        }

        /* UNDO : Aanpassingen ongedaan maken = database terug inlezen en tonen
         */
        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (aansluitpunt != "")
            {
                ShowAansluitpunt(aansluitpunt);
            }
        }

        /* Op de zoekknop klikken --> start zoeken
         */
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ShowSearch(txtbxSearch.Text);
        }

        /* Op Enter drukken in de search box = op de zoekknop klikken
         */
        private void TxtbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSearch_Click(this, new EventArgs());

                // Toegevoegd omdat anders een ding-sound wordt afgespeeld na het drukken op enter.
                // <https://stackoverflow.com/questions/6290967/stop-the-ding-when-pressing-enter>
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /* Maak in een bepaalde row van de DataGridView de knoppen aan voor +/-/A en Aansluitpunten
         * Maak op de laatste row alles grijs als het over de laatste lege lijn met een + gaat (bij weergave aansluitpunt).
         * 
         * _count = de index van de row
         */
        private void MakeButtons(int _count)
        {
            // Maak van de +  een knop.
            dgvLaagspanningsnet.Rows[_count].Cells["+"] = new DataGridViewButtonCell();

            if (dgvLaagspanningsnet.Rows[_count].Cells["Type"].Value != DBNull.Value)    // op de laatste lege lijn met enkel een + heeft "Type" geen waarde.
            {
                // Maak van -/A en Aansluitpunten een knop.
                if ((String)dgvLaagspanningsnet.Rows[_count].Cells["Type"].Value == "A")
                {
                    dgvLaagspanningsnet.Rows[_count].Cells["Nummer"] = new DataGridViewButtonCell();
                }
                dgvLaagspanningsnet.Rows[_count].Cells["-"] = new DataGridViewButtonCell();
                dgvLaagspanningsnet.Rows[_count].Cells["A"] = new DataGridViewButtonCell();
            }
            else // deze code wordt uitgevoerd als we op de laatste lege lijn met enkel een + zijn 
            {
                // Op deze rij alles grijs maken, behalve de +knop.
                for (int y = 1; y < dgvLaagspanningsnet.ColumnCount; y++) 
                {
                    dgvLaagspanningsnet.Rows[dgvLaagspanningsnet.RowCount - 1].Cells[y].Style.BackColor = Color.DarkGray;
                }
            }
        }

        // ---------------------------------- NEW ----------------------------------------------------------------------------

        private void NieuwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineNieuw mn = new MachineNieuw();
            if (mn.ShowDialog() == DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                return;
            }
        }

        private void aanpassenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineAanpassen ma = new MachineAanpassen();
            if (ma.ShowDialog() == DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                return;
            }

            // Vermits een machine is geupdated, kan het zijn dat op het scherm nog oude waarden staan --> refresh
            if (aansluitpunt == "")
            {
                ShowTransfos();
                return;
            }
            ShowAansluitpunt(aansluitpunt);
        }

        private void verwijderenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineVerwijderen mv = new MachineVerwijderen();
            if (mv.ShowDialog() == DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                return;
            }
        }

        private void nieuwToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AansluitpuntNieuw an = new AansluitpuntNieuw();
            if (an.ShowDialog() == DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                return;
            }
            // Wanneer de transfos() getoond worden, deze updaten want het kan zijn dat er een transfo toegevoegd is
            if (aansluitpunt == "")
            {
                ShowTransfos();
                return;
            }
        }

        private void aanpassenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AansluitpuntAanpassen aa = new AansluitpuntAanpassen();
            if (aa.ShowDialog() == DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                return;
            }

            // Vermits een machine is geupdated, kan het zijn dat op het scherm nog oude waarden staan --> refresh
            if (aansluitpunt == "")
            {
                ShowTransfos();
                return;
            }
            ShowAansluitpunt(aansluitpunt);
        }

        private void verwijderenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AansluitpuntVerwijderen av = new AansluitpuntVerwijderen();
            if (av.ShowDialog() == DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                return;
            }

            // Wanneer de transfos() getoond worden, deze updaten want het kan zijn dat er een transfo verwijderd is
            if (aansluitpunt == "")
            {
                ShowTransfos();
                return;
            }
        }

        // Voorlopig een test als er op afdrukken wordt geklikt
        private void afdrukkenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String selectie = aansluitpunt;
            String printer = "";
            int kopies = 0;
            bool inclusief = false;

            using (var prn = new Afdrukken(selectie))
            {
                //    Afdrukken prn = new Afdrukken(selectie, printer, kopies);
                var result = prn.ShowDialog();
                if (result == DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
                {
                    return;
                }
                selectie = prn.selectie;
                printer = prn.printer;
                kopies = prn.kopies;
                inclusief = prn.inclusief;
            }
            
            // Afdrukken. 
            // We starten met het afdrukken van de selectie en
            // indien gewenst worden ook de aansluitpunten die op de selectie zijn aangesloten afgedrukt.
            List<string> todo = new List<string>();         // todo = lijst van aansluitpunten waarvan we nog moeten testen of er aansluitpunten op zijn aangesloten
            todo.Add(selectie);

            while (todo.Count != 0) {                       // zijn er nog te testen aansluitpunten?
                List<string> tmp = new List<string>();      // tmp = om nieuwe todo lijst aan te maken
                foreach (String ap in todo)                 // doorloop alle aansluitpunten in todo 
                {
                    ShowAansluitpunt(ap);                   // Toon en Print selectie
                    Print();
                    if (inclusief)                          // Gaan we ook de aangesloten aansluitpunten afdrukken?
                    {
                        foreach (DataGridViewRow row in dgvLaagspanningsnet.Rows)
                        {
                            if (row.Cells["Type"].Value != DBNull.Value)
                            {
                                if ((String)row.Cells["Type"].Value == "A")             // is de aansluiting een aansluitpunt?
                                {
                                    tmp.Add((String)row.Cells["Nummer"].Value);         // voeg toe aan tmp (nieuwe todo lijst)
                                }
                            }
                        }
                    }
                }
                todo = tmp;                                 // todo = nieuwe todo lijst.
            }
        }

        private void Print()
        {
            
            // Hieronder een eerste test ivm afdrukken

            Document doc = new Document(PageSize.A4);
            var output = new FileStream("C:\\Users\\Jan Wagemakers\\Documents\\MEGA\\" + aansluitpunt + ".pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);
            doc.Open();

            iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10);
            iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 32);

            Paragraph text;
            // Titel
            text = new Paragraph(lblLayout.Text, titleFont);
            text.Alignment = Element.ALIGN_CENTER;
            text.SpacingAfter = 20;
            doc.Add(text);

            // Info
            text = new Paragraph("Voeding : " + btnDynVoeding.Text + "\n" +
                "Kabel : " + lblDynKabel.Text + "\n" +
                "Stroom : " + lblDynStroom.Text + "\n" +
                "Locatie : " + lblDynLocatie.Text, font);
            text.Alignment = Element.ALIGN_LEFT;
            text.SpacingAfter = 20;
            doc.Add(text);

            // Table
            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100;
            float[] widths = new float[] { 1,2,5,3,2,2,2 };
            table.SetWidths(widths);
            //PdfPRow row = null;
            //float[] widths = new float[] { 4f, 4f, 4f, 4f };

            table.AddCell(new Phrase("Kring", font));
            table.AddCell(new Phrase("Nummer", font));
            table.AddCell(new Phrase("Omschrijving", font));
            table.AddCell(new Phrase("Kabel", font));
            table.AddCell(new Phrase("Stroom", font));
            table.AddCell(new Phrase("Polen", font));
            table.AddCell(new Phrase("Locatie", font));

            foreach (DataRow dtRow in dtDisplay.Rows)
            {
                if (dtRow["Type"] == DBNull.Value)
                {
                    break;
                }
                table.AddCell(new Phrase(dtRow["Kring"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Nummer"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Omschrijving"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Kabeltype"].ToString() + " " + dtRow["Kabelsectie"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Stroom (A)"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Aantal Polen"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Locatie"].ToString(), font));
            }
            doc.Add(table);
            doc.Close();
        }
    }
}
