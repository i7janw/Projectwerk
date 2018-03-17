/* Venster dat verschijnt als er Machine -> Nieuw is gekozen.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - TxtbxMachine_KeyPress toegevoegd, enkel cijfers en letters kunnen ingevoegd worden.
 *        Fix voor probleem 'S019 ' ingeven --> database error want 'S019' bestaat reeds.
 *      - .ico toegevoegd
 */
using System;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class MachineNieuw : Form
    {
        private readonly Database _database;      

        public MachineNieuw()
        {
            InitializeComponent();
            _database = new Database();
        }

        private void MachineNieuw_Load(object sender, EventArgs e)
        {
            // beperk de lengte van de velden - zie datawoordenboek
            txtbxMachine.MaxLength = 10;
            txtbxOmschrijving.MaxLength = 80;
            txtbxLocatie.MaxLength = 10;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Ga na of het dit Machine ID reeds bestaat.
            if (_database.IsMachine(txtbxMachine.Text))
            {
                MessageBox.Show("Deze machine bestaat reeds!");
                return;
            }

            // Voeg de machine toe aan de database
            _database.InsertMachine(txtbxMachine.Text, txtbxOmschrijving.Text, txtbxLocatie.Text);

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
        private void TxtbxMachine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
