using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PaymentUpdateDetail")]
    public class PaymentUpdateDetail  : CommonProperties
    {
        public long PaymentUpdateDetailId { get; set; }
        public double SecDeposit { get; set; }
        public double THCDoc { get; set; }
        public double THCInsurance { get; set; }
        public double THC { get; set; }
        public double THCOthers { get; set; }
        public double TotalTHCCharges { get; set; }
        public double Lolo { get; set; }
        public double TotalCharges { get; set; }
        public double LOLOIncInTHC { get; set; }
        public double Detention { get; set; }
          
        public long? ShippingAgentIdForSD { get; set; }
         
        public long? ShippingAgentIdForTHC { get; set; }
         
        public long ? ShippingAgentIdForLoLo { get; set; }
         
        public ShippingAgent ShippingAgent { get; set; } 

        public long? ShippingLineIdForSD  { get; set; }
        public long? ShippingLineIdForTHC  { get; set; }
        public long? ShippingLineIdForLOLO  { get; set; }

        public ShippingLine ShippingLine { get; set; }

        public long PreAlterDetailId { get; set; }
        public PreAlterDetail PreAlterDetail { get; set; }
        public long PaymentUpdateId { get; set; }
        public PaymentUpdate PaymentUpdate { get; set; }

    }
}
