using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("TransportCollectionTariff")]
    public class TransportCollectionTariff :CommonProperties
    {
        public long TransportCollectionTariffId { get; set; }
         
        public long TariffId { get; set; }
        public Tariff Tariff { get; set; }
        public long TransportCollectionId { get; set; }
        public TransportCollection TransportCollection { get; set; }

    }
}
