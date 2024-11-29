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
    public class CargoReceivedRepository : RepoBase<CargoReceived>, ICargoReceivedRepository
    {
        public CargoReceivedRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }



        
        public CargoReceivedVIewModel GetCargoReceived(string lpnumber, string TruckNo)
        {
            var cargo = (from lp in Db.LoadingPrograms
                         from eg in Db.EnteringCargos.Where(e => e.LoadingProgramNumber == lpnumber).DefaultIfEmpty()
                         from cr in Db.CargoReceiveds.Where(c => c.LoadingProgramId == lp.LoadingProgramId && c.TruckNumber == TruckNo).DefaultIfEmpty()
                         where lp.LoadingProgramNumber.Trim().ToUpper() == lpnumber.Trim().ToUpper()
                         select new CargoReceivedVIewModel
                         {
                             TruckNumber = cr != null ? cr.TruckNumber : "",
                             ClearingAgent = lp != null ? lp.ClearingAgentExport.ClearingAgentName : "",
                             ClearingAgentExportId = lp.ClearingAgentExportId ?? 0,
                             LoadingProgramId = lp != null ? lp.LoadingProgramId : 0,
                             CargoReceivedId = cr.CargoReceivedId,
                             GDNumber = eg != null ? eg.GDNumber : null,
                             VehicleReceivedDate = eg != null ? eg.OPIAReceiveTime : null,
                             AgentRepresentative = cr.CLearingAgentReprsentative,
                             AgentCNIC = cr.ClearingAgentCNIC,
                             PackageType = cr.PackageType,
                             DriverName = cr.DriverName,
                             DriverCNIC = cr.DriverCNIC,
                             WeightDeclaredByDriver = cr.WeightDeclaredByDriver,
                             NumberOfPackagesReceived = cr.NumberOfPackagesReceived,
                             RecevingStartTime = cr.RecevingStartTime,
                             RecevingEndTime = cr.RecevingEndTime,
                             CargoRecevingCondition = cr.CargoRecevingCondition
                         }).FirstOrDefault();

            return cargo;

        }

        public long CargoReceivedCount()
        {
            var data = (from cargoReceived in Db.CargoReceiveds
                        select cargoReceived).ToList();
            if (data == null)
            {
                return 0;
            }
            return data.Count;
        }


        public double? CargoReceivedCBM()
        {
            double? x = 0;
            var data = (from cargo  in Db.Cargos
                        select cargo).ToList();
            if (data != null)
            {
                foreach (var item in data)
                {
                    x += item.CBM;
                }
                return x;
            }
            return x;

        }


    }
}
