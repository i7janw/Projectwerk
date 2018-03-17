using System;
using System.Collections.Generic;
using System.Data;
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
        private Database database;
        private DGV dgvLaagspanningsnet;
        private String selectie;

        private String huidigAansluitpunt; // onthouden van het aansluitpunt dat getoond werd toen we op afdrukken klikte.

        private String printer;
        private short kopies;
        private bool inclusief;

        public Afdrukken(DGV _dgv)
        {
            InitializeComponent();
            database = new Database();
            huidigAansluitpunt = _dgv.GetAansluitpunt();
            selectie = huidigAansluitpunt;
            dgvLaagspanningsnet = _dgv;
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
            listAansluitpunt.Insert(0, "Huidige Pagina");

            // Steek ze in de selectie combobox
            cmbSelectie.DataSource = listAansluitpunt;

            // Kies de huidige selectie
            cmbSelectie.SelectedItem = selectie;
            Console.WriteLine(selectie);
        }

        // Er is op de OK knop geklikt.
        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Open doc om te schrijven naar een MemoryStream
            Document doc = new Document(PageSize.A4);
            // String pdf = "C:\\Users\\Jan Wagemakers\\Documents\\MEGA\\" + _ap + ".pdf";
            // var output = new FileStream(pdf, FileMode.Create);
            MemoryStream output = new MemoryStream();
            var writer = PdfWriter.GetInstance(doc, output);
            doc.Open();


            // steek de geselecteerde gegevens in de variablen
            kopies = Convert.ToInt16(cmbAantal.Text);
            selectie = cmbSelectie.Text;
            printer = cmbPrinter.Text;
            inclusief = rbtnInclusief.Checked;
            if (selectie.Equals("Huidige Pagina"))
            {
                selectie = huidigAansluitpunt;
            }

            // Als selectie == "", dan staan we op Transfo of zoekresultaten
            if (selectie.Equals(""))
            {
                Print(doc);
            }
            else
            {
                // Afdrukken. 
                // We starten met het afdrukken van de selectie en
                // indien gewenst worden ook de aansluitpunten die op de selectie zijn aangesloten afgedrukt.
                List<string>
                    todo = new List<string>(); // todo = lijst van aansluitpunten waarvan we nog moeten testen of er aansluitpunten op zijn aangesloten
                todo.Add(selectie);

                while (todo.Count != 0)
                {
                    // zijn er nog te testen aansluitpunten?
                    List<string> tmp = new List<string>(); // tmp = om nieuwe todo lijst aan te maken
                    foreach (String ap in todo) // doorloop alle aansluitpunten in todo 
                    {
                        dgvLaagspanningsnet.ShowAansluitpunt(ap); // Toon en Print selectie
                        Print(doc);
                        if (inclusief) // Gaan we ook de aangesloten aansluitpunten afdrukken?
                        {
                            foreach (DataGridViewRow row in dgvLaagspanningsnet.Rows)
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
            }

            // Sluit doc
            doc.Close();

            // Print aangemaakte doc af
            ToPrn(output.ToArray());

            // sluit het venster
            this.DialogResult = DialogResult.OK;
            Close();
        }

        // Het afdrukken zelf. TODO
        private void Print(Document doc)
        {
            String _ap = dgvLaagspanningsnet.GetAansluitpunt();
            Console.WriteLine("Afdrukken van " + _ap);

            iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10);
            iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 32);

            Paragraph text;
            // Titel
            String title;
            switch (dgvLaagspanningsnet.GetMode())
            {
                case 1: // transfos
                    title = "Overzicht transfos";
                    break;
                case 3: // search
                    title = "Zoeken : " + _ap;
                    break;
                default: // aansluitpunt // case 2 = default
                    title = "Layout van " + _ap;
                    break;
            }

            text = new Paragraph(title, titleFont);
            text.Alignment = Element.ALIGN_CENTER;
            text.SpacingAfter = 20;
            doc.Add(text);

            // Info
            String voeding = database.GetVoeding(_ap);
            String locatie = database.GetAansluitpuntLocatie(_ap);
            String kabel = database.GetKabel(_ap);
            String stroom = database.GetStroom(_ap);
            text = new Paragraph("Voeding : " + voeding + "\n" +
                                 "Kabel : " + kabel + "\n" +
                                 "Stroom : " + stroom + "\n" +
                                 "Locatie : " + locatie, font);
            text.Alignment = Element.ALIGN_LEFT;
            text.SpacingAfter = 20;
            doc.Add(text);

            // Table
            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100;
            float[] widths = new float[] {1, 2, 5, 3, 2, 2, 2};
            table.SetWidths(widths);
            //PdfPRow row = null;
            //float[] widths = new float[] { 4f, 4f, 4f, 4f };

            table.AddCell(new Phrase("Kring", font));
            table.AddCell(new Phrase("Nummer", font));
            table.AddCell(new Phrase("Omschrijving", font));
            table.AddCell(new Phrase("Kabel", font));
            table.AddCell(new Phrase("Stroom", font));
            table.AddCell(new Phrase("Polen", font));
            table.AddCell(new Phrase("Locatie", font));

            foreach (DataRow dtRow in dgvLaagspanningsnet.GetDataTable().Rows)
            {
                if (dtRow["Type"] == DBNull.Value)
                {
                    break;
                }

                table.AddCell(new Phrase(dtRow["Kring"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Nummer"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Omschrijving"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Kabeltype"].ToString() + " " + dtRow["Kabelsectie"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Stroom (A)"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Aantal Polen"].ToString(), font));
                table.AddCell(new Phrase(dtRow["Locatie"].ToString(), font));
            }

            doc.Add(table);

            // Volgende pagina
            doc.NewPage();
        }

        private bool ToPrn(byte[] _stream)
        {
            try
            {
                var printerSettings = new PrinterSettings
                {
                    PrinterName = printer,
                    Copies = (short) kopies,
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

                var stream = new MemoryStream(_stream);
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

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Er is op de anuleer knop geklikt.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Radio button als on/off switch (AutoCheck = false)
        private void rbtnInclusief_Click(object sender, EventArgs e)
        {
            rbtnInclusief.Checked = !rbtnInclusief.Checked;
        }
    }
}
