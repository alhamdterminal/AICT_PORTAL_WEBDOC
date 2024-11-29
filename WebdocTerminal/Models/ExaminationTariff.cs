using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExaminationTariff")]
    public class ExaminationTariff : CommonProperties
    { 
        public long ExaminationTariffId { get; set; }

        public int Rate20 { get; set; }

        public int Rate40 { get; set; }

        public int Rate45 { get; set; }

        public int CBMRate { get; set; }

        public int WeightRate { get; set; }

        public int PerIndexRate { get; set; }
        public int DevededIndexAmount { get; set; }
          
        public long? ExaminationChargeId { get; set; }

        public ExaminationCharge ExaminationCharge { get; set; }
 
        public List<TariffInofrmationTariff> TariffInofrmationTariffs { get; set; }



    }
}
