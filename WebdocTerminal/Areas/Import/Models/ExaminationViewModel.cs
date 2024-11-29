using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ExaminationViewModel
    {

        public string VirNumber { get; set; }

        public string BlNumber { get; set; }

        public int? IndexNumber { get; set; }

        public string HandlingCode { get; set; }

        public DateTime? Date { get; set; }
    }
}
