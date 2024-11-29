using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("PreAlertVessel")]
    public class PreAlertVessel : CommonProperties
    {
        public long PreAlertVesselId { get; set; }
        public string PreAlertVesselName { get; set; }
    }
}
