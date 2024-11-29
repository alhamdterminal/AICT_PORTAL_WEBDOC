using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class PreAlertViewModel
    {
         public string ContainerNumber { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public string CargoType { get; set; }
        public string SOCCOC { get; set; } 
        public long PreAlterDetailId { get; set; }
        public long PaymentUpdateId { get; set; }
        public string PreAlertNumber { get; set; }
        public long RequestNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public string AccNo { get; set; }
        public double? SecDeposit { get; set; }
        public double? THCDoc { get; set; }
        public double? THCInsurance { get; set; }
        public double? THC { get; set; }
        public double? THCOthers { get; set; }
        public double? KDBL { get; set; }
        public double? Lolo { get; set; }
        public double? TotalCharges { get; set; }
        public string Narration { get; set; }
        public double? LOLOIncInTHC { get; set; }
        public double? Detention { get; set; }

        public long? ShippingLineIdForSD { get; set; }
        public long? ShippingLineIdForTHC { get; set; }
        public long? ShippingLineIdForLOLO { get; set; }


    }
}
