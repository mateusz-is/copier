using System;
using System.Collections.Generic;
using System.Text;
using ver1;

namespace Zadanie2
{
    public interface IFax : IDevice
    {
        void Send(IDocument document, string faxCounter);
    }
}