/* Venster dat verschijnt als er Aansluitpunt -> Nieuw is gekozen.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - TxtbxAansluitpunt_KeyPress toegevoegd, enkel cijfers en letters kunnen ingevoegd worden.
 *        Fix voor probleem 'VB810 ' ingeven --> database error want 'VB810' bestaat reeds.
 *      - .ico toegevoegd
 */
using System;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class AansluitpuntNieuw : Form
    {
        private readonly Database _database;      

        public AansluitpuntNieuw()
        {
            InitializeComponent();
            _database = new Database();
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
            // Ga na of het dit Aansluitpunt ID reeds bestaat.
            if (_database.IsAansluitpunt(txtbxAansluitpunt.Text))
            {
                MessageBox.Show("Dit aansluitpunt bestaat reeds!");
                return;
            }

            // Voeg het aansluitpunt toe aan de database
            _database.InsertAansluitpunt(txtbxAansluitpunt.Text, txtbxLocatie.Text);

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
