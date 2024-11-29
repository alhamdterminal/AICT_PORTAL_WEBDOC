using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class InterportIntraportViewModel : CommonProperties
    {

        public List<VIRO> VIROFiles { get; set; }

        public List<IGMO> IGMOFiles { get; set; }

        public List<IPAO> IPAOFiles { get; set; }
    }
}
