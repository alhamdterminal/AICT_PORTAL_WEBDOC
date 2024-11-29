using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
  public  interface IPortChargeRepository : IRepo<PortCharge>
    {
        IEnumerable<PortChargesViewModel> GetPortChargesDetail(string virno, string containerno, string indexno, string type);
        IEnumerable<PortChargesViewModel> GetPortChargesDetailContainerWise(string virno, string containerno  ,string indexno , string type);
        IEnumerable<PortChargesRatesViewModel> GetPortChargesRates(string virno, string containerno, string indexno, string type, long? agent, long? goodhead, DateTime? fromdate, DateTime? todate, string status);
        List<ContainerIndex> GetPortchargesForVechile();
        PortCharge GetPortChargesByIgmIndexContainer(string virno, long indexno, string containerno);
        List<ContainerIndex> GetAutoUpdatePortcharges();
    }
}
