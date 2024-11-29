using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface ITerminalAndFFShareRateRepository : IRepo<TerminalAndFFShareRate>
    {
       // TerminalAndFFShareRate GetShareRateByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? ContainerType, long? portAndTerminalId ,  string indexCargoType);
        TerminalAndFFShareRate GetShareRateByPerametersId(long? ShippingAgentId,  long? GoodId,  string indexCargoType);
    }
}
