using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("SealIssue")]
    public class SealIssue : CommonProperties
    {
        public long SealIssueId { get; set; }   
        public string BatchNumber { get; set; }   
        public long SealNumber { get; set; }   
        public long Rate { get; set; }   
        public long CYContainerId { get; set; }   
        public CYContainer CYContainer { get; set; }   
    }
}
