/* Venster dat verschijnt als er Aansluitpunt -> Hernoemen is gekozen.
 *
 * Aanpassingen :
 *  - 20180508 :
 *      - MessageboxIcon aan messageboxen toegevoegd
 *  - 20180509 :
 *      - Plaats de cursor automatisch in het nieuw aansluitpunt ID tekstveld
 *  - 20180511 :
 *      - Constructor AansluitpuntHernoemen(string[] aansluitpuntId) toegevoegd
 *        --> mogelijk om naamvoorstel voor een aansluitpunt door te geven
 */
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class AansluitpuntHernoemen : Form
    {
        private readonly Database _database;
        private readonly string[] _aansluitpunt;

        // Aansluitpunt hernoemen
        public AansluitpuntHernoemen()
        {
            InitializeComponent();
            _database = new Database();
            _aansluitpunt = new string[2];
            _aansluitpunt[0] = "";
            _aansluitpunt[1] = "";
        }

        // Aansluitpunt hernoemen : 
        // aansluitpuntId[0] = naam te hernoemen aansluitpunt
        // aansluitpuntId[1] = naamvoorstel voor nieuwe naam aansluitpunt
        public AansluitpuntHernoemen(string[] aansluitpuntId)
        {
            InitializeComponent();
            _database = new Database();
            _aansluitpunt = aansluitpuntId;
        }

        private void AansluitpuntHernoemenLoad(object sender, EventArgs e)
        {
            // Haal lijst met alle aansluitpunten op
            BindingList<string> listAansluitpunt = _database.GetAansluitpunten();

            // Steek ze in de selectie combobox
            cmbAansluitpunt.DataSource = listAansluitpunt;

            // beperk de lengte van de velden - zie datawoordenboek
            txtbxAansluitpunt.MaxLength = 10;

            // Plaats de cursor automatisch in het nieuw aansluitpunt ID tekstveld
            txtbxAansluitpunt.Select();

            // Selecteer de huidige naam van het aansluitpunt
            if (_aansluitpunt[0].Length > 0 ) cmbAansluitpunt.Text = _aansluitpunt[0];

            // Stel een naam voor
            if (_aansluitpunt[1].Length > 0) txtbxAansluitpunt.Text = _aansluitpunt[1];
        }

        // Er is op de OK knop geklikt.
        private void BtnOkClick(object sender, EventArgs e)
        {
            // 1. We dupliceren eerst het aansluitpunt naar de nieuwe naam.
            if (txtbxAansluitpunt.Text.Equals(""))
            {
                MessageBox.Show("Nieuw Aansluitpunt ID mag niet leeg zijn.", "Leeg ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // Ga na of dit Aansluitpunt ID reeds bestaat.
            if (_database.IsAansluitpunt(txtbxAansluitpunt.Text))
            {
                MessageBox.Show("Dit aansluitpunt bestaat reeds!", "Dubbel ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string locatie = _database.GetAansluitpuntLocatie(cmbAansluitpunt.Text);
            // Voeg het aansluitpunt toe aan de database
            _database.InsertAansluitpunt(txtbxAansluitpunt.Text, locatie);

            // 2. We updaten alle aansluitingen naar de nieuwe naam
            _database.UpdateAansluitingen(cmbAansluitpunt.Text, txtbxAansluitpunt.Text);

            // 3. We wissen het oud aansluitpunt
            _database.DeleteAansluitpunt(cmbAansluitpunt.Text);

            // 4. Terugkoppeling van de nieuwe naam
            _aansluitpunt[0] = txtbxAansluitpunt.Text;

            // sluit het venster
            DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        // In de id box kunnen enkel cijfers en letters ingegeven worden.
        // Bron : <https://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers>
        private void TxtbxAansluitpuntKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Andere aansluitpunt uit het menu gekozen
        private void CmbAansluitpuntSelectedIndexChanged(object sender, EventArgs e)
        {
            // Plaats de cursor automatisch in het nieuw aansluitpunt ID tekstveld
            txtbxAansluitpunt.Select();
        }
    }
}
