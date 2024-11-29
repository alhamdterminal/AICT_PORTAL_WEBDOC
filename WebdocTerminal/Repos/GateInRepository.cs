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
    public class GateInRepository : RepoBase<GateIn>, IGateInRepository
    {
        public GateInRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public long CountGateInContainerCFS()
        {
            var gatein = (from container in Db.ContainerIndices
                          where
                        container.IsGateIn == true
                          select new ContainerIndex { 
                          ContainerNo  = container.ContainerNo,
                          VoyageId = container.VoyageId
                          }).Distinct().ToList();

            return gatein.Count;
        }
        public long CountGateInContainerIndexCFS()
        {
            var gateinIndex = (from container in Db.ContainerIndices
                           where 
                         container.IsGateIn == true
                          select container).ToList();

            return gateinIndex.Count;
        }

        public long CountGateInContainerCY()
        {
            var gatein = (from CYContainer in Db.CYContainers
                          where CYContainer.IsGateIn == true 
                          select CYContainer
                         ).ToList();

            return gatein.Count;
        }

        public long CountUpcommingContainerCFS()
        {
            var upComingContainer = (

                                from IGMO in Db.IGMOs
                                join Container in Db.ContainerIndices on IGMO.ContainerNumber equals Container.ContainerNo
                                where
                                Container.IsGateIn == false
                                group new { Container.ContainerNo } by new { Container.ContainerNo } into gruopAC
                                let countCategory = gruopAC.Count()
                                orderby countCategory
                                select countCategory

                         ).ToList();

            return upComingContainer.Count;
        }

        public long CountUpcommingContainerCY()
        {
            var upComingContainerCY = (from IGMO in Db.IGMOs
                                       join CYContainer in Db.CYContainers on IGMO.ContainerNumber equals CYContainer.ContainerNo
                                       where
                                       CYContainer.IsGateIn == false && CYContainer.VIRNo == IGMO.VIRNumber && CYContainer.BLNo == IGMO.BLNumber
                                       && CYContainer.IndexNo == IGMO.IndexNumber
                                       group new { CYContainer.ContainerNo } by new { CYContainer.ContainerNo } into gruopAC
                                       let countCategory = gruopAC.Count()
                                       orderby countCategory
                                       select countCategory
                                        ).ToList();

            return upComingContainerCY.Count;
        }

    }


}
