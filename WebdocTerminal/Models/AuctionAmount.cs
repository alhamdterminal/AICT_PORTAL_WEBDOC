using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AuctionAmount")]
    public class AuctionAmount : CommonProperties
    {
        public long AuctionAmountId { get; set; }
        public double Rate20 { get; set; }
        public double Rate40 { get; set; }
        public double Rate45 { get; set; }
        public double CBM { get; set; }
        public double Weight { get; set; }
        public double HanndlingWeight { get; set; }
    }
}
