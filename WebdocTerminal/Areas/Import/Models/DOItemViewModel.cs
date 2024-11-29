using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class DOItemViewModel
    {
        public string BLNumber { get; set; }

        public string ConsigneeName { get; set; }

        public string ConsigneeNTN { get; set; }

        public string DONumber { get; set; }

        public string GDNumber { get; set; }
        public string Status { get; set; }
        public string DocumentCode { get; set; }
    }
}
