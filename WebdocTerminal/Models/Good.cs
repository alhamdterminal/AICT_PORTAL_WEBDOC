using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{

    [Table("Good")]
    public class Good : CommonProperties
    {
        public long GoodId { get; set; }
        public string GoodsName { get; set; }
        public string GoodsCode { get; set; }
        public string GoodsDescription { get; set; }

        public long GoodsHeadId { get; set; }
        public GoodsHead GoodsHead { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
