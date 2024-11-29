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
    public class GateOutRepository : RepoBase<GateOut>, IGateOutRepository
    {
        public GateOutRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public long GateOutContainersCountCFS()
        {
            var gateOutCountainer = (from emptyoutcontainer in Db.EmptyGateOutContainers
                                      select emptyoutcontainer
                         ).ToList();
            if (gateOutCountainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return gateOutCountainer.Count;
            }
        }
        
        public long GateOutContainersIndexCountCFS()
        {
            var gateOutCountainerIndexs = (from containerIndex in Db.ContainerIndices
                                      where containerIndex.IsGateOut == true
                                     select containerIndex
                         ).ToList();
            if (gateOutCountainerIndexs.Count == 0)
            {
                return 0;
            }

            else
            {
                return gateOutCountainerIndexs.Count;
            }
        }

        public long GateOutContainersCountCY()
        {
            var gateOutCountainer = (from CYContainer in Db.CYContainers
                                     join GateOut in Db.GateOuts on CYContainer.CYContainerId equals GateOut.CYContainerId
                                     where CYContainer.IsGateOut == true
                                     select CYContainer

                         ).ToList();
            if (gateOutCountainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return gateOutCountainer.Count;
            }
        }

    }
}
