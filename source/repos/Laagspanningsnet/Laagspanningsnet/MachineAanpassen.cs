﻿/* Venster dat verschijnt als er Machine -> Aanpassen is gekozen.
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - .ico toegevoegd
 *  - 20180509 :
 *      - Plaats de cursor automatisch in het omschrijving tekstveld
 */
using System;
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

        private void MachineAanpassenLoad(object sender, EventArgs e)
        {
            // Haal lijst met alle machines op
            BindingList<string> listMachines = _database.GetMachines();

            // Steek ze in de selectie combobox
            cmbMachine.DataSource = listMachines;

            // beperk de lengte van de velden - zie datawoordenboek
            txtbxOmschrijving.MaxLength = 80;
            txtbxLocatie.MaxLength = 10;

            // Plaats de cursor automatisch in het omschrijving tekstveld
            txtbxOmschrijving.Select();
        }

        // Er is op de OK knop geklikt.
        private void BtnOkClick(object sender, EventArgs e)
        {
            // Pas de machine aan in de database
            _database.UpdateMachine(cmbMachine.Text, txtbxOmschrijving.Text, txtbxLocatie.Text);

            // sluit het venster
            DialogResult = DialogResult.OK;
            Close();
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

            // Plaats de cursor automatisch in het omschrijving tekstveld
            txtbxOmschrijving.Select();
        }
    }
}
