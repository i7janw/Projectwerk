/* Het hoofdvenster van het programma.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - .ico toegevoegd
 *      - DgvLaagspanningsnet_CellContentClick clean up & verbeterd , unsaved werkt nu correct
 *  - 20180324 :
 *      - Messagebox unsaved toegevoegd
 *      - Menu start en afsluiten toegevoegd
 */
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class Hoofdscherm : Form
    {
        private readonly Database _database;        // Alle communicatie met de database verloopt via de database klasse
        private bool _unsaved;                      // Staan er niet bewaarde gegevens op het scherm?
        
        public Hoofdscherm()
        {
            InitializeComponent();
            _database = new Database();
        }

        private void Hoofdscherm_Load(object sender, EventArgs e)
        {
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

            // Is er op een cell met een button gedrukt?
            if (sender is LaagspanningGridView dgv && dgv[e.ColumnIndex, e.RowIndex].GetType() != typeof(DataGridViewButtonCell))
            {
                return; // Geen button --> negeren
            }

            switch (e.ColumnIndex)
            {
                case 0: // +
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

                    // Open het venster om aanpassingen te doen
                    AansluitingAanpassen an = new AansluitingAanpassen(dgvLaagspanningsnet.GetDataTable(), e.RowIndex);
                    switch (an.ShowDialog())
                    {
                        case DialogResult.Cancel:
                            dgvLaagspanningsnet.GetDataTable().Rows.Remove(row); // Verwijder de toegevoegde rij als er op cancel is gedrukt.
                            break;
                        default:
                            dgvLaagspanningsnet.MakeButtons(e.RowIndex); // knoppen updaten
                            SetUnsaved(true); // er is iets aangepast --> unsaved = true
                            break;
                    }
                    break;
                case 1: // -
                    dgvLaagspanningsnet.GetDataTable().Rows.RemoveAt(e.RowIndex);
                    SetUnsaved(true);
                    break;
                case 2: // A
                    // Open het venster om aanpassingen te doen
                    AansluitingAanpassen aa = new AansluitingAanpassen(dgvLaagspanningsnet.GetDataTable(), e.RowIndex);
                    switch (aa.ShowDialog())
                    {
                        case DialogResult.Cancel:
                            break;
                        default:
                            dgvLaagspanningsnet.MakeButtons(e.RowIndex); // knoppen updaten
                            SetUnsaved(true); // er is iets aangepast --> unsaved = true
                            break;
                    }
                    break;
                default:
                    // Doorbladeren naar een ander aansluitpunt
                    if (!GetUnsaved()) dgvLaagspanningsnet.ShowAansluitpunt(dgvLaagspanningsnet.GetDataTable().Rows[e.RowIndex][e.ColumnIndex].ToString());
                    break;
            }
        }

        /* Als er op de knop van de voeding wordt geklikt, gaan we naar het scherm van dit aansluitpunt.
         */
        private void BtnDynVoeding_Click(object sender, EventArgs e)
        {
            if (!(sender is Button button)) return;
            if (GetUnsaved()) return;
                string ap = button.Text.Split(' ').First();     // Het eerste veld is de naam van het aansluitpunt
            if (ap.Equals("-"))
            {
                dgvLaagspanningsnet.ShowTransfos();
                return;
            }
            dgvLaagspanningsnet.ShowAansluitpunt(ap);
        }

        /* Bijhouden of data reeds in database is opgeslagen
         */
        private void SetUnsaved(bool status)
        {
            Console.WriteLine("UNSAVED" + status);
            _unsaved = status;
            btnSave.BackColor = _unsaved ? Color.Red : Color.Green;
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
            if (!_unsaved || dgvLaagspanningsnet.GetMode() != 2) return;
            
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
                DataRow rowDatabase = dtDatabase.NewRow();                                // nieuwe row dtDatabase 
                DataRow rowDisplay = dgvLaagspanningsnet.GetDataTable().Rows[count];      // lees een row dtDisplay

                rowDatabase["AP_id"] = dgvLaagspanningsnet.GetAansluitpunt();
                rowDatabase["A_id"] = rowDisplay["Kring"];
                if (rowDisplay["Type"].Equals("N"))
                {
                    rowDatabase["Naar_AP_id"] = null;
                    rowDatabase["Naar_M_id"] = null;
                    rowDatabase["Omschrijving"] = rowDisplay["Omschrijving"];
                }
                if (rowDisplay["Type"].Equals("M"))
                {
                    rowDatabase["Naar_AP_id"] = null;
                    rowDatabase["Naar_M_id"] = rowDisplay["Nummer"];
                    rowDatabase["Omschrijving"] = null;
                }
                if (rowDisplay["Type"].Equals("A"))
                {
                    rowDatabase["Naar_AP_id"] = rowDisplay["Nummer"];
                    rowDatabase["Naar_M_id"] = null;
                    rowDatabase["Omschrijving"] = null;
                }
                rowDatabase["Kabeltype"] = rowDisplay["Kabeltype"];
                rowDatabase["Kabelsectie"] = rowDisplay["Kabelsectie"];

                // stroom & polen = int --> via Int32.TryParse
                if (rowDisplay["Stroom (A)"] == DBNull.Value) rowDisplay["Stroom (A)"] = "";
                if (rowDisplay["Aantal polen"] == DBNull.Value) rowDisplay["Aantal polen"] = "";
                if (Int32.TryParse((string)rowDisplay["Stroom (A)"], out int convert))
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
            _database.SetAansluitingen(dgvLaagspanningsnet.GetAansluitpunt(), dsDatabase);

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
                SetUnsaved(false);
            }
        }

        /* Op de zoekknop klikken --> start zoeken
         */
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if(!GetUnsaved()) dgvLaagspanningsnet.ShowSearch(txtbxSearch.Text);
        }

        /* Op Enter drukken in de search box = op de zoekknop klikken
         */
        private void TxtbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Druk op de zoek-knop
                btnSearch.PerformClick();

                // Toegevoegd omdat anders een ding-sound wordt afgespeeld na het drukken op enter.
                // <https://stackoverflow.com/questions/6290967/stop-the-ding-when-pressing-enter>
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
                
        private void MenuMachineNieuw_Click(object sender, EventArgs e)
        {
            if (GetUnsaved()) return;
            MachineNieuw mn = new MachineNieuw();
            if (mn.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuMachineAanpassen_Click(object sender, EventArgs e)
        {
            if (GetUnsaved()) return;
            MachineAanpassen ma = new MachineAanpassen();
            if (ma.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload(); 
            }
        }

        private void MenuMachineVerwijderen_Click(object sender, EventArgs e)
        {
            if (GetUnsaved()) return;
            MachineVerwijderen mv = new MachineVerwijderen();
            if (mv.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuAansluitpuntNieuw_Click(object sender, EventArgs e)
        {
            AansluitpuntNieuw an = new AansluitpuntNieuw();
            if (an.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuAansluitpuntAanpassen_Click(object sender, EventArgs e)
        {
            AansluitpuntAanpassen aa = new AansluitpuntAanpassen();
            if (aa.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuAansluitpuntVerwijderen_Click(object sender, EventArgs e)
        {
            AansluitpuntVerwijderen av = new AansluitpuntVerwijderen();
            if (av.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuAfdrukken_Click(object sender, EventArgs e)
        {
            string aansluitpunt = dgvLaagspanningsnet.GetAansluitpunt();
            int mode = dgvLaagspanningsnet.GetMode();                       // Afdrukken pas de huidige view aan, oude view onthouden
            Afdrukken prn = new Afdrukken(dgvLaagspanningsnet);
            if (prn.ShowDialog() != DialogResult.Cancel)                    // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload(aansluitpunt, mode);             // oude view terugzetten.
            }
        }

        // Data in dgvLaagspanningsnet is ge-updated --> de nodige velden op het scherm updaten
        private string _huidigAansluitpunt;                                 // Gebruikt in 'DgvLaagspanningsnet_CellValueChanged' om enkel te updaten indien echt nodig
        private void DgvLaagspanningsnet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // We gaan er van uit dat als de data geupdated wordt, deze overeenkomt met de database.
            // Indien dit niet zo is, is het de taak van de andere methodes om SetUnsaved(true) uit te voeren.
            SetUnsaved(false);

            // Welk aansluitpunt wordt er nu getoond?
            string aansluitpunt = dgvLaagspanningsnet.GetAansluitpunt();

            // Enkel de labels op het scherm  updaten als het echt nodig is.
            if (_huidigAansluitpunt != null && _huidigAansluitpunt.Equals(aansluitpunt)) return;
            _huidigAansluitpunt = aansluitpunt;

            Console.WriteLine("Updating DataGridView"  + aansluitpunt);

            switch (dgvLaagspanningsnet.GetMode())
            {
                case 1:     // transfos
                    lblLayout.Text = "Overzicht transfos";
                    break;
                case 3:     // search
                    lblLayout.Text = "Zoeken : " + aansluitpunt;
                    break;
                default:    // aansluitpunt // case 2 = default
                    lblLayout.Text = "Layout van " + aansluitpunt;
                    break;
            }
            
            // Text in button voeding aanpassen
            btnDynVoeding.Text = _database.GetVoeding(aansluitpunt);

            // Text Locatie aanpassen
            lblDynLocatie.Text = _database.GetAansluitpuntLocatie(aansluitpunt);

            // Text kabel aanpassen
            lblDynKabel.Text = _database.GetKabel(aansluitpunt);

            // Text stroom aanpassen
            lblDynStroom.Text = _database.GetStroom(aansluitpunt);
        }

        // Zijn er niet bewaarde gegevens
        private bool GetUnsaved()
        {
            // Als er geen unsaved data is, geven we dit door
            if (_unsaved == false) return false;

            // Als er wel unsaved data is, geven we de mogelijkheid om de gevens op te slaan
            DialogResult dialogResult = MessageBox.Show("Wilt u de wijzigingen die zijn aangebracht opslaan?", "Opslaan?", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)           // YES
            {
                btnSave.PerformClick();                     // gegevens opslaan
                return false;
            }
            if (dialogResult == DialogResult.No)            // NO
            {
                return false;                               // we negeren het feit dat de gegevens niet zijn opgeslagen
            }
            // Cancel
            return true;                                    // er zijn niet opgeslagen gegevens
        }

        // Event : venster wordt gesloten
        private void Hoofdscherm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GetUnsaved()) e.Cancel = true;              // Als er unsaved data is, niet sluiten!
        }

        // Menu : Afsluiten geklikt
        private void MenuAfsluiten_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Menu : Start geklikt
        private void MenuStart_Click(object sender, EventArgs e)
        {
            if(!GetUnsaved()) dgvLaagspanningsnet.ShowTransfos();
        }
    }
}