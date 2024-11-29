using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("CRSummaryHeadDetail")]
    public class CRSummaryHeadDetail : CommonProperties
    {
        public long CRSummaryHeadDetailId  { get; set; }
        public long SerialNo  { get; set; }
        public string Description  { get; set; }
        public string Caption  { get; set; }
        public string Category { get; set; }
        public string Query { get; set; }
    }
}
