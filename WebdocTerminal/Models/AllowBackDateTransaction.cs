using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("AllowBackDateTransaction")]
    public class AllowBackDateTransaction : CommonProperties
    {
        public long AllowBackDateTransactionId { get; set; }
        public string UserName { get; set; }
    }
}
