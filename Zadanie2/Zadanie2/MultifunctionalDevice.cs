using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace Zadanie2
{
   public class MultifunctionalDevice : BaseDevice, IPrinter, IScanner
    {

        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public int SendCounter { get; private set; } = 0;

        public string FaxCounter { get; }

        public MultifunctionalDevice(string faxCounter)
        {
            FaxCounter = faxCounter;
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
                PrintCounter++;
            Console.WriteLine($"{DateTime.Today} Print: {document.GetFileName()}");
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            string fileType;

            switch (formatType)
            {
                case IDocument.FormatType.JPG:
                    fileType = "Image";
                    break;
                case IDocument.FormatType.PDF:
                    fileType = "PDF";
                    break;
                default:
                    fileType = "Text";
                    break;
            }
            string name = string.Format("{0}imageFile{1}.{2}", fileType, ScanCounter + 1, formatType.ToString().ToLower());

            if (formatType == IDocument.FormatType.TXT)
                document = new TextDocument(name);
            if (formatType == IDocument.FormatType.JPG)
                document = new ImageDocument(name);
            else
                document = new PDFDocument(name);


            if (state == IDevice.State.on)
                ScanCounter++;
            Console.WriteLine($"{DateTime.Today} Scan: {document.GetFileName()}");
        }
        public void ScanAndPrint()
        {
            Scan(out IDocument newDocument);
            Print(newDocument);
        }

    }
}
