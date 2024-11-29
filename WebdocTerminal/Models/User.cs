using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class User : CommonProperties
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string CNIC { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public bool? Status { get; set; }

        public string IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }

        public long? ShippingAgentId { get; set; }

        public ShippingAgent ShippingAgent { get; set; }

        public long? ShippingAgentExportId { get; set; }

        public ShippingAgentExport ShippingAgentExport { get; set; }

        public long? CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
