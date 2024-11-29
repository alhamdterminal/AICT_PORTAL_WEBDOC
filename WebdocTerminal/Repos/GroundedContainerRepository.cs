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
    public class GroundedContainerRepository : RepoBase<GroundedContainer>, IGroundedContainerRepository
    {
        public GroundedContainerRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<GroundingStatusViewModel> GrounedContainers()
        {
            var containers = (from containerIndex in Db.ContainerIndices
                              join srco in Db.SRCOs on containerIndex.BLNo equals srco.BLNumber
                              where srco.HandlingCode == "EM"
                              select new GroundingStatusViewModel
                              {
                                  ContainerIndexId = containerIndex.ContainerIndexId,
                                  BLNumber = containerIndex.BLNo,
                                  ContainerNumber = containerIndex.ContainerNo,
                                  VIRNumber = srco.VIRNumber,
                                  IndexNumber = srco.IndexNumber,
                                  ActivityStatus = srco.ActivityType,
                                  HandlingCode = srco.HandlingCode,
                                  Location = srco.Location

                              }).ToList();

            return containers;
        }


        public List<GroundingStatusViewModel> GroundingStatusCFS()
        {
            var containers = (from containerIndex in Db.ContainerIndices
                              from srco in Db.SRCOs.Where(x => x.BLNumber == containerIndex.BLNo && x.IndexNumber == containerIndex.IndexNo && x.VIRNumber == containerIndex.Voyage.VIRNo).DefaultIfEmpty()
                              join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId 
                              join ihco in Db.IHCOs on containerIndex.BLNo equals ihco.BLNumber
                              where 
                              ihco.IndexNumber == containerIndex.IndexNo
                              && ihco.VIRNumber == voyage.VIRNo
                              select new GroundingStatusViewModel
                              {
                                  ContainerIndexId = containerIndex.ContainerIndexId,
                                  BLNumber = containerIndex.BLNo,
                                  ContainerNumber = containerIndex.ContainerNo,
                                  VIRNumber = ihco.VIRNumber,
                                  IndexNumber = ihco.IndexNumber, 
                                  ActivityStatus = srco != null ? srco.ActivityType : "", 
                                  HandlingCode = ihco.HandlingCode,
                                  Performed = ihco.Performed != null  ? ihco.Performed : DateTime.Now

                              }).ToList();

            return containers;
        }


        public List<GroundingStatusViewModel> GrounedCYContainers()
        {
            var containers = (from container in Db.CYContainers
                              join src in Db.SRCs on container.ContainerNo equals src.ContainerNumber
                              where src.HandlingCode == "EM"
                              select new GroundingStatusViewModel
                              {
                                  CyContainerId = container.CYContainerId,
                                  ContainerNumber = container.ContainerNo,
                                  VIRNumber = src.VIRNumber,
                                  ActivityStatus = src.ActivityType,
                                  HandlingCode = src.HandlingCode,
                                  ServiceStatus = src.Status,
                                  Location = src.Location

                              }).ToList();

            return containers;
        }

        public long GrounedContainersCountCFS()
        {
            var grounedCountainer = (from srco in Db.SRCOs
                                     select srco
                         ).ToList();
            if (grounedCountainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return grounedCountainer.Count;
            }
        }

        public long GrounedContainersCountCY()
        {
            var grounedCountainer = (from src in Db.SRCs  
                                     select src
                         ).ToList();
            if (grounedCountainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return grounedCountainer.Count;
            }
        }

        public  long GroundingAwaitedCY()
        {


            var groundingAwaitedContainer = (from container in Db.CYContainers
            join ihc in Db.IHCs on container.ContainerNo equals ihc.ContainerNumber
              where container.IsGrounded == false
                              && container.IsGateIn == true
                              && ihc.HandlingCode == "EM"
                              && container.VIRNo == ihc.VIRNumber
                                             select container ).ToList();
            if (groundingAwaitedContainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return groundingAwaitedContainer.Count;
            }

        }


        public long GroundingAwaitedCFS()
        {
            var groundingAwaitedContainer = (from containerIndex in Db.ContainerIndices
                                              join ihco in Db.IHCOs on containerIndex.BLNo equals ihco.BLNumber
                                             join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                                              where
                                             containerIndex.IsGrounded == false
                                             && containerIndex.IsGateIn == true
                                             && containerIndex.IndexNo == ihco.IndexNumber
                                             && voyage.VIRNo == ihco.VIRNumber
                                             && ihco.HandlingCode == "EM"
                                             && containerIndex.Status.ToUpper() == "CFS"
                                             select containerIndex
                        ).ToList();
            if (groundingAwaitedContainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return groundingAwaitedContainer.Count;
            }

        }


        public long CountTotalIHCOEm()
        {
            var res = Db.IHCOs.FromSql($"select  * from IHCO where IsDeleted = 0 and HandlingCode = 'EM'").ToList();

            return res.Count();


        }

        public long CountTotalSRCO()
        {
            var res = Db.SRCOs.FromSql($"select  * from SRCO where IsDeleted = 0 ").ToList();

            return res.Count();
             
        }

        public long CountTotalECMO()
        {
            var res = Db.ECMOs.FromSql($"select  * from ECMO where IsDeleted = 0 ").ToList();

            return res.Count();

        }

        public bool GetFileNameSRC(string FileName)
        {
            var res = Db.SRCs.FromSql($"select  * from SRC where IsDeleted = 0  and FileName = {FileName} ").ToList();

            if (res.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool GetFileNameSRCO(string FileName)
        {
            var res = Db.SRCOs.FromSql($"select  * from SRCO where IsDeleted = 0  and FileName = {FileName} ").ToList();

            if (res.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<SRCO> GetUndeliveredSRCO(string virno, long indexno)
        {

            var containers = (from srco in Db.SRCOs 
                              where
                              !Db.GTOOs.Any(x => x.VIRNumber == srco.VIRNumber && x.BLNumber == srco.BLNumber)
                               && srco.VIRNumber == virno
                               && srco.IndexNumber == indexno
                              select new SRCO
                              {
                                  SRCOId = srco.SRCOId,
                                  VIRNumber = srco.VIRNumber,
                                  BLNumber = srco.BLNumber,
                                  IndexNumber = srco.IndexNumber, 
                                  ActivityType = srco.ActivityType, 
                                  Location = srco.Location, 
                                  Weight = srco.Weight, 
                                  HandlingCode = srco.HandlingCode   
                              }).Distinct().ToList();
             
            return containers;
        }

        public List<SRC> GetUndeliveredSRC(string virno, string containerno)
        {

            var containers = (from src in Db.SRCs
                              where
                              !Db.GTOs.Any(x => x.VIRNumber == src.VIRNumber && x.ContainerNumber == src.ContainerNumber)
                               && src.VIRNumber == virno
                               && src.ContainerNumber == containerno
                              select new SRC
                              {
                                  SRCId = src.SRCId,
                                  VIRNumber = src.VIRNumber,
                                  ContainerNumber = src.ContainerNumber,
                                  ActivityType = src.ActivityType,
                                  Location = src.Location,
                                  Weight = src.Weight,
                                  Status = src.Status,
                                  HandlingCode = src.HandlingCode
                              }).Distinct().ToList();

            return containers;
        }

    }
}
