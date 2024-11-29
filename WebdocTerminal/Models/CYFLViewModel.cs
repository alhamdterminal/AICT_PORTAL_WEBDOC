using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class CYFLViewModel : CommonProperties
    {
        public List<GDI> GDIFiles { get; set; }

        public List<IHC> IHCFiles { get; set; }

        public List<ECM> ECMFiles { get; set; }

        public List<CRL> CRLFIles { get; set; }
    }
}
