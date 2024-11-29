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
    public class SCMORepository : RepoBase<SCMO>, ISCMORepository
    {
        public SCMORepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<StuffingConfirmationViewModel> GetUnAssignSCMO(string containerNumer)
        {
            var ccmo =   (from cmo in Db.CCMOs
                          join igmo in Db.IGMOs on cmo.VIRNo equals igmo.VIRNumber
                          where
                         cmo.BLNo == igmo.BLNumber
                         && cmo.IndexNo  == igmo.IndexNumber  
                         && cmo.ContainerNo == containerNumer 
                         && !Db.SCMOs.Any(x => x.BLNo == cmo.BLNo && x.ContainerNo == cmo.ContainerNo && x.VIRNo == cmo.VIRNo && x.IndexNo == cmo.IndexNo)
                         && Db.TTSOs.Any(x => x.BLNo == cmo.BLNo  && x.VIRNo == cmo.VIRNo && x.IndexNo == cmo.IndexNo)
 
                         select new StuffingConfirmationViewModel 
                              {
                                  CCMOId = cmo.CCMOId,
                                  VIRNo = cmo.VIRNo,
                                  IndexNo = cmo.IndexNo,
                                  BLNo = cmo.BLNo,
                                  ContainerNumberIGMO = igmo.ContainerNumber,
                                  ContainerNo = cmo.ContainerNo,
                                  Weight = igmo.BLGrossWeight,
                                  NoOfPackages = cmo.NoOfPackages,
                                  TPNo = cmo.TPNo,
                                  VehicleNo = cmo.VehicleNo,
                                  BondedCarrierName = cmo.BondedCarrierName,
                                  Status = false 
                         }).Distinct().ToList();

            return ccmo;
        }
    }
}
