using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class PartyCodeAndBalanceViewModel
    {
        public long PartyId { get; set; }
        public string Status { get; set; }
        public string Nature { get; set; }
        public double balance { get; set; }
    }
}
