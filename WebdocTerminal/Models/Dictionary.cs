using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("Dictionary")]
    public class Dictionary : CommonProperties
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string TRNumber { get; set; }

        public string GatePassNumber { get; set; }
        public string MonthYear { get; set; }
    }
}
