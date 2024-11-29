using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ShippingAgentExport")]
    public class ShippingAgentExport : CommonProperties
    {
        public ShippingAgentExport()
        {
            LoadingPrograms = new List<LoadingProgram>();
            CustomerNOCs = new List<CustomerNOC>();
            EmptyReceivings = new List<EmptyReceiving>();
            Users = new List<User>();
            ShippingAgentTariffExports = new List<ShippingAgentTariffExport>();
        }
        public long ShippingAgentExportId { get; set; }

        public string ShippingAgentName { get; set; }
        //public long? NatureOfTariffId { get; set; }

        //public NatureOfTariff NatureOfTariff { get; set; }

        public string ContactPerson { get; set; }
        //public string ImplementFrom { get; set; }

        //public string ImplementTo { get; set; }

        public string BillDateType { get; set; }
        public string BillDateTypeFCL { get; set; }
 
        public int OtherCharges { get; set; }

        public string ChargesName { get; set; }
        public string Category { get; set; }
        public string NTNNumber { get; set; }
        public string WeightAllow { get; set; }
        public string VehcileAmountAllow { get; set; }

        public string AllowSpecialChargeLCL { get; set; }
        public string AllowSpecialChargeFCL { get; set; }
 
        public double WeightAmount { get; set; }

        public string Address { get; set; }

        public string TelephoneNumber { get; set; }

        public string Email { get; set; }

        public List<LoadingProgram> LoadingPrograms { get; set; }

        public List<CustomerNOC> CustomerNOCs { get; set; }

        public List<EmptyReceiving> EmptyReceivings { get; set; }
        public List<ShippingAgentTariffExport> ShippingAgentTariffExports { get; set; }

        public List<User> Users { get; set; }
    }
}
