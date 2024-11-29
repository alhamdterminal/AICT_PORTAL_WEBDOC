using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{

    [Table("EmptyContainerGatePass")]

    public class EmptyContainerGatePass : CommonProperties
    {
        public long EmptyContainerGatePassId { get; set; }
         
        public string ShiftedYard { get; set; }

        public string ContainerCondition { get; set; }

        public long ContainerIndexId { get; set; }
        public ContainerIndex ContainerIndex { get; set; }
          
        public DateTime? CreatedDate { get; set; }
    }
}
