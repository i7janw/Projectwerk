﻿/* Venster dat verschijnt wanneer er op de + of A is geklikt (aanpassen van een aansluiting)
 *
 * Aanpassingen :
 *  - 20180317 :
 *      - beperken van welke characters er in de kring txtbx ingegeven kunnen worden
 *      - max.lengte van txtbx'en aangepast volgens datawoordenboek
 *      - titel "Nieuwe aansluiting ingeven" toegevoegd
 *  - 20180402 :
 *      - combobox aansluitpunt/machine --> nieuw toegevoegd
 *      - cmbPolen : DropDownStyle = DropDownList
 *      - check lege kring
 *  - 20180508 :
 *      - Transfo's komen niet in keuzelijst aansluitpunten meer voor
 *      - MessageBoxIcon.Exclamation aan messageboxen toegevoegd
 *  - 20180509 :
 *      - cursor in kring tekstveld
 *  - 20180510 :
 *      - Ontvangt nu kring-voorstellen bij nieuwe kring
 *  - 20180511 :
 *      - Doorsturen van naamvoorstel nieuwe kast naar AansluitpuntNieuw
 *      - Checken op samenhang naam aansluitpunt/kring en AansluitpuntHernoemen aanroepen met naam voorstel
 *  - 20180512 :
 *      - Kabelsectie , --> .
 */
using System;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
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
        private BindingList<string> _listMachines;
        private BindingList<string> _listAansluitpunten;
        private bool _locked;

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

        private void AansluitingAanpassenLoad(object sender, EventArgs e)
        {
            txtbxKring.MaxLength = 10;
            txtbxKabeltype.MaxLength = 7;
            txtbxKabelsectie.MaxLength = 12;
            txtbxOmschrijving.MaxLength = 90;
            txtbxStroom.MaxLength = 3;

            // Zet cursor bij start in kring
            txtbxKring.Select();
            
            // Vul de Polen combobox met de cijfers 1-4
            for (int count = 1; count < 5; count++)
            {
                cmbPolen.Items.Add(count);
            }

            // Lijst met Machines aanmaken
            _listMachines = _database.GetMachines(true);                            // uit database ophalen (true = enkel niet aangesloten)
            _listMachines.Insert(0, "Nieuw");                                       // 'nieuw' als keuze toevoegen
            _listMachines.Insert(1, "Geen");                                        // 'geen' als keuze toevoegen
            
            // Lijst met aansluitpunten aanmaken                                 
            _listAansluitpunten = new BindingList<string>();                        // starten met een lege lijst
            
            // ophalen van niet aangesloten aansluitpunten uit de database 
            // en alles wat niet start met een 'T' toevoegen aan de lege lijst
            // T = Transfo --> enkel aan te sluiten op hoogspanning
            foreach (string a in _database.GetAansluitpunten(true))
            {
                // if it is List<String>
                if (!a.StartsWith("T"))
                {
                    _listAansluitpunten.Add(a);
                }
            }
            _listAansluitpunten.Insert(0, "Nieuw");                                 // 'nieuw' als keuze toevoegen
            _listAansluitpunten.Insert(1, "Geen");                                  // 'geen' als keuze toevoegen
            
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
                txtbxOmschrijving.Enabled = false;                      // Machine = Omschrijving komt uit Machine-Table
                txtbxOmschrijving.Text = _database.GetMachineOmschrijving((string)_row["Nummer"]);
                _listMachines.Insert(0, (string)_row["Nummer"]);        // Huidige machine aan lijst toevoegen
                cmbMachine.Text = (string)_row["Nummer"];
                cmbAansluitpunt.Text = "Geen";
            }
            if ((string)_row["Type"] == "A")
            {
                txtbxOmschrijving.Enabled = false;                      // Aansluitpunt =  geen omschrijving
                txtbxOmschrijving.Text = "";
                _listAansluitpunten.Insert(0, (string)_row["Nummer"]);  // Huidig aansluitpunt aan lijst toevoegen
                cmbAansluitpunt.Text = (string)_row["Nummer"];
                cmbMachine.Text = "Geen";
            }
            
            // Nieuwe of bestaande kring?
            if (((string)_row["Kring"]).StartsWith("Nieuw"))
            { // --> nieuw
                Text = "Nieuwe aansluiting ingeven";                                                    // venster-titel
                txtbxKring.Text = ((string)_row["Kring"]).Split(' ')[1];                                // Nieuw van kring verwijderen
            }
            else
            { // --> bestaand
                Text = "Aansluiting " + _aansluitpunt + " - " + (string)_row["Kring"] + " aanpassen";   // venster-titel
                txtbxKring.Enabled = false;
                txtbxKring.Text = (string)_row["Kring"];
            }

            // Titel label aanpassen
            lblTitel.Text = _aansluitpunt + " - " + ((string)_row["Kring"]).Split(' ')[0];


            txtbxKabeltype.Text = "XVB";            // Default kabel type = XVB 
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

            // Koppel de combobox'en aan de list'en,
            // vermijd dat de selectie "Nieuw" getriggerd wordt 
            _locked = true;
            cmbMachine.DataSource = _listMachines;
            cmbAansluitpunt.DataSource = _listAansluitpunten;
            if (cmbMachine.Text.Equals("Nieuw")) cmbMachine.Text = "Geen";
            if (cmbAansluitpunt.Text.Equals("Nieuw")) cmbAansluitpunt.Text = "Geen";
            _locked = false;
        }

        // Er is op de OK knop geklikt.
        private void BtnOkClick(object sender, EventArgs e)
        {
            // We staan geen lege kring toe.
            if (txtbxKring.Text.Equals(""))
            {
                MessageBox.Show("Kring mag niet leeg zijn.", "Lege kring", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtbxKring.Select();
                return;
            }
            
            // Ga na of deze aansluiting wel uniek is
            if (((string)_row["Kring"]).StartsWith("Nieuw"))    // enkel checken als we een nieuwe kring toevoegen
            {
                foreach (DataRow dtRow in _dt.Rows)
                {
                    if (dtRow["Kring"] != DBNull.Value)
                    {
                        if ((string)dtRow["Kring"] == txtbxKring.Text)
                        {
                            MessageBox.Show("Deze kring bestaat reeds.", "Bestaande kring", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtbxKring.Select();
                            return;
                        }
                    }
                }
            }

            // Testen af de naamgevening wel consistent is
            if (!cmbAansluitpunt.Text.Equals("Geen"))
            {  
                bool consistent = true;
                int.TryParse(Regex.Replace(cmbAansluitpunt.Text, "[^0-9]", ""), out int nr);
                int.TryParse(Regex.Replace(_aansluitpunt, "[^0-9]", ""), out int apNr);
                if (_aansluitpunt.StartsWith("VB"))
                {
                    if (apNr != nr) consistent = false;
                    if (!txtbxKring.Text.Substring(txtbxKring.Text.Length - 1)
                        .Equals(cmbAansluitpunt.Text.Substring(cmbAansluitpunt.Text.Length - 1))) consistent = false;
                }

                if (_aansluitpunt.StartsWith("K"))
                {
                    if (!cmbAansluitpunt.Text.StartsWith(_aansluitpunt)) consistent = false;
                }

                if (_aansluitpunt.StartsWith("T"))
                {
                    if (!txtbxKring.Text.Replace("H", "VB").Equals(cmbAansluitpunt.Text)) consistent = false;
                }

                if (!consistent)
                {
                    DialogResult result = MessageBox.Show("Onsamenhangende naam : " + _aansluitpunt + " - " + txtbxKring.Text + " --> " + cmbAansluitpunt.Text +
                                                          "\n\n" + cmbAansluitpunt.Text + " hernoemen?" , "Onsamenhangende naam?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string[] aangesloten = new string[2];
                        aangesloten[0] = cmbAansluitpunt.Text;
                        aangesloten[1] = MakeAansluitpuntName();
                        AansluitpuntHernoemen ah = new AansluitpuntHernoemen(aangesloten);
                        if (ah.ShowDialog() != DialogResult.Cancel)             // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
                        {
                            _listAansluitpunten.Remove(cmbAansluitpunt.Text);   // Oude naam weg
                            _listAansluitpunten.Add(aangesloten[0]);            // Nieuwe naam erbij
                            cmbAansluitpunt.Text = aangesloten[0];              // terugkoppeling van de nieuwe naam
                        }
                    }
                }
            }

            Console.WriteLine("OK");
            Console.WriteLine(cmbAansluitpunt.Text);

            // Alles Ok, de gegevens kunnen in een nieuwe datarow gestoken worden.
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
        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        // Machine is aangepast
        private void CmbMachineSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_locked) return;
            if (cmbMachine.Text == "Nieuw")
            {
                _locked = true;
                cmbMachine.Text = "Geen";
                string[] machine = new string[1];
                MachineNieuw mn = new MachineNieuw(machine);
                if (mn.ShowDialog() != DialogResult.Cancel) // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
                {
                    _listMachines.Insert(0, machine[0]);
                    cmbMachine.Text = machine[0];
                }
                _locked = false;
            }
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
        private void CmbAansluitpuntSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_locked) return;
            if (cmbAansluitpunt.Text == "Nieuw")
            {
                _locked = true;
                cmbAansluitpunt.Text = "Geen";
                string[] aansluitpunt = new string[1];
                aansluitpunt[0] = MakeAansluitpuntName();       // Stel zelf een aansluitpuntnaam voor

                // Dialoogscherm tonen
                AansluitpuntNieuw an = new AansluitpuntNieuw(aansluitpunt);
                if (an.ShowDialog() != DialogResult.Cancel) // ShowDialog --> het hoofdvenster is niet aktief meer tot dit venster gesloten is
                {
                    _listAansluitpunten.Insert(0, aansluitpunt[0]);
                    cmbAansluitpunt.Text = aansluitpunt[0];
                }
                _locked = false;
            }
            if (cmbAansluitpunt.Text.Equals("Geen"))
            {
                if (cmbMachine.Text.Equals("Geen"))
                {
                    txtbxOmschrijving.Enabled = true;
                }
                return;
            }
            cmbMachine.Text = "Geen";
            txtbxOmschrijving.Enabled = false;  // Machine = Omschrijving komt uit Machine-Table
            txtbxOmschrijving.Text = "";
        }

        // Maak een naam voor een aansluitpunt aan die samenhangend is met de kring waar hij op aangesloten wordt
        private string MakeAansluitpuntName()
        {
            string name = "";
            // Stel zelf een naam voor, afhankelijk van T/VB/K verschilt de opbouw van de naam
            if (_aansluitpunt.StartsWith("VB"))
            {
                name = _aansluitpunt.Replace("VB", "K") + txtbxKring.Text.Substring(txtbxKring.Text.Length - 1);
            }

            if (_aansluitpunt.StartsWith("K"))
            {
                name = _aansluitpunt + txtbxKring.Text.Replace(".", "");
            }

            if (_aansluitpunt.StartsWith("T"))
            {
                name = txtbxKring.Text.Replace("H", "VB");
            }
            return name;
        }

        // In de stroom box kunnen enkel getallen ingegeven worden.
        // Bron : <https://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers>
        private void TxtbxStroomKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && (((TextBox) sender).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        // In de kring box kunnen enkel getallen, letters, "." ingegeven worden.
        // Een ',' wordt een '.'
        // Bron : <https://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers>
        private void TxtbxKringKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',') e.KeyChar = '.';  // , --> .
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        // Kabelsectie, maak van een , een . : niet echt nodig, maar steeds een . gebruiken, maakt het geheel meer uniform
        private void KabelsectieKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',') e.KeyChar = '.';  // , --> .
        }
    }
}
