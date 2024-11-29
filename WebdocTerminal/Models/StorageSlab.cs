using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("StorageSlab")]
    public class StorageSlab : CommonProperties
    {
        public long StorageSlabId { get; set; }

        public int Rate20 { get; set; }

        public int Rate40 { get; set; }

        public int Rate45 { get; set; }

        public int Rate { get; set; }

        //AICT
        public int AICTRate20 { get; set; }

        public int AICTRate40 { get; set; }

        public int AICTRate45 { get; set; }

        public int AICTRate { get; set; }

        //FF
        public int FFRate20 { get; set; }

        public int FFRate40 { get; set; }

        public int FFRate45 { get; set; }

        public int FFRate { get; set; }


        public string Description { get; set; }

        public string NoOfDays { get; set; }

        public int FreeDays { get; set; } = 0;

        public long TariffId { get; set; }

        public Tariff Tariff { get; set; }
         
    }
}
