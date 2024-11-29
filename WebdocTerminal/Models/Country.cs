using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Country")]
    public class Country : BaseClass 
    {
        public long CountryId { get;  set; }
        public  string CountryCode { get; set; }
        public  string CountryName { get; set; }
        public  string CountryPhoneCode { get; set; }
        public  bool Status { get; set; }

    }
}
