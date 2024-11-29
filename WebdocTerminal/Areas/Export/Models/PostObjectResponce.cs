using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Export.Models
{
    public class PostObjectResponce
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public bool error { get; set; }
    }
}
