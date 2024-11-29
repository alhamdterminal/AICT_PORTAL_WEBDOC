using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class ExaminationTariffInfoViewModel
    {
        public long ExaminationTariffId { get; set; }
          
        public int Rate20 { get; set; }

        public int Rate40 { get; set; }

        public int Rate45 { get; set; }

        public int CBMRate { get; set; }

        public int WeightRate { get; set; }

        public int PerIndexRate { get; set; }
        public int DevededIndexAmount { get; set; }
         
        public long? ExaminationTariffInformationTariffId { get; set; }

        public long? ExaminationChargeId { get; set; }

        public string ExaminationChargeName { get; set; }

  
    }
}
