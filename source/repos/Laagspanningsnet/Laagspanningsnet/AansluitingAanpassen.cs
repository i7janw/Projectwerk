/* Venster dat verschijnt wanneer er op de + of A is geklikt (aanpassen van een aansluiting)
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - beperken van welke characters er in de kring txtbx ingegeven kunnen worden
 *      - max.lengte van txtbx'en aangepast volgens datawoordenboek
 *      - titel "Nieuwe aansluiting ingeven" toegevoegd
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    
    public partial class AansluitingAanpassen : Form
    {
        private readonly string _aansluitpunt;
        private readonly DataTable _dt;
        private readonly DataRow _row;
        private readonly int _index;
        private readonly Database _database;      // Nodig om machine en aansluitpunten lijst op te halen

        // Aansluiting aanpassen
        public AansluitingAanpassen(DataTable dt, int index)
        {
            InitializeComponent();
            _dt = dt;
            _index = index;
            _row = _dt.Rows[index];
            _aansluitpunt = (string)_row["T/VB/K"];
            _database = new Database();
        }

        private void AansluitingAanpassen_Load(object sender, EventArgs e)
        {
            txtbxKring.MaxLength = 10;
            txtbxKabeltype.MaxLength = 7;
            txtbxKabelsectie.MaxLength = 12;
            txtbxOmschrijving.MaxLength = 90;
            txtbxStroom.MaxLength = 3;

            // Vul de Polen combobox met de cijfers 1-4
            for (int count = 1; count < 5; count++)
            {
                cmbPolen.Items.Add(count);
            }
            // Lijst met Machines aanmaken
            List<string> listMachines = _database.GetMachines(true);             // uit database ophalen (true = enkel niet aangesloten)
            listMachines.Insert(0, "Geen");                                      // 'geen' als keuze toevoegen
            
            // Lijst met aansluitpunten aanmaken                                 // uit database ophalen
            List<string> listAansluitpunten = _database.GetAansluitpunten(true); // 'geen' als keuze toevoegen (true = enkel niet aangesloten)
            listAansluitpunten.Insert(0, "Geen");

            // Pas de titel aan
            if (_row["Kring"].Equals("Nieuw"))
            {
                Text = "Nieuwe aansluiting ingeven";
            } else { 
                Text = "Aansluiting " + _aansluitpunt + " - "+ (string)_row["Kring"] + " aanpassen";
            }
            lblTitel.Text = _aansluitpunt + " - " + (string)_row["Kring"];

            // Wat is er op deze aansluiting aangesloten?
            if ((string)_row["Type"] == "N")
            {
                txtbxOmschrijving.Enabled = true;   // Normaal = Omschrijving kan ingegeven worden
                txtbxOmschrijving.Text = "";
                if (_row["Omschrijving"] != DBNull.Value)
                {
                    txtbxOmschrijving.Text = (string)_row["Omschrijving"];
                }
                cmbMachine.Text = "Geen";
            }
            if ((string)_row["Type"] == "M")
            {
                txtbxOmschrijving.Enabled = false;                  // Machine = Omschrijving komt uit Machine-Table
                txtbxOmschrijving.Text = _database.GetMachineOmschrijving((string)_row["Nummer"]);
                listMachines.Insert(0, (string)_row["Nummer"]);      // Huidige machine aan lijst toevoegen
                cmbMachine.Text = (string)_row["Nummer"];
                cmbAansluitpunt.Text = "Geen";
            }
            if ((string)_row["Type"] == "A")
            {
                txtbxOmschrijving.Enabled = false;                  // Aansluitpunt =  geen omschrijving
                txtbxOmschrijving.Text = "";
                listAansluitpunten.Insert(0, (string)_row["Nummer"]);// Huidig aansluitpunt aan lijst toevoegen
                cmbAansluitpunt.Text = (string)_row["Nummer"];
                cmbMachine.Text = "Geen";
            }
            // Zet de overige gegevens op het scherm
            txtbxKring.Text = (string)_row["Kring"];
            if ((string)_row["Kring"] != "Nieuw")
            {
                txtbxKring.Enabled = false;
            }
            else
            {
                txtbxKring.Text = "";
            }
            txtbxKabeltype.Text = "";
            if (_row["KabelType"] != DBNull.Value)
            {
                txtbxKabeltype.Text = (string)_row["Kabeltype"];
            }
            txtbxKabelsectie.Text = "";
            if (_row["Kabelsectie"] != DBNull.Value)
            { 
                txtbxKabelsectie.Text = (string)_row["Kabelsectie"];
            }
            txtbxStroom.Text = "";
            if (_row["Stroom (A)"] != DBNull.Value)
            {
                txtbxStroom.Text = (string)_row["Stroom (A)"];
            }
            cmbPolen.Text = "3";                    // Default voor polen = 3 ( R S T + Vaste PEN aansluiting)
            if (_row["Aantal polen"] != DBNull.Value)
            {
                cmbPolen.Text = (string)_row["Aantal polen"];
            }
            cmbMachine.DataSource = listMachines;
            cmbAansluitpunt.DataSource = listAansluitpunten;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Ga na of deze aansluiting wel uniek is
            if ((string)_row["Kring"] == "Nieuw")    // enkel checken als we een nieuwe kring toevoegen
            {
                foreach (DataRow dtRow in _dt.Rows)
                {
                    if (dtRow["Kring"] != DBNull.Value)
                    {
                        Console.WriteLine(dtRow["Kring"] + " " + txtbxKring.Text);
                        if ((string)dtRow["Kring"] == txtbxKring.Text)
                        {
                            MessageBox.Show("Deze aansluiting bestaat reeds.\nKijk na of alles correct is ingegeven...");
                            return;
                        }
                    }
                }
            }

            // Ok, aansluiting is uniek, we kunnen verder gaan
            _dt.Rows.Remove(_row);
            DataRow row = _dt.NewRow();
            row["+"] = "+";
            row["-"] = "-";
            row["A"] = "A";
            row["T/VB/K"] = _aansluitpunt;
            _dt.Rows.InsertAt(row, _index);

            if (cmbAansluitpunt.Text == "Geen" && cmbMachine.Text == "Geen")
            {
                row["Type"] = "N";
                row["Nummer"] = "";
            }
            if (cmbAansluitpunt.Text != "Geen")
            {
                row["Type"] = "A";
                row["Nummer"] = cmbAansluitpunt.Text;
                row["Locatie"] = _database.GetAansluitpuntLocatie(cmbAansluitpunt.Text);
            }
            if (cmbMachine.Text != "Geen")
            {
                row["Type"] = "M";
                row["Nummer"] = cmbMachine.Text;
                row["Locatie"] = _database.GetMachineLocatie(cmbMachine.Text);
            }
            row["Kring"] = txtbxKring.Text;
            row["Omschrijving"] = txtbxOmschrijving.Text;
            row["Kabeltype"] = txtbxKabeltype.Text;
            row["Kabelsectie"] = txtbxKabelsectie.Text;
            row["Stroom (A)"] = txtbxStroom.Text;
            row["Aantal polen"] = cmbPolen.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Machine is aangepast
        private void CmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbMachine.Text == "Geen")
            {
                if(cmbAansluitpunt.Text == "Geen")
                {
                    txtbxOmschrijving.Enabled = true;
                    txtbxOmschrijving.Text = "";
                }
                return;
            }
            cmbAansluitpunt.Text = "Geen";
            txtbxOmschrijving.Enabled = false;  // Machine = Omschrijving komt uit Machine-Table
            txtbxOmschrijving.Text = _database.GetMachineOmschrijving(cmbMachine.Text);
        }

        // Aansluitpunt is aangepast
        private void CmbAansluitpunt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAansluitpunt.Text == "Geen")
            {
                if (cmbMachine.Text == "Geen")
                {
                    txtbxOmschrijving.Enabled = true;
                }
                return;
            }
            cmbMachine.Text = "Geen";
            txtbxOmschrijving.Enabled = false;  // Machine = Omschrijving komt uit Machine-Table
            txtbxOmschrijving.Text = "";
        }

        // In de stroom box kunnen enkel getallen ingegeven worden.
        // Bron : <https://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers>
        private void TxtbxStroom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        // In de kring box kunnen enkel getallen, letter, ".", "," ingegeven worden.
        // Bron : <https://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers>
        private void TxtbxKring_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }
    }
}
