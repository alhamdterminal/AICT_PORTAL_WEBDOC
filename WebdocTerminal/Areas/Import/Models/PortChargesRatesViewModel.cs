using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class PortChargesRatesViewModel
    {
        public long ContainerId { get; set; }
        public string Key { get; set; }
        public string VirNumber { get; set; }
        public string ContainerNumber { get; set; }
        public int? IndexNumber { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public int Size { get; set; }
        public string ShippingAgent { get; set; }
        public string GoodsHeadName { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Type { get; set; }
        public double DemurrageCharges { get; set; }
        public double WeighmentCharges { get; set; }
        public double OverWeightPenalty { get; set; }
        public double DetentionChargesOrBulletSeal { get; set; }
        public double ThcPhcKdlpCharges { get; set; }
        public double LoloCharges { get; set; }
        public double QictCharges { get; set; }
        public double OtherCharges { get; set; }
        public double WashAndCleanCharges { get; set; }
        public double ANF { get; set; }
        public double Pallet { get; set; }
        public double Recover { get; set; }
        public double TransportCharges { get; set; }
        public double TotalCharges { get; set; }
    }
}
