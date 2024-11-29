using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebdocTerminal.Areas.Import.Models
{
    public class DashboardItemViewModel
    {
        public long GateInContainerCFS { get; set; }

        public long GroundedCFS { get; set; }

        public long DestuffedCFS { get; set; }
        public long DestuffedIndexCFS { get; set; }

        public long ExaminationMarkCFS { get; set; }

        public long OnHoldCFS { get; set; }

        public long GateOutCFS { get; set; }

        public long GateInContainerCY { get; set; }

        public long GroundedCY { get; set; }

        public long GroundingAwaitedCFS { get; set; }

        public long GroundingAwaitedCY { get; set; }

        public long DestuffedCY { get; set; }

        public long ExaminationMarkCY { get; set; }

        public long OnHoldCY { get; set; }

        public long GateOutCY { get; set; }

        public long WeighmentCY { get; set; }

        public long UpCommingContainer { get; set; }

        public long UpCommingContainerCY { get; set; }

        public long GateinIndex { get; set; }
        
        public long GateOutCountainerIndex { get; set; }
    }
}
