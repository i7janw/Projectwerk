/* Venster dat verschijnt als er Aansluitpunt -> Hernoemen is gekozen.
 *
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    public partial class AansluitpuntHernoemen : Form
    {
        private readonly Database _database;      

        public AansluitpuntHernoemen()
        {
            InitializeComponent();
            _database = new Database();
        }

        private void AansluitpuntHernoemen_Load(object sender, EventArgs e)
        {
            // Haal lijst met alle aansluitpunten op
            BindingList<string> listAansluitpunt = _database.GetAansluitpunten();

            // Steek ze in de selectie combobox
            cmbAansluitpunt.DataSource = listAansluitpunt;

            // beperk de lengte van de velden - zie datawoordenboek
            txtbxAansluitpunt.MaxLength = 10;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // 1. We dupliceren eerst het aansluitpunt naar de nieuwe naam.
            if (txtbxAansluitpunt.Text.Equals(""))
            {
                MessageBox.Show("Aansluitpunt ID mag niet leeg zijn.");
                return;
            }
            // Ga na of het dit Aansluitpunt ID reeds bestaat.
            if (_database.IsAansluitpunt(txtbxAansluitpunt.Text))
            {
                MessageBox.Show("Dit aansluitpunt bestaat reeds!");
                return;
            }

            string locatie = _database.GetAansluitpuntLocatie(cmbAansluitpunt.Text);
            // Voeg het aansluitpunt toe aan de database
            _database.InsertAansluitpunt(txtbxAansluitpunt.Text, locatie);

            // 2. We updaten alle aansluitingen naar de nieuwe naam
            _database.UpdateAansluitingen(cmbAansluitpunt.Text, txtbxAansluitpunt.Text);

            // 3. We wissen het oud aansluitpunt
            _database.DeleteAansluitpunt(cmbAansluitpunt.Text);

            // sluit het venster
            DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // In de id box kunnen enkel cijfers en letters ingegeven worden.
        // Bron : <https://stackoverflow.com/questions/463299/how-do-i-make-a-textbox-that-only-accepts-numbers>
        private void TxtbxAansluitpunt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
