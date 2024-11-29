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
    public class GDTariffRepository : RepoBase<GDTariff> , IGDTariffRepository
    {
        public GDTariffRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public List<TariffExport> GetUnassignedExportContainerGDTariffs(long EnteringCargoId)
        {
            if (EnteringCargoId > 0)
            {
                var tariffs = (from tariff in Db.TariffExports
                               from GDTariff in Db.GDTariffs.Where(c => c.TariffExportId == tariff.TariffExportId).DefaultIfEmpty()
                               where !Db.GDTariffs.Any(es => (es.TariffExportId == tariff.TariffExportId) && (es.EnteringCargoId == EnteringCargoId))
                               select tariff
                               ).Include(t => t.TariffHeadExport).Distinct().ToList();


                return tariffs;
            }

            return Db.TariffExports.Include(s => s.TariffHeadExport).ToList();
        }

        public List<TariffViewModel> GetCargoTariffs(long EnteringCargoId)
        {
            var InvoiceData = false;

            if (EnteringCargoId >= 0)
            {

                var invoice = (from Invoice in Db.InvoiceExports
                               where Invoice.EnteringCargoId == EnteringCargoId && Invoice.IsCancelled == false
                               select Invoice).FirstOrDefault();
                if (invoice != null)
                {
                    
                    InvoiceData = true;
                }
            }


            var tariffs = (from tariff in Db.TariffExports
                           join GDTariff in Db.GDTariffs on tariff.TariffExportId equals GDTariff.TariffExportId
                           join enteringCargo in Db.EnteringCargos on GDTariff.EnteringCargoId equals enteringCargo.EnteringCargoId
                           //from invoice in Db.Invoices.Where(c => c.CYContainerId == CYContainer.CYContainerId).DefaultIfEmpty()
                           where enteringCargo.EnteringCargoId == EnteringCargoId
                           select new TariffViewModel
                           {
                               CBMRate = tariff.CBMRate,
                               DailyCharges = tariff.DailyCharges,
                               ImplementFrom = tariff.ImplementFrom,
                               IsActive = tariff.IsActive,
                               PerIndexRate = tariff.PerIndexRate,
                               Rate20 = tariff.Rate20,
                               Rate40 = tariff.Rate40,
                               Rate45 = tariff.Rate45,
                               RoundCBMCode = tariff.RoundCBMCode,
                               TariffDate = tariff.TariffDate,
                               TariffHeadExport = tariff.TariffHeadExport,
                               TariffHeadExportId = tariff.TariffHeadExportId,
                               TariffExportId = tariff.TariffExportId,
                               WeightRate = tariff.WeightRate,
                               InvoiceCreated = InvoiceData

                           }).Include(t => t.TariffHead).ToList();

            var shippingAgent = (
                     from EnteringCargo in Db.EnteringCargos
                     join LoadingProgram in Db.LoadingPrograms on EnteringCargo.LoadingProgramNumber equals LoadingProgram.LoadingProgramNumber
                     where EnteringCargo.EnteringCargoId == EnteringCargoId
                     select LoadingProgram.ShippingAgentExport).FirstOrDefault();

            if (shippingAgent != null)
            {
                var agentTariffs = (from agent in Db.ShippingAgentExports
                                    join agentTariff in Db.ShippingAgentTariffExports on agent.ShippingAgentExportId equals agentTariff.ShippingAgentExportId
                                    join tariff in Db.TariffExports on agentTariff.TariffExportId equals tariff.TariffExportId
                                    where agent.ShippingAgentExportId == shippingAgent.ShippingAgentExportId
                                    select new TariffViewModel
                                    {
                                        CBMRate = tariff.CBMRate,
                                        DailyCharges = tariff.DailyCharges,
                                        ImplementFrom = tariff.ImplementFrom,
                                        IsActive = tariff.IsActive,
                                        PerIndexRate = tariff.PerIndexRate,
                                        Rate20 = tariff.Rate20,
                                        Rate40 = tariff.Rate40,
                                        Rate45 = tariff.Rate45,
                                        RoundCBMCode = tariff.RoundCBMCode,
                                        TariffDate = tariff.TariffDate,
                                        TariffHeadExport = tariff.TariffHeadExport,
                                        TariffHeadExportId = tariff.TariffHeadExportId,
                                        TariffExportId = tariff.TariffExportId,
                                        WeightRate = tariff.WeightRate,
                                        InvoiceCreated = InvoiceData

                                    }).Include(t => t.TariffHead).Distinct().ToList();

                tariffs.AddRange(agentTariffs);
            }

            return tariffs;

        }
    }
}
