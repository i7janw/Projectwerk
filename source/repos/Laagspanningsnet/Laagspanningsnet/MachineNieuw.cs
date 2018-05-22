/* Venster dat verschijnt als er Machine -> Nieuw is gekozen.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - TxtbxMachineKeyPress toegevoegd, enkel cijfers en letters en bepaalde chars kunnen ingevoegd worden.
 *      - Fix voor probleem 'S019 ' ingeven --> database error want 'S019' bestaat reeds.
 *      - .ico toegevoegd
 * - 20180402 :
 *      - constructor 'public MachineNieuw(string[] MachineId)' toegevoegd
 *      - check lege machine ID
 * - 20180508 :
 *      - MessageboxIcon aan messageboxen toegevoegd
 * - 20180509 :
 *      - Plaats de cursor automatisch in machine tekstveld
 */
using System;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class MachineNieuw : Form
    {
        private readonly Database _database;
        private readonly string[] _machine;

        // Nieuwe machine
        public MachineNieuw()
        {
            InitializeComponent();
            _database = new Database();
            _machine = new string[1];
            _machine[0] = "";
        }

        // Nieuwe machine
        //  machineId[0] wordt gebruikt om nieuwe naam door te geven
        public MachineNieuw(string[] machineId)
        {
            InitializeComponent();
            _database = new Database();
            _machine = machineId;
        }

        private void MachineNieuwLoad(object sender, EventArgs e)
        {
            // beperk de lengte van de velden - zie datawoordenboek
            txtbxMachine.MaxLength = 10;
            txtbxOmschrijving.MaxLength = 80;
            txtbxLocatie.MaxLength = 10;

            // Plaats de cursor automatisch in machine tekstveld
            txtbxMachine.Select();
        }

        // Er is op de OK knop geklikt.
        private void BtnOkClick(object sender, EventArgs e)
        {
            if (txtbxMachine.Text.Equals(""))
            {
                MessageBox.Show("Machine ID mag niet leeg zijn.", "Leeg ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // Ga na of het dit Machine ID reeds bestaat.
            if (_database.IsMachine(txtbxMachine.Text))
            {
                MessageBox.Show("Deze machine bestaat reeds!", "Dubbel ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Voeg de machine toe aan de database
            _database.InsertMachine(txtbxMachine.Text, txtbxOmschrijving.Text, txtbxLocatie.Text);

            // Aangemaakt item retourneren
            _machine[0] = txtbxMachine.Text;

            // sluit het venster
            DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        // In de id box kunnen enkel cijfers en letters en bepaalde characters kunnen ingegeven worden.
        // Bron : <https://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers>
        private void TxtbxMachineKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',') e.KeyChar = '.';  // , --> .
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }
        }
    }
}