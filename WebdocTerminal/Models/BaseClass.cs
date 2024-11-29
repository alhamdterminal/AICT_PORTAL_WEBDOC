using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class BaseClass
    {
        //public long? CountryId { get; set; }
        //public long? CityId { get; set; }
        //public long? BranchId { get; set; }
        //public long? DepartmentId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; } = DateTime.Now;
        public long? CreatedBy { get; set; }
        public long? EditedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeleteDate { get; set; } = DateTime.Now;
    }
}
