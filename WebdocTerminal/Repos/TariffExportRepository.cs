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
    public class TariffExportRepository : RepoBase<TariffExport> , ITariffExportRepository
    {
        public TariffExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<TariffChargesExportViewModel> GetExportTariffsByPerametersId(long ShippingAgentId)
        {

            var data = (from shippingAgentTariff in Db.ShippingAgentTariffExports
                        join tariff in Db.TariffExports on shippingAgentTariff.TariffExportId equals tariff.TariffExportId
                        from tariffHead in Db.TariffHeadExports.Where(x => x.TariffHeadExportId == tariff.TariffHeadExportId).DefaultIfEmpty()
                        where
                        shippingAgentTariff.ShippingAgentExportId == ShippingAgentId
                        select new TariffChargesExportViewModel
                        {
                            NatureOfTariffId = tariff.NatureOfTariffId,
                            TariffType = tariff.TariffType,
                            CBMRate = tariff.CBMRate,
                            DailyCharges = tariff.DailyCharges,
                            ImplementFrom = tariff.ImplementFrom,
                            IsActive = tariff.IsActive,
                            IsCBMorRate = tariff.IsCBMorRate,
                            PerIndexRate = tariff.PerIndexRate,
                            Rate20 = tariff.Rate20,
                            Rate40 = tariff.Rate40,
                            Rate45 = tariff.Rate45,
                            TariffDate = tariff.TariffDate,
                            TariffHeadDescription = tariffHead != null ? tariffHead.Description : "",
                            TariffHeadName = tariffHead != null ? tariffHead.Name : "",
                            TariffExportId = tariff.TariffExportId,
                            TariffHeadExportId = tariffHead.TariffHeadExportId,
                            WeightRate = tariff.WeightRate,
                            CBMFixedRate = tariff.CBMFixedRate,
                            CBMMultiplyValue = tariff.CBMMultiplyValue



                        }).ToList();


            return data;
        }


        public string GetShippingAgentBygd(string gdnumber)
        {

            var asd = Db.LoadingPrograms.FromSql($"select * from LoadingProgram   where GDNumber = {gdnumber } and IsDeleted = 0").LastOrDefault();


            if (asd != null)
            {
                var resdata = Db.ShippingAgentExports.FromSql($"select * from ShippingAgentExport   where ShippingAgentExportId = {asd.ShippingAgentExportId } and IsDeleted = 0").LastOrDefault();

                if (resdata != null)
                {
                    return resdata.ShippingAgentName;
                }
                else
                {
                    return "";
                }

            }
            else
            {
                return "";
            }

            //var res  = (from enteringCargo in Db.EnteringCargos
            //            join loadingProgram in Db.LoadingPrograms on enteringCargo.LoadingProgramNumber equals loadingProgram.LoadingProgramNumber
            //            join shippingAgentExport in Db.ShippingAgentExports on loadingProgram.ShippingAgentExportId equals shippingAgentExport.ShippingAgentExportId
            //            where enteringCargo.GDNumber == gdnumber
            //            select shippingAgentExport.ShippingAgentName).FirstOrDefault();

            //if (res != null)
            //{
            //    return res;
            //}

            return "";
        }

        public string GetTariffTypeBygd(string gdnumber)
        {


            var asd = Db.LoadingPrograms.FromSql($"select * from LoadingProgram   where GDNumber = {gdnumber } and IsDeleted = 0").LastOrDefault();

            if (asd != null)
            {
                return asd.NatureOfTariffId.ToString();
            }
            else
            {
                return "";
            }

            //var asd = Db.EnteringCargos.FromSql($"select * from EnteringCargo   where GDNumber = {gdnumber } and IsDeleted = 0").LastOrDefault();
            //if (asd != null)
            //{
            //    var lp = Db.LoadingPrograms.FromSql($"select * from LoadingProgram where LoadingProgramNumber = {asd.LoadingProgramNumber}  and IsDeleted = 0").LastOrDefault();
            //    if (lp != null && lp.NatureOfTariffId != null)
            //    {

            //        return  Convert.ToString( lp.NatureOfTariffId);

            //    }
            //}

            //var agent = (from enteringCargo in Db.EnteringCargos
            //             where enteringCargo.GDNumber == gdnumber
            //             select enteringCargo.TariffType).FirstOrDefault();
            //if (agent != null)
            //{
            //    return agent;
            //}

            return "";
        }


    

        public List<EnteringCargo> GetUnDeliveredGDS()
        {
            var res = (from enteringCargo in Db.EnteringCargos
                       where
                       !Db.ExportContainerItems.Any(x => x.GDNumber == enteringCargo.GDNumber && x.IsGateOut == true)
                       select enteringCargo).ToList();
            return res;
        }


    }
}
