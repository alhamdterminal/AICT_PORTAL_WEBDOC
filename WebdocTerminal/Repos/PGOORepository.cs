using System.Collections.Generic;
using System.Linq;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class PGOORepository : RepoBase<PGOO>, IPGOORepository
    {
        public PGOORepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<StuffingConfirmationViewModel> GetUnAssignPGOO()
        {
            var ccmo = (from cmo in Db.CCMOs
                        join crlo in Db.CRLOs on cmo.VIRNo equals crlo.VIRNumber
                        where
                        crlo.Status == "BR"
                        && cmo.BLNo == crlo.BLNumber
                        && cmo.IndexNo == crlo.IndexNumber
                        && !Db.PGOOs.Any(x =>  x.ContainerNo == cmo.ContainerNo  )
                        && Db.SCMOs.Any(x => x.ContainerNo == cmo.ContainerNo && x.VIRNo == cmo.VIRNo)
                        select new StuffingConfirmationViewModel
                        {
                            //  CCMOId = cmo.CCMOId,
                            BondedCarrierId = cmo.BondedCarrierId,
                            BondedCarrierName = cmo.BondedCarrierName,
                            ContainerNo = cmo.ContainerNo,
                            VehicleNo = cmo.VehicleNo,
                           // VIRNo = cmo.VIRNo,
                            // BLNo = cmo.BLNo ,
                            // IndexNo = cmo.IndexNo
                        }).Distinct().ToList();

            return ccmo;
        }
    }
}
