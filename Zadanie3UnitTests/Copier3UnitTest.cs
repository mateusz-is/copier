using Microsoft.VisualStudio.TestTools.UnitTesting;
using zad3;
using System;
using System.IO;

namespace Zadanie3UnitTests
{

    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }


    [TestClass]
    public class UnitTestCopier
    {
        [TestMethod]
        public void Copier_GetState_StateOff()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [TestMethod]
        public void Copier_GetState_StateOn()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }


        // weryfikacja, czy po wywo�aniu metody `Print` i w��czonej kopiarce w napisie pojawia si� s�owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOn()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo�aniu metody `Print` i wy��czonej kopiarce w napisie NIE pojawia si� s�owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Print_DeviceOff()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo�aniu metody `Scan` i wy��czonej kopiarce w napisie NIE pojawia si� s�owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOff()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo�aniu metody `Scan` i wy��czonej kopiarce w napisie pojawia si� s�owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_Scan_DeviceOn()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywo�anie metody `Scan` z parametrem okre�laj�cym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void Copier_Scan_FormatTypeDocument()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(out doc1, type: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(out doc1, type: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(out doc1, type: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywo�aniu metody `ScanAndPrint` i wy��czonej kopiarce w napisie pojawiaj� si� s�owa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOn()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc;
                copier.ScanAndPrint(out doc);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo�aniu metody `ScanAndPrint` i wy��czonej kopiarce w napisie NIE pojawia si� s�owo `Print`
        // ani s�owo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Copier_ScanAndPrint_DeviceOff()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc;
                copier.ScanAndPrint(out doc);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Copier_PrintCounter()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint(out doc1, doc1.GetFormatType());
            copier.ScanAndPrint(out doc2, doc2.GetFormatType());

            // 5 wydruk�w, gdy urz�dzenie w��czone
            Assert.AreEqual(5, copier.PrintCounter);
        }

        [TestMethod]
        public void Copier_ScanCounter()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint(out doc3, doc3.GetFormatType());
            copier.ScanAndPrint(out doc2);

            // 4 skany, gdy urz�dzenie w��czone
            Assert.AreEqual(4, copier.ScanCounter);
        }

        [TestMethod]
        public void Copier_PowerOnCounter()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var copier = new Copier(p, s);
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(out doc1);
            IDocument doc2;
            copier.Scan(out doc2);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(in doc3);

            copier.PowerOff();
            copier.Print(in doc3);
            copier.Scan(out doc1);
            copier.PowerOn();

            copier.ScanAndPrint(out doc1);
            copier.ScanAndPrint(out doc2);

            // 3 w��czenia
            Assert.AreEqual(3, copier.Counter);
        }

    }

    [TestClass]
    public class Copier3UnitTest
    {
        [TestMethod]
        public void Device_Scan_DeviceisOn()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var f = new Fax("test");
            var device = new MultiFunctionalDevice(p, s, f);
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();

            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                device.Send(doc1, "test");

                Assert.IsTrue(consoleOutput.GetOutput().Contains("Sent"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Device_Send_DeviceIsOff()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var f = new Fax("test");
            var device = new MultiFunctionalDevice(p, s, f);
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                device.Send(doc1, "test");
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Device_ScanAndSend_DeviceIsOn()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var f = new Fax("test");
            var device = new MultiFunctionalDevice(p, s, f);
            device.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.ScanAndSend("test");
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Sent"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        public void Device_ScanAndSend_DeviceIsOff()
        {
            var p = new PrintDevice();
            var s = new ScanDevice();
            var f = new Fax("test");
            var device = new MultiFunctionalDevice(p, s, f);
            device.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                device.ScanAndSend("test");
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
    }
}
