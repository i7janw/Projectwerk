/* Venster dat verschijnt als er Aansluitpunt -> Nieuw is gekozen.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - TxtbxAansluitpunt_KeyPress toegevoegd, enkel cijfers en letters kunnen ingevoegd worden.
 *        Fix voor probleem 'VB810 ' ingeven --> database error want 'VB810' bestaat reeds.
 *      - .ico toegevoegd
 *  - 20180402 :
 *      - constructor 'public AansluitpuntNieuw(string[] aansluitpuntId)' toegevoegd
 *      - check leeg aansluitpunt ID
 */
using System;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class AansluitpuntNieuw : Form
    {
        private readonly Database _database;
        private readonly string[] _aansluitpunt;

        public AansluitpuntNieuw()
        {
            InitializeComponent();
            _database = new Database();
            _aansluitpunt = new string[1];
        }

        public AansluitpuntNieuw(string[] aansluitpuntId)
        {
            InitializeComponent();
            _database = new Database();
            _aansluitpunt = aansluitpuntId;
        }

        private void AansluitpuntNieuw_Load(object sender, EventArgs e)
        {
            // beperk de lengte van de velden - zie datawoordenboek
            txtbxAansluitpunt.MaxLength = 10;
            txtbxLocatie.MaxLength = 10;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (txtbxAansluitpunt.Text.Equals(""))
            {
                MessageBox.Show("Aansluitpunt ID mag niet leeg zijn.");
                return;
            }
            // Ga na of het dit Aansluitpunt ID reeds bestaat.
            if (_database.IsAansluitpunt(txtbxAansluitpunt.Text))
            {
                MessageBox.Show("Dit aansluitpunt bestaat reeds!");
                return;
            }

            // Voeg het aansluitpunt toe aan de database
            _database.InsertAansluitpunt(txtbxAansluitpunt.Text, txtbxLocatie.Text);

            // Aangemaakt item retourneren
            _aansluitpunt[0] = txtbxAansluitpunt.Text;

            // sluit het venster
            DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // In de id box kunnen enkel cijfers en letters ingegeven worden.
        // Bron : <https://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers>
        private void TxtbxAansluitpunt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
