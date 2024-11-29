using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Auction")]
    public class Auction : CommonProperties
    {
        public long AuctionId { get; set; }

        public DateTime AuctionDate { get; set; }

        public long ? CustomDoNo { get; set; }

        public DateTime CustomDoDate { get; set; }

        public string AuctionLotNumber { get; set; }

        public double? FinalBidAmount { get; set; }

        public string BankVoucherNo25 { get; set; }

        public string BankVoucherNo75 { get; set; }

        public long? BidderId { get; set; }
        public Bidder Bidder { get; set; }

        public long? ContainerIndexId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public long? CYContainerId { get; set; }

        public CYContainer CYContainer { get; set; }



    }
}
