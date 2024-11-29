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
    public class GDHoldRepository : RepoBase<GDHold> , IGDHoldRepository
    {
        public GDHoldRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public GDHoldDetailViewModel GetCargoDetail(string gdnumber)
        {

            var data = (from enteringCargo in Db.EnteringCargos
                        join loadingProgram in Db.LoadingPrograms on enteringCargo.LoadingProgramNumber equals loadingProgram.LoadingProgramNumber
                        join clearingAgentExport in Db.ClearingAgentExports on loadingProgram.ClearingAgentExportId equals clearingAgentExport.ClearingAgentExportId
                        from gdhold in Db.GDHolds.Where(h=> h.EnteringCargoId == enteringCargo.EnteringCargoId).DefaultIfEmpty()
                        where enteringCargo.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper()
                        select new GDHoldDetailViewModel
                        {
                            EnteringCargoId = enteringCargo.EnteringCargoId,
                            ClearingAgent = clearingAgentExport.ClearingAgentName,
                            //ShipperName = loadingProgram.Shipper.ShipperName,
                            ShipperName = "",
                            NoOfPackages = enteringCargo.NumberOfPackages,
                            PackageType = enteringCargo.PackageType,
                            GDHoldId = gdhold != null ? gdhold.GDHoldId : 0,
                        }).FirstOrDefault();

            return data;
        }

        public long GetGDHold(string gdNumber)
        {
            var data = (from enteringCargo in Db.EnteringCargos
                        from hold in Db.GDHolds.Where(h=> h.EnteringCargoId == enteringCargo.EnteringCargoId).DefaultIfEmpty()
                        where enteringCargo.GDNumber.Trim().ToUpper() == gdNumber.Trim().ToUpper() && enteringCargo.IsHold == true
                        select hold.GDHoldId ).FirstOrDefault();
            return data;
        }

        public long GetGDHoldByEnteringCargoId(long EnteringCargoId)
        {
            var data = (from enteringCargo in Db.EnteringCargos
                         from hold in Db.GDHolds.Where(h => h.EnteringCargoId == enteringCargo.EnteringCargoId).DefaultIfEmpty()
                        where enteringCargo.EnteringCargoId == EnteringCargoId
                        select hold.GDHoldId).FirstOrDefault();
            return data;
        }

        public long GDHoldCount()
        {
            var data = (from gdHold in Db.GDHolds
                        select gdHold).ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }

        
        public GDHoldDetailViewModel GetCargoDetailByGD(string gdnumber)
        {

            var data = (from loadingProgram in Db.LoadingPrograms
                        join opia in Db.OPIAs on loadingProgram.GDNumber equals opia.GDNumber
                        join enteringCargo in Db.EnteringCargos on loadingProgram.GDNumber equals enteringCargo.GDNumber
                        where loadingProgram.GDNumber.Trim().ToUpper() == gdnumber.Trim().ToUpper()
                        select new GDHoldDetailViewModel
                        {
                            EnteringCargoId = enteringCargo.EnteringCargoId,
                            ClearingAgent = opia.ClearingAgentName,
                            ShipperName = opia.CongisneeName,
                            NoOfPackages = Convert.ToInt32(opia.NoOfPackages),
                            PackageType = opia.PackageType,

                        }).FirstOrDefault();

            if (data != null)
            {
                var gdholddata = Db.GDHolds.FromSql($"select * from GDHold where EnteringCargoId = {data.EnteringCargoId}  and IsDeleted = 0").LastOrDefault();

                if (gdholddata != null)
                {
                    data.Remarks = gdholddata.Remarks;
                }

            }




            return data;
        }


    }
}
