using System;

namespace WebdocTerminal.Models
{
    public class ErrorViewModel : CommonProperties
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}