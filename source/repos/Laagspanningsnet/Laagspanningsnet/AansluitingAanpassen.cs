using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    
    public partial class AansluitingAanpassen : Form
    {
        private String aansluitpunt;
        private bool aanpassen;                 // !!!!! TODO deze weg en string aanpassen NIEUW als bool gebruiken
        private DataRow row;
        private Database database;

        // Aansluiting aanpassen , _aanpassen TRUE --> reeds bestaande kring aanpassen
        public AansluitingAanpassen(String _aansluitpunt, DataRow _row, bool _aanpassen)
        {
            InitializeComponent();
            row = _row;
            aansluitpunt = _aansluitpunt;
            aanpassen = _aanpassen;                 // !!!!! TODO , deze weg en aansluitpunt = NIEUW als aanpassen gebruiken
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
            List<String> listMachines = database.getMachines(true);             // uit database ophalen (true = enkel niet aangesloten)
            listMachines.Insert(0, "Geen");                                     // 'geen' als keuze toevoegen
            
            // Lijst met aansluitpunten aanmaken                                // uit database ophalen
            List<String> listAansluitpunten = database.getAansluitpunten(true); // 'geen' als keuze toevoegen (true = enkel niet aangesloten)
            listAansluitpunten.Insert(0, "Geen");

            // Pas de titel aan
            this.Text = "Aansluiting " + aansluitpunt + " - "+ (String)row["Kring"] + "aanpassen";
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
                txtbxOmschrijving.Text = database.getMachineOmschrijving((String)row["Nummer"]);
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
            if (aanpassen)
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
        private void btnOK_Click(object sender, EventArgs e)
        {
            // Ga na of deze aansluiting wel uniek is
            if (!aanpassen) // enkel checken als we een nieuwe kring toevoegen
            {
                // !!!! TODO , nakijken of het niet beter is om het uit de displaydataset te halen.
                DataSet ds = database.getAansluitingen(aansluitpunt);
                foreach (DataRow dsRow in ds.Tables[0].Rows)
                {
                    Console.WriteLine(dsRow["A_id"] + " " + txtbxKring.Text);
                    if ((String)dsRow["A_id"] == txtbxKring.Text)
                    {
                        MessageBox.Show("Deze aansluiting is niet uniek.\nKijk de gegevens na!");
                        return;
                    }
                }
            }

            // Ok, aansluiting is uniek, we kunnen verder gaan
            if (cmbAansluitpunt.Text == "Geen" && cmbMachine.Text == "Geen")
            {
                row["Type"] = "N";
                row["Nummer"] = "";
            }
            if (cmbAansluitpunt.Text != "Geen")
            {
                row["Type"] = "A";
                row["Nummer"] = cmbAansluitpunt.Text;
            }
            if (cmbMachine.Text != "Geen")
            {
                row["Type"] = "M";
                row["Nummer"] = cmbMachine.Text;
            }
            row["Kring"] = txtbxKring.Text;
            row["Omschrijving"] = txtbxOmschrijving.Text;
            row["Kabeltype"] = txtbxKabeltype.Text;
            row["Kabelsectie"] = txtbxKabelsectie.Text;
            row["Stroom (A)"] = txtbxStroom.Text;
            row["Aantal polen"] = cmbPolen.Text;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Machine is aangepast
        private void cmbMachine_SelectedIndexChanged(object sender, EventArgs e)
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
            txtbxOmschrijving.Text = database.getMachineOmschrijving(cmbMachine.Text);
        }

        // Aansluitpunt is aangepast
        private void cmbAansluitpunt_SelectedIndexChanged(object sender, EventArgs e)
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
        private void txtbxStroom_KeyPress(object sender, KeyPressEventArgs e)
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
