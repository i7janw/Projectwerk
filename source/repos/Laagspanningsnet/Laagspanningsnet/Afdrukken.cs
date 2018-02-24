using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laagspanningsnet
{
    
    public partial class Afdrukken : Form
    {
        private Database database;
        public String selectie;
        public String printer;
        public int kopies;

        public Afdrukken(String _selectie)
        {
            InitializeComponent();
            database = new Database();
            selectie = _selectie;
            Console.WriteLine(selectie);
        }

        private void Afdrukken_Load(object sender, EventArgs e)
        {
            String defaultPrn = "";
            // Haal een lijst op met de beschikbare printers
            PrinterSettings settings = new PrinterSettings();
            List<string> printers = new List<string>();
            foreach (string _printer in PrinterSettings.InstalledPrinters)
            {
                // Voeg elke printer toe aan de printerlijst
                printers.Add(_printer);

                // Ga op zoek naa de default printer
                settings.PrinterName = _printer;
                if (settings.IsDefaultPrinter)
                {
                    defaultPrn = _printer;
                }
            }

            // Steek ze in de printer combobox
            cmbPrinter.DataSource = printers;

            // Kies de default printer
            cmbPrinter.SelectedItem = defaultPrn;

            // Vul aantal combobox met de cijfers 1-...
            for (int count = 1; count < 21; count++)
            {
                cmbAantal.Items.Add(count);
            }

            // Kies aantal = 1
            cmbAantal.SelectedItem = 1;

            // Haal lijst met alle aansluitpunten op
            List<String> listAansluitpunt = database.GetAansluitpunten();

            // Steek ze in de selectie combobox
            cmbSelectie.DataSource = listAansluitpunt;

            // Kies de huidige selectie
            cmbSelectie.SelectedItem = selectie;
            Console.WriteLine(selectie);
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // steek de geselecteerde gegevens in de public variablen
            kopies = 666;
            selectie = cmbSelectie.Text;
            printer = cmbPrinter.Text;

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
