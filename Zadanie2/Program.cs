using System;
using ver1;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var device = new MultifunctionalDevice("123456789");
            device.PowerOn();

            device.Send(new PDFDocument("abc.pdf"), "987654321");
            device.ScanAndSend("987654321");

            device.PowerOff();
        }
    }
}
