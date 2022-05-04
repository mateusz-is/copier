using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ver1;
using Zadanie2;

namespace Zadanie2UnitTests
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
    public class UnitTestMulti
    {
        [TestMethod]
        public void ScanDeviceIsOn()
        {
            var device = new MultifunctionalDevice("test");
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
        public void SendDeviceIsOff()
        {
            var device = new MultifunctionalDevice("test");
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
        public void ScanAndSendDeviceIsOn()
        {
            var device = new MultifunctionalDevice("test");
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

        public void ScanAndSendDeviceIsOff()
        {
            var device = new MultifunctionalDevice("test");
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
