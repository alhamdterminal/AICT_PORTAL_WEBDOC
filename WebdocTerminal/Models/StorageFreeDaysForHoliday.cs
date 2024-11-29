using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("StorageFreeDaysForHoliday")]
    public class StorageFreeDaysForHoliday : CommonProperties
    {
        public long StorageFreeDaysForHolidayId { get; set; }
        public string VirNumber { get; set; }
        public int IndexNumber { get; set; }
        public long? Freedays { get; set; }
    }
}
