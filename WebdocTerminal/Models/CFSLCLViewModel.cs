using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class CFSLCLViewModel : CommonProperties
    {
        public List<GDIO> GDIOFiles { get; set; }

        public List<ECMO> ECMOFiles { get; set; }

        public List<CRLO> CRLOFiles { get; set; }

        public List<GTOO> GTOOFiles { get; set; }
    }
}
