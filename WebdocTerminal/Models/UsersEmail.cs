using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    [Table("UsersEmail")]
    public class UsersEmail : CommonProperties
    {
        public long UsersEmailId { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
    }
}
