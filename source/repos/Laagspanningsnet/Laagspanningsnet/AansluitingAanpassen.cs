using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    
    public partial class AansluitingAanpassen : Form
    {
        private String aansluitpunt;
        private DataTable dt;
        private DataRow row;
        private int index;
        private Database database;      // Nodig om machine en aansluitpunten lijst op te halen

        // Aansluiting aanpassen , _aanpassen TRUE --> reeds bestaande kring aanpassen
        public AansluitingAanpassen(DataTable _dt, int _index)
        {
            InitializeComponent();
            dt = _dt;
            index = _index;
            row = dt.Rows[_index];
            aansluitpunt = (String)row["T/VB/K"];
            database = new Database();
        }

        private void AansluitingAanpassen_Load(object sender, EventArgs e)
        {
            txtbxStroom.MaxLength = 3;
            
            // !!! TODO MaxLength voor andere txtbx'en ook nog toevoegen, nu geen goesting...

            // Vul de Polen combobox met de cijfers 1-4
            for (int count = 1; count < 5; count++)
            {
                cmbPolen.Items.Add(count);
            }
            // Lijst met Machines aanmaken
            List<String> listMachines = database.GetMachines(true);             // uit database ophalen (true = enkel niet aangesloten)
            listMachines.Insert(0, "Geen");                                     // 'geen' als keuze toevoegen
            
            // Lijst met aansluitpunten aanmaken                                // uit database ophalen
            List<String> listAansluitpunten = database.GetAansluitpunten(true); // 'geen' als keuze toevoegen (true = enkel niet aangesloten)
            listAansluitpunten.Insert(0, "Geen");

            // Pas de titel aan
            this.Text = "Aansluiting " + aansluitpunt + " - "+ (String)row["Kring"] + " aanpassen";
            lblTitel.Text = aansluitpunt + " - " + (String)row["Kring"];

            // Wat is er op deze aansluiting aangesloten?
            if ((String)row["Type"] == "N")
            {
                txtbxOmschrijving.Enabled = true;   // Normaal = Omschrijving kan ingegeven worden
                txtbxOmschrijving.Text = "";
                if (row["Omschrijving"] != DBNull.Value)
                {
                    txtbxOmschrijving.Text = (String)row["Omschrijving"];
                }
                cmbMachine.Text = "Geen";
            }
            if ((String)row["Type"] == "M")
            {
                txtbxOmschrijving.Enabled = false;                  // Machine = Omschrijving komt uit Machine-Table
                txtbxOmschrijving.Text = database.GetMachineOmschrijving((String)row["Nummer"]);
                listMachines.Insert(0, (String)row["Nummer"]);      // Huidige machine aan lijst toevoegen
                cmbMachine.Text = (String)row["Nummer"];
                cmbAansluitpunt.Text = "Geen";
            }
            if ((String)row["Type"] == "A")
            {
                txtbxOmschrijving.Enabled = false;                  // Aansluitpunt =  geen omschrijving
                txtbxOmschrijving.Text = "";
                listAansluitpunten.Insert(0, (String)row["Nummer"]);// Huidig aansluitpunt aan lijst toevoegen
                cmbAansluitpunt.Text = (String)row["Nummer"];
                cmbMachine.Text = "Geen";
            }
            // Zet de overige gegevens op het scherm
            txtbxKring.Text = (String)row["Kring"];
            if ((String)row["Kring"] != "Nieuw")
            {
                txtbxKring.Enabled = false;
            }
            txtbxKabeltype.Text = "";
            if (row["KabelType"] != DBNull.Value)
            {
                txtbxKabeltype.Text = (String)row["Kabeltype"];
            }
            txtbxKabelsectie.Text = "";
            if (row["Kabelsectie"] != DBNull.Value)
            { 
                txtbxKabelsectie.Text = (String)row["Kabelsectie"];
            }
            txtbxStroom.Text = "";
            if (row["Stroom (A)"] != DBNull.Value)
            {
                txtbxStroom.Text = (String)row["Stroom (A)"];
            }
            cmbPolen.Text = "3";                    // Default voor polen = 3 ( R S T + Vaste PEN aansluiting)
            if (row["Aantal polen"] != DBNull.Value)
            {
                cmbPolen.Text = (String)row["Aantal polen"];
            }
            cmbMachine.DataSource = listMachines;
            cmbAansluitpunt.DataSource = listAansluitpunten;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Ga na of deze aansluiting wel uniek is
            if ((String)this.row["Kring"] == "Nieuw")    // enkel checken als we een nieuwe kring toevoegen
            {
                foreach (DataRow dtRow in dt.Rows)
                {
                    if (dtRow["Kring"] != DBNull.Value)
                    {
                        Console.WriteLine(dtRow["Kring"] + " " + txtbxKring.Text);
                        if ((String)dtRow["Kring"] == txtbxKring.Text)
                        {
                            MessageBox.Show("Deze aansluiting bestaat reeds.\nKijk na of alles correct is ingegeven...");
                            return;
                        }
                    }
                }
            }

            // Ok, aansluiting is uniek, we kunnen verder gaan
            dt.Rows.Remove(this.row);
            DataRow row = dt.NewRow();
            row["+"] = "+";
            row["-"] = "-";
            row["A"] = "A";
            row["T/VB/K"] = aansluitpunt;
            dt.Rows.InsertAt(row, index);

            if (cmbAansluitpunt.Text == "Geen" && cmbMachine.Text == "Geen")
            {
                row["Type"] = "N";
                row["Nummer"] = "";
            }
            if (cmbAansluitpunt.Text != "Geen")
            {
                row["Type"] = "A";
                row["Nummer"] = cmbAansluitpunt.Text;
                row["Locatie"] = database.GetAansluitpuntLocatie(cmbAansluitpunt.Text);
            }
            if (cmbMachine.Text != "Geen")
            {
                row["Type"] = "M";
                row["Nummer"] = cmbMachine.Text;
                row["Locatie"] = database.GetMachineLocatie(cmbMachine.Text);
            }
            row["Kring"] = txtbxKring.Text;
            row["Omschrijving"] = txtbxOmschrijving.Text;
            row["Kabeltype"] = txtbxKabeltype.Text;
            row["Kabelsectie"] = txtbxKabelsectie.Text;
            row["Stroom (A)"] = txtbxStroom.Text;
            row["Aantal polen"] = cmbPolen.Text;
            this.DialogResult = DialogResult.OK;
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
                }
                return;
            }
            cmbAansluitpunt.Text = "Geen";
            txtbxOmschrijving.Enabled = false;  // Machine = Omschrijving komt uit Machine-Table
            txtbxOmschrijving.Text = database.GetMachineOmschrijving(cmbMachine.Text);
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
    }
}
