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
    
    public partial class AansluitpuntAanpassen : Form
    {
        private Database database;      

        public AansluitpuntAanpassen()
        {
            InitializeComponent();
            database = new Database();
        }

        private void AansluitpuntAanpassen_Load(object sender, EventArgs e)
        {
            // Haal lijst met alle machines op
            List<String> listAansluitpunt = database.GetAansluitpunten();

            // Steek ze in de selectie combobox
            cmbAansluitpunt.DataSource = listAansluitpunt;

            // beperk de lengte van de velden - zie datawoordenboek
            txtbxLocatie.MaxLength = 10;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Pas de machine aan in de database
            database.UpdateAansluitpunt(cmbAansluitpunt.Text, txtbxLocatie.Text);

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
        private void cmbAansluitpunt_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxLocatie.Text = database.GetAansluitpuntLocatie(cmbAansluitpunt.Text);
        }
    }
}
