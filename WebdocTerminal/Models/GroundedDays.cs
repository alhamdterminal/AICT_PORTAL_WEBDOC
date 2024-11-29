using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("GroundedDays")]
    public class GroundedDays : CommonProperties
    {
        public long GroundedDaysId { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public DateTime? SealIssueDate { get; set; }
        public long? Days { get; set; }
        public long CYContainerId { get; set; }
        public CYContainer CYContainer { get; set; }
    }
}
