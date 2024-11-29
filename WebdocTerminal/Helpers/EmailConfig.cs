using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Helpers
{
    public class EmailConfig
    {
        public string MailServer { get; set; }

        public string MailPort { get; set; }

        public string SenderName { get; set; }

        public string Sender { get; set; }

        public string Password { get; set; }
    }
}
