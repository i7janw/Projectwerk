using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfiumViewerTest
{
    class PrintPDF {

        public bool Print(string printer, string paperName, string filename, int copies)
        {
            try
            {
                var printerSettings = new PrinterSettings
                {
                    PrinterName = printer,
                    Copies = (short)copies,
                };

                Console.WriteLine("printerSettings DONE");

                var pageSettings = new PageSettings(printerSettings)
                {
                    Margins = new Margins(0, 0, 0, 0),
                };

                Console.WriteLine("pageSettings DONE");

                foreach (PaperSize paperSize in printerSettings.PaperSizes)
                {
                    if(paperSize.PaperName == paperName)
                    {
                        pageSettings.PaperSize = paperSize;
                        break;
                    }
                }

                Console.WriteLine("paperSize DONE");

                using (var document = PdfDocument.Load(filename))
                {
                    using(var printDocument = document.CreatePrintDocument())
                    {
                        printDocument.PrinterSettings = printerSettings;
                        printDocument.DefaultPageSettings = pageSettings;
                        printDocument.PrintController = new StandardPrintController();
                        printDocument.Print();
                    }
                }
                return true;
            } catch 
            {
                return false;
            }
        }
    }
}
