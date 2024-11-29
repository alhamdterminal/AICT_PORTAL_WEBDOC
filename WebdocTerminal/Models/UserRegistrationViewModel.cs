using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class UserViewModel : CommonProperties
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string CNIC { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public List<string> Role { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string IdentityId { get; set; }

        public long? ShippingAgentId { get; set; }

        public long? ShippingAgentExportId { get; set; }

        public long? CompanyId { get; set; }

        public bool? Status { get; set; }

 
    }
}
