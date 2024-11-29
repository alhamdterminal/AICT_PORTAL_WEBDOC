using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AutoExaminationCharge")]
    public class AutoExaminationCharge : CommonProperties
    {
        public long AutoExaminationChargeId { get; set; }

        public string AutoExaminationChargeName { get; set; }
        public double ExaminationChargeAmount20 { get; set; }
        public double ExaminationChargeAmount40 { get; set; }
        public double ExaminationChargeAmount45 { get; set; }

        public long ExaminationChargeId { get; set; }
        public ExaminationCharge ExaminationCharge { get; set; }

    }
}
