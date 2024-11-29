using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class TariffRepository : RepoBase<Tariff>, ITariffRepository
    {
        public TariffRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<TariffInformationViewModel> GetAllTariffs()
        {
            return GetAll().Select(t => new TariffInformationViewModel
            {
                TariffId = t.TariffId,
                Description = t.TariffHead.Description,
                ImplementFrom = t.ImplementFrom,
                Name = t.TariffHead.Name
            }).ToList();
        }

        public string getShippingAgentByContainerId(long containerId)
        {
            var agent = (from Container in Db.ContainerIndices
                         join ShipingAgent in Db.ShippingAgents on Container.ShippingAgentId equals ShipingAgent.ShippingAgentId
                         where Container.ContainerIndexId == containerId
                         select ShipingAgent.Name).FirstOrDefault();
            if(agent != null)
            {
                return agent;
            }

            return "";
        }

        public string getShippingAgentByCyContainerId(long CyContainerId)
        {
            var agent = (from CYContainer in Db.CYContainers
                         join ShipingAgent in Db.ShippingAgents on CYContainer.ShippingAgentId equals ShipingAgent.ShippingAgentId
                         where CYContainer.CYContainerId == CyContainerId
                         select ShipingAgent.Name).FirstOrDefault();
            if (agent != null)
            {
                return agent;
            }

            return "";
        }

        public string getShippingAgentByIgm(string igm)
        {
            var agent = (from CYContainer in Db.CYContainers
                         join ShipingAgent in Db.ShippingAgents on CYContainer.ShippingAgentId equals ShipingAgent.ShippingAgentId
                         where CYContainer.VIRNo == igm
                         select ShipingAgent.Name).FirstOrDefault();
            if (agent != null)
            {
                return agent;
            }

            return "";
        }

    }
}
