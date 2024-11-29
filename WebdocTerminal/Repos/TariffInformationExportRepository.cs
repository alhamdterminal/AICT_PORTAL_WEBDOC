using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class TariffInformationExportRepository : RepoBase<TariffInformationExport> , ITariffInformationExportRepository
    {
        public TariffInformationExportRepository(IUserResolveService userResolveService) :base(userResolveService)
        {

        }

        public List<ShippingAgentExport> GetShippingAgentFromPercentAgeTariff()
        {
            var data = (from tariffPercentage in Db.TariffPercentageExports
                        join shippingAgent in Db.ShippingAgentExports on tariffPercentage.ShippingAgentExportId equals shippingAgent.ShippingAgentExportId
                        select new ShippingAgentExport
                        {
                            ShippingAgentExportId = shippingAgent.ShippingAgentExportId,
                            ShippingAgentName = shippingAgent.ShippingAgentName
                        }).Distinct().ToList();

            return data;
        }

        public List<TariffInfoExportViewModel> GetTariffsByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId
                                                 , long? portAndTerminalId, string indexCargoType)
        {


            var tariffs = (from tariffinfo in Db.TariffInformationExports

                           join tariffTariffInofrmationTariff in Db.TariffInofrmationTariffExports on tariffinfo.TariffInformationExportId equals tariffTariffInofrmationTariff.TariffInformationExportId
                           join tariff in Db.ExportTariffs on tariffTariffInofrmationTariff.ExportTariffId equals tariff.ExportTariffId
                           join tariffHead in Db.TariffHeadExports on tariff.TariffHeadExportId equals tariffHead.TariffHeadExportId
 
                           where

                           (tariffinfo.ShippingAgentExportId == ShippingAgentId)
                            && (tariffinfo.ExportConsigneeId == ConsigneId)
                            && (tariffinfo.ClearingAgentExportId == ClearingAgentId)
                            && (tariffinfo.GoodsHeadExportId == GoodId)
                            && (tariffinfo.ShippingLineExportId == ShipperId)
                            && (tariffinfo.IndexCargoType == indexCargoType)
                            && (tariffinfo.PortAndTerminalExportId == portAndTerminalId)


                           //select tariff).Include(t => t.TariffHead).ToList();

                           select new TariffInfoExportViewModel
                           {
                               CBMRate = tariff.CBMRate,
                               DailyCharges = tariff.DailyCharges,
                               Description = tariffHead.Description,
                               DevededIndexAmount = tariff.DevededIndexAmount,
                               ImplementFrom = tariff.ImplementFrom,
                               ImplementTo = tariff.ImplementTo,
                               IsActive = tariff.IsActive,
                               IsCBMorRate = tariff.IsCBMorRate,
                               IsFixedRate = tariff.IsFixedRate,
                               IsDollerRate = tariff.IsDollerRate,
                               IsGeneral = tariff.IsGeneral,
                               TypeOfTariff = tariff.TypeOfTarifff,
                               Name = tariffHead.Name,
                               //portname = portandterminal.PortName, 
                               PerIndexRate = tariff.PerIndexRate,
                               PortAndTerminalExportId = tariff.PortAndTerminalExportId,
                               Rate20 = tariff.Rate20,
                               Rate40 = tariff.Rate40,
                               Rate45 = tariff.Rate45,
                               RoundCBMCode = tariff.RoundCBMCode,
                               TariffDate = tariff.TariffDate,
                               TariffHeadExportId = tariffHead.TariffHeadExportId,
                               TariffHeadExportType = tariffHead.TariffHeadExportType,
                               ExportTariffId = tariff.ExportTariffId,
                               TariffInofrmationTariffExportId = tariffTariffInofrmationTariff.TariffInofrmationTariffExportId,
                               WeightRate = tariff.WeightRate,
                               IsEnableDisabled = tariffinfo.IsEnableDisabled,
                               TariffCode = tariffinfo.TariffInformationExportId,
                               StorageFreeDays = tariffinfo.StorageFreeDays,
                               DGFreeDays = tariffinfo.DGFreeDays,
                               TypeOfImplementation = tariff.TypeOfImplementation,

                           }).ToList();


            return tariffs;






        }
    }
}
