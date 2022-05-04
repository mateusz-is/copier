using System;

namespace zad3
{
    class Program
    {
        static void Main(string[] args)
        {
            var printer = new PrintDevice();
            var scanner = new ScanDevice();

            var copier = new Copier(printer, scanner);
            copier.PowerOn();

            var MyDocument = new PDFDocument("MyDocument.pdf");
            var YourDocument = new PDFDocument("YourDocument.pdf");

            copier.Print(YourDocument);
            copier.Print(MyDocument);
            copier.ScanAndPrint(out IDocument doc, IDocument.FormatType.PDF);

            copier.PowerOff();
        }
    }
}
