using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class MarkedAuctionDetailViewModel
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string VirNumber { get; set; }
        public int? IndexNumber { get; set; } 
        public bool? IsChecked { get; set; } 
    }
}
