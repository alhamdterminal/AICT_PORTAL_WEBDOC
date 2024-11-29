using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class ExaminationTariffInformationRepository : RepoBase<ExaminationTariffInformation>, IExaminationTariffInformationRepository
    {

        public ExaminationTariffInformationRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }



        public List<ExaminationTariffInfoViewModel> GetExaminationTariffsByPerametersId(long? ShippingAgentId, long? GoodId, string indexCargoType, string examinationType)
        {


            var tariffs = (from examinationTariffInformation in Db.ExaminationTariffInformation
                           join examinationTariffInformationTariff in Db.ExaminationTariffInformationTariffs on examinationTariffInformation.ExaminationTariffInformationId equals examinationTariffInformationTariff.ExaminationTariffInformationId
                           join examinationTariff in Db.ExaminationTariffs on examinationTariffInformationTariff.ExaminationTariffId equals examinationTariff.ExaminationTariffId
                           join examinationCharge in Db.ExaminationCharges on examinationTariff.ExaminationChargeId equals examinationCharge.ExaminationChargeId
                           where
                           (examinationTariffInformation.ShippingAgentId == ShippingAgentId)
                            && (examinationTariffInformation.GoodsHeadId == GoodId)
                            && (examinationTariffInformation.IndexCargoType == indexCargoType)
                            && (examinationTariffInformation.ExaminationType == examinationType)

                           select new ExaminationTariffInfoViewModel
                           {
                               ExaminationTariffId = examinationTariff.ExaminationTariffId,
                               ExaminationChargeName = examinationCharge.ExaminationChargeName,
                               Rate20 = examinationTariff.Rate20,
                               Rate40 = examinationTariff.Rate40,
                               Rate45 = examinationTariff.Rate45,
                               CBMRate = examinationTariff.CBMRate,
                               WeightRate = examinationTariff.WeightRate,
                               DevededIndexAmount = examinationTariff.DevededIndexAmount,
                               PerIndexRate = examinationTariff.PerIndexRate,
                               ExaminationChargeId = examinationCharge.ExaminationChargeId,
                               ExaminationTariffInformationTariffId = examinationTariffInformationTariff.ExaminationTariffInformationTariffId,

                           }).ToList();

            return tariffs;
             
        }
    }
}
