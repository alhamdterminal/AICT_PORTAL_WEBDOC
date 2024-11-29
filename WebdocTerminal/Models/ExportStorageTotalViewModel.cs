using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class ExportStorageTotalViewModel : CommonProperties
    {
        public int StorageDays { get; set; }

        public double StorageTotal { get; set; }

        public long StorageTariffId { get; set; }
    }
}
