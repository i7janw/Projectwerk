/* Venster dat verschijnt als er Aansluitpunt -> Verwijderen is gekozen.
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
    public partial class AansluitpuntVerwijderen : Form
    {
        private readonly Database _database;      

        public AansluitpuntVerwijderen()
        {
            InitializeComponent();
            _database = new Database();
        }

        private void AansluitpuntenVerwijderen_Load(object sender, EventArgs e)
        {
            // Haal lijst met alle vrije aansluitpunten op.
            BindingList<String> listAansluitpunt = _database.GetAansluitpunten(true); 

            // Steek ze in de selectie combobox
            cmbAansluitpunt.DataSource = listAansluitpunt;

            // In de txtbx'en kan niets aangepast worden
            txtbxLocatie.Enabled = false;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Aansluitpunt " + 
                                                  cmbAansluitpunt.Text +
                                                  " volledig verwijderen?\n\nOok alle aansluitingen van "+
                                                  cmbAansluitpunt.Text + " zullen verwijderd worden.", "Verwijderen?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Wis de aansluitpunt uit de database
                _database.DeleteAansluitpunt(cmbAansluitpunt.Text);

                // sluit het venster
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Andere machine uit het menu gekozen
        private void cmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxLocatie.Text = _database.GetAansluitpuntLocatie(cmbAansluitpunt.Text);
        }
    }
}
