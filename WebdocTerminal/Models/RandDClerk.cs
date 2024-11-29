using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("RandDClerk")]
    public class RandDClerk : CommonProperties
    {
        public long RandDClerkId { get; set; }
        public string RandDClerkName { get; set; }
    }
}
