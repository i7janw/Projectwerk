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
    
    public partial class MachineNieuw : Form
    {
        private Database database;      

        public MachineNieuw()
        {
            InitializeComponent();
            database = new Database();
        }

        private void MachineNieuw_Load(object sender, EventArgs e)
        {
            // beperk de lengte van de velden - zie datawoordenboek
            txtbxMachine.MaxLength = 10;
            txtbxOmschrijving.MaxLength = 80;
            txtbxLocatie.MaxLength = 10;
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // TODO : Ga na of het dit Machine ID reeds bestaat.
            if (database.IsMachine(txtbxMachine.Text))
            {
                MessageBox.Show("Deze machine bestaat reeds!");
                return;
            }

            // Voeg de machine toe aan de database
            database.InsertMachine(txtbxMachine.Text, txtbxOmschrijving.Text, txtbxLocatie.Text);

            // sluit het venster
            Close();
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
