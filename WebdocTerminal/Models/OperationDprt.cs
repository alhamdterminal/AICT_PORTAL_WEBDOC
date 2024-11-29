using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("OperationDprt")]
    public class OperationDprt : CommonProperties
    {
         
        public long OperationDprtId { get; set; }
        public string OperationDprtCode { get; set; }
        public string OperationDprtName { get; set; }
    }
}
