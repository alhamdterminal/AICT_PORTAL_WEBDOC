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
    public class ShippingAgentTariffExportRepository : RepoBase<ShippingAgentTariffExport>, IShippingAgentTariffExportRepository
    {
        public ShippingAgentTariffExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<AgentTariffExportViewModel> GetTariffsByShippingAgentId(long ShippingAgentId)
        {
            if (ShippingAgentId > 0)
            {
                var tariffs = (from agent in Db.ShippingAgentExports
                               join agentTariff in Db.ShippingAgentTariffExports on agent.ShippingAgentExportId equals agentTariff.ShippingAgentExportId
                               join tariff in Db.TariffExports on agentTariff.TariffExportId equals tariff.TariffExportId
                               where agent.ShippingAgentExportId == ShippingAgentId
                               select new AgentTariffExportViewModel
                               {
                                   CBMMultiply = agentTariff.CBMMultiply,
                                   CBMMultiplyValue = agentTariff.CBMMultiplyValue,
                                   CBMRate = tariff.CBMRate,
                                   DailyCharges = tariff.DailyCharges,
                                   ImplementFrom = tariff.ImplementFrom,
                                   IsActive = tariff.IsActive,
                                   PerIndexRate = tariff.PerIndexRate,
                                   Rate20 = tariff.Rate20,
                                   Rate40 = tariff.Rate40,
                                   Rate45 = tariff.Rate45,
                                   RoundCBMCode = tariff.RoundCBMCode,
                                   CBMFixedRate = tariff.CBMFixedRate,
                                   ShippingAgentTariffExportId = agentTariff.ShippingAgentTariffExportId,
                                   TariffDate = tariff.TariffDate,
                                   TariffExportId = tariff.TariffExportId,
                                   TariffHeadExport = tariff.TariffHeadExport,
                                   TariffHeadExportId = tariff.TariffHeadExportId,
                                   WeightRate = tariff.WeightRate

                               }).Distinct().ToList();

                return tariffs;
            }

            return new List<AgentTariffExportViewModel>();
        }

        public List<TariffExport> GetUnassignedShippingAgentTAriffs(long ShippingAgentId)
        {
            if (ShippingAgentId > 0)
            {
                var tariffs = (from tariff in Db.TariffExports
                               from shippingAgentTariff in Db.ShippingAgentTariffExports.Where(c => c.TariffExportId == tariff.TariffExportId).DefaultIfEmpty()
                               where !Db.ShippingAgentTariffExports.Any(es => (es.TariffExportId == tariff.TariffExportId) && (es.ShippingAgentExportId == ShippingAgentId))
                               select tariff
                               ).Include(t => t.TariffHeadExport).Distinct().ToList();


                return tariffs;
            }

            return Db.TariffExports.Include(t => t.TariffHeadExport).ToList();
        }
    }
}
