using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Department")]
    public class Department : BaseClass
    {
         
        public long DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }

 
    }
}
