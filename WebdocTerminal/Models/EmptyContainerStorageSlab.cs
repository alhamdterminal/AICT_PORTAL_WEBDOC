using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("EmptyContainerStorageSlab")]
    public class EmptyContainerStorageSlab : CommonProperties
    {
        public long EmptyContainerStorageSlabId { get; set; }

        public int Rate20 { get; set; }

        public int Rate40 { get; set; }

        public int Rate45 { get; set; }

        public string Description { get; set; }

        public string NoOfDays { get; set; }

        public long EmptyContainerTariffId { get; set; }

        public EmptyContainerTariff EmptyContainerTariff { get; set; }
    }
}
