using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface ITariffRepository : IRepo<Tariff>
    {
        List<TariffInformationViewModel> GetAllTariffs();

        string getShippingAgentByContainerId(long containerId);

        string getShippingAgentByCyContainerId(long CyContainerId);

        string getShippingAgentByIgm(string igm);

    }
}
