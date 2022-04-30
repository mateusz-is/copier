using System;
using ver1;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var device = new MultifunctionalDevice("2");
            device.PowerOn();
            device.Print(new PDFDocument("abc.pdf"));
            device.Send(new PDFDocument("abc.pdf"), "23");
 
            device.ScanAndSend("23");

            

            device.PowerOff();
        }
    }
}
