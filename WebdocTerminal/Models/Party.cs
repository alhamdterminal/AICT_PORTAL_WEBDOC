using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Party")]
    public class Party : CommonProperties
    {
        public Party()
        {
            chequeReciveds = new List<ChequeRecived>();

            RefundAmounts = new List<RefundAmount>();

            AmountReciveds = new List<AmountReceived>();

        }

        public long PartyId { get; set; }

        public string PartyName { get; set; }

        public string Consignee { get; set; }


        public long? ShippingAgentId { get; set; }

        public ShippingAgent ShippingAgent { get; set; }
        public long? ShippingLineId { get; set; }

        public ShippingLine ShippingLine { get; set; }

        public long? ClearingAgentId { get; set; }

        public ClearingAgent ClearingAgent { get; set; }

        public long? ConsigneId { get; set; }

        public Consigne Consigne { get; set; }

        public List<ChequeRecived> chequeReciveds { get; set; }

        public List<RefundAmount> RefundAmounts { get; set; }

        public List<AmountReceived> AmountReciveds { get; set; }

        public List<PartyLedger> PartyLedgers { get; set; }
    }
}
