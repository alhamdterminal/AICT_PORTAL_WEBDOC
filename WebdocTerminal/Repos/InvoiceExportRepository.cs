using Microsoft.EntityFrameworkCore;
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
    public class InvoiceExportRepository : RepoBase<InvoiceExport>, IInvoiceExportRepository
    {
        public InvoiceExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public InvoiceViewModel GetExportInvoice(long EnteringCargoId)
        {
            var invoice = (from lp in Db.LoadingPrograms
                           //join Shipper in Db.Shippers on lp.ShipperId equals Shipper.ShipperId
                           join ClearingAgent in Db.ClearingAgentExports on lp.ClearingAgentExportId equals ClearingAgent.ClearingAgentExportId
                           join agent in Db.ShippingAgentExports on lp.ShippingAgentExportId equals agent.ShippingAgentExportId
                           join cargo in Db.EnteringCargos on lp.LoadingProgramNumber equals cargo.LoadingProgramNumber
                           join gatein in Db.GateInExports on cargo.GDNumber.Trim().ToUpper() equals gatein.GDNumber.Trim().ToUpper()
                           from containerItems in Db.ExportContainerItems.Where(c => c.GDNumber.Trim().ToUpper() == cargo.GDNumber.Trim().ToUpper()).DefaultIfEmpty()
                           from container in Db.ExportContainers.Where(c => c.ExportContainerId == containerItems.ExportContainerId).DefaultIfEmpty()
                           where cargo.EnteringCargoId == EnteringCargoId
                           select new InvoiceViewModel
                           {
                               EnteringCargoId = cargo.EnteringCargoId,
                               NoOfPackages = cargo.NumberOfPackages,
                               PackageType = (from lpd in Db.LoadingProgramDetails where lpd.LoadingProgramId == lp.LoadingProgramId select lpd).Select(x => x.PackageType).FirstOrDefault() ?? "",
                               DishargePort = (from cargoInfo in Db.Cargos where cargoInfo.GDNumber.Trim().ToUpper() == cargo.GDNumber.Trim().ToUpper() select cargoInfo).Select(x => x.DishargePort).FirstOrDefault() ?? "",
                               GateInDate = gatein.GateInDate,
                               CBM = Convert.ToDouble((from cargoInfo in Db.Cargos where cargoInfo.GDNumber.Trim().ToUpper() == cargo.GDNumber.Trim().ToUpper() select cargoInfo).Sum(s => s.CBM)),
                               Weight = (from cargoInfo in Db.Cargos where cargoInfo.GDNumber.Trim().ToUpper() == cargo.GDNumber.Trim().ToUpper() select cargoInfo).Sum(s => s.Weight) ?? 0,
                               //Consignee = Shipper.ShipperName,
                               ClearingAgentExportId = ClearingAgent.ClearingAgentExportId,
                               StorageDays = Convert.ToInt32((DateTime.Now - gatein.GateInDate).Value.TotalDays),
                               ContainerNo = container != null ? container.ContainerNumber : "",
                               ContainerSize = container != null ? container.ContainerSize : "",
                               ShippingAgnet = agent.ShippingAgentName

                           }).FirstOrDefault();

            return invoice;
        }


        public List<InvoiceItemViewModel> GetExportTariffList(string gdnumber, double Weight, double CBM)
        {
            var totalItems = new List<InvoiceItemViewModel>();

            var gd = Db.EnteringCargos.FromSql($"select * from EnteringCargo where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();

            
            if (gd == null)
            {
                return totalItems;
            }

            var opiasers = Db.OPIAs.FromSql($"select * from OPIAs where GDNumber = {gd.GDNumber}  and IsDeleted = 0").LastOrDefault();


            if (opiasers == null)
            {
                return totalItems;
            }

            var loadingprogrm = Db.LoadingPrograms.FromSql($"select * from LoadingProgram where GDNumber = {gd.GDNumber}  and IsDeleted = 0").LastOrDefault();




            //if (gd != null || (Weight > 0 && CBM > 0))
            if (gd != null )
            {
                var calculateCBM = CBM > 0 ? CBM  : 0;

                var calculateWeight = Weight > 0 ? Weight : 0;

                var weightOrCBM = WeightGreaterThenCBM(calculateWeight, calculateCBM);

                var weightConv = (Convert.ToDouble(calculateWeight) / 1000.0);
                 
                if (loadingprogrm != null )
                {

                    var balancepackages = opiasers.NoOfPackages -  Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where GDNumber = {loadingprogrm.GDNumber}   and IsDeleted = 0 and Ischecked = 1 and IsSubmitted = 1").Select(l => l.NumberOfPackages).DefaultIfEmpty(0).Sum();
                     
 
                    if (balancepackages == 0 && loadingprogrm.GDAssociateStatus == true)
                    {
                        if (loadingprogrm.GoodsHeadExportId != null && loadingprogrm.ExportConsigneeId != null && loadingprogrm.CargoType == "FCL")
                        {
                            var resdata = Db.TariffInformationExports.FromSql($"select * from TariffInformationExport where GoodsHeadExportId = {loadingprogrm.GoodsHeadExportId} and ExportConsigneeId = {loadingprogrm.ExportConsigneeId} and IndexCargoType = {loadingprogrm.CargoType}  and IsDeleted = 0").FirstOrDefault();


                            if(resdata != null)
                            {
                                var itemslist = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where GDNumber = {loadingprogrm.GDNumber} and Ischecked = 1 and IsSubmitted = 1     and IsDeleted = 0").ToList();

                                foreach (var item in itemslist)
                                {
                                    var itemres = Db.ExportContainers.FromSql($"select * from ExportContainers where ExportContainerId = {item.ExportContainerId}   and IsDeleted = 0").LastOrDefault();
                                     
                                    var CFSLCLtariffInfos = (from tariffInofrmationTariff in Db.TariffInofrmationTariffExports
                                                             from tariffInfo in Db.TariffInformationExports.Where(x => x.TariffInformationExportId == resdata.TariffInformationExportId).DefaultIfEmpty()
                                                             join tariff in Db.ExportTariffs on tariffInofrmationTariff.ExportTariffId equals tariff.ExportTariffId
                                                             where

                                                             tariffInofrmationTariff.TariffInformationExportId == resdata.TariffInformationExportId
                                                             && tariff.IsGeneral == false
                                                             && (tariff.Rate20 > 0 || tariff.Rate40 > 0 || tariff.Rate45 > 0)
                                                             && (tariff.PortAndTerminalExportId != null ? tariff.PortAndTerminalExportId == loadingprogrm.PortAndTerminalExportId : tariff.PortAndTerminalExportId == null)
                                                              
                                                             select new InvoiceItemViewModel
                                                             {
                                                                 NatureOfTariff = "PerBox",
                                                                 Description = tariff.TariffHeadExport.Name,
                                                                 Amount = itemres.ContainerSize == "20" ? tariff.Rate20 : itemres.ContainerSize == "40" ? tariff.Rate40 : itemres.ContainerSize == "45" ? tariff.Rate45 : 0,
                                                                 TariffId = tariff.ExportTariffId,
                                                                 TariffPercentId = tariffInfo.PercentAgeShippingAgentExportId,
                                                                 ExportTariffId = tariff.ExportTariffId,
                                                                 Category = "PerBox",
                                                                 Type = "Tariff"

                                                             }).Distinct().ToList();
                                     
                                    totalItems.AddRange(CFSLCLtariffInfos);
                                } 
                                
                            }

                            if (resdata != null)
                            {

                                var forcbmweightrate = (from tariffInofrmationTariff in Db.TariffInofrmationTariffExports
                                                         from tariffInfo in Db.TariffInformationExports.Where(x => x.TariffInformationExportId == resdata.TariffInformationExportId).DefaultIfEmpty()
                                                         join tariff in Db.ExportTariffs on tariffInofrmationTariff.ExportTariffId equals tariff.ExportTariffId
                                                         where

                                                         tariffInofrmationTariff.TariffInformationExportId == resdata.TariffInformationExportId
                                                         && tariff.IsGeneral == false
                                                         && (tariff.PortAndTerminalExportId != null ? tariff.PortAndTerminalExportId == loadingprogrm.PortAndTerminalExportId : tariff.PortAndTerminalExportId == null)
                                                         && (tariff.CBMRate > 0 || tariff.WeightRate > 0)
                                                          
                                                         select new InvoiceItemViewModel
                                                         {
                                                             NatureOfTariff = "CBM",
                                                             Description = tariff.TariffHeadExport.Name,
                                                             Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                                                             TariffId = tariff.ExportTariffId,
                                                             TariffPercentId = tariffInfo.PercentAgeShippingAgentExportId,
                                                             ExportTariffId = tariff.ExportTariffId,
                                                             Category = "CBM",
                                                             Type = "Tariff"

                                                         }).Distinct().ToList();

                                totalItems.AddRange(forcbmweightrate);
                            }

                            if (resdata != null)
                            {

                                var forindexrate = (from tariffInofrmationTariff in Db.TariffInofrmationTariffExports
                                                        from tariffInfo in Db.TariffInformationExports.Where(x => x.TariffInformationExportId == resdata.TariffInformationExportId).DefaultIfEmpty()
                                                        join tariff in Db.ExportTariffs on tariffInofrmationTariff.ExportTariffId equals tariff.ExportTariffId
                                                        where

                                                        tariffInofrmationTariff.TariffInformationExportId == resdata.TariffInformationExportId
                                                        && tariff.IsGeneral == false
                                                        && (tariff.PortAndTerminalExportId != null ? tariff.PortAndTerminalExportId == loadingprogrm.PortAndTerminalExportId : tariff.PortAndTerminalExportId == null)
                                                        && (tariff.PerIndexRate > 0 )

                                                        select new InvoiceItemViewModel
                                                        {
                                                            NatureOfTariff = "Index",
                                                            Description = tariff.TariffHeadExport.Name,
                                                            Amount = tariff.PerIndexRate,
                                                            TariffId = tariff.ExportTariffId,
                                                            TariffPercentId = tariffInfo.PercentAgeShippingAgentExportId,
                                                            ExportTariffId = tariff.ExportTariffId,
                                                            Category = "Index",
                                                            Type = "Tariff"

                                                        }).Distinct().ToList();

                                totalItems.AddRange(forindexrate);
                            }

                        }

                    }



                } 
            }

            totalItems.RemoveAll(c => c.Amount <= 0);

            return totalItems;
        }

        public List<InvoiceItemViewModel> GetExportCargoCBMTotal(long EnteringCargoId, int Weight, double CBM)
        {
            var totalItems = new List<InvoiceItemViewModel>();

            var gd = Db.EnteringCargos.FromSql($"select * from EnteringCargo where EnteringCargoId = {EnteringCargoId}  and IsDeleted = 0").LastOrDefault();

            if (gd == null)
            {
                return totalItems;
            }
            var loadingprogrm = Db.LoadingPrograms.FromSql($"select * from LoadingProgram where LoadingProgramNumber = {gd.LoadingProgramNumber}  and IsDeleted = 0").LastOrDefault();


            if (gd != null || (Weight > 0 && CBM > 0))
            {
                var calculateCBM = CBM > 0 ? Convert.ToInt32(Math.Ceiling(CBM)) : 0;

                var calculateWeight = Weight > 0 ? Weight : 0;

                var weightOrCBM = WeightGreaterThenCBM(calculateWeight, calculateCBM);

                var weightConv = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(calculateWeight) / 1000.0)));

                if (loadingprogrm != null && loadingprogrm.ShippingAgentExportId != null)
                {

                    var shippingAgentdata = Db.ShippingAgentExports.FromSql($"select * from ShippingAgentExport where ShippingAgentExportId = {loadingprogrm.ShippingAgentExportId}  and IsDeleted = 0").LastOrDefault();

                    long? restype = 0;
                    //if (loadingprogrm.NatureOfTariffId != null)
                    //{
                    //    restype = loadingprogrm.NatureOfTariffId;

                    //}
                    //else
                    //{
                    //    restype = shippingAgentdata.NatureOfTariffId;
                    //}

                    var agentitems = (from tariff in Db.TariffExports
                                      join agentTariff in Db.ShippingAgentTariffExports on tariff.TariffExportId equals agentTariff.TariffExportId
                                      join loadingProgram in Db.LoadingPrograms on agentTariff.ShippingAgentExportId equals loadingProgram.ShippingAgentExportId
                                      where
                                      loadingProgram.LoadingProgramId == loadingprogrm.LoadingProgramId
                                       && tariff.NatureOfTariffId == restype
                                       && (tariff.CBMRate > 0 || tariff.WeightRate > 0)
                                      select new InvoiceItemViewModel
                                      {
                                          NatureOfTariff = "CBM",
                                          Description = tariff.TariffHeadExport.Name,
                                          //Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                                          Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : ((calculateCBM >= tariff.CBMMultiplyValue) ? tariff.CBMRate * calculateCBM : tariff.CBMFixedRate),
                                          TariffExportId = tariff.TariffExportId,
                                          Type = "Tariff"
                                      }).Distinct().ToList();


                    var igmItems = (from tariff in Db.TariffExports
                                    join gdtariff in Db.GDTariffs on tariff.TariffExportId equals gdtariff.TariffExportId
                                    join enteringCargos in Db.EnteringCargos on gdtariff.EnteringCargoId equals enteringCargos.EnteringCargoId
                                    where enteringCargos.EnteringCargoId == gd.EnteringCargoId
                                         && (tariff.CBMRate > 0 || tariff.WeightRate > 0)
                                    select new InvoiceItemViewModel
                                    {
                                        NatureOfTariff = "CBM",
                                        Description = tariff.TariffHeadExport.Name,
                                        //Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                                        Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : ((calculateCBM >= tariff.CBMMultiplyValue) ? tariff.CBMRate * calculateCBM : tariff.CBMFixedRate),

                                        TariffExportId = tariff.TariffExportId,
                                        Type = tariff.TariffType
                                    }).Distinct().ToList();


                    var agentitemindex = (from tariff in Db.TariffExports
                                          join agentTariff in Db.ShippingAgentTariffExports on tariff.TariffExportId equals agentTariff.TariffExportId
                                          join loadingProgram in Db.LoadingPrograms on agentTariff.ShippingAgentExportId equals loadingProgram.ShippingAgentExportId
                                          where loadingProgram.LoadingProgramId == loadingprogrm.LoadingProgramId
                                          && tariff.PerIndexRate > 0 && tariff.NatureOfTariffId == restype

                                          select new InvoiceItemViewModel
                                          {
                                              NatureOfTariff = "Index",
                                              Description = tariff.TariffHeadExport.Name,
                                              Amount = tariff.PerIndexRate,
                                              TariffExportId = tariff.TariffExportId,
                                              Type = "Tariff"
                                          }).Distinct().ToList();

                    var igmItemindex = (from tariff in Db.TariffExports
                                        join gdtariff in Db.GDTariffs on tariff.TariffExportId equals gdtariff.TariffExportId
                                        join enteringCargos in Db.EnteringCargos on gdtariff.EnteringCargoId equals enteringCargos.EnteringCargoId
                                        where enteringCargos.EnteringCargoId == gd.EnteringCargoId && tariff.PerIndexRate > 0
                                        select new InvoiceItemViewModel
                                        {
                                            NatureOfTariff = "Index",
                                            Description = tariff.TariffHeadExport.Name,
                                            Amount = tariff.PerIndexRate,
                                            TariffExportId = tariff.TariffExportId,
                                            Type = tariff.TariffType
                                        }).Distinct().ToList();

                    totalItems.AddRange(igmItems);
                    totalItems.AddRange(agentitems);
                    totalItems.AddRange(agentitemindex);
                    totalItems.AddRange(igmItemindex);



                }

                //var agentitems = (from tariff in Db.TariffExports
                //                  join agentTariff in Db.ShippingAgentTariffExports on tariff.TariffExportId equals agentTariff.TariffExportId
                //                  join lp in Db.LoadingPrograms on agentTariff.ShippingAgentExportId equals lp.ShippingAgentExportId
                //                  join cargo in Db.EnteringCargos on lp.LoadingProgramNumber equals cargo.LoadingProgramNumber
                //                  where cargo.EnteringCargoId == EnteringCargoId
                //                  select new InvoiceItemViewModel
                //                  {
                //                      Description = tariff.TariffHeadExport.Name,
                //                      Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : ((calculateCBM > agentTariff.CBMMultiplyValue && agentTariff.CBMMultiply) ? tariff.CBMRate * calculateCBM : tariff.CBMFixedRate),
                //                      TariffId = tariff.TariffExportId
                //                  }).ToList();


                //var igmItems = (from tariff in Db.TariffExports
                //                join igmTariff in Db.GDTariffs on tariff.TariffExportId equals igmTariff.TariffExportId
                //                join cargo in Db.EnteringCargos on igmTariff.EnteringCargoId equals cargo.EnteringCargoId
                //                where cargo.EnteringCargoId == EnteringCargoId
                //                select new InvoiceItemViewModel
                //                {
                //                    Description = tariff.TariffHeadExport.Name,
                //                    Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                //                    TariffExportId = tariff.TariffExportId
                //                }).ToList();

                //totalItems.AddRange(igmItems);
                //totalItems.AddRange(agentitems);
            }

            totalItems.RemoveAll(c => c.Amount == 0);

            return totalItems;
        }

         

        public List<InvoiceItemExportViewModel> GetExportCBMTariffsForInvoice(string gdnumber, int Weight, double CBM)
        {
            var totalItems = new List<InvoiceItemExportViewModel>();

            var gd = Db.EnteringCargos.FromSql($"select * from EnteringCargo where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();

            if (gd == null)
            {
                return totalItems;
            }
            var loadingprogrm = Db.LoadingPrograms.FromSql($"select * from LoadingProgram where GDNumber = {gd.GDNumber}  and IsDeleted = 0").LastOrDefault();


            if (gd != null || (Weight > 0 && CBM > 0))
            {
                var calculateCBM = CBM > 0 ? Convert.ToInt32(Math.Ceiling(CBM)) : 0;

                var calculateWeight = Weight > 0 ? Weight : 0;

                var weightOrCBM = WeightGreaterThenCBM(calculateWeight, calculateCBM);

                var weightConv = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(calculateWeight) / 1000.0)));

                if (loadingprogrm != null && loadingprogrm.ShippingAgentExportId != null)
                {

                    var shippingAgentdata = Db.ShippingAgentExports.FromSql($"select * from ShippingAgentExport where ShippingAgentExportId = {loadingprogrm.ShippingAgentExportId}  and IsDeleted = 0").LastOrDefault();

                    long? restype = 0;
                    //if (loadingprogrm.NatureOfTariffId != null)
                    //{
                    //    restype = loadingprogrm.NatureOfTariffId;

                    //}
                    //else
                    //{
                    //    restype = shippingAgentdata.NatureOfTariffId;
                    //}

                    var agentitems = (from tariff in Db.TariffExports
                                      join agentTariff in Db.ShippingAgentTariffExports on tariff.TariffExportId equals agentTariff.TariffExportId
                                      join loadingProgram in Db.LoadingPrograms on agentTariff.ShippingAgentExportId equals loadingProgram.ShippingAgentExportId
                                      where
                                     loadingProgram.LoadingProgramId == loadingprogrm.LoadingProgramId
                                      && tariff.NatureOfTariffId == restype
                                      && (tariff.CBMRate > 0 || tariff.WeightRate > 0)
                                      &&
                                           !Db.DisabledAgentTariffExports.Any(s => s.EnteringCargoId == gd.EnteringCargoId && s.ShippingAgentTariffExportId == agentTariff.ShippingAgentTariffExportId)
                                      select new InvoiceItemExportViewModel
                                      {
                                          NatureOfTariff = "CBM",
                                          Description = tariff.TariffHeadExport.Name,
                                          //Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                                          Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : ((calculateCBM >= tariff.CBMMultiplyValue) ? tariff.CBMRate * calculateCBM : tariff.CBMFixedRate),
                                          TariffExportId = tariff.TariffExportId,
                                          Type = "Tariff"
                                      }).Distinct().ToList();


                    var igmItems = (from tariff in Db.TariffExports
                                    join gdtariff in Db.GDTariffs on tariff.TariffExportId equals gdtariff.TariffExportId
                                    join enteringCargos in Db.EnteringCargos on gdtariff.EnteringCargoId equals enteringCargos.EnteringCargoId
                                    where enteringCargos.EnteringCargoId == gd.EnteringCargoId
                                         && (tariff.CBMRate > 0 || tariff.WeightRate > 0)
                                    select new InvoiceItemExportViewModel
                                    {
                                        NatureOfTariff = "CBM",
                                        Description = tariff.TariffHeadExport.Name,
                                        //Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : (tariff.CBMRate * calculateCBM),
                                        Amount = weightOrCBM == 1 ? (tariff.WeightRate * weightConv) : ((calculateCBM >= tariff.CBMMultiplyValue) ? tariff.CBMRate * calculateCBM : tariff.CBMFixedRate),

                                        TariffExportId = tariff.TariffExportId,
                                        Type = tariff.TariffType
                                    }).Distinct().ToList();


                    var agentitemindex = (from tariff in Db.TariffExports
                                          join agentTariff in Db.ShippingAgentTariffExports on tariff.TariffExportId equals agentTariff.TariffExportId
                                          join loadingProgram in Db.LoadingPrograms on agentTariff.ShippingAgentExportId equals loadingProgram.ShippingAgentExportId
                                          where loadingProgram.LoadingProgramId == loadingprogrm.LoadingProgramId
                                          && tariff.PerIndexRate > 0 && tariff.NatureOfTariffId == restype
                                           &&
                                            !Db.DisabledAgentTariffExports.Any(s => s.EnteringCargoId == gd.EnteringCargoId && s.ShippingAgentTariffExportId == agentTariff.ShippingAgentTariffExportId)
                                          select new InvoiceItemExportViewModel
                                          {
                                              NatureOfTariff = "Index",
                                              Description = tariff.TariffHeadExport.Name,
                                              Amount = tariff.PerIndexRate,
                                              TariffExportId = tariff.TariffExportId,
                                              Type = "Tariff"
                                          }).Distinct().ToList();

                    var igmItemindex = (from tariff in Db.TariffExports
                                        join gdtariff in Db.GDTariffs on tariff.TariffExportId equals gdtariff.TariffExportId
                                        join enteringCargos in Db.EnteringCargos on gdtariff.EnteringCargoId equals enteringCargos.EnteringCargoId
                                        where enteringCargos.EnteringCargoId == gd.EnteringCargoId && tariff.PerIndexRate > 0
                                        select new InvoiceItemExportViewModel
                                        {
                                            NatureOfTariff = "Index",
                                            Description = tariff.TariffHeadExport.Name,
                                            Amount = tariff.PerIndexRate,
                                            TariffExportId = tariff.TariffExportId,
                                            Type = tariff.TariffType
                                        }).Distinct().ToList();

                    totalItems.AddRange(igmItems);
                    totalItems.AddRange(agentitems);
                    totalItems.AddRange(agentitemindex);
                    totalItems.AddRange(igmItemindex);


                    totalItems.RemoveAll(c => c.Amount == 0);



                    var slabwisetarifflist = (from tariff in Db.TariffExports
                                              join agentTariff in Db.ShippingAgentTariffExports on tariff.TariffExportId equals agentTariff.TariffExportId
                                              where agentTariff.ShippingAgentExportId == loadingprogrm.ShippingAgentExportId
                                             && tariff.NatureOfTariffId == restype
                                             &&
                                             !Db.DisabledAgentTariffExports.Any(s => s.EnteringCargoId == gd.EnteringCargoId && s.ShippingAgentTariffExportId == agentTariff.ShippingAgentTariffExportId)
                                            && tariff.TariffHeadExport.TariffHeadExportType == "SlabWise"
                                              select tariff).ToList();


                    if (slabwisetarifflist.Count() > 0)
                    {


                        //calculateCBM



                        foreach (var item in slabwisetarifflist)
                        {


                            var Slabs = Db.TariffRateSlabWises.Where(c => c.TariffExportId == item.TariffExportId).ToList();


                            var tariffhead = Db.TariffHeadExports.FromSql($" select  * from  tariffheadexport  where   TariffHeadExportId = {item.TariffHeadExportId} and  IsDeleted = 0").LastOrDefault();

                            if (Slabs.Count() > 0)
                            {
                                foreach (var itemres in Slabs)
                                {
                                    if (calculateCBM >= itemres.FromCBM && calculateCBM <= itemres.ToCBM)
                                    {
                                        var res = new InvoiceItemExportViewModel()
                                        {
                                            NatureOfTariff = "CBM",
                                            Description = tariffhead != null ? tariffhead.Name : "",
                                            //Amount = itemres.Rate,
                                            Amount = weightOrCBM == 1 ? (itemres.Rate * weightConv) : (itemres.Rate * calculateCBM),
                                            TariffExportId = item.TariffExportId,
                                            Type = "Tariff"

                                        };
                                        totalItems.Add(res);

                                        break;
                                    }

                                }



                            }





                        }
                    }



                    totalItems.RemoveAll(c => c.Amount == 0);



                }

            }
             
            return totalItems;
        }


        public List<InvoiceItemViewModel> GetIndexTotalExport(long EnteringCargoId)
        {
            var totalItems = new List<InvoiceItemViewModel>();

            var agentitems = (from tariff in Db.TariffExports
                              join agentTariff in Db.ShippingAgentTariffExports on tariff.TariffExportId equals agentTariff.TariffExportId
                              join lp in Db.LoadingPrograms on agentTariff.ShippingAgentExportId equals lp.ShippingAgentExportId
                              join cargo in Db.EnteringCargos on lp.LoadingProgramNumber equals cargo.LoadingProgramNumber
                              where cargo.EnteringCargoId == EnteringCargoId && tariff.PerIndexRate > 0
                              select new InvoiceItemViewModel
                              {
                                  Description = tariff.TariffHeadExport.Name,
                                  Amount = tariff.PerIndexRate,
                                  TariffId = tariff.TariffExportId
                              }).ToList();

            var igmItems = (from tariff in Db.TariffExports
                            join igmTariff in Db.GDTariffs on tariff.TariffExportId equals igmTariff.TariffExportId
                            where igmTariff.EnteringCargoId == EnteringCargoId && tariff.PerIndexRate > 0
                            select new InvoiceItemViewModel
                            {
                                Description = tariff.TariffHeadExport.Name,
                                Amount = tariff.PerIndexRate,
                                TariffExportId = tariff.TariffExportId
                            }).ToList();

            totalItems.AddRange(igmItems);
            totalItems.AddRange(agentitems);

            return totalItems;
        }


        public StorageTotalViewModel GetStorageTotalExport(long EnteringCargoId, DateTime BillDate, DateTime gateInDate, int Weight, double CBM)
        {

            // update migraiton and work on invoice
            var storageTotal = 0.00;
            var storageDays = 0;
            var freedaysCounted = false;

            var noOfDays = 0;
            var invoiceDays = 0;
            var invoicestorageTotal = 0.00;
            var totalpartcontainers = 0;
            int additionalFreeDays = 0;


            var enteringCargo = Db.EnteringCargos.FromSql($" select * from EnteringCargo where EnteringCargoId = {EnteringCargoId}  and IsDeleted = 0").LastOrDefault();

            var loadingProgram = Db.LoadingPrograms.FirstOrDefault(s => s.LoadingProgramNumber == enteringCargo.LoadingProgramNumber);

            var gatein = Db.GateInExports.FirstOrDefault(s => s.GDNumber == enteringCargo.GDNumber);

            var shippingAget = Db.ShippingAgentExports.FromSql($" select * from ShippingAgentExport where ShippingAgentExportId = {loadingProgram.ShippingAgentExportId}  and IsDeleted = 0").LastOrDefault();


            long? restype = 0;
            //if (loadingProgram.NatureOfTariffId != null)
            //{
            //    restype = loadingProgram.NatureOfTariffId;

            //}
            //else
            //{
            //    restype = shippingAget.NatureOfTariffId;
            //}


            var tarifList = new List<TariffExport>();


            var calculateCBM = Convert.ToInt32(Math.Ceiling(CBM));

            var calculateWeight = Weight;

            var weightOrCBM = WeightGreaterThenCBM(calculateWeight, calculateCBM);

            var weightConv = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(calculateWeight) / 1000.0)));

            var TariffIds = (from tariff in Db.TariffExports
                             join igmTariff in Db.GDTariffs on tariff.TariffExportId equals igmTariff.TariffExportId
                             where igmTariff.EnteringCargoId == EnteringCargoId && tariff.TariffHeadExport.Name.Contains("STORAGE")
                             select tariff).ToList();

            tarifList.AddRange(TariffIds);

            var AgentTariffIds = (from tariff in Db.TariffExports
                                  join agentTariff in Db.ShippingAgentTariffExports on tariff.TariffExportId equals agentTariff.TariffExportId
                                  where agentTariff.ShippingAgentExportId == loadingProgram.ShippingAgentExportId
                                   && tariff.NatureOfTariffId == restype
                                  && tariff.TariffHeadExport.Name.Contains("STORAGE")
                                  select tariff).ToList();

            tarifList.AddRange(AgentTariffIds);



            var cbm = Convert.ToInt32(Math.Ceiling(CBM));



            var nooffreedays = 0;

            nooffreedays = (nooffreedays - 1);

            noOfDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((nooffreedays)).Date).Days);



            if (enteringCargo.AdditionalFreeDays > 0)
            {

                noOfDays = noOfDays - enteringCargo.AdditionalFreeDays;

            }


            if (tarifList.Count() > 0)
            {
                foreach (var tariffId in tarifList)
                {
                    var storageSlabs = Db.StorageSlabExports.Where(c => c.ExportTariffId == tariffId.TariffExportId).ToList();


                    if (storageSlabs.Count() > 0)
                    {
                        if (storageSlabs.FirstOrDefault().FreeDays > 0)
                        {
                            noOfDays = noOfDays - storageSlabs.FirstOrDefault().FreeDays;
                        }

                    }
                }
            }

            List<StorageDaysViewModel> storageDayslist = new List<StorageDaysViewModel>();

            for (int i = 0; i < noOfDays; i++)
            {
                var storageDay = new StorageDaysViewModel
                {

                    IndexNo = i,
                    Status = "UnDelivered"
                };
                storageDayslist.Add(storageDay);
            }



            if (enteringCargo != null)
            {

                var invoicesstoragedays = (from inovice in Db.InvoiceExports
                                           where enteringCargo.EnteringCargoId == inovice.EnteringCargoId
                                           select inovice).Sum(x => x.StorageDays);

                if (invoicesstoragedays > 0)
                {
                    invoiceDays = invoicesstoragedays;
                }

                var invoicesstorageamount = (from inovice in Db.InvoiceExports
                                             join invoiceitem in Db.InvoiceItemExports on inovice.InvoiceExportId equals invoiceitem.InvoiceExportId
                                             where enteringCargo.EnteringCargoId == inovice.EnteringCargoId &&
                                             invoiceitem.Description.Contains("Storage Amount")
                                             select invoiceitem).Sum(x => x.Amount);


                if (invoicesstorageamount > 0)
                {
                    invoicestorageTotal = invoicesstorageamount;
                }



            }


            if (invoiceDays > 0)
            {

                for (int i = 0; i < invoiceDays; i++)
                {
                    if (storageDayslist.Where(x => x.Status == "UnDelivered").FirstOrDefault() != null)
                    {
                        storageDayslist.Where(x => x.Status == "UnDelivered").FirstOrDefault().Status = "Delivered";
                    }

                }

            }

            storageDays = storageDayslist.Where(x => x.Status == "UnDelivered").Count();


            if (tarifList.Count() > 0)
            {
                foreach (var tariffId in tarifList)
                {
                    var storageSlabs = Db.StorageSlabExports.Where(c => c.ExportTariffId == tariffId.TariffExportId).ToList();


                    if (storageSlabs.Count() > 0)
                    {
                        if (!freedaysCounted)
                        {
                            //noOfDays -= storageSlabs.FirstOrDefault().FreeDays;

                            //storageDays = noOfDays;

                            freedaysCounted = true;

                        }

                        if (storageDayslist.Where(x => x.Status == "UnDelivered").Count() < 0)
                        {
                            noOfDays = 0;

                            storageDays = noOfDays;
                        }

                        // new work end

                        var unitToMultiply = weightOrCBM == 1 ? weightConv : calculateCBM;

                        foreach (var storageSlab in storageSlabs)
                        {
                            if (noOfDays <= 0)
                                break;

                            if (!storageSlab.NoOfDays.ToUpper().Contains("OVER"))
                            {
                                var period = (storageDayslist.Count()) - (Convert.ToInt32(storageSlab.NoOfDays) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : storageDayslist.Count());

                                if (period > Convert.ToInt32(storageSlab.NoOfDays) || storageDayslist.Count() > Convert.ToInt32(storageSlab.NoOfDays))
                                    period = Convert.ToInt32(storageSlab.NoOfDays);
                                else if (period < Convert.ToInt32(storageSlab.NoOfDays))
                                    period = storageDayslist.Count();

                                var takeitems = storageDayslist.Take(period).Select(x => x).ToList();


                                foreach (var itemstor in takeitems)
                                {
                                    if (itemstor.Status == "UnDelivered")
                                    {
                                        if (tariffId.IsCBMorRate == true)
                                        {
                                            storageTotal += (storageSlab.Rate * unitToMultiply);
                                        }

                                    }

                                    storageDayslist.RemoveAll(x => x.IndexNo == itemstor.IndexNo);
                                }
                                if (storageDayslist.Count() < Convert.ToInt32(storageSlab.NoOfDays))
                                {
                                    noOfDays -= period;

                                }

                                else
                                {
                                    noOfDays -= Convert.ToInt32(storageSlab.NoOfDays);

                                }

                            }
                            else
                            {

                                foreach (var itemstor in storageDayslist)
                                {
                                    if (itemstor.Status == "UnDelivered")
                                    {
                                        if (tariffId.IsCBMorRate == true)
                                        {
                                            storageTotal += (storageSlab.Rate * unitToMultiply);
                                        }
                                    }
                                }

                            }
                        }

                    }

                }

                if (storageDays < 0)
                {
                    storageDays = 0;
                }

                //if (deliveredweight > 0)
                //{
                if (invoicestorageTotal > 0)
                {
                    storageTotal += Convert.ToInt32(invoicestorageTotal);
                }
                // }



                if (storageDays < 0)
                {
                    storageDays = 0;
                }


                storageTotal = Math.Round(storageTotal, 2);

                return new StorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal };

                //

            }

            else
            {

                if (storageDays < 0)
                {
                    storageDays = 0;
                }

                storageTotal = Math.Round(storageTotal, 2);
                return new StorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal };
            }




            //var distinctTariffExportIds = TariffExportIds.Distinct<long>();

            //      noOfDays = Convert.ToInt32((BillDate - gatein.GateInDate.Value.AddDays(3)).TotalDays);

            //    storageDays = noOfDays;

            //    foreach (var tariffExportId in TariffExportIds)
            //    {
            //        var StorageSlabExports = Db.StorageSlabExports.Where(c => c.TariffExportId == tariffExportId).ToList();

            //        if (!freedaysCounted)
            //        {
            //            noOfDays -= StorageSlabExports.FirstOrDefault().FreeDays;

            //            storageDays = noOfDays;

            //            freedaysCounted = true;

            //        }

            //        if (noOfDays < 0)
            //        {
            //            noOfDays = 0;

            //            storageDays = noOfDays;
            //        }

            //        var unitToMultiply = weightOrCBM == 1 ? weightConv : calculateCBM;

            //        foreach (var storageSlab in StorageSlabExports)
            //        {
            //            if (noOfDays <= 0)
            //                break;

            //            if (!storageSlab.NoOfDays.ToUpper().Contains("OVER"))
            //            {
            //                var period = (noOfDays - Convert.ToInt32(storageSlab.NoOfDays)) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : noOfDays;

            //                if (period < Convert.ToInt32(storageSlab.NoOfDays))
            //                    period = noOfDays;

            //                for (int i = 0; i < period; i++)
            //                {
            //                    storageTotal += (storageSlab.Rate * unitToMultiply);
            //                }

            //                if (noOfDays < Convert.ToInt32(storageSlab.NoOfDays))
            //                    noOfDays -= period;
            //                else
            //                    noOfDays -= Convert.ToInt32(storageSlab.NoOfDays);
            //            }
            //            else
            //            {
            //                for (int i = 0; i < noOfDays; i++)
            //                {
            //                    storageTotal += (storageSlab.Rate * unitToMultiply);
            //                }
            //            }
            //        }

            //    }



            return new StorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal };
        }


        //leatest storage work
        public ExportStorageTotalViewModel GetStorageTotalExportForInvoice(string gdnumber, DateTime BillDate, DateTime gateInDate, int Weight, double CBM)
        {

            // update migraiton and work on invoice
            var storageTotal = 0.00;
            var storageDays = 0;
            var freedaysCounted = false;

            var noOfDays = 0;
            var invoiceDays = 0;
            var invoicestorageTotal = 0.00;


            var enteringCargo = Db.EnteringCargos.FromSql($" select * from EnteringCargo where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();

            var loadingProgram = Db.LoadingPrograms.FromSql($" select * from LoadingProgram where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();

            // var gatein = Db.GateInExports.FromSql($" select * from GateInExports where GDNumber = {gdnumber}  and IsDeleted = 0").FirstOrDefault();

            var shippingAget = Db.ShippingAgentExports.FromSql($" select * from ShippingAgentExport where ShippingAgentExportId = {loadingProgram.ShippingAgentExportId}  and IsDeleted = 0").LastOrDefault();


            if (enteringCargo != null && loadingProgram != null && shippingAget != null)
            {
                long? restype = 0;
                //if (loadingProgram.NatureOfTariffId != null)
                //{
                //    restype = loadingProgram.NatureOfTariffId;

                //}
                //else
                //{
                //    restype = shippingAget.NatureOfTariffId;
                //}


                var tarifList = new List<TariffExport>();


                var calculateCBM = Convert.ToInt32(Math.Ceiling(CBM));

                var calculateWeight = Weight;

                var weightOrCBM = WeightGreaterThenCBM(calculateWeight, calculateCBM);

                var weightConv = Convert.ToInt32(Math.Ceiling((Convert.ToDouble(calculateWeight) / 1000.0)));

                var TariffIds = (from tariff in Db.TariffExports
                                 join igmTariff in Db.GDTariffs on tariff.TariffExportId equals igmTariff.TariffExportId
                                 where igmTariff.EnteringCargoId == enteringCargo.EnteringCargoId && tariff.TariffHeadExport.Name.Contains("STORAGE")
                                 select tariff).ToList();

                tarifList.AddRange(TariffIds);

                var AgentTariffIds = (from tariff in Db.TariffExports
                                      join agentTariff in Db.ShippingAgentTariffExports on tariff.TariffExportId equals agentTariff.TariffExportId
                                      where agentTariff.ShippingAgentExportId == loadingProgram.ShippingAgentExportId
                                       && tariff.NatureOfTariffId == restype
                                       &&
                                      !Db.DisabledAgentTariffExports.Any(s => s.EnteringCargoId == enteringCargo.EnteringCargoId && s.ShippingAgentTariffExportId == agentTariff.ShippingAgentTariffExportId)
                                      && tariff.TariffHeadExport.Name.Contains("STORAGE")
                                      select tariff).ToList();

                tarifList.AddRange(AgentTariffIds);



                var cbm = Convert.ToInt32(Math.Ceiling(CBM));



                var nooffreedays = 0;

                nooffreedays = (nooffreedays - 1);

                noOfDays = Convert.ToInt32((BillDate.Date - gateInDate.AddDays((nooffreedays)).Date).Days);



                if (enteringCargo.AdditionalFreeDays > 0)
                {

                    noOfDays = noOfDays - enteringCargo.AdditionalFreeDays;

                }


                if (tarifList.Count() > 0)
                {
                    foreach (var tariffId in tarifList)
                    {
                        var storageSlabs = Db.StorageSlabExports.Where(c => c.ExportTariffId == tariffId.TariffExportId).ToList();


                        if (storageSlabs.Count() > 0)
                        {
                            if (storageSlabs.FirstOrDefault().FreeDays > 0)
                            {
                                noOfDays = noOfDays - storageSlabs.FirstOrDefault().FreeDays;
                            }

                        }
                    }
                }

                List<StorageDaysViewModel> storageDayslist = new List<StorageDaysViewModel>();

                for (int i = 0; i < noOfDays; i++)
                {
                    var storageDay = new StorageDaysViewModel
                    {

                        IndexNo = i,
                        Status = "UnDelivered"
                    };
                    storageDayslist.Add(storageDay);
                }



                if (enteringCargo != null)
                {

                    var invoicesstoragedays = (from inovice in Db.InvoiceExports
                                               where enteringCargo.EnteringCargoId == inovice.EnteringCargoId
                                               select inovice).Sum(x => x.StorageDays);

                    if (invoicesstoragedays > 0)
                    {
                        invoiceDays = invoicesstoragedays;
                    }

                    var invoicesstorageamount = (from inovice in Db.InvoiceExports
                                                 join invoiceitem in Db.InvoiceItemExports on inovice.InvoiceExportId equals invoiceitem.InvoiceExportId
                                                 where enteringCargo.EnteringCargoId == inovice.EnteringCargoId &&
                                                 invoiceitem.Description.Contains("Storage Amount")
                                                 select invoiceitem).Sum(x => x.Amount);


                    if (invoicesstorageamount > 0)
                    {
                        invoicestorageTotal = invoicesstorageamount;
                    }

                }

                if (invoiceDays > 0)
                {

                    for (int i = 0; i < invoiceDays; i++)
                    {
                        if (storageDayslist.Where(x => x.Status == "UnDelivered").FirstOrDefault() != null)
                        {
                            storageDayslist.Where(x => x.Status == "UnDelivered").FirstOrDefault().Status = "Delivered";
                        }

                    }

                }

                storageDays = storageDayslist.Where(x => x.Status == "UnDelivered").Count();


                if (tarifList.Count() > 0)
                {
                    foreach (var tariffId in tarifList)
                    {
                        var storageSlabs = Db.StorageSlabExports.Where(c => c.ExportTariffId == tariffId.TariffExportId).ToList();


                        if (storageSlabs.Count() > 0)
                        {
                            if (!freedaysCounted)
                            {
                                //noOfDays -= storageSlabs.FirstOrDefault().FreeDays;

                                //storageDays = noOfDays;

                                freedaysCounted = true;

                            }

                            if (storageDayslist.Where(x => x.Status == "UnDelivered").Count() < 0)
                            {
                                noOfDays = 0;

                                storageDays = noOfDays;
                            }

                            // new work end

                            var unitToMultiply = weightOrCBM == 1 ? weightConv : calculateCBM;

                            foreach (var storageSlab in storageSlabs)
                            {
                                if (noOfDays <= 0)
                                    break;

                                if (!storageSlab.NoOfDays.ToUpper().Contains("OVER"))
                                {
                                    var period = (storageDayslist.Count()) - (Convert.ToInt32(storageSlab.NoOfDays) > 0 ? Convert.ToInt32(storageSlab.NoOfDays) : storageDayslist.Count());

                                    if (period > Convert.ToInt32(storageSlab.NoOfDays) || storageDayslist.Count() > Convert.ToInt32(storageSlab.NoOfDays))
                                        period = Convert.ToInt32(storageSlab.NoOfDays);
                                    else if (period < Convert.ToInt32(storageSlab.NoOfDays))
                                        period = storageDayslist.Count();

                                    var takeitems = storageDayslist.Take(period).Select(x => x).ToList();


                                    foreach (var itemstor in takeitems)
                                    {
                                        if (itemstor.Status == "UnDelivered")
                                        {
                                            if (tariffId.IsCBMorRate == true)
                                            {
                                                storageTotal += (storageSlab.Rate * unitToMultiply);
                                            }

                                        }

                                        storageDayslist.RemoveAll(x => x.IndexNo == itemstor.IndexNo);
                                    }
                                    if (storageDayslist.Count() < Convert.ToInt32(storageSlab.NoOfDays))
                                    {
                                        noOfDays -= period;

                                    }

                                    else
                                    {
                                        noOfDays -= Convert.ToInt32(storageSlab.NoOfDays);

                                    }

                                }
                                else
                                {

                                    foreach (var itemstor in storageDayslist)
                                    {
                                        if (itemstor.Status == "UnDelivered")
                                        {
                                            if (tariffId.IsCBMorRate == true)
                                            {
                                                storageTotal += (storageSlab.Rate * unitToMultiply);
                                            }
                                        }
                                    }

                                }
                            }

                        }

                    }

                    if (storageDays < 0)
                    {
                        storageDays = 0;
                    }

                    //if (deliveredweight > 0)
                    //{
                    if (invoicestorageTotal > 0)
                    {
                        storageTotal += Convert.ToInt32(invoicestorageTotal);
                    }
                    // }



                    if (storageDays < 0)
                    {
                        storageDays = 0;
                    }


                    storageTotal = Math.Round(storageTotal, 2);

                    return new ExportStorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal };

                    //

                }

                else
                {

                    if (storageDays < 0)
                    {
                        storageDays = 0;
                    }

                    storageTotal = Math.Round(storageTotal, 2);
                    return new ExportStorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal };
                }

            }




            return new ExportStorageTotalViewModel { StorageDays = storageDays, StorageTotal = storageTotal };
        }
        //public CargoReceived GetCargoReceivedByEnteringCargoId(long EnteringCargoId)
        //{
        //    var data = (from enteringCargo in Db.EnteringCargos
        //                join loadingProgram in Db.LoadingPrograms on enteringCargo.LoadingProgramNumber equals loadingProgram.LoadingProgramNumber
        //                join cargoReceived in Db.CargoReceiveds on loadingProgram.LoadingProgramId equals cargoReceived.LoadingProgramId
        //                where enteringCargo.EnteringCargoId == EnteringCargoId
        //                select cargoReceived).FirstOrDefault();
        //    return data;
        //}

        public InvoiceViewModel GetExportInvoiceByBillNo(string BillNo)
        {
            var inv = (from invoice in Db.InvoiceExports
                       join enteringCargo in Db.EnteringCargos on invoice.EnteringCargoId equals enteringCargo.EnteringCargoId
                       where invoice.InvoiceNo == BillNo
                       select new InvoiceViewModel
                       {
                           InvoiceExportId = invoice.InvoiceExportId,
                           BillNo = invoice.InvoiceNo.ToString(),
                           CBM = invoice.CBM,
                           Consignee = invoice.Consignee,
                           //ContainerNo = container.ContainerNo,
                           DestuffDate = invoice.DestuffDate,
                           GateInDate = invoice.GateInDate,
                           GDNumber = enteringCargo.GDNumber,
                           InvoiceDate = invoice.InvoiceDate,
                           Size = invoice.Size,
                           IsAdvanceBill = invoice.IsAdvanceBill,
                           ClearingAgentExportId = invoice.ClearingAgentExportId ?? 0,
                           TotalAmount = invoice.TotalAmount,
                           StorageTotal = invoice.StorageTotal,
                           GrandTotal = Convert.ToInt32( invoice.GrandTotal)
                       }).FirstOrDefault();

            return inv;
        }

        internal double WeightGreaterThenCBM(double Weight, double CBM)
        {
            var CBMToWeight = CBM * 1000;

            if (CBMToWeight < Weight)
                return 1;

            return 0;
        }


        public InvoiceExportViewModel GetExportInvoiceByGdnumber(string gdnumber)
        {
            var invoice = (from enteringCargo in Db.EnteringCargos
                           from saleTax in Db.SalesTaxes.Where(x => x.Type == "Export").DefaultIfEmpty()
                           join gatein in Db.GateInExports on enteringCargo.GDNumber.Trim().ToUpper() equals gatein.GDNumber.Trim().ToUpper()
                           join opia in Db.OPIAs on enteringCargo.GDNumber.Trim().ToUpper() equals opia.GDNumber.Trim().ToUpper()
                           join loadingProgram in Db.LoadingPrograms on enteringCargo.GDNumber equals loadingProgram.GDNumber
                            join vessel in Db.VesselExports on loadingProgram.VesselExportId equals vessel.VesselExportId
                           join voyage in Db.VoyageExports on loadingProgram.VoyageExportId equals voyage.VoyageExportId
                           join exportConsignee in Db.ExportConsignees on loadingProgram.ExportConsigneeId equals exportConsignee.ExportConsigneeId
                           join clearingAgentExport in Db.ClearingAgentExports on loadingProgram.ClearingAgentExportId equals clearingAgentExport.ClearingAgentExportId
                           join shippingAgentExport in Db.ShippingAgentExports on loadingProgram.ShippingAgentExportId equals shippingAgentExport.ShippingAgentExportId
                           where enteringCargo.GDNumber == gdnumber
                           select new InvoiceExportViewModel
                           {
                               EnteringCargoId = enteringCargo.EnteringCargoId,
                               NoOfPackages = Convert.ToInt32(opia.NoOfPackages),
                               PackageType = opia.PackageType,
                               //DishargePort = (from cargoInfo in Db.Cargos where cargoInfo.GDNumber.Trim().ToUpper() == enteringCargo.GDNumber.Trim().ToUpper() select cargoInfo).Select(x => x.DishargePort).FirstOrDefault() ?? "",
                               DishargePort = loadingProgram.Destination,
                               //GateInDate = agent.ImplementFrom == "GateInDate" ? gatein.GateInDate : agent.ImplementFrom == "OPIA" ? opia.Performed : null,
                               CBM = Convert.ToDouble((from lpdetail in Db.LoadingProgramDetails where lpdetail.LoadingProgramId == loadingProgram.LoadingProgramId select lpdetail).Sum(s => s.CBM)),
                               Weight = (from lpdetail in Db.LoadingProgramDetails where lpdetail.LoadingProgramId == loadingProgram.LoadingProgramId select lpdetail).Sum(s => s.Weight) ?? 0,
                               Consignee = exportConsignee.ConsigneName,
                               ConsigneeNTN = exportConsignee.ConsigneNTN,
                               ClearingAgentName = clearingAgentExport.ClearingAgentName,
                               StorageDays = Convert.ToInt32((DateTime.Now - gatein.GateInDate).Value.TotalDays),
                               ShippingAgnet = shippingAgentExport.ShippingAgentName,
                               Vessel = vessel.VesselName,
                               Voyage = voyage.VoyageNumber,
                               //ETD = agent.ImplementTo == "ETD" ? voyage.ETD : DateTime.Now,
                               ETD =  voyage.ETD,
                               SalesTax = saleTax != null ? saleTax.SalesTaxAmount : 0,
                               PreviousBillAmount = GetInvoiceTotal(gdnumber),
                               WaiverAmount = enteringCargo.WaiverAmount,
                               AdditionalFreeDays = enteringCargo.AdditionalFreeDays,

                           }).FirstOrDefault();

            if (invoice != null)
            {
                var invoicedetail = Db.InvoiceExports.FromSql($"select   *  from InvoiceExport    where  EnteringCargoId = {invoice.EnteringCargoId}  and IsDeleted = 0").LastOrDefault();

                if (invoicedetail != null)
                {
                    invoice.CNIC = invoicedetail.CNIC;
                    invoice.AgentRepName = invoicedetail.ClearingAgentRep;
                    invoice.PhoneNumber = invoicedetail.PhoneNumber;
                    invoice.CBM = invoicedetail.CBM;
                    invoice.Weight = invoicedetail.Weight;
                    invoice.AdvanceDate = invoicedetail.AdvanceDate;
                }
            }

            var ogie = Db.OGIEs.FromSql($" select top 1 * from ogies where GDNumber = {gdnumber}  and IsDeleted = 0").FirstOrDefault();

            if (ogie != null)
            {
                invoice.GateInDate = ogie.Performed;
            }

            return invoice;
        }

        public double GetInvoiceTotal(string gdnumber)
        {
            var asd = Db.EnteringCargos.FromSql($"select * from EnteringCargo where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();

            if (asd != null)
            {
                var inv = (from invoice in Db.InvoiceExports
                           where invoice.EnteringCargoId == asd.EnteringCargoId && invoice.IsCancelled == false
                           select invoice.TotalCharges).ToList();


                if (inv.Count() > 0)
                {

                    double perviouseAmount = 0;

                    foreach (var item in inv)
                    {
                        perviouseAmount += item;

                    }

                    return perviouseAmount;


                }

                else
                {
                    return 0;
                }
            }


            return 0;
        }

        public InvoiceExport GetInvoiceLast()
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.InvoiceExports.FromSql($"select * from InvoiceExport").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public List<InvoiceItemExport> GetInvoiceItemBycontainerIndexId(long EnteringCargoId)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.InvoiceItemExports.FromSql($" select InvoiceItemExport.* from InvoiceExport join InvoiceItemExport on InvoiceExport.InvoiceExportId = InvoiceItemExport.InvoiceExportId and InvoiceItemExport.IsDeleted = 0  where InvoiceExport.EnteringCargoId = {EnteringCargoId} and InvoiceExport.IsDeleted = 0 ").ToList();

            return asd;

        }


        public List<InvoiceExport> GetinvoicesBygdnumber(string gdnumber)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var res = new List<InvoiceExport>();

            var cargo = Db.EnteringCargos.FromSql($" select  * from EnteringCargo  where   EnteringCargo.GDNumber = {gdnumber} and EnteringCargo.IsDeleted = 0").LastOrDefault();


            if (cargo != null)
            {
                var cfsdata = Db.InvoiceExports.FromSql($" select  * from InvoiceExport  where   InvoiceExport.EnteringCargoId = {cargo.EnteringCargoId} and InvoiceExport.IsDeleted = 0").ToList();

                res.AddRange(cfsdata);
            }

            return res;

        }

        public List<InvoiceExport> GetinvoicesByInvoiceno(string invoiceno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.InvoiceExports.FromSql($" select * from InvoiceExport Where  InvoiceNo = {invoiceno} and IsDeleted = 0 ").ToList();

            return asd;

        }

        public InvoiceExport GetInvoiceByInvoiceId(long InvoiceId)
        {

            var cfsdata = Db.InvoiceExports.FromSql($" select * from InvoiceExport where InvoiceExportId = {InvoiceId}  and IsDeleted = 0").LastOrDefault();

            if (cfsdata != null)
            {
                return cfsdata;
            }

            return null;
        }
        public AmountReceivedExport GetAmountReceivedByInvoiceId(long InvoiceId)
        {

            var data = Db.AmountReceivedExports.FromSql($" select * from AmountReceivedExport where InvoiceExportId =  {InvoiceId} and IsDeleted = 0").LastOrDefault();

            if (data != null)
            {
                return data;
            }

            return null;
        }


        public List<PartyLedgerExport> GetPrtyLedgerListByAmountReceivedExportId(long AmountReceivedId)
        {

            var data = Db.PartyLedgerExports.FromSql($" select * from PartyLedgerExport where AmountReceivedExportId =  {AmountReceivedId} and IsDeleted = 0").ToList();

            if (data.Count() > 0)
            {
                return data;
            }

            return null;
        }


        public List<InvoiceExport> GetinvoicesBygdno(string gdno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var res = new List<InvoiceExport>();

            var cfsdata = Db.InvoiceExports.FromSql($" select InvoiceExport.* from EnteringCargo join InvoiceExport on EnteringCargo.EnteringCargoId = InvoiceExport.EnteringCargoId and InvoiceExport.IsDeleted = 0 where   EnteringCargo.GDNumber = {gdno} and EnteringCargo.IsDeleted = 0").ToList();

            if (cfsdata.Count() > 0)
            {
                res.AddRange(cfsdata);
            }


            return res;

        }


        public List<ChequeRecivedExport> GetChequeReceivedDetailsExport(long partyId, long instrumentNo)
        {

            var data = Db.ChequeRecivedExports.FromSql($" select * from ChequeRecivedExport where PartyExportId = {partyId}  and Number = {instrumentNo} and IsDeleted = 0").ToList();

            if (data != null)
            {
                return data;
            }

            return null;
        }


        public InvoiceExport GetExportAgentNameAndPhoneNo(string val)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var cfsdata = Db.InvoiceExports.FromSql($" select  * from  InvoiceExport  where   InvoiceExport.CNIC = {val} and InvoiceExport.IsDeleted = 0").LastOrDefault();

            if (cfsdata != null)
            {
                return cfsdata;
            }


            return null;

        }



        public bool IsGdAssociatedWithInvoice(long enteringcargoid)
        {

            var data = Db.EnteringCargos.FromSql($" select * from EnteringCargo where EnteringCargoId =  {enteringcargoid} and IsDeleted = 0").LastOrDefault();

            if (data != null)
            {

                var ec = Db.ExportContainerItems.FromSql($" select * from ExportContainerItem where GDNumber =  {data.GDNumber} and IsDeleted = 0").LastOrDefault();

                if (ec != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }



        public List<InvoiceExport> GetInvoiceExportbygdnumber(string gdnumber)
        {

            var invoicesexport = new List<InvoiceExport>();

            var data = Db.EnteringCargos.FromSql($" select * from EnteringCargo where GDNumber =  {gdnumber} and IsDeleted = 0").LastOrDefault();

            if (data != null)
            {

                var invoices = Db.InvoiceExports.FromSql($" select * from InvoiceExport where EnteringCargoId =  {data.EnteringCargoId} and IsDeleted = 0").ToList();

                if (invoices.Count() > 0)
                {
                    return invoices;
                }
                else
                {
                    return invoicesexport;
                }
            }
            else
            {
                return invoicesexport;
            }

        }

    }
}
