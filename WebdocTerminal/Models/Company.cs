using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Company")]
    public class Company : CommonProperties
    {
        public Company()
        {
            Users = new List<User>();
        }
        public long CompanyId { get; set; }
        public string Code { get; set; }  //new as per account
        public string CompanyName { get; set; }
        public string Address { get; set; } 
        public string Phone { get; set; }//new as per account
        public string FaxNo { get; set; }//new as per account
        public string CompanyEmail { get; set; }
        public string URL { get; set; }//new as per account
        public string NTN { get; set; }//new as per account
        public long CityId { get; set; }//new as per account
        public long CountryId { get; set; }//new as per account
        public string FilePath { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now; //new as per account
        public DateTime? EditedAt { get; set; } = DateTime.Now;//new as per account
        public long? CreatedBy { get; set; }//new as per account
        public long? EditedBy { get; set; }//new as per account
        public List<User> Users { get; set; }
 
    }
}
