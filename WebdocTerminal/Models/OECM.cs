﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Models
{
    public class OECM : CommonProperties
    {
        public long OECMId { get; set; }

        public int? TotalRecords { get; set; }

        public int? RecordSequence { get; set; }

        public string VIRNumber { get; set; }

        public string BLNumber { get; set; }

        public string IndexNo { get; set; }

        public string GDNumber { get; set; }

        public string HandlingCode { get; set; }

        public string ServiceStatus { get; set; }

        public string Remarks { get; set; }

        public DateTime? Performed { get; set; }

        public string MessageFrom { get; set; }

        public string MessageTo { get; set; }

        public string FileName { get; set; }
    }
}