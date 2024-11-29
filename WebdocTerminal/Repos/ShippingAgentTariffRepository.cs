using Microsoft.EntityFrameworkCore;
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
    public class ShippingAgentTariffRepository : RepoBase<ShippingAgentTariff>, IShippingAgentTariffRepository
    {
        public ShippingAgentTariffRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<Tariff> GetTariffsByShippingAgentId(long ShippingAgentId)
        {
            if (ShippingAgentId > 0)
            {
                var tariffs = (from agent in Db.ShippingAgents
                               join agentTariff in Db.ShippingAgentTariffs on agent.ShippingAgentId equals agentTariff.ShippingAgentId
                               join tariff in Db.Tariffs on agentTariff.TariffId equals tariff.TariffId
                               where agent.ShippingAgentId == ShippingAgentId
                               select tariff).Include(t=>t.TariffHead).Distinct().ToList();

                return tariffs;
            }

            return new List<Tariff>();
        }

        public List<Tariff> GetUnassignedShippingAgentTAriffs(long ShippingAgentId)
        {
            if (ShippingAgentId > 0)
            {
                var tariffs = (from tariff in Db.Tariffs
                               from shippingAgentTariff in Db.ShippingAgentTariffs.Where(c => c.TariffId == tariff.TariffId).DefaultIfEmpty()
                               where !Db.ShippingAgentTariffs.Any(es => (es.TariffId == tariff.TariffId) && (es.ShippingAgentId == ShippingAgentId))
                               select tariff
                               ).Include(t => t.TariffHead).Distinct().ToList();


                return tariffs;
            }

            return Db.Tariffs.Include(t=>t.TariffHead).ToList();
        }

    }
}
