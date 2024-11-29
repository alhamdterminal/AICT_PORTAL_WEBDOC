using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AccountSubCategory")]
    public class AccountSubCategory : BaseClass
    {
        public long AccountSubCategoryId { get; set; }     
        public long AccountCategoryId { get; set; }
        public AccountCategory AccountCategory { get; set; }
        public string Code { get;  set; }
        public string Name { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public bool IsActive { get; set; }
        public long CompanyId { get; set; }
        //public Company Company { get; set; }
    }
}
