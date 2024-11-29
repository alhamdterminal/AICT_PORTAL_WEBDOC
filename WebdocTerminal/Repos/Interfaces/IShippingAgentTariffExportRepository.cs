using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IShippingAgentTariffExportRepository : IRepo<ShippingAgentTariffExport>
    {
        List<AgentTariffExportViewModel> GetTariffsByShippingAgentId(long ShippingAgentId);

        List<TariffExport> GetUnassignedShippingAgentTAriffs(long ShippingAgentId);
    }
}
