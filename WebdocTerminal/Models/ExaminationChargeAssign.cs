using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExaminationChargeAssign")]
    public class ExaminationChargeAssign : CommonProperties
    {
        public long ExaminationChargeAssignId { get; set; }

        public long CYContainerId { get; set; }
        public CYContainer CYContainer { get; set; }
        public string ChargeName { get; set; }
        public double ChargeQuantity { get; set; }
        public double ChargeQuantity40 { get; set; }
        public double ChargeQuantity45 { get; set; }
        public double ChargeAmount20 { get; set; }
        public double ChargeAmount40 { get; set; }
        public double ChargeAmount45 { get; set; }
        public string Remarks { get; set; } 
    }
}
