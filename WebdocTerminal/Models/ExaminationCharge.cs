using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExaminationCharge")]
    public class ExaminationCharge : CommonProperties
    {
        public long ExaminationChargeId { get; set; }
        public string ExaminationChargeName { get; set; }
        public double ExaminationChargeAmount20 { get; set; }
        public double ExaminationChargeAmount40 { get; set; }
        public double ExaminationChargeAmount45 { get; set; }
    }
}
