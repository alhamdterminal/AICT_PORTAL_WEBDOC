using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PortCharge")]
    public class PortCharge : CommonProperties
    {
        public long PortChargeId { get; set; }
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
        public string VirNumber { get; set; }
        public int IndexNumber { get; set; }
        public string ContainerNumber { get; set; }

    }
}
