using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    
    public partial class AansluitpuntVerwijderen : Form
    {
        private Database database;      

        public AansluitpuntVerwijderen()
        {
            InitializeComponent();
            database = new Database();
        }

        private void AansluitpuntenVerwijderen_Load(object sender, EventArgs e)
        {
            // Haal lijst met alle niet aangesloten machines op
            List<String> listAansluitpunt = database.GetAansluitpunten(true); // true - notConnected

            // Steek ze in de selectie combobox
            cmbAansluitpunt.DataSource = listAansluitpunt;

            // In de txtbx'en kan niets aangepast worden
            txtbxLocatie.Enabled = false;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Wis de machine uit de database
            database.DeleteAansluitpunt(cmbAansluitpunt.Text);

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
            txtbxLocatie.Text = database.GetAansluitpuntLocatie(cmbAansluitpunt.Text);
        }
    }
}
