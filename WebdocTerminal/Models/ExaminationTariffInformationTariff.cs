using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("ExaminationTariffInformationTariff")]
    public class ExaminationTariffInformationTariff : CommonProperties
    {

        public long ExaminationTariffInformationTariffId { get; set; }
        public long ExaminationTariffInformationId { get; set; }
        public long ExaminationTariffId { get; set; }
        public ExaminationTariff ExaminationTariff { get; set; }
        public ExaminationTariffInformation ExaminationTariffInformation { get; set; }

    }
}
