using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class CargoRollOverRepository : RepoBase<CargoRollOver>, ICargoRollOverRepository
    {
        public CargoRollOverRepository(IUserResolveService userResolveService) : base(userResolveService) { }

        public CargoRollOverViewModel GetGDDeatil(string gdnumber)
        {
            var Data = (from
                         enteringCargo in Db.EnteringCargos
                        join loadingProgram in Db.LoadingPrograms on enteringCargo.LoadingProgramNumber equals loadingProgram.LoadingProgramNumber
                        join loadingProgramDetail in Db.LoadingProgramDetails on loadingProgram.LoadingProgramId equals loadingProgramDetail.LoadingProgramId
                        join cargo in Db.Cargos on loadingProgramDetail.LoadingProgramDetailId equals cargo.LoadingProgramDetailId
                        join clearingAgentExport in Db.ClearingAgentExports on loadingProgram.ClearingAgentExportId equals clearingAgentExport.ClearingAgentExportId
                        join shippingAgentExport in Db.ShippingAgentExports on loadingProgram.ShippingAgentExportId equals shippingAgentExport.ShippingAgentExportId
                        //join shipper in Db.Shippers on loadingProgram.ShipperId equals shipper.ShipperId
                        join commodity in Db.Commodities on cargo.CommodityId equals commodity.CommodityId
                        join vesselExport in Db.VesselExports on loadingProgram.VesselExportId equals vesselExport.VesselExportId
                        join voyageExport in Db.VoyageExports on loadingProgram.VoyageExportId equals voyageExport.VoyageExportId
                        where

                        enteringCargo.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper()
                        //&&
                        //!Db.ExportContainerItems.Any(s => s.GDNumber == enteringCargo.GDNumber)
                        select new CargoRollOverViewModel
                        {
                            NoOfPackages = enteringCargo.NumberOfPackages,
                            PackageType = enteringCargo.PackageType,
                            ClearingAgent = clearingAgentExport.ClearingAgentName,
                            Commodity = commodity.CommodityName,
                            //Shipper = shipper.ShipperName,
                            Shipper = "",
                            ShippingAgent = shippingAgentExport.ShippingAgentName,
                            Vessel = vesselExport.VesselName,
                            Voyage = voyageExport.VoyageNumber

                        }).FirstOrDefault();


            return Data;
        }

        public List<CargoRollOverViewModel> GetUnAssignGDNumbers()
        {
            var Data = (from loadingprogram in Db.LoadingPrograms
                        join enteringCargo in Db.EnteringCargos on loadingprogram.LoadingProgramNumber equals enteringCargo.LoadingProgramNumber
                        where  !Db.GateoutExports.Any(s => s.GDNumber.Trim().ToUpper() == enteringCargo.GDNumber.Trim().ToUpper())
                        select new CargoRollOverViewModel
                        {
                            GDNumber = enteringCargo.GDNumber

                        }).ToList();



            return Data;
        }
        
        public List<CargoRollOverViewModel> GetUnAssignGDNumbersForDO()
        {
            var Data = (from loadingprogram in Db.LoadingPrograms
                        join enteringCargo in Db.EnteringCargos on loadingprogram.LoadingProgramNumber equals enteringCargo.LoadingProgramNumber
                        join ocrl in Db.OCRLs on enteringCargo.GDNumber equals ocrl.GDNumber

                        where !Db.GateoutExports.Any(s => s.GDNumber.Trim().ToUpper() == enteringCargo.GDNumber.Trim().ToUpper())
                        &&
                        ocrl.Status == "OD"
                        select new CargoRollOverViewModel
                        {
                            GDNumber = enteringCargo.GDNumber

                        }).ToList();



            return Data;
        }

        public CargoRollOverViewModel GetGDDeatilbygdnumber(string gdnumber)
        {
            var Data = (from loadingProgram in Db.LoadingPrograms
                        join shippingAgentExport in Db.ShippingAgentExports on loadingProgram.ShippingAgentExportId equals shippingAgentExport.ShippingAgentExportId
                        //join commodity in Db.Commodities on loadingProgram.CommodityId equals commodity.CommodityId
                        join vesselExport in Db.VesselExports on loadingProgram.VesselExportId equals vesselExport.VesselExportId
                        join voyageExport in Db.VoyageExports on loadingProgram.VoyageExportId equals voyageExport.VoyageExportId
                        join opias in Db.OPIAs on loadingProgram.GDNumber equals opias.GDNumber
                        where

                        loadingProgram.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper()
                        //&&
                        //!Db.ExportContainerItems.Any(s => s.GDNumber == enteringCargo.GDNumber)
                        select new CargoRollOverViewModel
                        {
                            NoOfPackages = Convert.ToInt32(opias.NoOfPackages),
                            PackageType = opias.PackageType,
                            ClearingAgent = opias.ClearingAgentName,
                            Commodity = loadingProgram.CommodityName,
                            Shipper = opias.ConsignorName,
                            ShippingAgent = shippingAgentExport.ShippingAgentName,
                            Vessel = vesselExport.VesselName,
                            Voyage = voyageExport.VoyageNumber

                        }).FirstOrDefault();


            return Data;
        }


    }
}
