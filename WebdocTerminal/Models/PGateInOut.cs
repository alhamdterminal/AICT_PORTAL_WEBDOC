using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{

    [Table("PGateInOut")]
    public class PGateInOut : CommonProperties
    {
        public long PGateInOutId { get; set; }
        public string ImageUrl { get; set; }
        public string CNIC { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string VisitReason { get; set; }
        public string Remarks { get; set; }
        //public string PassNumber { get; set; }
        public string VisitorCompany { get; set; }
        public string DocumentRetain { get; set; }
        public DateTime? InDateTime { get; set; }
        public DateTime? OuttDateTime { get; set; }
        public bool IsGateIn { get; set; }
        public bool IsGateOut { get; set; }
    }
}
