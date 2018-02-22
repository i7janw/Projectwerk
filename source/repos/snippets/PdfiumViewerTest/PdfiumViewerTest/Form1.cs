using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PdfiumViewerTest
{
    public partial class Form1 : Form
    {
        private string prn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
                PrinterSettings settings = new PrinterSettings();
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    Console.WriteLine(printer);
                    settings.PrinterName = printer;
                    if (settings.IsDefaultPrinter) {
                        Console.WriteLine("Default = " + printer);
                        prn = printer;
                    }
            }
                
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintPDF p = new PrintPDF();
            if (p.Print(prn, "a4", "C:/Users/Jan Wagemakers/Documents/MEGA/2017-2018/ProjectWerk/DesignDocument/1.1_korteOmschrijving/korteOmschrijving_Wagemakers_Jan_v20171101.pdf", 1))
            {
                MessageBox.Show("OK");
            } else
            {
                MessageBox.Show("FAIL");
            }
        }
    }
}
