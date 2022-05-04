using System;
using System.Collections.Generic;
using System.Text;

namespace zad3
{
    public class Copier : BaseDevice
    {
        protected IPrinter _printer;
        protected IScanner _scanner;

        public Copier(IPrinter printer, IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public int PrintCounter { get; private set; }
        public int ScanCounter { get; protected set; }

        public void PrinterOn()
        {
            this._printer.PowerOn();
        }

        public void PrinterOff()
        {
            this._printer.PowerOff();
        }

        public void ScannerOn()
        {
            this._scanner.PowerOn();
        }

        public void ScannerOff()
        {
            this._scanner.PowerOff();
        }

        public void Print(in IDocument document)
        {
            if (GetState() == IDevice.State.on)
            {
                PrinterOn();
                _printer.Print(in document);
                PrintCounter++;
                PrinterOff();
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType type = IDocument.FormatType.JPG)
        {
            if (type == IDocument.FormatType.JPG)
                document = new ImageDocument("HolidayPhoto.jpg");
            else if (type == IDocument.FormatType.PDF)
                document = new PDFDocument("DocumentPDF.pdf");
            else
                document = new TextDocument("TextFile.txt");

            if (GetState() == IDevice.State.on)
            {
                ScannerOn();
                _scanner.Scan(out document, type);
                ScanCounter++;
                ScannerOff();
            }
        }

        public void ScanAndPrint(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (formatType == IDocument.FormatType.JPG)
                document = new ImageDocument("HolidayPhoto.jpg");
            else if (formatType == IDocument.FormatType.PDF)
                document = new PDFDocument("DocumentPDF.pdf");
            else
                document = new TextDocument("TextScan.txt");

            if (GetState() == IDevice.State.on)
            {
                ScannerOn();
                _scanner.Scan(out document, formatType);
                ScanCounter++;
                ScannerOff();

                PrinterOn();
                _printer.Print(in document);
                PrintCounter++;
                PrinterOff();
            }
        }
    }
}
