/* Venster dat verschijnt als er Machine -> Verwijderen is gekozen.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - .ico toegevoegd
 *  - 20180508 :
 *      - Messagebox ter bevestiging toegevoegd
 */
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class MachineVerwijderen : Form
    {
        private readonly Database _database;      

        public MachineVerwijderen()
        {
            InitializeComponent();
            _database = new Database();
        }

        private void MachineVerwijderenLoad(object sender, EventArgs e)
        {
            // Haal lijst met alle niet aangesloten machines op
            BindingList<string> listMachines = _database.GetMachines(true); // true - notConnected

            // Steek ze in de selectie combobox
            cmbMachine.DataSource = listMachines;

            // In de txtbx'en kan niets aangepast worden
            txtbxLocatie.Enabled = false;
            txtbxOmschrijving.Enabled = false;
        }

        // Er is op de OK knop geklikt.
        private void BtnOkClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Machine " +
                                                  cmbMachine.Text +
                                                  " verwijderen?", "Verwijderen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Wis de machine uit de database
                _database.DeleteMachine(cmbMachine.Text);

                // sluit het venster
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        // Andere machine uit het menu gekozen
        private void CmbMachineSelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxOmschrijving.Text = _database.GetMachineOmschrijving(cmbMachine.Text);
            txtbxLocatie.Text = _database.GetMachineLocatie(cmbMachine.Text);
        }
    }
}
