using System;

namespace ver1
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);
            IDocument doc2;
            xerox.Scan(out doc2);
            System.Console.WriteLine("************** ScanAndPrint function **************");
            xerox.ScanAndPrint();
            System.Console.WriteLine("************** End ScanAndPrint function **************");
            System.Console.WriteLine("************** Counters **************");
            System.Console.WriteLine("Device counter: " + xerox.Counter);
            System.Console.WriteLine("Print counter: " + xerox.PrintCounter);
            System.Console.WriteLine("Scan counter: " + xerox.ScanCounter);
            System.Console.WriteLine("************** End Counters **************");

            xerox.PowerOff();

        }
    }
}
