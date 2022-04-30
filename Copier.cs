using System;


namespace ver1
{
    public class Copier : BaseDevice
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
                PrintCounter++;
            Console.WriteLine($"{DateTime.Today} Print: {document.GetFileName()}");

        }
    }
}
