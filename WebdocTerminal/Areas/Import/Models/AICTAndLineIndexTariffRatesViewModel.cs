using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class AICTAndLineIndexTariffRatesViewModel
    {
        public long containerIndexId { get; set; }
        public string Key { get; set; }
        public string IGMNo { get; set; }
        public string ContainerNumber { get; set; }
        public int ContainerSize { get; set; }
        public int? IndexNo { get; set; }
        public string ShippingAgentName { get; set; }
        public string GoodHeadName { get; set; }
        public DateTime? CFSContainerGateInDate { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double HigherCBM { get; set; }
        public double AICTPerCBMRate { get; set; }
        public double AICTPerIndexRate { get; set; }
        public double FFPerCBMRate { get; set; }
        public double FFPerIndexRate { get; set; }
        public double AdditionalChargeAICTPerCBMRate { get; set; }
        public double AdditionalChargeAICTPerIndexRate { get; set; }
        public double AdditionalChargeFFPerCBMRate { get; set; }
        public double AdditionalChargeFFPerIndexRate { get; set; }
        public string BTLRemarks { get; set; }
        public double TotalCBMRate { get; set; }
        public double TotalPerIndexRate { get; set; }
        public double TotalCharges { get; set; }
        public double PerBoxRate { get; set; }
        public double PortCharges { get; set; }
        public double SpecialCharges { get; set; }
        public string billToLine { get; set; }

    }
}
