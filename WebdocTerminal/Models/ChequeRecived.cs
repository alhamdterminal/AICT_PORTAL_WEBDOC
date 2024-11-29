using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ChequeRecived")]
    public class ChequeRecived : CommonProperties
    {
        public long ChequeRecivedId { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }

        public DateTime  ChequeDate { get; set; }

        public long? PartyId { get; set; }

        public Party Party { get; set; }

        public long? BankId { get; set; }

        public Bank Bank { get; set; }
         
        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
        public List<PartyLedger> PartyLedgers { get; set; }

    }
}
