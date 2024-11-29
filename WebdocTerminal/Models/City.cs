using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("City")]
    public class City : BaseClass
    {
        public long CityId { get; set; }
        public string CityCode { get;  set; }
        public string CityName { get; set; }
        public string CityPhoneCode { get; set; }
        public bool Status { get;   set; }
        public long CountryId { get;  set; }
        public Country Country { get;  set; }
    }
}
