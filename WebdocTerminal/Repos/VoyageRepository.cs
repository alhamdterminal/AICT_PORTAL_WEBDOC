using Microsoft.EntityFrameworkCore;
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
    public class VoyageRepository : RepoBase<Voyage> , IVoyageRepository
    {
        public VoyageRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<Voyage> GetVoyages()
        {
            var containers = (from auction in Db.Auctions
                              join containerindex in Db.ContainerIndices on auction.ContainerIndexId equals containerindex.ContainerIndexId
                              join voyage in Db.Voyages on containerindex.VoyageId equals voyage.VoyageId
                              
                              where
                              containerindex.AuctionLotNo != null
                               && !Db.GatePassAuctions.Any(s => s.ContainerIndexId == containerindex.ContainerIndexId)

                              select new Voyage
                              {
                                  VoyageId = voyage.VoyageId,
                                  BerthAt = voyage.BerthAt,
                                  BerthOn = voyage.BerthOn,
                                  VIRNo = voyage.VIRNo,
                                  VoyageNo = voyage.VoyageNo,
                                  VesselId = voyage.VesselId, 
                                   
                              }).ToList();

            return containers;
        }
        
        public List<Voyage> GetVoyagesCY()
        {
            var containers = (from auction in Db.Auctions
                              join cycontainer in Db.CYContainers on auction.CYContainerId equals cycontainer.CYContainerId
                              join voyage in Db.Voyages on cycontainer.VoyageNo equals voyage.VoyageNo
                              
                              where
                              cycontainer.VIRNo == voyage.VIRNo

                              && cycontainer.AuctionLotNo != null
                               && !Db.GatePassAuctions.Any(s => s.CYContainerId == cycontainer.CYContainerId)

                              select new Voyage
                              {
                                  VoyageId = voyage.VoyageId,
                                  BerthAt = voyage.BerthAt,
                                  BerthOn = voyage.BerthOn,
                                  VIRNo = voyage.VIRNo,
                                  VoyageNo = voyage.VoyageNo,
                                  VesselId = voyage.VesselId, 
                                   
                              }).ToList();

            return containers;
        }
        
        public List<IndexDropdownViewModel> GetContainerIndexs(string voyageNo, string IGM)
        {
            var containers = (from auction in Db.Auctions
                              join containerIndex in Db.ContainerIndices on auction.ContainerIndexId equals containerIndex.ContainerIndexId
                              join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                              
                              where
                               voyage.VoyageNo == voyageNo
                              && voyage.VIRNo == IGM
                              && containerIndex.AuctionLotNo != null
                               && !Db.GatePassAuctions.Any(s => s.ContainerIndexId == containerIndex.ContainerIndexId)

                              select new IndexDropdownViewModel
                              {
                                  ContainerIndexId = containerIndex.ContainerIndexId,
                                  IndexNo = containerIndex.IndexNo ?? 0
                              }).ToList();

            return containers;
        }
        
        
        public List<CYContainer> GetCYContainers(string IGM)
        {
            var containers = (from auction in Db.Auctions
                              join cycontainer in Db.CYContainers on auction.CYContainerId equals cycontainer.CYContainerId
                               
                              where 
                               cycontainer.VIRNo == IGM
                              && cycontainer.AuctionLotNo != null
                               && !Db.GatePassAuctions.Any(s => s.CYContainerId == cycontainer.CYContainerId)

                              select cycontainer).ToList();

            return containers;
        }

        public List<Voyage> GetVoyageIGMSList()
        {
            var res = Db.Voyages.FromSql($"select * from Voyage where IsDeleted = 0").ToList();

            return res;
        }


        public Voyage GetVoyageByIGM(string virno)
        {
            var res = Db.Voyages.FromSql($"select * from Voyage where VIRNo = {  virno } and IsDeleted = 0").FirstOrDefault();

            return res;
        }

        public List<string> GetPreAlertVesselName()
        {
            var res = Db.PreAlerts.FromSql($"select * from PreAlert where IsDeleted = 0").Select(v => v.Vessel).ToList();

            return res;
        }
        public List<string> GetPreAlertVoyage(string vesselname)
        {
            var res = Db.PreAlerts.FromSql($"select * from PreAlert  where  Vessel = {vesselname}  and  IsDeleted = 0").Select(v => v.Voyage).ToList();

            return res;
        }

    }
}
