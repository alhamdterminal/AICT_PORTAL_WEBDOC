using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("LabourWorkOrder")]
    public class LabourWorkOrder : CommonProperties
    {
        public long LabourWorkOrderId { get; set; }         
        //public int WorkOrderNumber { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public string Shift { get; set; }


        public long NumberOfLabour { get; set; }
        public string Reason { get; set; }

    }
}
