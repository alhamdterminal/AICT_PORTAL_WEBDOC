using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmptyContainerTariff")]
    public class EmptyContainerTariff : CommonProperties
    {

        public EmptyContainerTariff()
        {
            EmptyContainerStorageSlabs = new List<EmptyContainerStorageSlab>();
        }

        public long EmptyContainerTariffId { get; set; }
        public string EmptyContainerTariffName { get; set; }
        public int Rate20 { get; set; }

        public int Rate40 { get; set; }

        public int Rate45 { get; set; }

        public long ShippingAgentId { get; set; }
        public ShippingAgent ShippingAgent { get; set; }

        public List<EmptyContainerStorageSlab> EmptyContainerStorageSlabs { get; set; }

    }
}
