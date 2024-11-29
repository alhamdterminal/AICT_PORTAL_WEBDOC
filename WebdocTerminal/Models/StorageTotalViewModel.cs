using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class StorageTotalViewModel : CommonProperties
    {
        public int StorageDays { get; set; }

        public double StorageTotal { get; set; }

        public int AddtionalFreeDays { get; set; }
        public int TotalFreeDays { get; set; }
        public int PartContainers { get; set; }
        public double TotalBalanceCargo { get; set; }
        public string StorageFreeDaysType { get; set; }
        public string StorageApplicableon { get; set; }
        public long StorageSharePercent { get; set; }
        public double AictPerBoxRate { get; set; }

    }
}
