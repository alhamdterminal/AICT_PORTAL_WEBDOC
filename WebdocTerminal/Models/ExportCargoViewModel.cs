using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class ExportCargoViewModel : CommonProperties
    {
        public string LpNo { get; set; }

        public long CargoId { get; set; }

        public string GD { get; set; }

        public string Vessel { get; set; }

        public string Voyage { get; set; }
    }

}
