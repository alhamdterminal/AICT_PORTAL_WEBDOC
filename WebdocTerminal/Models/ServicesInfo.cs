using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ServicesInfo")]
    public class ServicesInfo : CommonProperties
    {
        public long ServicesInfoId { get; set; }
        public string ServicesInfoCode { get; set; }
        public string ServicesInfoName { get; set; }
        public double Rate { get; set; }
        public long ServicesSectionId { get; set; }
        public ServicesSection ServicesSection { get; set; }
        public long ServicesTypeId { get; set; }
        public ServicesType ServicesType { get; set; }
        public long NatureOfPaymentId { get; set; }
        public NatureOfPayment NatureOfPayment { get; set; }

    }
}
