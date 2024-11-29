using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ScheduleOfChargeItem")]
    public class ScheduleOfChargeItem : CommonProperties
    {
        public long ScheduleOfChargeItemId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public double Amount { get; set; }

        public long ScheduleOfChargeId { get; set; }

        public ScheduleOfCharge ScheduleOfCharge { get; set; }
    }
}
