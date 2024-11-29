using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IShippingAgentTariffRepository : IRepo<ShippingAgentTariff>
    {
        List<Tariff> GetTariffsByShippingAgentId(long ShippingAgentId);

        List<Tariff> GetUnassignedShippingAgentTAriffs(long ShippingAgentId);
    }
}
