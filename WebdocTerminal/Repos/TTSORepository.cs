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
    public class TTSORepository : RepoBase<TTSO>, ITTSORepository
    {
        public TTSORepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<StuffingConfirmationViewModel> GetCCMO()
        {
            var ccmo = (from cmo in Db.CCMOs
                        join containerIndex in Db.ContainerIndices on cmo.ContainerNo equals containerIndex.ContainerNo
                         
                        where
                        cmo.VIRNo  == containerIndex.Voyage.VIRNo 
                        && cmo.IndexNo ==  containerIndex.IndexNo 
                        && cmo.BLNo  == containerIndex.BLNo  
                        && !Db.TTSOs.Any(x=> x.VIRNo == cmo.VIRNo && x.BLNo == cmo.BLNo && x.IndexNo == cmo.IndexNo && x.ContainerNo == cmo.ContainerNo)
                        select new StuffingConfirmationViewModel
                        {
                            CCMOId = cmo.CCMOId,
                            VIRNo = cmo.VIRNo,
                            BLNo = cmo.BLNo,
                            IndexNo = cmo.IndexNo, 
                            ContainerNo = cmo.ContainerNo,
                            Weight = containerIndex.BLGrossWeight
                            
                        }).Distinct().ToList();

            return ccmo;
        }
    }
}
