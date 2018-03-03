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
        private bool unsaved;           // Staan er niet bewaarde gegevens op het scherm?
        
        public Hoofdscherm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            database = new Database();
            dgvLaagspanningsnet.ShowTransfos();             // We starten met een overzicht van de Transfos
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
                DataRow row = dgvLaagspanningsnet.GetDataTable().NewRow();
                row["+"] = "+";
                row["-"] = "-";
                row["A"] = "A";
                row["Kring"] = "Nieuw";
                row["Type"] = "N";
                row["T/VB/K"] = dgvLaagspanningsnet.GetAansluitpunt();
                dgvLaagspanningsnet.GetDataTable().Rows.InsertAt(row, e.RowIndex);
                // maak van de +/-/A velden reeds knoppen
                dgvLaagspanningsnet.MakeButtons(e.RowIndex);
                AansluitingAanpassen aa = new AansluitingAanpassen(dgvLaagspanningsnet.GetDataTable(), e.RowIndex);
                if (aa.ShowDialog() == DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
                {
                    dgvLaagspanningsnet.GetDataTable().Rows.Remove(row);                 // Verwijder de toegevoegde rij als er op cancel is gedrukt.
                    return;
                }
                // knoppen updaten
                dgvLaagspanningsnet.MakeButtons(e.RowIndex);
                return;
            }
            if (e.ColumnIndex == 1) // -
            {
                dgvLaagspanningsnet.GetDataTable().Rows.RemoveAt(e.RowIndex);
                SetUnsaved(true);
                return;
            }
            if (e.ColumnIndex == 2) // A
            {
                SetUnsaved(true);
                // Open het venster om aanpassingen te doen
                AansluitingAanpassen aa = new AansluitingAanpassen(dgvLaagspanningsnet.GetDataTable(), e.RowIndex);
                aa.ShowDialog();    // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
                // knoppen updaten
                dgvLaagspanningsnet.MakeButtons(e.RowIndex);
                return;
            }

            // Doorbladeren naar een ander aansluitpunt
            dgvLaagspanningsnet.ShowAansluitpunt(dgvLaagspanningsnet.GetDataTable().Rows[e.RowIndex][e.ColumnIndex].ToString());
        }

        /* Als er op de knop van de voeding wordt geklikt, gaan we naar het scherm van dit aansluitpunt.
         */
        private void BtnDynVoeding_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String ap = button.Text.Split(' ').First();
            if (ap == "-")
            {
                dgvLaagspanningsnet.ShowTransfos();
                return;
            }
            dgvLaagspanningsnet.ShowAansluitpunt(ap);
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
            if (!unsaved || dgvLaagspanningsnet.GetMode() != 2)
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
            for (int count = 0; count < dgvLaagspanningsnet.GetDataTable().Select().Length - 1; count++)
            {
                DataRow rowDatabase = dtDatabase.NewRow();       // nieuwe row dtDatabase 
                DataRow rowDisplay = dgvLaagspanningsnet.GetDataTable().Rows[count];      // lees een row dtDisplay

                rowDatabase["AP_id"] = dgvLaagspanningsnet.GetAansluitpunt();
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
            dgvLaagspanningsnet.Reload();
        }

        /* UNDO : Aanpassingen ongedaan maken = database terug inlezen en tonen
         */
        private void BtnUndo_Click(object sender, EventArgs e)
        {
            if (dgvLaagspanningsnet.GetMode() == 2)
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        /* Op de zoekknop klikken --> start zoeken
         */
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            dgvLaagspanningsnet.ShowSearch(txtbxSearch.Text);
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
                
        // ---------------------------------- NEW ----------------------------------------------------------------------------

        private void NieuwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineNieuw mn = new MachineNieuw();
            if (mn.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void aanpassenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineAanpassen ma = new MachineAanpassen();
            if (ma.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload(); 
            }
        }

        private void verwijderenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineVerwijderen mv = new MachineVerwijderen();
            if (mv.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void nieuwToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AansluitpuntNieuw an = new AansluitpuntNieuw();
            if (an.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
            
        }

        private void aanpassenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AansluitpuntAanpassen aa = new AansluitpuntAanpassen();
            if (aa.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void verwijderenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AansluitpuntVerwijderen av = new AansluitpuntVerwijderen();
            if (av.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        // Voorlopig een test als er op afdrukken wordt geklikt
        private void afdrukkenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String _ap = dgvLaagspanningsnet.GetAansluitpunt();
            int _mode = dgvLaagspanningsnet.GetMode();              // Afdrukken pas de huidige view aan, oude view onthouden
            Afdrukken prn = new Afdrukken(dgvLaagspanningsnet);
            if (prn.ShowDialog() != DialogResult.Cancel)            // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload(_ap, _mode);             // oude view terugzetten.
            }
        }

        private void dgvLaagspanningsnet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            String _ap = dgvLaagspanningsnet.GetAansluitpunt();
            switch (dgvLaagspanningsnet.GetMode())
            {
                case 1:     // transfos
                    lblLayout.Text = "Overzicht transfos";
                    break;
                case 3:     // search
                    lblLayout.Text = "Zoeken : " + _ap;
                    break;
                default:    // aansluitpunt // case 2 = default
                    lblLayout.Text = "Layout van " + _ap;
                    break;
            }
            
            // Text in button voeding aanpassen
            btnDynVoeding.Text = database.GetVoeding(_ap);

            // Text Locatie aanpassen
            lblDynLocatie.Text = database.GetAansluitpuntLocatie(_ap);

            // Text kabel aanpassen
            lblDynKabel.Text = database.GetKabel(_ap);

            // Text stroom aanpassen
            lblDynStroom.Text = database.GetStroom(_ap);

            // Alle data staat op het scherm --> unsaved=false
            SetUnsaved(false);
        }
    }
}