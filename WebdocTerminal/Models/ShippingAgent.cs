using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ShippingAgent")]
    public class ShippingAgent : CommonProperties
    {

        public ShippingAgent()
        {
            GateIns = new List<GateIn>();

            TellySheets = new List<TellySheet>();

            ShippingAgentTariffs = new List<ShippingAgentTariff>();

            Users = new List<User>();

        }
        public long ShippingAgentId { get; set; }

        public string Name { get; set; }

        public string Line { get; set; }

        public string LineCode { get; set; }
        public string BillDateType { get; set; }
        public string BillDateTypeFCL { get; set; }
        public string BillDateTypeCY { get; set; }

        public int OtherCharges { get; set; }

        public string ChargesName { get; set; }
        public string Category { get; set; }
        public string NTNNumber { get; set; }
        public string WeightAllow { get; set; }
        public string VehcileAmountAllow { get; set; }

        public string AllowSpecialChargeLCL { get; set; }
        public string AllowSpecialChargeFCL { get; set; }
        public string AllowSpecialChargeCY { get; set; }
        public string AllowExaminationAutoChrges { get; set; }

        public double WeightAmount { get; set; }

        public long? MasterShippingAgentId { get; set; }

        public MasterShippingAgent MasterShippingAgent { get; set; }

        public List<GateIn> GateIns { get; set; }

        public List<GateOut> GateOuts { get; set; }

        public List<EmptyGateOutContainer> EmptyGateOutContainers { get; set; }
         
        public List<TellySheet> TellySheets { get; set; }

        public List<ShippingAgentTariff> ShippingAgentTariffs { get; set; }

        public List<User> Users{ get; set; }

    }
}
