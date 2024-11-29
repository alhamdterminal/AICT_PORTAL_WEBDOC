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
    public class CargoRepository : RepoBase<Cargo>, ICargoRepository
    {

        public CargoRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public CargoViewModel GetCargoInformation (string gdNumber, string lpNumber)
        {
            var data = (from enteringCargo in Db.EnteringCargos
                        join loadingProgram in Db.LoadingPrograms on enteringCargo.LoadingProgramNumber equals loadingProgram.LoadingProgramNumber
                        join clearingAgentExport in Db.ClearingAgentExports on loadingProgram.ClearingAgentExportId equals clearingAgentExport.ClearingAgentExportId
                        where enteringCargo.GDNumber.Trim().ToUpper() == gdNumber.Trim().ToUpper() && enteringCargo.LoadingProgramNumber == lpNumber
                        select new CargoViewModel
                        {

                            ClearingAgentName = clearingAgentExport.ClearingAgentName,
                            //ShipperName = loadingProgram.Shipper.ShipperName,
                            ShipperName = "",
                            ConsigneeName = enteringCargo.ConsigneeName,

                        }).FirstOrDefault();

            return data;
        }

        public  CargoViewModel  GetCargoInfo(string gdNumber, string lpNumber, long lpDetailId)
        {
 

            var cargo = (from loadingProgram in Db.LoadingPrograms
                         join clearingAgentExport in Db.ClearingAgentExports on loadingProgram.ClearingAgentExportId equals clearingAgentExport.ClearingAgentExportId
                         from enteringCargo in Db.EnteringCargos.Where(ec => ec.GDNumber.Trim().ToUpper() == gdNumber.Trim().ToUpper() && ec.LoadingProgramNumber == lpNumber).DefaultIfEmpty()
                         from carg in Db.Cargos.Where(c => c.GDNumber.Trim().ToUpper() == gdNumber.Trim().ToUpper() && c.LoadingProgramDetailId == lpDetailId).DefaultIfEmpty()
                         where loadingProgram.LoadingProgramNumber.Trim().ToUpper() == lpNumber.Trim().ToUpper()  
                         select new CargoViewModel
                         {
                             ConsigneeName = enteringCargo != null ? enteringCargo.ConsigneeName : "",
                             VehicleNumber = string.Join(",", Db.CargoReceiveds.Where(x=>x.LoadingProgramId == loadingProgram.LoadingProgramId).Select(x=>  x.TruckNumber)), 
                             ClearingAgentName = clearingAgentExport != null ? clearingAgentExport.ClearingAgentName : "",
                             //ShipperName = loadingProgram != null ? loadingProgram.Shipper.ShipperName : "",
                             // PackagesReceived =  Db.CargoReceiveds.Where(x => x.LoadingProgramId == loadingProgram.LoadingProgramId).Sum(x => x.NumberOfPackagesReceived),
                             PackagesReceived = enteringCargo!= null ? enteringCargo.NumberOfPackages : 0,
                             //PortAndterminalId = loadingProgram != null ? loadingProgram.PortAndTerminalId : 0,
                             FinalDestination = carg != null ? carg.FinalDestination : "",
                             DischargePort = carg != null ? carg.DishargePort : "",
                             VesselExportId = loadingProgram != null ? loadingProgram.VesselExportId : 0,
                             VoyageExportId = loadingProgram != null ? loadingProgram.VoyageExportId : 0,
                             CommodityId = carg != null ? carg.CommodityId : 0,
                             PLIDNumber = carg != null ? carg.PLIDNumber : "",
                             WarehouseLocation = carg != null ? carg.WarehouseLocation : "",
                             CBM = carg != null ? carg.CBM : 0,
                             Weight = carg != null ? carg.Weight : 0,
                             Location = carg != null ? carg.Location : "",
                             ColorCode = carg != null ? carg.ColorCode : "",
                             PackageReceived = carg != null ? carg.PackageReceived : 0,
                             CargoId = carg != null ? carg.CargoId : 0,
                             PackageType = Db.CargoReceiveds.Where(x=> x.LoadingProgramId == loadingProgram.LoadingProgramId).Select(x=>x.PackageType).FirstOrDefault(),
                              CargoReceivedDate = Db.CargoReceiveds.Where(x => x.LoadingProgramId == loadingProgram.LoadingProgramId).Select(x => x.RecevingStartTime).FirstOrDefault(),
                             LoadingProgramDetailId = lpDetailId

                         }).FirstOrDefault();


          
            return (cargo);
        }


        public List<string> GetGDsForTerminalReceipt(long vesselId, long voyageId)
        {
            var data = (from cargo in Db.Cargos
                        join vesselExport in Db.VesselExports on cargo.VesselExportId equals vesselExport.VesselExportId
                        join voyageExport in Db.VoyageExports on cargo.VoyageExportId equals voyageExport.VoyageExportId
                        from gateoutExport in Db.GateoutExports.Where(x => x.GDNumber == cargo.GDNumber).Distinct()
                        where
                        vesselExport.VesselExportId == vesselId && voyageExport.VoyageExportId == voyageId
                        select  gateoutExport.GDNumber
                        
                        ).ToList();

            return data;
        }




    }
}
