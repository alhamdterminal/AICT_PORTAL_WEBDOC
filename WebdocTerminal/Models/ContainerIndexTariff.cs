using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ContainerIndexTariff")]
    public class ContainerIndexTariff : CommonProperties
    {
        public long ContainerIndexTariffId { get; set; }

        public long? ContainerIndexId { get; set; }

        public long TariffId { get; set; }

        public ContainerIndex ContainerIndex { get; set; }

        public Tariff Tariff { get; set; }

        public long? CYContainerId { get; set; }

        public CYContainer CYContainer { get; set; }
 


    }
}
