using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Helpers
{
    public class AppConfig
    {
        public string EdiFTPHost { get; set; }
        public string EdiFTPUsername { get; set; }
        public string EdiFTPPassword { get; set; }


        public string CustomFTPHost { get; set; }
        public string CustomFTPPassword { get; set; }
        public string CustomFTPUsername { get; set; }



        public bool EDIEnabled { get; set; }
        public string MessageFrom { get; set; }

        public string EDOFTPHost { get; set; }
        public string EDOUsername { get; set; }
        public string EDOPassword { get; set; }


        public string EDIDataTransferFTPHost { get; set; }
        public string EDIDataTransferUsername { get; set; }
        public string EDIDataTransferPassword { get; set; }

        public string MaerskFTPHost { get; set; }
        public string MaerskUsername { get; set; }
        public string MaerskPassword { get; set; }

        public string WeightmentPort { get; set; }
        public string WeightmentIP { get; set; }
    }
}
