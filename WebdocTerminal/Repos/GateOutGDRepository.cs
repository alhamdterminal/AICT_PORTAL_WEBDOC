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
    public class GateOutGDRepository : RepoBase<GateOutGD> , IGateOutGDRepository
    {

        public GateOutGDRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<GateOutGD> GetGateOutGDs()
        {

            var data = (from GatePassExport in Db.GatePassExports
                        join ocrl in Db.OCRLs on GatePassExport.GDNumber equals ocrl.GDNumber
                        join opia in Db.OPIAs on GatePassExport.GDNumber equals opia.GDNumber
                        join doitem in Db.DOItemExports on GatePassExport.GatePassExportId equals doitem.GatePassExportId
                        where
                        ocrl.Status == "OD"
                        &&
                        !Db.GateOutGDs.Any(x => x.GDNumber == GatePassExport.GDNumber)
                        select new GateOutGD
                        {
                            GDNumber = opia.GDNumber,
                            NumberOfPackages = Convert.ToInt32(opia.NoOfPackages),
                            PackageType = opia.PackageType,
                            Weight = opia.GrossWeight,
                            VehicleNumber = doitem.TruckNumber,
                            IsGateOut = false
                        }).ToList();

            return data;

        }




    }
}
