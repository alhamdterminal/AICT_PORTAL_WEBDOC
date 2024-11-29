using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Customer")]
    public class Customer : BaseClass
    {
        public long CustomerId { get; set; }        
        
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string NTN { get; set; }
        public string SalesTaxRegNumber { get; set; }
        public string Phone1 { get; set; }
        public string Email { get; set; }
        public long CountryId { get; set; }
        public long CityId { get; set; }
        public long ChartOfAccountId { get; set; }
        public ChartOfAccount ChartOfAccount { get; set; }
        public long CompanyId { get; set; }
        public bool IsActive { get; set; }

    }
}
