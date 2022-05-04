using System;
using System.Collections.Generic;
using System.Text;

namespace zad3
{
    public class MultiFunctionalDevice : Copier
    {
        private IFax _fax;
        public int Counter { get; private set; }
      
        public MultiFunctionalDevice(IPrinter printer, IScanner scanner, IFax fax) : base(printer, scanner)
        {
            this._fax = fax;
        }

        public void Send(IDocument doc, string faxAddress)
        {
            if (GetState() == IDevice.State.on)
            {
                this._fax.PowerOn();
                _fax.Send(doc, faxAddress);
                Counter++;
                this._fax.PowerOff();
            }

        }

        public void ScanAndSend(string faxAddress)
        {
            if (GetState() == IDevice.State.on)
            {
                ScannerOn();
                this._scanner.Scan(out IDocument doc, IDocument.FormatType.JPG);
                Counter++;
                ScannerOff();

                this._fax.PowerOn();
                this._fax.Send(doc, faxAddress);
                Counter++;
                this._fax.PowerOff();
            }
        }
    }
}
