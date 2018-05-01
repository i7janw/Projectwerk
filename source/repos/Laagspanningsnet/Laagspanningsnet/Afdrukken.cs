using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Printing;
using System.Windows.Forms;
// print
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Laagspanningsnet
{

    public partial class Afdrukken : Form
    {
        private readonly Database _database;
        private readonly LaagspanningGridView _dgvLaagspanningsnet;
        private string _selectie;           // het geselecteerde aansluitpunt
        private string _printer;            // bijhouden van de printer
        private short _kopies;              // aantal kopies
        private bool _inclusief;            // inclusief aangesloten aansluitpunt false/true

        public Afdrukken(LaagspanningGridView dgv)
        {
            InitializeComponent();
            _database = new Database();
            _dgvLaagspanningsnet = dgv;
        }

        private void Afdrukken_Load(object sender, EventArgs e)
        {
            string defaultPrn = "";
            // Haal een lijst op met de beschikbare printers
            PrinterSettings settings = new PrinterSettings();
            List<string> printers = new List<string>();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                // Voeg elke printer toe aan de printerlijst
                printers.Add(printer);

                // Ga op zoek naa de default printer
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                {
                    defaultPrn = printer;
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
            BindingList<String> listAansluitpunt = _database.GetAansluitpunten();
            listAansluitpunt.Insert(0, "Huidige Pagina");

            // Steek ze in de selectie combobox
            cmbSelectie.DataSource = listAansluitpunt;

            // Kies de huidige selectie
            cmbSelectie.SelectedItem = "Huidige Pagina";
            if (_dgvLaagspanningsnet.GetMode() == LaagspanningGridView.Aansluitpunt)    // Als er een aansluitpunt getoond wordt,
            {
                cmbSelectie.SelectedItem = _dgvLaagspanningsnet.GetAansluitpunt();      // selecteren we het aansluitpunt ipv van huidige Pagina 
            }
            Console.WriteLine(_selectie);
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Open doc om te schrijven naar een MemoryStream
            Document doc = new Document(PageSize.A4);
            MemoryStream output = new MemoryStream();
            PdfWriter.GetInstance(doc, output);
            doc.Open();


            // steek de geselecteerde gegevens in de variablen
            _kopies = Convert.ToInt16(cmbAantal.Text);
            _selectie = cmbSelectie.Text;
            _printer = cmbPrinter.Text;
            _inclusief = cbxInclusief.Checked;
            bool huidige = false;
            if (_selectie.Equals("Huidige Pagina"))
            {
                _selectie = _dgvLaagspanningsnet.GetAansluitpunt();
                huidige = true;
            }
            
            // Afdrukken. 
            // We starten met het afdrukken van de selectie en
            // indien gewenst worden ook de aansluitpunten die op de selectie zijn aangesloten afgedrukt.
            List<string> todo = new List<string>(); // todo = lijst van aansluitpunten waarvan we nog moeten testen of er aansluitpunten op zijn aangesloten
            todo.Add(_selectie);

            while (todo.Count != 0)
            {
                // zijn er nog te testen aansluitpunten?
                List<string> tmp = new List<string>(); // tmp = om nieuwe todo lijst aan te maken
                foreach (String ap in todo) // doorloop alle aansluitpunten in todo 
                {
                    if (!huidige)       // Als de huidige pagina moet afgedrukt worden, moet dgv niet herladen worden
                    {
                        _dgvLaagspanningsnet.ShowAansluitpunt(ap); // Toon en Print selectie
                    }
                    Print(doc);
                    huidige = false;    // de eerste pagina is afgedrukt, de rest zijn dus geen huidige pagina's meer 
                    if (_inclusief) // Gaan we ook de aangesloten aansluitpunten afdrukken?
                    {
                        foreach (DataGridViewRow row in _dgvLaagspanningsnet.Rows)
                        {
                            if (row.Cells["Type"].Value != DBNull.Value)
                            {
                                if ((String) row.Cells["Type"].Value == "A") // is de aansluiting een aansluitpunt?
                                {
                                    tmp.Add((String) row.Cells["Nummer"]
                                        .Value); // voeg toe aan tmp (nieuwe todo lijst)
                                }
                            }
                        }
                    }
                }

                todo = tmp; // todo = nieuwe todo lijst.
            }
            

            // Sluit doc
            doc.Close();

            // Print aangemaakte doc af
            ToPrn(output.ToArray());

            // sluit het venster
            DialogResult = DialogResult.OK;
            Close();
        }

        // Het afdrukken zelf. TODO
        [SuppressMessage("ReSharper", "RedundantNameQualifier")]
        private void Print(Document doc)
        {
            String ap = _dgvLaagspanningsnet.GetAansluitpunt();
            Console.WriteLine("Afdrukken van " + ap + " mode = " + _dgvLaagspanningsnet.GetMode());

            iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10);
            iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 32);

            Paragraph text;
            // Titel
            String title;
            switch (_dgvLaagspanningsnet.GetMode())
            {
                case LaagspanningGridView.Transfos: 
                    title = "Overzicht transfos";
                    break;
                case LaagspanningGridView.Search:
                    title = "Zoeken : " + ap;
                    break;
                default: // case Aansluitpunt = default
                    title = "Layout van " + ap;
                    break;
            }

            text = new Paragraph(title, titleFont);
            text.Alignment = Element.ALIGN_CENTER;
            text.SpacingAfter = 20;
            doc.Add(text);

            // Info
            String voeding = _database.GetVoeding(ap);
            String locatie = _database.GetAansluitpuntLocatie(ap);
            String kabel = _database.GetKabel(ap);
            String stroom = _database.GetStroom(ap);
            text = new Paragraph("Voeding : " + voeding + "\n" +
                                 "Kabel : " + kabel + "\n" +
                                 "Stroom : " + stroom + "\n" +
                                 "Locatie : " + locatie, font);
            text.Alignment = Element.ALIGN_LEFT;
            text.SpacingAfter = 20;
            doc.Add(text);

            // Table
            PdfPTable table;
            float[] widths;
            switch (_dgvLaagspanningsnet.GetMode())
            {
                case LaagspanningGridView.Transfos:
                    table = new PdfPTable(7);
                    table.WidthPercentage = 100;
                    widths = new float[] { 5, 2, 5, 3, 2, 2, 2 };
                    table.SetWidths(widths);
                    break;
                case LaagspanningGridView.Search:
                    table = new PdfPTable(8);
                    table.WidthPercentage = 100;
                    widths = new float[] { 2, 1, 2, 5, 3, 2, 2, 2 };
                    table.SetWidths(widths);
                    table.AddCell(new Phrase("T/VB/K", font));  // Zoeken --> extra veld tonen
                    break;
                default:
                    table = new PdfPTable(7);
                    table.WidthPercentage = 100;
                    widths = new float[] { 1, 2, 5, 3, 2, 2, 2 };
                    table.SetWidths(widths);
                    break;
                //PdfPRow row = null;
                //float[] widths = new float[] { 4f, 4f, 4f, 4f };
            }
            table.AddCell(new Phrase("Kring", font));
            table.AddCell(new Phrase("Nummer", font));
            table.AddCell(new Phrase("Omschrijving", font));
            table.AddCell(new Phrase("Kabel", font));
            table.AddCell(new Phrase("Stroom", font));
            table.AddCell(new Phrase("Polen", font));
            table.AddCell(new Phrase("Locatie", font));

            foreach (DataRow dtRow in _dgvLaagspanningsnet.GetDataTable().Rows)
            {
                if (dtRow["Type"] == DBNull.Value)
                {
                    break;
                }

                if (_dgvLaagspanningsnet.GetMode() == LaagspanningGridView.Search)
                {
                    table.AddCell(new Phrase(dtRow["T/VB/K"].ToString(), font));
                }
                table.AddCell(new Phrase(dtRow["Kring"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Nummer"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Omschrijving"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Kabeltype"] + " " + dtRow["Kabelsectie"], font));
                table.AddCell(new Phrase(dtRow["Stroom (A)"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Aantal Polen"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Locatie"].ToString(), font));
            }

            doc.Add(table);

            // Volgende pagina
            doc.NewPage();
        }

        private void ToPrn(byte[] memStream)
        {
            var printerSettings = new PrinterSettings
            {
                PrinterName = _printer,
                Copies = _kopies,
            };

            var pageSettings = new PageSettings(printerSettings)
            {
                Margins = new Margins(0, 0, 0, 0),
            };

            foreach (PaperSize paperSize in printerSettings.PaperSizes)
            {
                if (paperSize.PaperName == "a4")
                {
                    pageSettings.PaperSize = paperSize;
                    break;
                }
            }

            var stream = new MemoryStream(memStream);
            using (var document = PdfiumViewer.PdfDocument.Load(stream))
            {
                using (var printDocument = document.CreatePrintDocument())
                {
                    printDocument.PrinterSettings = printerSettings;
                    printDocument.DefaultPageSettings = pageSettings;
                    printDocument.PrintController = new StandardPrintController();
                    printDocument.Print();
                }
            }
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
