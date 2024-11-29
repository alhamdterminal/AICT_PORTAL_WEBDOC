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
    public class EmptyContainerGatePassRepository : RepoBase<EmptyContainerGatePass> , IEmptyContainerGatePassRepository
    {
        public EmptyContainerGatePassRepository(IUserResolveService userResolveService) : base(userResolveService) { }


      public  List<EmptyContainerGatePassViewModel> GetUnEmptyContainerGatePass()
        {
            var emptygatePass = (from emptyGateOutContainer in Db.EmptyGateOutContainers
                        join containerindex in Db.ContainerIndices on emptyGateOutContainer.ContainerIndexId equals containerindex.ContainerIndexId
                        join voyage in Db.Voyages  on containerindex.VoyageId  equals voyage.VoyageId
                        where 
                        !Db.EmptyContainerGatePasses.Any(x => x.ContainerIndexId == emptyGateOutContainer.ContainerIndexId)
                        
                        select new EmptyContainerGatePassViewModel
                        {
                            ContainerIndexId = emptyGateOutContainer.ContainerIndexId ?? 0 ,
                            ContainerCondition = emptyGateOutContainer.ContainerCondition,
                            BLNumber = containerindex.BLNo,
                            Size = containerindex.Size,
                            CreatedDate = DateTime.Now,
                            ShiftedYard = emptyGateOutContainer.DeliveredYard,
                            ContainerNumber = containerindex.ContainerNo,
                            VirNumber = voyage.VIRNo,
                            IsChecked = false

                        }).Distinct().ToList();

            return emptygatePass;

        }

     

    }
}
