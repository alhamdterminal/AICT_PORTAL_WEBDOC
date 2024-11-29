using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class BrtItem : CommonProperties
    {
        public long BrtItemId { get; set; }
        public long NumberOfItems { get; set; }
        public string PackageLocation { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public long? BRTId { get; set; }
        public BRT BRT { get; set; }
    }
}
