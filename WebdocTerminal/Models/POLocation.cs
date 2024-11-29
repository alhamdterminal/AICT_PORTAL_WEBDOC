using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("POLocation")]
    public class POLocation : CommonProperties
    {
        public long POLocationId { get; set; }

        public string PONumber { get; set; }

        public string LoadingProgramNumber { get; set; }

        public string Location { get; set; }
    }
}
