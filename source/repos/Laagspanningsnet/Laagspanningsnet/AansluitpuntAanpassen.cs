/* Venster dat verschijnt als er Aansluitpunt -> Aanpassen is gekozen.
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
    public partial class AansluitpuntAanpassen : Form
    {
        private readonly Database _database;      

        public AansluitpuntAanpassen()
        {
            InitializeComponent();
            _database = new Database();
        }

        private void AansluitpuntAanpassen_Load(object sender, EventArgs e)
        {
            // Haal lijst met alle aansluitpunten op
            BindingList<string> listAansluitpunt = _database.GetAansluitpunten();

            // Steek ze in de selectie combobox
            cmbAansluitpunt.DataSource = listAansluitpunt;

            // beperk de lengte van de velden - zie datawoordenboek
            txtbxLocatie.MaxLength = 10;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Pas de machine aan in de database
            _database.UpdateAansluitpunt(cmbAansluitpunt.Text, txtbxLocatie.Text);

            // sluit het venster
            DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Andere aansluitpunt uit het menu gekozen
        private void cmbAansluitpunt_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxLocatie.Text = _database.GetAansluitpuntLocatie(cmbAansluitpunt.Text);
        }
    }
}
