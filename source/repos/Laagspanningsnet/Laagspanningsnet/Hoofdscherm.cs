/* Het hoofdvenster van het programma.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - .ico toegevoegd
 *      - DgvLaagspanningsnet_CellContentClick clean up & verbeterd , unsaved werkt nu correct
 *  - 20180324 :
 *      - Messagebox unsaved toegevoegd
 *      - Menu start en afsluiten toegevoegd
 *  - 20180430 :
 *      - save & undo knoppen verwijderd
 *  - 20180502 :
 *      - menu's aangepast (afsluiten, afdrukken, overzicht --> laagspanningsnet)
 *      - knop zoeken naast input veld (alles in een panel gestoken)
 */
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class Hoofdscherm : Form
    {
        private readonly Database _database;        // Alle communicatie met de database verloopt via de database klasse
        
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
        private void DgvLaagspanningsnetCellContentClick(object sender, DataGridViewCellEventArgs e)
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
                            // dgvLaagspanningsnet.MakeButtons(e.RowIndex);    // knoppen updaten
                            Save(e.ColumnIndex, e.RowIndex);                // Aanpassingen opslaan
                            break;
                    }
                    break;
                case 1: // -
                    // dgvLaagspanningsnet.GetDataTable().Rows.RemoveAt(e.RowIndex);
                    DialogResult result = MessageBox.Show("Kring " + dgvLaagspanningsnet.GetDataTable().Rows[e.RowIndex]["Kring"] + " verwijderen?", "Verwijderen?", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Save(e.ColumnIndex, e.RowIndex);
                    }
                    break;
                case 2: // A
                    // Open het venster om aanpassingen te doen
                    AansluitingAanpassen aa = new AansluitingAanpassen(dgvLaagspanningsnet.GetDataTable(), e.RowIndex);
                    switch (aa.ShowDialog())
                    {
                        case DialogResult.Cancel:
                            break;
                        default:
                            // dgvLaagspanningsnet.MakeButtons(e.RowIndex); // knoppen updaten
                            Save(e.ColumnIndex, e.RowIndex); // er is iets aangepast --> unsaved = true
                            break;
                    }
                    break;
                default:
                    // Doorbladeren naar een ander aansluitpunt
                    dgvLaagspanningsnet.ShowAansluitpunt(dgvLaagspanningsnet.GetDataTable().Rows[e.RowIndex][e.ColumnIndex].ToString());
                    break;
            }
        }

        /* Als er op de knop van de voeding wordt geklikt, gaan we naar het scherm van dit aansluitpunt.
         */
        private void BtnDynVoedingClick(object sender, EventArgs e)
        {
            if (!(sender is Button button)) return;
            string ap = button.Text.Split(' ').First();     // Het eerste veld is de naam van het aansluitpunt
            if (ap.Equals("-"))
            {
                dgvLaagspanningsnet.ShowTransfos();
                return;
            }
            dgvLaagspanningsnet.ShowAansluitpunt(ap);
        }

        /* De blauwe selectie-balk hebben we in dit programma niet nodig.
         * --> disable
         * 
         * Info:
         * <https://stackoverflow.com/questions/11330147/how-to-disable-the-ability-to-select-in-a-datagridview>
         */
        private void DgvLaagspanningsnetSelectionChanged(object sender, EventArgs e)
        {
            dgvLaagspanningsnet.ClearSelection();
        }

        /* Bewaren van gegevens in de database.
         */
        private void Save(int plusMinAdd, int count)
        {
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

            // Maak een database dataRow aan met de juiste inhoud
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

            switch (plusMinAdd)
            {
                case 0: // + : row toevoegen aan de database
                    _database.InsertAansluiting(rowDatabase);
                    break;
                case 1: // - : row verwijderen van de database
                    _database.DeleteAansluiting(rowDatabase);
                    break;
                case 2: // A : row updaten in de database
                    _database.UpdateAansluiting(rowDatabase);
                    break;
            }

            // Gegevens terug inladen zodat hetgene op het scherm staat zeker hetzelfde is als in de database is opgeslagen
            dgvLaagspanningsnet.Reload();
        }

        /* Op de zoekknop klikken --> start zoeken
         */
        private void BtnSearchClick(object sender, EventArgs e)
        {
            dgvLaagspanningsnet.ShowSearch(txtbxSearch.Text);
        }

        /* Op Enter drukken in de search box = op de zoekknop klikken
         */
        private void TxtbxSearchKeyDown(object sender, KeyEventArgs e)
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
                
        private void MenuMachineNieuwClick(object sender, EventArgs e)
        {
            MachineNieuw mn = new MachineNieuw();
            if (mn.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuMachineAanpassenClick(object sender, EventArgs e)
        {
            MachineAanpassen ma = new MachineAanpassen();
            if (ma.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload(); 
            }
        }

        private void MenuMachineVerwijderenClick(object sender, EventArgs e)
        {
            MachineVerwijderen mv = new MachineVerwijderen();
            if (mv.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuAansluitpuntNieuwClick(object sender, EventArgs e)
        {
            AansluitpuntNieuw an = new AansluitpuntNieuw();
            if (an.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuAansluitpuntAanpassenClick(object sender, EventArgs e)
        {
            AansluitpuntAanpassen aa = new AansluitpuntAanpassen();
            if (aa.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuAansluitpuntVerwijderenClick(object sender, EventArgs e)
        {
            AansluitpuntVerwijderen av = new AansluitpuntVerwijderen();
            if (av.ShowDialog() != DialogResult.Cancel)      // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
            {
                dgvLaagspanningsnet.Reload();
            }
        }

        private void MenuAfdrukkenClick(object sender, EventArgs e)
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
        private string _huidigAansluitpunt;                                 // Gebruikt in 'DgvLaagspanningsnetCellValueChanged' om enkel te updaten indien echt nodig
        private void DgvLaagspanningsnetCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Welk aansluitpunt wordt er nu getoond?
            string aansluitpunt = dgvLaagspanningsnet.GetAansluitpunt();

            // Enkel de labels op het scherm  updaten als het echt nodig is.
            if (_huidigAansluitpunt != null && _huidigAansluitpunt.Equals(aansluitpunt)) return;
            _huidigAansluitpunt = aansluitpunt;

            Console.WriteLine("Updating DataGridView"  + aansluitpunt);

            switch (dgvLaagspanningsnet.GetMode())
            {
                case LaagspanningGridView.Transfos:    
                    lblLayout.Text = "Overzicht transfos";
                    break;
                case LaagspanningGridView.Search:     
                    lblLayout.Text = "Zoeken : " + aansluitpunt;
                    break;
                default:    // case Aansluitpunt = default
                    lblLayout.Text = "Layout van " + aansluitpunt;
                    break;
            }
            
            // Text in button voeding aanpassen
            btnDynVoeding.Text = _database.GetVoeding(aansluitpunt);

            // Kruimelpad updaten
            lblKruimelpad.Text = "";
            if (dgvLaagspanningsnet.GetMode() != LaagspanningGridView.Transfos)
            {
                string kruimelpad = "";
                string txt = "";
                string ap = aansluitpunt;

                while (!ap.Equals("-"))
                {
                    txt = _database.GetVoeding(ap);
                    ap = txt.Split(' ').First();
                    if (txt.Equals("-"))
                    {
                        if(dgvLaagspanningsnet.GetMode() == LaagspanningGridView.Search)
                        {
                            kruimelpad = "[Zoeken: " + kruimelpad;
                        }
                        else
                        {
                            kruimelpad = "[" + kruimelpad;
                        }
                    }
                    else
                    { 
                        kruimelpad = txt + " > " + kruimelpad;
                    }
                } 
                kruimelpad = kruimelpad + aansluitpunt +"]";
                lblKruimelpad.Text = kruimelpad;
            }





            // Text Locatie aanpassen
            lblDynLocatie.Text = _database.GetAansluitpuntLocatie(aansluitpunt);

            // Text kabel aanpassen
            lblDynKabel.Text = _database.GetKabel(aansluitpunt);

            // Text stroom aanpassen
            lblDynStroom.Text = _database.GetStroom(aansluitpunt);

            Console.WriteLine("Updating DataGridView DONE" + aansluitpunt);
        }

        // Menu : Afsluiten geklikt
        private void MenuAfsluitenClick(object sender, EventArgs e)
        {
            Close();
        }

        // Menu : Overzicht transfos geklikt
        private void MenuTransfoClick(object sender, EventArgs e)
        {
            dgvLaagspanningsnet.ShowTransfos();
        }
    }
}