using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class TariffInformationViewModel : CommonProperties
    {
        public long TariffId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? ImplementFrom { get; set; }
    }
}
