using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExaminationTariffInformation")]
    public class ExaminationTariffInformation :CommonProperties
    {
        public long ExaminationTariffInformationId { get; set; }

         
        public long? ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }
        public long? GoodsHeadId { get; set; }
        public GoodsHead GoodsHead { get; set; } 
        public string ExaminationType { get; set; }
        public string IndexCargoType { get; set; } 
        public List<ExaminationTariffInformationTariff> ExaminationTariffInformationTariffs { get; set; }

    }
}
