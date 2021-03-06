﻿/* Venster dat verschijnt als er Aansluitpunt -> Aanpassen is gekozen.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - .ico toegevoegd
 *  - 20180509 :
 *      - Plaats de cursor automatisch in locatie tekstveld
 */
using System;
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

        private void AansluitpuntAanpassenLoad(object sender, EventArgs e)
        {
            // Haal lijst met alle aansluitpunten op
            BindingList<string> listAansluitpunt = _database.GetAansluitpunten();

            // Steek ze in de selectie combobox
            cmbAansluitpunt.DataSource = listAansluitpunt;

            // beperk de lengte van de velden - zie datawoordenboek
            txtbxLocatie.MaxLength = 10;

            // Plaats de cursor automatisch in het locatie tekstveld
            txtbxLocatie.Select();
        }

        // Er is op de OK knop geklikt.
        private void BtnOkClick(object sender, EventArgs e)
        {
            // Pas de machine aan in de database
            _database.UpdateAansluitpunt(cmbAansluitpunt.Text, txtbxLocatie.Text);

            // sluit het venster
            DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        // Andere aansluitpunt uit het menu gekozen
        private void cmbAansluitpunt_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxLocatie.Text = _database.GetAansluitpuntLocatie(cmbAansluitpunt.Text);
            
            // plaats cursor in Locatie veld
            txtbxLocatie.Select();
        }
    }
}
