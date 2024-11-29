using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class GDListViewModel : CommonProperties
    {
        public long Id { get; set; }
        public DateTime? DateReceived { get; set; }
        public string GDNumber { get; set; }
        public string ClearingAgent { get; set; }
        public string Shipper { get; set; }
        public int NoOfPackages { get; set; }
        public bool IsChecked { get; set; }
        public DateTime? PaperReadyDate { get; set; }

        public DateTime? ANFDate { get; set; }
    }
}
