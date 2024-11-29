using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Bidder")]
    public class Bidder : CommonProperties
    {
        public long BidderId { get; set; }
        public string BidderName { get; set; }
        public string BidderNic { get; set; }
    }
}
