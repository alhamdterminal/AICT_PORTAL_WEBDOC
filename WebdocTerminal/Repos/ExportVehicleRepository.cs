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
    public class ExportVehicleRepository : RepoBase<ExportVehicle> , IExportVehicleRepository
    {
        public ExportVehicleRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<ExportVehicle> GetTurckNumbersBYLPNo(string lpNumber) {
            var data = (from lp in Db.LoadingPrograms
                        join ec in Db.EnteringCargos on lp.LoadingProgramNumber equals ec.LoadingProgramNumber
                        join ev in Db.ExportVehicles on ec.EnteringCargoId equals ev.EnteringCargoId
                        where lp.LoadingProgramNumber == lpNumber
                        select new ExportVehicle
                        {
                            VehicleNumber = ev.VehicleNumber
                        }).ToList();
            return data;
        }
    }
}
