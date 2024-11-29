using Microsoft.EntityFrameworkCore;
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
    public class ExportGroundedCargoRepository : RepoBase<ExportGroundedCargo>, IExportGroundedCargoRepository
    {
        public ExportGroundedCargoRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<ExportGroundingCargoViewModel> GetUngroundedCargos()
        {
            var cargos = (from enteringCargos in Db.EnteringCargos
                          join oehc in Db.OEHCs on enteringCargos.GDNumber.Trim().ToUpper() equals oehc.GDNumber.Trim().ToUpper()
                          where enteringCargos.isGrounded == false
                          where oehc.HandlingCode == "EM"
                          select new ExportGroundingCargoViewModel
                          {
                              EnteringCargoId = enteringCargos.EnteringCargoId,
                              GDNumber = enteringCargos.GDNumber,
                              ActivityType = "GROUNDED",
                              Weight = Convert.ToInt32((from loadingProgram in Db.LoadingPrograms
                                                        join loadingProgramDetail in Db.LoadingProgramDetails on loadingProgram.LoadingProgramId equals loadingProgramDetail.LoadingProgramId
                                                        where loadingProgram.GDNumber == enteringCargos.GDNumber
                                                        select loadingProgramDetail
                                        ).Sum(s => s.Weight) ?? 0),
                              IsChecked = false
                          }).Distinct().ToList();

            return cargos;
        }

        public EnteringCargo GetEnteringCargoInfo(string gdnumber)
        {

            var asd = Db.EnteringCargos.FromSql($"select * from EnteringCargo where GDNumber = {gdnumber}  and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
    }
}
