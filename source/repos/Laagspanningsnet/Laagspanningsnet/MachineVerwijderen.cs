using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    
    public partial class MachineVerwijderen : Form
    {
        private Database database;      

        public MachineVerwijderen()
        {
            InitializeComponent();
            database = new Database();
        }

        private void MachineVerwijderen_Load(object sender, EventArgs e)
        {
            // Haal lijst met alle niet aangesloten machines op
            List<String> listMachines = database.GetMachines(true); // true - notConnected

            // Steek ze in de selectie combobox
            cmbMachine.DataSource = listMachines;

            // In de txtbx'en kan niets aangepast worden
            txtbxLocatie.Enabled = false;
            txtbxOmschrijving.Enabled = false;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Wis de machine uit de database
            database.DeleteMachine(cmbMachine.Text);

            // sluit het venster
            this.DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Andere machine uit het menu gekozen
        private void cmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxOmschrijving.Text = database.GetMachineOmschrijving(cmbMachine.Text);
            txtbxLocatie.Text = database.GetMachineLocatie(cmbMachine.Text);
        }
    }
}
