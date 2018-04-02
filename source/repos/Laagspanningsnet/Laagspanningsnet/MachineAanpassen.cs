/* Venster dat verschijnt als er Machine -> Aanpassen is gekozen.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - .ico toegevoegd
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class MachineAanpassen : Form
    {
        private readonly Database _database;      

        public MachineAanpassen()
        {
            InitializeComponent();
            _database = new Database();
        }

        private void MachineAanpassen_Load(object sender, EventArgs e)
        {
            // Haal lijst met alle machines op
            BindingList<string> listMachines = _database.GetMachines();

            // Steek ze in de selectie combobox
            cmbMachine.DataSource = listMachines;

            // beperk de lengte van de velden - zie datawoordenboek
            txtbxOmschrijving.MaxLength = 80;
            txtbxLocatie.MaxLength = 10;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Pas de machine aan in de database
            _database.UpdateMachine(cmbMachine.Text, txtbxOmschrijving.Text, txtbxLocatie.Text);

            // sluit het venster
            DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Andere machine uit het menu gekozen
        private void CmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxOmschrijving.Text = _database.GetMachineOmschrijving(cmbMachine.Text);
            txtbxLocatie.Text = _database.GetMachineLocatie(cmbMachine.Text);
        }
    }
}
