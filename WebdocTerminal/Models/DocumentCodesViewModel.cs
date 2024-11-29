using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class DocumentCodesViewModel
    {
        public long DocumentCodeId { get; set; }
        public string Code { get; set; }
        public string DocumentName { get; set; }
        public string Remarks { get; set; }
        public bool IsChecked { get; set; }
    }
}
