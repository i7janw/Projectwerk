using System;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    
    public partial class AansluitpuntNieuw : Form
    {
        private Database database;      

        public AansluitpuntNieuw()
        {
            InitializeComponent();
            database = new Database();
        }

        private void AansluitpuntNieuw_Load(object sender, EventArgs e)
        {
            // beperk de lengte van de velden - zie datawoordenboek
            txtbxAansluitpunt.MaxLength = 10;
            txtbxLocatie.MaxLength = 10;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Ga na of het dit Aansluitpunt ID reeds bestaat.
            if (database.IsAansluitpunt(txtbxAansluitpunt.Text))
            {
                MessageBox.Show("Dit aansluitpunt bestaat reeds!");
                return;
            }

            // Voeg de machine toe aan de database
            database.InsertAansluitpunt(txtbxAansluitpunt.Text, txtbxLocatie.Text);

            // sluit het venster
            this.DialogResult = DialogResult.OK;
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
