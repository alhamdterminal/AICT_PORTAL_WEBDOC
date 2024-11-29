using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class ContainerRepository : RepoBase<WebdocTerminal.Models.ContainerIndex>, IContainerRepository
    {
        private ICYContainerRepository _cycontainerRepo;
        private IDictionaryRepository _dictionaryRepo;
        private IHoldRepository _holdRepo;
        private static IConfiguration _configuration;
        public string Con { get; set; }
        public ContainerRepository(IUserResolveService userResolveService,
            ICYContainerRepository cycontainerRepo,
            IDictionaryRepository dictionaryRepo,
            IHoldRepository holdRepo, IConfiguration configuration) : base(userResolveService)
        {
            _cycontainerRepo = cycontainerRepo;
            _dictionaryRepo = dictionaryRepo;
            _holdRepo = holdRepo;
            _configuration = configuration;
            Con =  _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }

        public WebdocTerminal.Models.ContainerIndex GetContainerByVIR(string VIR)
        {
            var cntr = (from voyage in Db.Voyages
                        join container in Db.ContainerIndices on voyage.VoyageId equals container.VoyageId
                        where voyage.VIRNo == VIR
                        select container).FirstOrDefault();

            return cntr;
        }
        public List<ContainerIndex> GetIndexes(string VIR, int IndexNo, string BlNumber)
        {
            var cntr = (from voyage in Db.Voyages
                        join container in Db.ContainerIndices on voyage.VoyageId equals container.VoyageId
                        where voyage.VIRNo == VIR && container.IndexNo == IndexNo && container.BLNo == BlNumber && voyage.VoyageId == container.VoyageId
                        select container).ToList();

            return cntr;
        }

        public List<CYContainer> GetCyAuctionContainers()
        {
            var containers = (from cycontainer in Db.CYContainers
                              join gatein in Db.GateIns on cycontainer.ContainerNo equals gatein.ContainerNo
                              where cycontainer.IsGateIn == true && cycontainer.IsGateOut == false && cycontainer.IsHold == false
                                    && EF.Functions.DateDiffDay(gatein.GateInDateTime, DateTime.Now).Value > 90
                              select cycontainer).ToList();

            return containers;
        }

        public List<ContainerIndex> GetCFSAuctionIndexes()
        {
            var containers = (from containerindex in Db.ContainerIndices
                              join gatein in Db.GateIns on containerindex.ContainerNo equals gatein.ContainerNo
                              where containerindex.IsGateIn == true && containerindex.IsGateOut == false
                                    && EF.Functions.DateDiffDay(DateTime.Now, gatein.GateInDateTime).Value > 90
                              select containerindex).ToList();

            return containers;
        }

        public List<GateInViewModel> GetGateinCYContainers()
        {
            var containers = (from ipao in Db.IPAOs
                              join cyContainer in Db.CYContainers on ipao.ContainerNumber equals cyContainer.ContainerNo
                              where cyContainer.IsGateIn == false &&
                              cyContainer.Status.ToUpper() == "CY" &&
                              cyContainer.IsDeleted == false &&
                              cyContainer.VIRNo == ipao.VIRNumber
                              select new GateInViewModel
                              {
                                  ContainerId = cyContainer.CYContainerId,
                                  ContainerNo = cyContainer.ContainerNo,
                                  CarrierId = ipao.BondedCarrierId,
                                  CarrierName = ipao.BondedCarrierName,
                                  Weight = cyContainer.ContainerGrossWeight,
                                  VehicleNo = ipao.VehicleNumber,
                                  PACSSSealNo = ipao.PCCSSSealNumber,
                                  VIRNo = ipao.VIRNumber,
                                  GateInDateTime = DateTime.Now,
                                  MenifestedSealNo = cyContainer.ManifestedSealNumber,
                                  ISOCode = cyContainer.ISOCode,
                                  IsGateIn = false,
                              }).ToList();

            return containers;
        }

        public List<GateInViewModel> GetGateinCFSContainers()
        {
            var containers = (from ipao in Db.IPAOs
                              join container in Db.ContainerIndices on ipao.ContainerNumber equals container.ContainerNo
                              join voyage in Db.Voyages on container.VoyageId equals voyage.VoyageId
                              from isocode in Db.ISOCodes.Where(x => x.Code == container.ISOCode).DefaultIfEmpty()
                              where
                              container.IsGateIn == false
                              &&
                              container.Status.ToUpper() == "CFS"
                              && container.IsDeleted == false
                              && container.VirNo == ipao.VIRNumber

                              select new GateInViewModel
                              {
                                  Key = container.VirNo + "-" + container.ContainerNo,
                                  //ContainerId = container.ContainerIndexId,
                                  ContainerNo = container.ContainerNo,
                                  ContainerTypeStatus = ipao.MessageFrom,
                                  GateInDateTime = DateTime.Now,
                                  CarrierId = ipao.BondedCarrierId,
                                  CarrierName = ipao.BondedCarrierName,
                                  ShippingAgentId = container != null ? container.ShippingAgentId : null,
                                  Status = container.Status,
                                  Weight = container.ContainerGrossWeight,
                                  ContainerSize = isocode != null ? isocode.ContainerSize : "",
                                  VoyageId = voyage.VoyageId,
                                  VehicleNo = ipao.VehicleNumber,
                                  PACSSSealNo = ipao.PCCSSSealNumber,
                                  VIRNo = ipao.VIRNumber,
                                  MenifestedSealNo = container.ManifestedSealNumber,
                                  ISOCode = container.ISOCode,
                                  TransporterId = container.TransporterId,
                                  IsGateIn = false
                              }).ToList();

            var containerMerged = (from container in containers
                                   group container by container.ContainerNo into g
                                   select new GateInViewModel
                                   {
                                       ContainerNo = g.Key,
                                       Key = g.FirstOrDefault().Key,
                                       ContainerTypeStatus = g.FirstOrDefault().ContainerTypeStatus,
                                       GateInDateTime = g.FirstOrDefault().GateInDateTime,
                                       CarrierId = g.FirstOrDefault().CarrierId,
                                       CarrierName = g.FirstOrDefault().CarrierName,
                                       ContainerId = g.FirstOrDefault().ContainerId,
                                       ShippingAgentId = g.FirstOrDefault().ShippingAgentId,
                                       Status = g.FirstOrDefault().Status,
                                       Weight = g.Sum(s => s.Weight),
                                       ContainerSize = g.FirstOrDefault().ContainerSize,
                                       VoyageId = g.FirstOrDefault().VoyageId,
                                       VehicleNo = g.FirstOrDefault().VehicleNo,
                                       PACSSSealNo = g.FirstOrDefault().PACSSSealNo,
                                       VIRNo = g.FirstOrDefault().VIRNo,
                                       MenifestedSealNo = g.FirstOrDefault().MenifestedSealNo,
                                       ISOCode = g.FirstOrDefault().ISOCode,
                                       TransporterId = g.FirstOrDefault().TransporterId,
                                       IsGateIn = g.FirstOrDefault().IsGateIn
                                   }).ToList();



            var cycontainers = (from ipao in Db.IPAOs
                                join cyContainer in Db.CYContainers on ipao.ContainerNumber equals cyContainer.ContainerNo
                                from isocode in Db.ISOCodes.Where(x => x.Code == cyContainer.ISOCode).DefaultIfEmpty()

                                where cyContainer.IsGateIn == false &&
                              cyContainer.Status.ToUpper() == "CY" &&
                              cyContainer.IsDeleted == false &&
                              cyContainer.VIRNo == ipao.VIRNumber
                                select new GateInViewModel
                                {
                                    Key = cyContainer.VIRNo + "-" + cyContainer.ContainerNo,
                                    ContainerNo = cyContainer.ContainerNo,
                                    ContainerTypeStatus = ipao.MessageFrom,
                                    GateInDateTime = DateTime.Now,
                                    CarrierId = ipao.BondedCarrierId,
                                    CarrierName = ipao.BondedCarrierName,
                                    // ContainerId = cyContainer.CYContainerId,
                                    ShippingAgentId = cyContainer != null ? cyContainer.ShippingAgentId : null,
                                    Status = cyContainer.Status,
                                    Weight = cyContainer.ContainerGrossWeight,
                                    ContainerSize = isocode != null ? isocode.ContainerSize : "",
                                    VehicleNo = ipao.VehicleNumber,
                                    PACSSSealNo = ipao.PCCSSSealNumber,
                                    VIRNo = ipao.VIRNumber,
                                    MenifestedSealNo = cyContainer.ManifestedSealNumber,
                                    TransporterId = cyContainer.TransporterId,
                                    ISOCode = cyContainer.ISOCode,
                                    IsGateIn = false,
                                }).ToList();


            var cycontainerMerged = (from container in cycontainers
                                     group container by container.ContainerNo into g
                                     select new GateInViewModel
                                     {
                                         ContainerNo = g.Key,
                                         Key = g.FirstOrDefault().Key,
                                         ContainerTypeStatus = g.FirstOrDefault().ContainerTypeStatus,
                                         GateInDateTime = g.FirstOrDefault().GateInDateTime,
                                         CarrierId = g.FirstOrDefault().CarrierId,
                                         CarrierName = g.FirstOrDefault().CarrierName,
                                         ContainerId = g.FirstOrDefault().ContainerId,
                                         ShippingAgentId = g.FirstOrDefault().ShippingAgentId,
                                         Status = g.FirstOrDefault().Status,
                                         Weight = g.Sum(s => s.Weight),
                                         ContainerSize = g.FirstOrDefault().ContainerSize,
                                         VehicleNo = g.FirstOrDefault().VehicleNo,
                                         PACSSSealNo = g.FirstOrDefault().PACSSSealNo,
                                         VIRNo = g.FirstOrDefault().VIRNo,
                                         MenifestedSealNo = g.FirstOrDefault().MenifestedSealNo,
                                         ISOCode = g.FirstOrDefault().ISOCode,
                                         TransporterId = g.FirstOrDefault().TransporterId,
                                         IsGateIn = g.FirstOrDefault().IsGateIn
                                     }).ToList();


            containerMerged.AddRange(cycontainerMerged);



            return containerMerged;
        }


        public List<GTTOViewModel> GetTPContainers()
        {
            var dateTime = DateTime.Now;

            var containers = (from svmo in Db.SVMOs
                              join scmo in Db.SCMOs on svmo.ContainerNo equals scmo.ContainerNo
                              where
                              !Db.GTTOs.Any(x => x.ContainerNo == svmo.ContainerNo && x.VIRNumber == svmo.VIRNo)
                               && scmo.VIRNo == svmo.VIRNo
                              select new GTTOViewModel
                              {
                                  Key = string.Concat(svmo.ContainerNo, "-", svmo.VIRNo, "-", svmo.VehicleNo),
                                  SVMOId = svmo.SVMOId,
                                  Category = "I",
                                  VIRNo = svmo.VIRNo,
                                  ContainerNo = svmo.ContainerNo,
                                  IndexNo = scmo.IndexNo,
                                  GateOutTime = dateTime,
                                  Status = "F",
                                  VehicleNo = svmo.VehicleNo,
                                  PCCSSSealNumber = svmo.PCCSSSealNumber,
                                  BondedCarrier = svmo.BondedCarrierName,
                                  CountryOfExit = "PK",
                                  //GrossWeight = Db.SCMOs.Where(s => s.ContainerNo  == svmo.ContainerNo && s.VIRNo == svmo.VIRNo).Sum(s => s.Weight),
                                  IsGateOut = false
                              }).Distinct().ToList();



            var containerMerged = (from container in containers
                                   group container by container.ContainerNo into g

                                   select new GTTOViewModel
                                   {
                                       Key = g.FirstOrDefault().Key,
                                       SVMOId = g.FirstOrDefault().SVMOId,
                                       Category = g.FirstOrDefault().Category,
                                       VIRNo = g.FirstOrDefault().VIRNo,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       IndexNo = g.FirstOrDefault().IndexNo,
                                       GateOutTime = g.FirstOrDefault().GateOutTime,
                                       Status = g.FirstOrDefault().Status,
                                       VehicleNo = g.FirstOrDefault().VehicleNo,
                                       PCCSSSealNumber = g.FirstOrDefault().PCCSSSealNumber,
                                       BondedCarrier = g.FirstOrDefault().BondedCarrier,
                                       CountryOfExit = g.FirstOrDefault().CountryOfExit,
                                       IsGateOut = g.FirstOrDefault().IsGateOut
                                   }).ToList();

            return containerMerged;
        }


        public List<CYGateOutViewModel> GetGateoutCYContainers()
        {

            var containersrf = (from cyContainer in this.Db.CYContainers
                                join gatein in this.Db.GateIns on cyContainer.ContainerNo equals gatein.ContainerNo
                                // join ipao in Db.IPAOs on gatein.ContainerNo equals ipao.ContainerNumber
                                join crl in Db.CRLs on cyContainer.ContainerNo equals crl.ContainerNumber
                                join gatePass in Db.OrderDetails on cyContainer.VIRNo equals gatePass.VirNumber
                                join doitem in Db.DOItems on gatePass.GatePassId equals doitem.GatePassId

                                where cyContainer.IsGateOut == false
                                && cyContainer.IsGateIn == true
                                && cyContainer.IsCSEmtyptyRecieved == false
                                && cyContainer.VIRNo == gatein.VIRNo
                                //&& ipao.VIRNumber == cyContainer.VIRNo 
                                && crl.VIRNumber == cyContainer.VIRNo
                                && gatePass.GatePasscontainerNumber == cyContainer.ContainerNo
                                && gatePass.GatePasscontainerNumber == cyContainer.ContainerNo
                                 && crl.Status == "RF"
                                //&& !Db.SVMs.Any(x => x.VIRNumber == cyContainer.VIRNo && x.ContainerNumber == cyContainer.ContainerNo &&  crl.Status != "RF")
                                select new CYGateOutViewModel
                                {

                                    CYContainerId = cyContainer.CYContainerId,
                                    BondedCarrier = "",
                                    GateOutDate = DateTime.Now,
                                    OldContainerNumberForCS = "",
                                    GatePassNumber = gatePass.GatePassNumber,
                                    GrossWeight = cyContainer.ContainerGrossWeight.ToString(),
                                    TerminalWeight = gatein != null ? gatein.Weight : 0,
                                    DoItemId = doitem.DOItemId,
                                    VIRNumber = cyContainer.VIRNo,
                                    SealNumber = gatein.FoundSealNumber,
                                    Type = "Normal",
                                    TruckNumber = doitem.TruckNumber,
                                    ContainerNumber = cyContainer.ContainerNo,
                                    Status = doitem.Status,
                                    PortOfExit = "AIT",
                                    Country = "Pakistan"
                                }).ToList();


            var containerMergedrf = (from container in containersrf
                                     group container by container.ContainerNumber into g

                                     select new CYGateOutViewModel
                                     {
                                         CYContainerId = g.FirstOrDefault().CYContainerId,
                                         BondedCarrier = g.FirstOrDefault().BondedCarrier,
                                         OldContainerNumberForCS = g.FirstOrDefault().OldContainerNumberForCS,
                                         GateOutDate = g.FirstOrDefault().GateOutDate,
                                         GatePassNumber = g.FirstOrDefault().GatePassNumber,
                                         GrossWeight = g.FirstOrDefault().GrossWeight,
                                         TerminalWeight = g.FirstOrDefault().TerminalWeight,
                                         DoItemId = g.FirstOrDefault().DoItemId,
                                         VIRNumber = g.FirstOrDefault().VIRNumber,
                                         SealNumber = g.FirstOrDefault().SealNumber,
                                         Type = g.FirstOrDefault().Type,
                                         TruckNumber = g.FirstOrDefault().TruckNumber,
                                         ContainerNumber = g.FirstOrDefault().ContainerNumber,
                                         Status = g.FirstOrDefault().Status,
                                         PortOfExit = g.FirstOrDefault().PortOfExit,
                                         Country = g.FirstOrDefault().Country
                                     }).ToList();




            var containers = (from cyContainer in this.Db.CYContainers
                              join gatein in this.Db.GateIns on cyContainer.ContainerNo equals gatein.ContainerNo
                              // join ipao in Db.IPAOs on gatein.ContainerNo equals ipao.ContainerNumber
                              join crl in Db.CRLs on cyContainer.ContainerNo equals crl.ContainerNumber
                              join gatePass in Db.OrderDetails on cyContainer.VIRNo equals gatePass.VirNumber
                              join doitem in Db.DOItems on gatePass.GatePassId equals doitem.GatePassId
                              join svm in Db.SVMs on cyContainer.VIRNo equals svm.VIRNumber

                              where cyContainer.IsGateOut == false
                              && cyContainer.IsGateIn == true
                              && cyContainer.IsCSEmtyptyRecieved == false
                              && cyContainer.VIRNo == gatein.VIRNo
                              //&& ipao.VIRNumber == cyContainer.VIRNo 
                              && crl.VIRNumber == cyContainer.VIRNo
                              && gatePass.GatePasscontainerNumber == cyContainer.ContainerNo
                              && gatePass.GatePasscontainerNumber == cyContainer.ContainerNo
                              && svm.ContainerNumber == cyContainer.ContainerNo
                              && crl.Status != "RF"
                              //&& !Db.SVMs.Any(x => x.VIRNumber == cyContainer.VIRNo && x.ContainerNumber == cyContainer.ContainerNo &&  crl.Status != "RF")
                              select new CYGateOutViewModel
                              {

                                  CYContainerId = cyContainer.CYContainerId,
                                  BondedCarrier = svm.BondedCarrierName,
                                  GateOutDate = DateTime.Now,
                                  OldContainerNumberForCS = "",
                                  GatePassNumber = gatePass.GatePassNumber,
                                  GrossWeight = cyContainer.ContainerGrossWeight.ToString(),
                                  TerminalWeight = gatein != null ? gatein.Weight : 0,
                                  DoItemId = doitem.DOItemId,
                                  VIRNumber = cyContainer.VIRNo,
                                  SealNumber = svm.PCCSSSealNumber,
                                  Type = "Normal",
                                  TruckNumber = doitem.TruckNumber,
                                  ContainerNumber = cyContainer.ContainerNo,
                                  Status = doitem.Status,
                                  PortOfExit = "AIT",
                                  Country = "Pakistan"
                              }).ToList();


            var containerMerged = (from container in containers
                                   group container by container.ContainerNumber into g

                                   select new CYGateOutViewModel
                                   {
                                       CYContainerId = g.FirstOrDefault().CYContainerId,
                                       BondedCarrier = g.FirstOrDefault().BondedCarrier,
                                       OldContainerNumberForCS = g.FirstOrDefault().OldContainerNumberForCS,
                                       GateOutDate = g.FirstOrDefault().GateOutDate,
                                       GatePassNumber = g.FirstOrDefault().GatePassNumber,
                                       GrossWeight = g.FirstOrDefault().GrossWeight,
                                       TerminalWeight = g.FirstOrDefault().TerminalWeight,
                                       DoItemId = g.FirstOrDefault().DoItemId,
                                       VIRNumber = g.FirstOrDefault().VIRNumber,
                                       SealNumber = g.FirstOrDefault().SealNumber,
                                       Type = g.FirstOrDefault().Type,
                                       TruckNumber = g.FirstOrDefault().TruckNumber,
                                       ContainerNumber = g.FirstOrDefault().ContainerNumber,
                                       Status = g.FirstOrDefault().Status,
                                       PortOfExit = g.FirstOrDefault().PortOfExit,
                                       Country = g.FirstOrDefault().Country
                                   }).ToList();





            var cscontainers = (from svms in this.Db.SVMs
                                join cyContainer in this.Db.CYContainers on svms.ContainerNumber equals cyContainer.CSContainerNumber
                                join gatein in this.Db.GateIns on cyContainer.ContainerNo equals gatein.ContainerNo
                                join pgo in this.Db.PGOs on svms.ContainerNumber equals pgo.ContainerNumber
                                join gatePass in Db.OrderDetails on cyContainer.VIRNo equals gatePass.VirNumber
                                join doitem in Db.DOItems on gatePass.GatePassId equals doitem.GatePassId
                                where
                                cyContainer.IsGateOut == false
                               && cyContainer.IsGateIn == true
                               && cyContainer.IsCSEmtyptyRecieved == true
                               && cyContainer.VIRNo == gatein.VIRNo
                               && cyContainer.VIRNo == svms.VIRNumber
                               && pgo.VIRNumber == svms.VIRNumber
                               && gatePass.GatePasscontainerNumber == cyContainer.CSContainerNumber
                                select new CYGateOutViewModel
                                {
                                    CYContainerId = cyContainer.CYContainerId,
                                    BondedCarrier = pgo.BondedCarrierName,
                                    OldContainerNumberForCS = cyContainer.ContainerNo,
                                    GateOutDate = DateTime.Now,
                                    GatePassNumber = gatePass.GatePassNumber,
                                    GrossWeight = cyContainer.ContainerGrossWeight.ToString(),
                                    TerminalWeight = gatein != null ? gatein.Weight : 0,
                                    DoItemId = doitem.DOItemId,
                                    VIRNumber = svms.VIRNumber,
                                    SealNumber = svms.PCCSSSealNumber,
                                    Type = "CrossStuff",
                                    TruckNumber = svms.VehicleNumber,
                                    ContainerNumber = svms.ContainerNumber,
                                    Status = doitem.Status,
                                    PortOfExit = "AIT",
                                    Country = "PK"
                                }).ToList();

            var CScontainerMerged = (from container in cscontainers
                                     group container by container.ContainerNumber into g

                                     select new CYGateOutViewModel
                                     {
                                         CYContainerId = g.FirstOrDefault().CYContainerId,
                                         BondedCarrier = g.FirstOrDefault().BondedCarrier,
                                         OldContainerNumberForCS = g.FirstOrDefault().OldContainerNumberForCS,
                                         GateOutDate = g.FirstOrDefault().GateOutDate,
                                         GatePassNumber = g.FirstOrDefault().GatePassNumber,
                                         GrossWeight = g.FirstOrDefault().GrossWeight,
                                         TerminalWeight = g.FirstOrDefault().TerminalWeight,
                                         DoItemId = g.FirstOrDefault().DoItemId,
                                         VIRNumber = g.FirstOrDefault().VIRNumber,
                                         SealNumber = g.FirstOrDefault().SealNumber,
                                         Type = g.FirstOrDefault().Type,
                                         TruckNumber = g.FirstOrDefault().TruckNumber,
                                         ContainerNumber = g.FirstOrDefault().ContainerNumber,
                                         Status = g.FirstOrDefault().Status,
                                         PortOfExit = g.FirstOrDefault().PortOfExit,
                                         Country = g.FirstOrDefault().Country
                                     }).ToList();

            CScontainerMerged.AddRange(containerMerged);
            CScontainerMerged.AddRange(containerMergedrf);




            return CScontainerMerged;

        }


        public List<CFSGateOutViewModel> GetGateoutCFS()
        {
            var resdata = new List<CFSGateOutViewModel>();

            var containers = (from containerindex in this.Db.ContainerIndices
                              join gatein in this.Db.GateIns on containerindex.ContainerNo equals gatein.ContainerNo
                              // join ipao in Db.IPAOs on gatein.ContainerNo equals ipao.ContainerNumber
                              join crlo in Db.CRLOs on containerindex.BLNo equals crlo.BLNumber
                              join gatePass in Db.OrderDetails on containerindex.VirNo equals gatePass.VirNumber
                              join doitem in Db.DOItems on gatePass.GatePassId equals doitem.GatePassId
                              where containerindex.IsGateOut == false
                              && containerindex.IsGateIn == true
                              && containerindex.VirNo == gatein.VIRNo
                              //&& ipao.VIRNumber == cyContainer.VIRNo 
                              && containerindex.IndexNo == crlo.IndexNumber
                              && containerindex.VirNo == crlo.VIRNumber
                              && gatePass.IndexNo == containerindex.IndexNo
                              && doitem.IsGateOut == false
                              && !Db.CCMOs.Any(x => x.VIRNo == containerindex.VirNo && x.BLNo == containerindex.BLNo && x.IndexNo == containerindex.IndexNo)
                              && gatePass.UnitType == "WEIGHT"
                              select new CFSGateOutViewModel
                              {
                                  Key = containerindex.VirNo + "-" + containerindex.IndexNo + "-" + containerindex.BLNo + "-" + doitem.DOItemId,
                                  //ContainerIndexId = containerindex.ContainerIndexId,
                                  VIRNo = containerindex.VirNo,
                                  IndexNo = containerindex.IndexNo,
                                  //GatePassId = gatePass.GatePassId,
                                  GatePassNumber = gatePass.GatePassNumber,
                                  //ContainerNo = containerindex.ContainerNo,
                                  BLNo = containerindex.BLNo,
                                  // because of aict gatepass work  NoOfPackages = gatePass.Delivered,
                                  NoOfPackages = Convert.ToInt32(gatePass.Delivered),
                                  PackageType = doitem.PackageType + "=" + doitem.Quantity,
                                  DoItemId = doitem.DOItemId,
                                  Status = doitem.Status,
                                  GateOutTime = DateTime.Now,
                                  TruckNo = doitem.TruckNumber,
                                  //PCCSSSealNo = gatein.FoundSealNumber,
                                  //BondedCarrier = ipao.BondedCarrierName,
                                  //GatePassNumber = gatePass.GatePassNumber,
                                  Weight = containerindex.BLGrossWeight,
                                  PortOfExit = "AIT",
                                  CountryOfExit = "Pakistan"
                              }).ToList();


            var containerMerged = (from container in containers
                                   group container by container.DoItemId into g

                                   select new CFSGateOutViewModel
                                   {
                                       Key = g.FirstOrDefault().Key,
                                       VIRNo = g.FirstOrDefault().VIRNo,
                                       IndexNo = g.FirstOrDefault().IndexNo,
                                       GatePassNumber = g.FirstOrDefault().GatePassNumber,
                                       BLNo = g.FirstOrDefault().BLNo,
                                       NoOfPackages = g.FirstOrDefault().NoOfPackages,
                                       PackageType = g.FirstOrDefault().PackageType,
                                       DoItemId = g.FirstOrDefault().DoItemId,
                                       Status = g.FirstOrDefault().Status,
                                       GateOutTime = g.FirstOrDefault().GateOutTime,
                                       TruckNo = g.FirstOrDefault().TruckNo,
                                       Weight = g.FirstOrDefault().Weight,
                                       PortOfExit = g.FirstOrDefault().PortOfExit,
                                       CountryOfExit = g.FirstOrDefault().CountryOfExit,

                                   }).ToList();



            resdata.AddRange(containerMerged);


            foreach (var item in resdata)
            {
                item.ContainerNo = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  VirNo = {item.VIRNo} and IndexNo = {item.IndexNo} and BLNo = {item.BLNo}  and IsDeleted = 0 ").LastOrDefault().ContainerNo;
            }

            var containerspackages = (from containerindex in this.Db.ContainerIndices
                                      join crlo in Db.CRLOs on containerindex.BLNo equals crlo.BLNumber
                                      join gatePass in Db.OrderDetails on containerindex.VirNo equals gatePass.VirNumber
                                      join doitem in Db.DOItems on gatePass.GatePassId equals doitem.GatePassId
                                      where containerindex.IsGateOut == false
                                      && containerindex.IsGateIn == true
                                      && containerindex.IndexNo == crlo.IndexNumber
                                      && containerindex.VirNo == crlo.VIRNumber
                                      && gatePass.IndexNo == containerindex.IndexNo
                                      && doitem.IsGateOut == false
                                      && !Db.CCMOs.Any(x => x.VIRNo == containerindex.VirNo && x.BLNo == containerindex.BLNo && x.IndexNo == containerindex.IndexNo)
                                      && gatePass.UnitType == "PACKAGES"
                                      select new CFSGateOutViewModel
                                      {
                                          Key = containerindex.VirNo + "-" + containerindex.IndexNo + "-" + containerindex.BLNo + "-" + doitem.DOItemId,
                                          VIRNo = containerindex.VirNo,
                                          IndexNo = containerindex.IndexNo,
                                          ContainerNo = containerindex.ContainerNo,
                                          GatePassNumber = gatePass.GatePassNumber,
                                          BLNo = containerindex.BLNo,
                                          NoOfPackages = Convert.ToInt32(gatePass.Delivered),
                                          PackageType = doitem.PackageType + "=" + doitem.Quantity,
                                          DoItemId = doitem.DOItemId,
                                          Status = doitem.Status,
                                          GateOutTime = DateTime.Now,
                                          TruckNo = doitem.TruckNumber,
                                          Weight = containerindex.BLGrossWeight,
                                          PortOfExit = "AIT",
                                          CountryOfExit = "Pakistan"
                                      }).ToList();


            var containerspackagesMerged = (from container in containerspackages
                                            group container by container.DoItemId into g
                                            select new CFSGateOutViewModel
                                            {
                                                Key = g.FirstOrDefault().Key,
                                                VIRNo = g.FirstOrDefault().VIRNo,
                                                IndexNo = g.FirstOrDefault().IndexNo,
                                                GatePassNumber = g.FirstOrDefault().GatePassNumber,
                                                ContainerNo = g.FirstOrDefault().ContainerNo,
                                                BLNo = g.FirstOrDefault().BLNo,
                                                NoOfPackages = g.FirstOrDefault().NoOfPackages,
                                                PackageType = g.FirstOrDefault().PackageType,
                                                DoItemId = g.FirstOrDefault().DoItemId,
                                                Status = g.FirstOrDefault().Status,
                                                GateOutTime = g.FirstOrDefault().GateOutTime,
                                                TruckNo = g.FirstOrDefault().TruckNo,
                                                Weight = g.FirstOrDefault().Weight,
                                                PortOfExit = g.FirstOrDefault().PortOfExit,
                                                CountryOfExit = g.FirstOrDefault().CountryOfExit,

                                            }).ToList();

            resdata.AddRange(containerspackagesMerged);




            return resdata;

        }

        public List<CFSGateOutViewModel> GetGateoutCFSContainers(string VIRNumber, int? IndexNo)
        {
            var containers = (from vessel in this.Db.Vessels
                              join voyage in this.Db.Voyages on vessel.VesselId equals voyage.VesselId
                              join containerIndex in Db.ContainerIndices on voyage.VoyageId equals containerIndex.VoyageId
                              join gatein in this.Db.GateIns on containerIndex.ContainerNo equals gatein.ContainerNo
                              join crlo in Db.CRLOs on containerIndex.BLNo equals crlo.BLNumber
                              join ipao in Db.IPAOs on gatein.ContainerNo equals ipao.ContainerNumber
                              join destuff in Db.DestuffedContainers on containerIndex.ContainerIndexId equals destuff.ContainerIndexId
                              where containerIndex.IsGateOut == false
                                    && containerIndex.IndexNo == crlo.IndexNumber
                                    && containerIndex.Status.ToUpper() == "CFS"
                                    && voyage.VIRNo == VIRNumber
                                    && containerIndex.IndexNo == IndexNo
                              select new CFSGateOutViewModel
                              {
                                  ContainerIndexId = containerIndex.ContainerIndexId,
                                  BLNo = crlo.BLNumber,
                                  IndexNo = containerIndex.IndexNo,
                                  ContainerNo = containerIndex.ContainerNo,
                                  PCCSSSealNo = ipao.PCCSSSealNumber,
                                  Category = "I",
                                  BondedCarrierNTN = ipao.BondedCarrierId,
                                  GateOutTime = DateTime.Now,
                                  NoOfPackages = containerIndex.NoOfPackages,
                                  PackageType = containerIndex.PackageType,
                                  VIRNo = ipao.VIRNumber,
                                  Weight = Db.DestuffedContainers.Where(s => s.ContainerIndex.VoyageId == voyage.VoyageId && s.ContainerIndex.Voyage.VIRNo == voyage.VIRNo).Sum(s => s.FoundWeight),
                                  Status = "F",
                                  ShippingAgentId = gatein.ShippingAgentId,
                                  PortOfExit = "AIT",
                                  CountryOfExit = "Pakistan"
                              }).Distinct().ToList();

            return containers;

        }



        public List<CFSGateOutViewModel> GetGateoutCFSIndex()
        {
            var containers = (from containerIndex in Db.ContainerIndices
                              join voyge in Db.Voyages on containerIndex.VoyageId equals voyge.VoyageId
                              join gatein in this.Db.GateIns on containerIndex.ContainerNo equals gatein.ContainerNo
                              join crlo in Db.CRLOs on containerIndex.BLNo equals crlo.BLNumber
                              where
                          containerIndex.IsGateIn == true &&
                          containerIndex.IsGateOut == false &&
                          gatein.VIRNo == voyge.VIRNo &&
                           crlo.IndexNumber == containerIndex.IndexNo &&
                           crlo.VIRNumber == voyge.VIRNo &&
                          containerIndex.Status.ToUpper() == "CFS"
                              select new CFSGateOutViewModel
                              {

                                  Key = containerIndex.ContainerIndexId.ToString(),
                                  BLNo = crlo.BLNumber,
                                  IndexNo = containerIndex.IndexNo,
                                  GateOutTime = crlo.Performed,
                                  NoOfPackages = containerIndex.NoOfPackages,
                                  PackageType = containerIndex.PackageType,
                                  VIRNo = voyge.VIRNo,
                                  Status = crlo.Status,
                                  ShippingAgentId = containerIndex.ShippingAgentId,

                              }).Distinct().ToList();

            return containers;

        }



        public List<CYGroundingViewModel> GetUngroundedCYContainers()
        {
            var containers = (from container in Db.CYContainers
                              join ihc in Db.IHCs on container.ContainerNo equals ihc.ContainerNumber
                              from brt in Db.BRTs.Where(c => c.CyContainerId == container.CYContainerId).DefaultIfEmpty()
                              from goodhead in Db.GoodsHeads.Where(c => c.GoodsHeadId == container.GoodsHeadId).DefaultIfEmpty()
                              from gatein in Db.GateIns.Where(c => c.VIRNo == container.VIRNo && c.ContainerNo == container.ContainerNo).DefaultIfEmpty()

                              where


                              container.IsGrounded == false
                              && container.IsGateIn == true
                              && ihc.HandlingCode == "EM"
                              && container.VIRNo == ihc.VIRNumber

                              select new CYGroundingViewModel
                              {
                                  ContainerId = container.CYContainerId,
                                  VIRNumber = container.VIRNo,
                                  Weight = gatein != null ? gatein.Weight / 1000 ?? 0 : 0,
                                  ManifestedSealNumber = container.ManifestedSealNumber,
                                  Description = container.Description,
                                  IndexNo = container.IndexNo.ToString(),
                                  Location = brt != null ? brt.Location : "",
                                  ContainerNo = container.ContainerNo,
                                  HandlingCode = ihc.HandlingCode,
                                  GoodName = goodhead != null ? goodhead.GoodsHeadName : "",
                                  ActivityType = "GROUNDED",
                                  Status = "F",
                                  Count = Db.IHCs.Where(c => c.VIRNumber == container.VIRNo && c.ContainerNumber == container.ContainerNo).Count().ToString(),
                                  IsChecked = false
                              }).ToList();


            var containerMerged = (from container in containers
                                   group container by container.ContainerNo into g

                                   select new CYGroundingViewModel
                                   {
                                       ContainerId = g.FirstOrDefault().ContainerId,
                                       VIRNumber = g.FirstOrDefault().VIRNumber,
                                       Weight = g.FirstOrDefault().Weight,
                                       ManifestedSealNumber = g.FirstOrDefault().ManifestedSealNumber,
                                       Description = g.FirstOrDefault().Description,
                                       IndexNo = g.FirstOrDefault().IndexNo,
                                       Location = g.FirstOrDefault().Location,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       HandlingCode = g.FirstOrDefault().HandlingCode,
                                       GoodName = g.FirstOrDefault().GoodName,
                                       ActivityType = g.FirstOrDefault().ActivityType,
                                       Status = g.FirstOrDefault().Status,
                                       Count = g.FirstOrDefault().Count,
                                       IsChecked = g.FirstOrDefault().IsChecked
                                   }).ToList();


            return containerMerged;

        }
        public List<CYGroundingViewModel> GetUngroundedCYContainersByGoodHeadId(long goodheadid)
        {
            var containers = (from container in Db.CYContainers
                              join ihc in Db.IHCs on container.ContainerNo equals ihc.ContainerNumber
                              from brt in Db.BRTs.Where(c => c.CyContainerId == container.CYContainerId).DefaultIfEmpty()
                              from goodhead in Db.GoodsHeads.Where(c => c.GoodsHeadId == container.GoodsHeadId).DefaultIfEmpty()
                              from gatein in Db.GateIns.Where(c => c.VIRNo == container.VIRNo && c.ContainerNo == container.ContainerNo).DefaultIfEmpty()

                              where


                              container.IsGrounded == false
                              && container.IsGateIn == true
                              && ihc.HandlingCode == "EM"
                              && container.VIRNo == ihc.VIRNumber
                              && goodhead.GoodsHeadId == goodheadid
                              select new CYGroundingViewModel
                              {
                                  ContainerId = container.CYContainerId,
                                  VIRNumber = container.VIRNo,
                                  Weight = gatein != null ? gatein.Weight / 1000 ?? 0 : 0,
                                  ManifestedSealNumber = container.ManifestedSealNumber,
                                  Description = container.Description,
                                  IndexNo = container.IndexNo.ToString(),
                                  Location = brt != null ? brt.Location : "",
                                  ContainerNo = container.ContainerNo,
                                  HandlingCode = ihc.HandlingCode,
                                  GoodName = goodhead != null ? goodhead.GoodsHeadName : "",
                                  ActivityType = "GROUNDED",
                                  Status = "F",
                                  Count = Db.IHCs.Where(c => c.VIRNumber == container.VIRNo && c.ContainerNumber == container.ContainerNo).Count().ToString(),
                                  IsChecked = false
                              }).ToList();

            var containerMerged = (from container in containers
                                   group container by container.ContainerNo into g

                                   select new CYGroundingViewModel
                                   {
                                       ContainerId = g.FirstOrDefault().ContainerId,
                                       VIRNumber = g.FirstOrDefault().VIRNumber,
                                       Weight = g.FirstOrDefault().Weight,
                                       ManifestedSealNumber = g.FirstOrDefault().ManifestedSealNumber,
                                       Description = g.FirstOrDefault().Description,
                                       IndexNo = g.FirstOrDefault().IndexNo,
                                       Location = g.FirstOrDefault().Location,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       HandlingCode = g.FirstOrDefault().HandlingCode,
                                       GoodName = g.FirstOrDefault().GoodName,
                                       ActivityType = g.FirstOrDefault().ActivityType,
                                       Status = g.FirstOrDefault().Status,
                                       Count = g.FirstOrDefault().Count,
                                       IsChecked = g.FirstOrDefault().IsChecked
                                   }).ToList();



            return containerMerged;
        }

        public List<CFSGroundingViewModel> GetUngroundedCFSContainers()
        {
            var indexes = (from containerIndex in Db.ContainerIndices
                           from destuffedContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           from goodsHead in Db.GoodsHeads.Where(x => x.GoodsHeadId == containerIndex.GoodsHeadId).DefaultIfEmpty()
                           join ihco in Db.IHCOs on containerIndex.VirNo equals ihco.VIRNumber

                           //from brt in Db.BRTs.Where(c => c.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           where
                           containerIndex.IsGrounded == false
                           && containerIndex.IsGateIn == true
                           && containerIndex.IndexNo == ihco.IndexNumber
                           //&& voyage.VIRNo == ihco.VIRNumber
                           && ihco.HandlingCode == "EM"
                           && containerIndex.Status.ToUpper() == "CFS"
                           //&& goodsHead.GoodsHeadName != "VEHICLES / AUTO PARTS"

                           select new CFSGroundingViewModel
                           {
                               ContainerIndexId = containerIndex.ContainerIndexId,
                               Key = containerIndex.VirNo + "-" + containerIndex.BLNo,
                               IndexNo = containerIndex.IndexNo ?? 0,
                               VIRNumber = containerIndex.VirNo,
                               BLNumber = ihco.BLNumber,
                               IgmoBLNumber = containerIndex.BLNo,
                               GoodName = goodsHead != null ? goodsHead.GoodsHeadName : "",
                               ContainerNo = containerIndex.ContainerNo,
                               Weight = 0.000000000,
                               ManifestedSealNumber = containerIndex.ManifestedSealNumber,
                               ExaminationAlertDate = ihco != null ? ihco.Performed : null,
                               Description = containerIndex.Description,
                               Location = destuffedContainer != null ? destuffedContainer.Location : "",
                               HandlingCode = ihco.HandlingCode,
                               //Count = Db.IHCOs.Where(c => c.VIRNumber == voyage.VIRNo && c.IndexNumber == containerIndex.IndexNo && c.BLNumber == containerIndex.BLNo).Count().ToString(),
                               Count = Db.IHCOs.Where(c => c.VIRNumber == containerIndex.VirNo && c.IndexNumber == containerIndex.IndexNo).Count().ToString(),
                               ActivityType = "GROUNDED"
                           }).Distinct().ToList();



            var containerMerged = (from container in indexes
                                   group container by container.Key into g

                                   select new CFSGroundingViewModel
                                   {
                                       ContainerIndexId = g.FirstOrDefault().ContainerIndexId,
                                       Key = g.FirstOrDefault().Key,
                                       IndexNo = g.FirstOrDefault().IndexNo,
                                       VIRNumber = g.FirstOrDefault().VIRNumber,
                                       BLNumber = g.FirstOrDefault().BLNumber,
                                       IgmoBLNumber = g.FirstOrDefault().IgmoBLNumber,
                                       GoodName = g.FirstOrDefault().GoodName,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       Weight = g.FirstOrDefault().Weight,
                                       ManifestedSealNumber = g.FirstOrDefault().ManifestedSealNumber,
                                       ExaminationAlertDate = g.FirstOrDefault().ExaminationAlertDate,
                                       Description = g.FirstOrDefault().Description,
                                       Location = g.FirstOrDefault().Location,
                                       HandlingCode = g.FirstOrDefault().HandlingCode,
                                       Count = g.FirstOrDefault().Count,
                                       ActivityType = g.FirstOrDefault().ActivityType,

                                   }).ToList();

            return containerMerged;
        }


        public List<CFSGroundingViewModel> GetUngroundedCFSContainersByGoodHeadId(long goodheadId)
        {
            var indexes = (from containerIndex in Db.ContainerIndices
                           from destuffedContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           from goodsHead in Db.GoodsHeads.Where(x => x.GoodsHeadId == containerIndex.GoodsHeadId).DefaultIfEmpty()
                           join ihco in Db.IHCOs on containerIndex.VirNo equals ihco.VIRNumber

                           //from brt in Db.BRTs.Where(c => c.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           where
                           containerIndex.IsGrounded == false
                           && containerIndex.IsGateIn == true
                           && containerIndex.IndexNo == ihco.IndexNumber
                           //&& voyage.VIRNo == ihco.VIRNumber
                           && ihco.HandlingCode == "EM"
                           && containerIndex.Status.ToUpper() == "CFS"
                           && containerIndex.GoodsHeadId == goodheadId
                           //&& goodsHead.GoodsHeadName != "VEHICLES / AUTO PARTS"

                           select new CFSGroundingViewModel
                           {
                               ContainerIndexId = containerIndex.ContainerIndexId,
                               Key = containerIndex.VirNo + "-" + containerIndex.BLNo,
                               IndexNo = containerIndex.IndexNo ?? 0,
                               VIRNumber = containerIndex.VirNo,
                               BLNumber = ihco.BLNumber,
                               IgmoBLNumber = containerIndex.BLNo,
                               GoodName = goodsHead != null ? goodsHead.GoodsHeadName : "",
                               ContainerNo = containerIndex.ContainerNo,
                               Weight = 0.000000000,
                               ManifestedSealNumber = containerIndex.ManifestedSealNumber,
                               ExaminationAlertDate = ihco != null ? ihco.Performed : null,
                               Description = containerIndex.Description,
                               Location = destuffedContainer != null ? destuffedContainer.Location : "",
                               HandlingCode = ihco.HandlingCode,
                               //Count = Db.IHCOs.Where(c => c.VIRNumber == voyage.VIRNo && c.IndexNumber == containerIndex.IndexNo && c.BLNumber == containerIndex.BLNo).Count().ToString(),
                               Count = Db.IHCOs.Where(c => c.VIRNumber == containerIndex.VirNo && c.IndexNumber == containerIndex.IndexNo).Count().ToString(),
                               ActivityType = "GROUNDED"
                           }).Distinct().ToList();



            var containerMerged = (from container in indexes
                                   group container by container.Key into g

                                   select new CFSGroundingViewModel
                                   {
                                       ContainerIndexId = g.FirstOrDefault().ContainerIndexId,
                                       Key = g.FirstOrDefault().Key,
                                       IndexNo = g.FirstOrDefault().IndexNo,
                                       VIRNumber = g.FirstOrDefault().VIRNumber,
                                       BLNumber = g.FirstOrDefault().BLNumber,
                                       IgmoBLNumber = g.FirstOrDefault().IgmoBLNumber,
                                       GoodName = g.FirstOrDefault().GoodName,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       Weight = g.FirstOrDefault().Weight,
                                       ManifestedSealNumber = g.FirstOrDefault().ManifestedSealNumber,
                                       ExaminationAlertDate = g.FirstOrDefault().ExaminationAlertDate,
                                       Description = g.FirstOrDefault().Description,
                                       Location = g.FirstOrDefault().Location,
                                       HandlingCode = g.FirstOrDefault().HandlingCode,
                                       Count = g.FirstOrDefault().Count,
                                       ActivityType = g.FirstOrDefault().ActivityType,

                                   }).ToList();

            return containerMerged;
        }


        public List<CFSGroundingViewModel> GetAutoUngroundedCFSContainers()
        {
            var resdata = new List<CFSGroundingViewModel>();

            var indexesLCL = (from containerIndex in Db.ContainerIndices
                              from destuffedContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                              join goodsHead in Db.GoodsHeads on containerIndex.GoodsHeadId equals goodsHead.GoodsHeadId
                              join ihco in Db.IHCOs on containerIndex.VirNo equals ihco.VIRNumber

                              //from brt in Db.BRTs.Where(c => c.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                              where
                              containerIndex.IsGrounded == false
                              && containerIndex.IsGateIn == true
                              && goodsHead.AllowAutoGroundingLCL == "YES"
                              && containerIndex.IndexNo == ihco.IndexNumber
                              //&& voyage.VIRNo == ihco.VIRNumber
                              && ihco.HandlingCode == "EM"
                              && containerIndex.Status.ToUpper() == "CFS"
                              && containerIndex.CargoType == "LCL"
                              //&& goodsHead.GoodsHeadName != "VEHICLES / AUTO PARTS"

                              select new CFSGroundingViewModel
                              {
                                  ContainerIndexId = containerIndex.ContainerIndexId,
                                  Key = containerIndex.VirNo + "-" + containerIndex.BLNo,
                                  IndexNo = containerIndex.IndexNo ?? 0,
                                  VIRNumber = containerIndex.VirNo,
                                  BLNumber = ihco.BLNumber,
                                  IgmoBLNumber = containerIndex.BLNo,
                                  GoodName = goodsHead != null ? goodsHead.GoodsHeadName : "",
                                  ContainerNo = containerIndex.ContainerNo,
                                  Weight = 0.000000000,
                                  ManifestedSealNumber = containerIndex.ManifestedSealNumber,
                                  ExaminationAlertDate = ihco != null ? ihco.Performed : null,
                                  Description = containerIndex.Description,
                                  Location = destuffedContainer != null ? destuffedContainer.Location : "",
                                  HandlingCode = ihco.HandlingCode,
                                  //Count = Db.IHCOs.Where(c => c.VIRNumber == voyage.VIRNo && c.IndexNumber == containerIndex.IndexNo && c.BLNumber == containerIndex.BLNo).Count().ToString(),
                                  Count = Db.IHCOs.Where(c => c.VIRNumber == containerIndex.VirNo && c.IndexNumber == containerIndex.IndexNo).Count().ToString(),
                                  ActivityType = "GROUNDED"
                              }).Distinct().ToList();



            var containerMergedLCL = (from container in indexesLCL
                                      group container by container.Key into g

                                      select new CFSGroundingViewModel
                                      {
                                          ContainerIndexId = g.FirstOrDefault().ContainerIndexId,
                                          Key = g.FirstOrDefault().Key,
                                          IndexNo = g.FirstOrDefault().IndexNo,
                                          VIRNumber = g.FirstOrDefault().VIRNumber,
                                          BLNumber = g.FirstOrDefault().BLNumber,
                                          IgmoBLNumber = g.FirstOrDefault().IgmoBLNumber,
                                          GoodName = g.FirstOrDefault().GoodName,
                                          ContainerNo = g.FirstOrDefault().ContainerNo,
                                          Weight = g.FirstOrDefault().Weight,
                                          ManifestedSealNumber = g.FirstOrDefault().ManifestedSealNumber,
                                          ExaminationAlertDate = g.FirstOrDefault().ExaminationAlertDate,
                                          Description = g.FirstOrDefault().Description,
                                          Location = g.FirstOrDefault().Location,
                                          HandlingCode = g.FirstOrDefault().HandlingCode,
                                          Count = g.FirstOrDefault().Count,
                                          ActivityType = g.FirstOrDefault().ActivityType,

                                      }).ToList();



            var indexesFCL = (from containerIndex in Db.ContainerIndices
                              from destuffedContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                              join goodsHead in Db.GoodsHeads on containerIndex.GoodsHeadId equals goodsHead.GoodsHeadId
                              join ihco in Db.IHCOs on containerIndex.VirNo equals ihco.VIRNumber

                              //from brt in Db.BRTs.Where(c => c.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                              where
                              containerIndex.IsGrounded == false
                              && containerIndex.IsGateIn == true
                              && goodsHead.AllowAutoGroundingFCL == "YES"
                              && containerIndex.IndexNo == ihco.IndexNumber
                              //&& voyage.VIRNo == ihco.VIRNumber
                              && ihco.HandlingCode == "EM"
                              && containerIndex.Status.ToUpper() == "CFS"
                              && containerIndex.CargoType == "FCL"
                              //&& goodsHead.GoodsHeadName != "VEHICLES / AUTO PARTS"

                              select new CFSGroundingViewModel
                              {
                                  ContainerIndexId = containerIndex.ContainerIndexId,
                                  Key = containerIndex.VirNo + "-" + containerIndex.BLNo,
                                  IndexNo = containerIndex.IndexNo ?? 0,
                                  VIRNumber = containerIndex.VirNo,
                                  BLNumber = ihco.BLNumber,
                                  IgmoBLNumber = containerIndex.BLNo,
                                  GoodName = goodsHead != null ? goodsHead.GoodsHeadName : "",
                                  ContainerNo = containerIndex.ContainerNo,
                                  Weight = 0.000000000,
                                  ManifestedSealNumber = containerIndex.ManifestedSealNumber,
                                  ExaminationAlertDate = ihco != null ? ihco.Performed : null,
                                  Description = containerIndex.Description,
                                  Location = destuffedContainer != null ? destuffedContainer.Location : "",
                                  HandlingCode = ihco.HandlingCode,
                                  //Count = Db.IHCOs.Where(c => c.VIRNumber == voyage.VIRNo && c.IndexNumber == containerIndex.IndexNo && c.BLNumber == containerIndex.BLNo).Count().ToString(),
                                  Count = Db.IHCOs.Where(c => c.VIRNumber == containerIndex.VirNo && c.IndexNumber == containerIndex.IndexNo).Count().ToString(),
                                  ActivityType = "GROUNDED"
                              }).Distinct().ToList();



            var containerMergedFCL = (from container in indexesFCL
                                      group container by container.Key into g

                                      select new CFSGroundingViewModel
                                      {
                                          ContainerIndexId = g.FirstOrDefault().ContainerIndexId,
                                          Key = g.FirstOrDefault().Key,
                                          IndexNo = g.FirstOrDefault().IndexNo,
                                          VIRNumber = g.FirstOrDefault().VIRNumber,
                                          BLNumber = g.FirstOrDefault().BLNumber,
                                          IgmoBLNumber = g.FirstOrDefault().IgmoBLNumber,
                                          GoodName = g.FirstOrDefault().GoodName,
                                          ContainerNo = g.FirstOrDefault().ContainerNo,
                                          Weight = g.FirstOrDefault().Weight,
                                          ManifestedSealNumber = g.FirstOrDefault().ManifestedSealNumber,
                                          ExaminationAlertDate = g.FirstOrDefault().ExaminationAlertDate,
                                          Description = g.FirstOrDefault().Description,
                                          Location = g.FirstOrDefault().Location,
                                          HandlingCode = g.FirstOrDefault().HandlingCode,
                                          Count = g.FirstOrDefault().Count,
                                          ActivityType = g.FirstOrDefault().ActivityType,

                                      }).ToList();

            resdata.AddRange(containerMergedLCL);
            resdata.AddRange(containerMergedFCL);

            return resdata;
        }

        public List<CYGroundingViewModel> GetAutoUngroundedCYContainers()
        {
            var containers = (from container in Db.CYContainers
                              join ihc in Db.IHCs on container.ContainerNo equals ihc.ContainerNumber
                              from brt in Db.BRTs.Where(c => c.CyContainerId == container.CYContainerId).DefaultIfEmpty()
                              join goodsHead in Db.GoodsHeads on container.GoodsHeadId equals goodsHead.GoodsHeadId
                              from gatein in Db.GateIns.Where(c => c.VIRNo == container.VIRNo && c.ContainerNo == container.ContainerNo).DefaultIfEmpty()

                              where

                              container.IsGrounded == false
                              && container.IsGateIn == true
                              && ihc.HandlingCode == "EM"
                              && container.VIRNo == ihc.VIRNumber
                              && goodsHead.AllowAutoGrounding == "YES"

                              select new CYGroundingViewModel
                              {
                                  ContainerId = container.CYContainerId,
                                  VIRNumber = container.VIRNo,
                                  Weight = gatein != null ? gatein.Weight / 1000 ?? 0 : 0,
                                  ManifestedSealNumber = container.ManifestedSealNumber,
                                  Description = container.Description,
                                  IndexNo = container.IndexNo.ToString(),
                                  Location = brt != null ? brt.Location : "",
                                  ContainerNo = container.ContainerNo,
                                  HandlingCode = ihc.HandlingCode,
                                  GoodName = goodsHead.GoodsHeadName,
                                  ActivityType = "GROUNDED",
                                  Status = "F",
                                  Count = Db.IHCs.Where(c => c.VIRNumber == container.VIRNo && c.ContainerNumber == container.ContainerNo).Count().ToString(),
                                  IsChecked = false
                              }).ToList();


            var containerMerged = (from container in containers
                                   group container by container.ContainerNo into g

                                   select new CYGroundingViewModel
                                   {
                                       ContainerId = g.FirstOrDefault().ContainerId,
                                       VIRNumber = g.FirstOrDefault().VIRNumber,
                                       Weight = g.FirstOrDefault().Weight,
                                       ManifestedSealNumber = g.FirstOrDefault().ManifestedSealNumber,
                                       Description = g.FirstOrDefault().Description,
                                       IndexNo = g.FirstOrDefault().IndexNo,
                                       Location = g.FirstOrDefault().Location,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       HandlingCode = g.FirstOrDefault().HandlingCode,
                                       GoodName = g.FirstOrDefault().GoodName,
                                       ActivityType = g.FirstOrDefault().ActivityType,
                                       Status = g.FirstOrDefault().Status,
                                       Count = g.FirstOrDefault().Count,
                                       IsChecked = g.FirstOrDefault().IsChecked
                                   }).ToList();

            return containerMerged;
        }


        public List<CFSGroundingViewModel> GetUngroundedCFSContainersVechiles()
        {
            var indexes = (from containerIndex in Db.ContainerIndices
                           from destuffedContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           join goodsHead in Db.GoodsHeads on containerIndex.GoodsHeadId equals goodsHead.GoodsHeadId
                           join ihco in Db.IHCOs on containerIndex.BLNo equals ihco.BLNumber
                           join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId

                           //from brt in Db.BRTs.Where(c => c.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           where
                           containerIndex.IsGrounded == false
                           && containerIndex.IsGateIn == true
                           && containerIndex.IndexNo == ihco.IndexNumber
                           && voyage.VIRNo == ihco.VIRNumber
                           && ihco.HandlingCode == "EM"
                           && containerIndex.Status.ToUpper() == "CFS"
                           && goodsHead.GoodsHeadName == "VEHICLES / AUTO PARTS"

                           select new CFSGroundingViewModel
                           {
                               ContainerIndexId = containerIndex.ContainerIndexId,
                               IndexNo = containerIndex.IndexNo ?? 0,
                               VIRNumber = voyage.VIRNo,
                               BLNumber = containerIndex.BLNo,
                               ContainerNo = containerIndex.ContainerNo,
                               Weight = 0.000000000,
                               ManifestedSealNumber = containerIndex.ManifestedSealNumber,
                               ExaminationAlertDate = ihco != null ? ihco.Performed : null,
                               Description = containerIndex.Description,
                               Location = destuffedContainer != null ? destuffedContainer.Location : "",
                               HandlingCode = ihco.HandlingCode,
                               Count = Db.IHCOs.Where(c => c.VIRNumber == voyage.VIRNo && c.IndexNumber == containerIndex.IndexNo && c.BLNumber == containerIndex.BLNo).Count().ToString(),
                               ActivityType = "GROUNDED"
                           }).Distinct().ToList();


            return indexes;
        }

        public List<DestuffingViewModel> GetDestuffingCFSContainers(long ContainerId, string Orientation)
        {
            if (Orientation == "container")
            {
                //var containerNumber = (from containerIndex in Db.ContainerIndices
                //                       where containerIndex.ContainerIndexId == ContainerId
                //                       select containerIndex).FirstOrDefault();

                var containerNumber = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  ContainerIndexId = {ContainerId} and IsDeleted = 0 ").FirstOrDefault();

                var containers = (from containerIndex in Db.ContainerIndices
                                  join gatein in Db.GateIns on containerIndex.ContainerNo equals gatein.ContainerNo
                                  from gdio in Db.GDIOs.Where(g => g.BLNumber == containerIndex.BLNo && g.VIRNumber == containerNumber.VirNo).DefaultIfEmpty()
                                  from destuffcontr in Db.DestuffedContainers.Where(g => g.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                                  where
                                  containerIndex.IsGateOut == false
                                  && containerIndex.ContainerNo == containerNumber.ContainerNo
                                  && containerIndex.VirNo == containerNumber.VirNo
                                  && gatein.VIRNo == containerNumber.VirNo
                                  select new DestuffingViewModel
                                  {
                                      ContainerIndexId = containerIndex.ContainerIndexId,
                                      ContainerNo = containerIndex.ContainerNo,
                                      IndexNumber = containerIndex.IndexNo,
                                      BLNo = containerIndex.BLNo,
                                      VIRNumber = containerIndex.VirNo,
                                      Description = containerIndex.Description,
                                      ManifestWeight = containerIndex.BLGrossWeight,
                                      Package = destuffcontr != null ? destuffcontr.PackageQuantity : Convert.ToInt32(containerIndex.NoOfPackages),
                                      CosigneeName = gdio != null ? gdio.ConsigneeName : "",
                                      ManifestQuantity = Convert.ToInt32(containerIndex.NoOfPackages),
                                      PackageType = containerIndex.PackageType.Substring(0, containerIndex.PackageType.IndexOf("=")),
                                      MarksAndNumber = containerIndex.MarksAndNumber,
                                      ShortExcess = destuffcontr != null ? destuffcontr.ShortExcess : "NO",
                                      CBM = destuffcontr != null ? destuffcontr.CBM : null,
                                      FoundWeight = destuffcontr != null ? destuffcontr.FoundWeight : null,
                                      FoundPackageType = destuffcontr != null ? destuffcontr.FoundPackageType : "",
                                      Location = destuffcontr != null ? destuffcontr.Location : "",
                                      Remarks = destuffcontr != null ? destuffcontr.Remarks : "",
                                      InvoiceFoud = destuffcontr != null ? destuffcontr.InvoiceFound : "",
                                      InvoiceAmount = destuffcontr != null ? destuffcontr.InvoiceAmount : "",
                                      IsHold = containerIndex.IsHold,
                                      Ischeck = destuffcontr != null ? true : false,
                                      DestuffStatus = destuffcontr != null ? "Yes" : "No",
                                      VehicleMeasurementId = destuffcontr != null ? destuffcontr.VehicleMeasurementId : null,
                                  }).Distinct().ToList();

                return containers;
            }
            else
            {
                //var container = (from containerIndex in Db.ContainerIndices
                //                       where containerIndex.ContainerIndexId == ContainerId
                //                       select containerIndex).FirstOrDefault();

                var containerNumber = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  ContainerIndexId = {ContainerId} and IsDeleted = 0 ").FirstOrDefault();

                var containers = (from containerIndex in Db.ContainerIndices
                                  join gatein in Db.GateIns on containerIndex.ContainerNo equals gatein.ContainerNo
                                  from gdio in Db.GDIOs.Where(g => g.BLNumber == containerIndex.BLNo && g.VIRNumber == containerIndex.VirNo).DefaultIfEmpty()
                                  from destuffcontr in Db.DestuffedContainers.Where(g => g.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                                  where
                                  containerIndex.IsGateOut == false
                                   //&& containerIndex.ContainerIndexId == ContainerId
                                   && containerIndex.IndexNo == containerNumber.IndexNo
                                  && containerIndex.VirNo == containerNumber.VirNo
                                  && gatein.VIRNo == containerIndex.VirNo
                                  select new DestuffingViewModel
                                  {
                                      ContainerIndexId = containerIndex.ContainerIndexId,
                                      ContainerNo = containerIndex.ContainerNo,
                                      IndexNumber = containerIndex.IndexNo,
                                      BLNo = containerIndex.BLNo,
                                      VIRNumber = containerIndex.VirNo,
                                      Description = containerIndex.Description,
                                      ManifestWeight = containerIndex.BLGrossWeight,
                                      Package = destuffcontr != null ? destuffcontr.PackageQuantity : Convert.ToInt32(containerIndex.NoOfPackages),
                                      CosigneeName = gdio != null ? gdio.ConsigneeName : "",
                                      ManifestQuantity = Convert.ToInt32(containerIndex.NoOfPackages),
                                      PackageType = containerIndex.PackageType.Substring(0, containerIndex.PackageType.IndexOf("=")),
                                      MarksAndNumber = containerIndex.MarksAndNumber,
                                      ShortExcess = destuffcontr != null ? destuffcontr.ShortExcess : "NO",
                                      CBM = destuffcontr != null ? destuffcontr.CBM : null,
                                      FoundWeight = destuffcontr != null ? destuffcontr.FoundWeight : null,
                                      FoundPackageType = destuffcontr != null ? destuffcontr.FoundPackageType : "",
                                      Location = destuffcontr != null ? destuffcontr.Location : "",
                                      Remarks = destuffcontr != null ? destuffcontr.Remarks : "",
                                      InvoiceFoud = destuffcontr != null ? destuffcontr.InvoiceFound : "",
                                      InvoiceAmount = destuffcontr != null ? destuffcontr.InvoiceAmount : "",
                                      IsHold = containerIndex.IsHold,
                                      Ischeck = destuffcontr != null ? true : false,
                                      DestuffStatus = destuffcontr != null ? "Yes" : "No",
                                      VehicleMeasurementId = destuffcontr != null ? destuffcontr.VehicleMeasurementId : null,
                                  }).Distinct().ToList();

                return containers;
            }

        }

        public DeliveryOrderViewModel GetDeliveryOrderByDONumber(long doNumber)

        {
            var cntr = (from deliveryOrder in Db.DeliveryOrders
                        join containerIndex in Db.ContainerIndices on deliveryOrder.ContainerIndexId equals containerIndex.ContainerIndexId
                        join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                        join vessel in Db.Vessels on voyage.VesselId equals vessel.VesselId
                        from gatepas in Db.OrderDetails.Where(x => x.DeliveryOrderId == deliveryOrder.DeliveryOrderId).DefaultIfEmpty()

                        where deliveryOrder.DONumber == doNumber
                        select new DeliveryOrderViewModel
                        {
                            VesselName = vessel.VesselName,
                            VoyageNo = voyage.VoyageNo,
                            IGMNumber = vessel.IGM,
                            IGMYear = vessel.IGMYear,
                            CargoType = containerIndex.CargoType,
                            ContainerNumber = containerIndex.ContainerNo,
                            BLNumber = containerIndex.BLNo,
                            indexNumner = containerIndex.IndexNo,
                            ContainerIndexId = containerIndex.ContainerIndexId,
                            ValidTo = deliveryOrder.ValidTo,
                            GatePassType = gatepas != null ? gatepas.UnitType : null,
                            DeliveryType = "CFS"
                        }).FirstOrDefault();

            if (cntr != null && cntr.CargoType == "FCL")
            {
                var res = Db.Invoices.FromSql($"SELECT * From Invoice Where ContainerIndexId = {cntr.ContainerIndexId}  and IsDeleted = 0 ").FirstOrDefault();

                if (res != null)
                {
                    if (res.StorageApplicableon == "Weight")
                    {
                        cntr.GatePassType = "WEIGHT";
                    }
                }

            }

            return cntr;
        }

        public DeliveryOrderViewModel GetDeliveryOrderByDONumberForCyConatiner(long doNumber)
        {
            var cntr = (from deliveryOrder in Db.DeliveryOrders
                        join CyContainer in Db.CYContainers on deliveryOrder.CYContainerId equals CyContainer.CYContainerId
                        where deliveryOrder.DONumber == doNumber
                        select new DeliveryOrderViewModel
                        {
                            VesselName = CyContainer.VesselName,
                            VoyageNo = CyContainer.VoyageNo,
                            IGMNumber = CyContainer.VIRNo,
                            CargoType = "CY",
                            IGMYear = CyContainer.IGMYear,
                            ContainerNumber = CyContainer.ContainerNo,
                            BLNumber = CyContainer.BLNo,
                            indexNumner = CyContainer.IndexNo,
                            ValidTo = deliveryOrder.ValidTo,
                            DeliveryType = "CY"
                        }).FirstOrDefault();

            return cntr;
        }


        public DeliveryOrderViewModel GetDeliveryOrderByContainerId(long containerId)

        {
            var cntr = (from containerIndex in Db.ContainerIndices
                        join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                        join vessel in Db.Vessels on voyage.VesselId equals vessel.VesselId
                        where containerIndex.ContainerIndexId == containerId
                        select new DeliveryOrderViewModel
                        {
                            VesselName = vessel.VesselName,
                            VoyageNo = voyage.VoyageNo,
                            IGMNumber = vessel.IGM,
                            IGMYear = vessel.IGMYear,
                            ContainerNumber = containerIndex.ContainerNo,
                            BLNumber = containerIndex.BLNo,
                            indexNumner = containerIndex.IndexNo,
                            ContainerIndexId = containerIndex.ContainerIndexId


                        }).LastOrDefault();

            return cntr;
        }



        public IEnumerable<CFSAndCYCargoInformationViewModel> GetCargoInformationImport(string igm, string container, string index, string blnumberlist)
        {


            var res = new List<CFSAndCYCargoInformationViewModel>();

            var data = (from containerIndex in Db.ContainerIndices
                        from destuffContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                        from tellySheet in Db.TellySheets.Where(x => x.TellySheetId == destuffContainer.TellySheetId).DefaultIfEmpty()
                        from goodshead in Db.GoodsHeads.Where(x => x.GoodsHeadId == containerIndex.GoodsHeadId).DefaultIfEmpty()
                        from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                        from deliveryOrder in Db.DeliveryOrders.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                        from viro in Db.VIROs.Where(x => x.VIRNumber == containerIndex.VirNo).DefaultIfEmpty()
                        from consigne in Db.Consignes.Where(x => x.ConsigneId == containerIndex.ConsigneId).DefaultIfEmpty()
                        from isocode in Db.ISOCodes.Where(x => x.Code == containerIndex.ISOCode).DefaultIfEmpty()
                        from emptyout in Db.EmptyGateOutContainers.Where(x => x.VirNo == containerIndex.VirNo && x.ContainerNo == containerIndex.ContainerNo).DefaultIfEmpty()

                            //from examinationRequests in Db.ExaminationRequests.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                            //from crlo in Db.CRLOs.Where(x => x.VIRNumber == containerIndex.VirNo && x.BLNumber == containerIndex.BLNo && x.IndexNumber == containerIndex.IndexNo ).DefaultIfEmpty().
                        where
                          (containerIndex.VirNo == igm || igm == null || igm == "")
                          &&
                          (containerIndex.ContainerNo == container || container == null || container == "")
                          &&
                          (containerIndex.BLNo == blnumberlist || blnumberlist == null || blnumberlist == "")
                          &&
                          (containerIndex.IndexNo == Convert.ToInt64(index) || index == null || index == "")
                        select new CFSAndCYCargoInformationViewModel
                        {

                            VirNumber = containerIndex.VirNo,
                            ContainerNumber = containerIndex.ContainerNo,
                            BLNumber = containerIndex.BLNo,
                            IndexNumber = containerIndex.IndexNo,
                            ManifestedQTY = destuffContainer != null ? destuffContainer.ManifestQuantity : null,
                            GoodsDesc = containerIndex.Description,
                            GoodsType = goodshead != null ? goodshead.GoodsHeadName : "",
                            Line = shippingAgent != null ? shippingAgent.Name : "",
                            VesselName = viro != null ? viro.VesselName : "",
                            VoyageNumber = viro != null ? viro.Voyage : "",
                            ArrivalInAICT = containerIndex.CFSContainerGateInDate != null ? containerIndex.CFSContainerGateInDate : null,
                            CFSCY = "CFS",
                            LCLFCL = containerIndex.CargoType,
                            DONumber = deliveryOrder != null ? deliveryOrder.DONumber.ToString() : "",
                            VesselArrival = viro != null ? viro.BerthOn : null,
                            IGMDate = containerIndex.DeleteDate,
                            FoundQTY = destuffContainer != null ? destuffContainer.PackageQuantity : null,
                            ActualQty = containerIndex.NoOfPackages,
                            ConsigneeName = consigne != null ? consigne.ConsigneName : "",
                            Type = isocode != null ? isocode.Code : "",
                            DestuffingDate = destuffContainer != null ? tellySheet.DestuffDate : null,
                            Size = containerIndex.Size,
                            IsDGCargo = containerIndex.IsDGCargo != true ? "No" : "Yes",
                            DeliveryDate = containerIndex.CFSContainerGateOutDate != null ? containerIndex.CFSContainerGateOutDate : null,
                            EmptyOffHireDate = emptyout.CreatedDate != null ? emptyout.CreatedDate : null,

                            //ExaminationDate = examinationRequests != null ? examinationRequests.CustomOutChargeDate,
                            //IsExamin  = string.Join(",", Db.ECMOs.Where(s => s.VIRNumber == containerIndex.VirNo && s.IndexNumber == containerIndex.IndexNo && s.BLNumber == containerIndex.BLNo).Select(x => x.Location))
                            //ReleaseStatus = crlo != null ? "RELEASE MESSAGE RECEIVED" : "NON-WEBOC GD OR RELEASE MESSAGE NOT RECEIVED",
                            //BalanceQty = Db.OrderDetails.Where(s => s.VirNumber == containerIndex.VirNo && s.IndexNo == containerIndex.IndexNo).LastOrDefault().BalancePackages,
                            BRTShed = string.Join(",", Db.BRTs.Where(s => s.ContainerIndexId == containerIndex.ContainerIndexId).Select(x => x.Location)),
                            CRNumber = string.Join(",", Db.Invoices.Where(s => s.ContainerIndexId == containerIndex.ContainerIndexId).Select(x => x.InvoiceNo)),
                            TruckNumber = string.Join(",", Db.GateIns.Where(s => s.VIRNo == containerIndex.VirNo && s.ContainerNo == containerIndex.ContainerNo).Select(x => x.VehicleNo)),
                            //TruckOut = string.Join(",", Db.OrderDetails.Where(s => s.VirNumber == containerIndex.VirNo && s.IndexNo == containerIndex.IndexNo).Select(x => x.)),


                        }).ToList();

            if (data.Count() > 0)
            {

                res.AddRange(data);

            }


            var datacy = (from containerIndex in Db.CYContainers
                          from goodshead in Db.GoodsHeads.Where(x => x.GoodsHeadId == containerIndex.GoodsHeadId).DefaultIfEmpty()
                          from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                          from deliveryOrder in Db.DeliveryOrders.Where(x => x.CYContainerId == containerIndex.CYContainerId).DefaultIfEmpty()
                          from viro in Db.VIROs.Where(x => x.VIRNumber == containerIndex.VIRNo).DefaultIfEmpty()
                          from consigne in Db.Consignes.Where(x => x.ConsigneId == containerIndex.ConsigneId).DefaultIfEmpty()
                          from isocode in Db.ISOCodes.Where(x => x.Code == containerIndex.ISOCode).DefaultIfEmpty()
                              //from examinationRequests in Db.ExaminationRequests.Where(x => x.CYContainerId == containerIndex.CYContainerId).DefaultIfEmpty()
                              //from crlo in Db.CRLOs.Where(x => x.VIRNumber == containerIndex.VirNo && x.BLNumber == containerIndex.BLNo && x.IndexNumber == containerIndex.IndexNo ).DefaultIfEmpty().
                          where
                            (containerIndex.VIRNo == igm || igm == null || igm == "")
                            &&
                            (containerIndex.ContainerNo == container || container == null || container == "")
                            &&
                            (containerIndex.BLNo == blnumberlist || blnumberlist == null || blnumberlist == "")
                            &&
                            (containerIndex.IndexNo == Convert.ToInt64(index) || index == null || index == "")
                          select new CFSAndCYCargoInformationViewModel
                          {
                              VirNumber = containerIndex.VIRNo,
                              ContainerNumber = containerIndex.ContainerNo,
                              BLNumber = containerIndex.BLNo,
                              IndexNumber = containerIndex.IndexNo,
                              ManifestedQTY = null,
                              GoodsDesc = containerIndex.Description,
                              GoodsType = goodshead != null ? goodshead.GoodsHeadName : "",
                              Line = shippingAgent != null ? shippingAgent.Name : "",
                              VesselName = viro != null ? viro.VesselName : "",
                              VoyageNumber = viro != null ? viro.Voyage : "",
                              ArrivalInAICT = containerIndex.CYContainerGateInDate != null ? containerIndex.CYContainerGateInDate : null,
                              CFSCY = "CY",
                              LCLFCL = containerIndex.CargoType,
                              DONumber = deliveryOrder != null ? deliveryOrder.DONumber.ToString() : "",
                              VesselArrival = viro != null ? viro.BerthOn : null,
                              IGMDate = containerIndex.DeleteDate,
                              FoundQTY = null,
                              ActualQty = containerIndex.NoOfPackages,
                              ConsigneeName = consigne != null ? consigne.ConsigneName : "",
                              Type = isocode != null ? isocode.Code : "",
                              DestuffingDate = null,
                              Size = containerIndex.Size,
                              IsDGCargo = containerIndex.IsDGCargo != true ? "No" : "Yes",
                              DeliveryDate = containerIndex.CYContainerGateOutDate != null ? containerIndex.CYContainerGateOutDate : null,
                              EmptyOffHireDate = null,
                              //ExaminationDate = examinationRequests != null ? examinationRequests.CustomOutChargeDate,
                              //IsExamin  = string.Join(",", Db.ECMOs.Where(s => s.VIRNumber == containerIndex.VirNo && s.IndexNumber == containerIndex.IndexNo && s.BLNumber == containerIndex.BLNo).Select(x => x.Location))
                              //ReleaseStatus = crlo != null ? "RELEASE MESSAGE RECEIVED" : "NON-WEBOC GD OR RELEASE MESSAGE NOT RECEIVED",
                              //BalanceQty = Db.OrderDetails.Where(s => s.VirNumber == containerIndex.VirNo && s.IndexNo == containerIndex.IndexNo).LastOrDefault().BalancePackages,
                              BRTShed = string.Join(",", Db.BRTs.Where(s => s.CyContainerId == containerIndex.CYContainerId).Select(x => x.Location)),
                              CRNumber = string.Join(",", Db.Invoices.Where(s => s.CYContainerId == containerIndex.CYContainerId).Select(x => x.InvoiceNo)),
                              TruckNumber = string.Join(",", Db.GateIns.Where(s => s.VIRNo == containerIndex.VIRNo && s.ContainerNo == containerIndex.ContainerNo).Select(x => x.VehicleNo)),
                              //TruckOut = string.Join(",", Db.OrderDetails.Where(s => s.VirNumber == containerIndex.VirNo && s.IndexNo == containerIndex.IndexNo).Select(x => x.)),


                          }).ToList();

            if (datacy.Count() > 0)
            {

                res.AddRange(datacy);

            }
            return res;
        }

        public IEnumerable<CargoInformationViewModel> GetCargoInformationCFSConatiner(long containerIndexId)
        {

            var data = (from containerIndex in Db.ContainerIndices
                        from destuffContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                        from tellySheet in Db.TellySheets.Where(x => x.TellySheetId == destuffContainer.TellySheetId).DefaultIfEmpty()
                        from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                        where containerIndex.ContainerIndexId == containerIndexId
                        select new CargoInformationViewModel
                        {
                            ShippingAgentId = containerIndex != null ? containerIndex.ShippingAgentId : null,
                            AgentName = shippingAgent.Name,
                            CotnainerId = containerIndex.ContainerIndexId,
                            IndexNo = containerIndex.IndexNo,
                            CBM = containerIndex.CBM,
                            ContainerNo = containerIndex.ContainerNo,
                            BLNo = containerIndex.BLNo,
                            Size = containerIndex.Size,
                            ManifestedWeight = destuffContainer != null ? destuffContainer.ManifestWeight : null,
                            FoundWeight = destuffContainer != null ? destuffContainer.FoundWeight : null,
                            ManifestedQTY = destuffContainer != null ? destuffContainer.ManifestQuantity : null,
                            ManifestedUOP = destuffContainer != null ? destuffContainer.PackageType : null,
                            FoundQTY = destuffContainer != null ? destuffContainer.PackageQuantity : null,
                            FoundUOP = destuffContainer != null ? destuffContainer.FoundPackageType : null,
                            Description = destuffContainer != null ? destuffContainer.Description : null,
                            MarksAndNumber = destuffContainer != null ? destuffContainer.MarksAndNumber : null,
                            Location = destuffContainer != null ? destuffContainer.Location : null,
                            GateInDate = destuffContainer != null ? containerIndex.CFSContainerGateInDate : null,
                            DestuffDate = destuffContainer != null ? tellySheet.DestuffDate : null,
                            DestuffedRemark = destuffContainer != null ? destuffContainer.Remarks : null,
                            VirNo = containerIndex.VirNo
                        }).ToList();
            return data;
        }

        public IEnumerable<CargoInformationViewModel> GetCargoInformationCYConatiner(long CYContainerId)
        {

            var data = (from cyContainer in Db.CYContainers
                        from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == cyContainer.ShippingAgentId).DefaultIfEmpty()
                        from gateIn in Db.GateIns.Where(x => x.ContainerNo == cyContainer.ContainerNo).DefaultIfEmpty()
                        where cyContainer.CYContainerId == CYContainerId
                        select new CargoInformationViewModel
                        {
                            ShippingAgentId = cyContainer != null ? cyContainer.ShippingAgentId : null,
                            AgentName = shippingAgent != null ? shippingAgent.Name : null,
                            IndexNo = cyContainer != null ? cyContainer.IndexNo : null,
                            CBM = cyContainer != null ? cyContainer.CBM : null,
                            ContainerNo = cyContainer != null ? cyContainer.ContainerNo : null,
                            VirNo = cyContainer != null ? cyContainer.VIRNo : null,
                            BLNo = cyContainer != null ? cyContainer.BLNo : null,
                            Size = cyContainer.Size,
                            ManifestedWeight = cyContainer != null ? cyContainer.BLGrossWeight : null,
                            FoundWeight = cyContainer != null ? cyContainer.BLGrossWeight : null,
                            ManifestedQTY = cyContainer.NoOfPackages,
                            ManifestedUOP = cyContainer != null ? cyContainer.PackageType : null,
                            FoundQTY = cyContainer.NoOfPackages,
                            FoundUOP = cyContainer != null ? cyContainer.PackageType : null,
                            Description = cyContainer != null ? cyContainer.Description : null,
                            MarksAndNumber = cyContainer != null ? cyContainer.MarksAndNumber : null,
                            Location = "",
                            GateInDate = gateIn != null ? gateIn.GateInDateTime : null
                        }).ToList();
            return data;
        }


        public IEnumerable<CargoInformationViewModel> GetCargodetailCFSConatiner(string virno, string containerno, string indexno, string desc)
        {

            var data = (from containerIndex in Db.ContainerIndices
                        from voyage in Db.Voyages.Where(x => x.VoyageId == containerIndex.VoyageId).DefaultIfEmpty()
                        from destuffContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                        from tellySheet in Db.TellySheets.Where(x => x.TellySheetId == destuffContainer.TellySheetId).DefaultIfEmpty()
                        from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                        from invoice in Db.Invoices.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                        from deliveryorder in Db.DeliveryOrders.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                        from gatePass in Db.OrderDetails.Where(x => x.DeliveryOrderId == deliveryorder.DeliveryOrderId).DefaultIfEmpty()
                        from gatein in Db.GateIns.Where(x => x.ContainerNo == containerIndex.ContainerNo && x.VIRNo == voyage.VIRNo).DefaultIfEmpty()
                        where
                        (voyage.VIRNo == virno || virno == null || virno == "")
                          &&
                        (containerIndex.ContainerNo == containerno || containerno == null || containerno == "")
                          &&
                        (containerIndex.IndexNo == Convert.ToInt64(indexno) || indexno == null || indexno == "")
                         &&
                         (containerIndex.Description.Contains(desc) || desc == null || desc == "")
                        select new CargoInformationViewModel
                        {
                            AgentName = shippingAgent.Name,
                            IndexNo = containerIndex.IndexNo,
                            CBM = containerIndex.CBM,
                            ContainerNo = containerIndex.ContainerNo,
                            BLNo = containerIndex.BLNo,
                            Size = containerIndex.Size,
                            ManifestedWeight = destuffContainer != null ? destuffContainer.ManifestWeight : null,
                            FoundWeight = destuffContainer != null ? destuffContainer.FoundWeight : null,
                            ManifestedQTY = destuffContainer != null ? destuffContainer.ManifestQuantity : null,
                            ManifestedUOP = destuffContainer != null ? destuffContainer.PackageType : null,
                            FoundQTY = destuffContainer != null ? destuffContainer.PackageQuantity : null,
                            FoundUOP = destuffContainer != null ? destuffContainer.FoundPackageType : null,
                            Description = containerIndex != null ? containerIndex.Description : null,
                            MarksAndNumber = destuffContainer != null ? destuffContainer.MarksAndNumber : null,
                            Location = destuffContainer != null ? destuffContainer.Location : null,
                            GateInDate = containerIndex != null ? containerIndex.CFSContainerGateInDate : null,
                            FoundSealNumber = gatein != null ? gatein.FoundSealNumber : null,
                            DestuffDate = tellySheet != null ? tellySheet.DestuffDate : null,
                            DestuffedRemark = destuffContainer != null ? destuffContainer.Remarks : null,
                            InvoiceNo = invoice != null ? invoice.InvoiceNo : null,
                            InvoiceDate = invoice != null ? invoice.InvoiceDate : null,
                            DoNo = deliveryorder != null ? deliveryorder.DONumber : 0,
                            DoDate = deliveryorder != null ? deliveryorder.Date : null,
                            GatePassNo = gatePass != null ? gatePass.GatePassNumber : "",
                            GatePassDate = gatePass != null ? gatePass.GatePassDate : null
                        }).ToList();
            return data;
        }

        public IEnumerable<CargoInformationViewModel> GetCargodetailCYConatiner(string virno, string containerno, string indexno, string desc)
        {

            var data = (from cycontainer in Db.CYContainers
                        from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == cycontainer.ShippingAgentId).DefaultIfEmpty()
                        from gatein in Db.GateIns.Where(x => x.ContainerNo == cycontainer.ContainerNo && x.VIRNo == cycontainer.VIRNo).DefaultIfEmpty()
                        from invoice in Db.Invoices.Where(x => x.CYContainerId == cycontainer.CYContainerId).DefaultIfEmpty()
                        from deliveryorder in Db.DeliveryOrders.Where(x => x.CYContainerId == cycontainer.CYContainerId).DefaultIfEmpty()
                        from gatePass in Db.OrderDetails.Where(x => x.DeliveryOrderId == deliveryorder.DeliveryOrderId).DefaultIfEmpty()
                        where
                         (cycontainer.VIRNo == virno || virno == null || virno == "")
                          &&
                        (cycontainer.ContainerNo == containerno || containerno == null || containerno == "")
                          &&
                        (cycontainer.IndexNo == Convert.ToInt64(indexno) || indexno == null || indexno == "")
                         &&
                         (cycontainer.Description.Contains(desc) || desc == null || desc == "")

                        select new CargoInformationViewModel
                        {

                            AgentName = shippingAgent != null ? shippingAgent.Name : null,
                            IndexNo = cycontainer != null ? cycontainer.IndexNo : null,
                            CBM = cycontainer != null ? cycontainer.CBM : null,
                            ContainerNo = cycontainer != null ? cycontainer.ContainerNo : null,
                            BLNo = cycontainer != null ? cycontainer.BLNo : null,
                            Size = cycontainer.Size,
                            ManifestedWeight = cycontainer != null ? cycontainer.BLGrossWeight : null,
                            FoundWeight = cycontainer != null ? cycontainer.BLGrossWeight : null,
                            ManifestedQTY = cycontainer.NoOfPackages,
                            ManifestedUOP = cycontainer != null ? cycontainer.PackageType : null,
                            FoundQTY = cycontainer.NoOfPackages,
                            FoundUOP = cycontainer != null ? cycontainer.PackageType : null,
                            Description = cycontainer != null ? cycontainer.Description : null,
                            MarksAndNumber = cycontainer != null ? cycontainer.MarksAndNumber : null,
                            Location = "",
                            GateInDate = cycontainer != null ? cycontainer.CYContainerGateInDate : null,
                            FoundSealNumber = gatein != null ? gatein.FoundSealNumber : null,
                            InvoiceNo = invoice != null ? invoice.InvoiceNo : null,
                            InvoiceDate = invoice != null ? invoice.InvoiceDate : null,
                            DoNo = deliveryorder != null ? deliveryorder.DONumber : 0,
                            DoDate = deliveryorder != null ? deliveryorder.Date : null,
                            GatePassNo = gatePass != null ? gatePass.GatePassNumber : "",
                            GatePassDate = gatePass != null ? gatePass.GatePassDate : null
                        }).ToList();
            return data;
        }


        public IEnumerable<CargoInformationViewModel> Section79ContainersCY()
        {
            var data = (from cyContainer in Db.CYContainers
                        join shippingAgent in Db.ShippingAgents on cyContainer.ShippingAgentId equals shippingAgent.ShippingAgentId
                        join gateIn in Db.GateIns on cyContainer.ContainerNo equals gateIn.ContainerNo
                        join ihc in Db.IHCs on gateIn.ContainerNo equals ihc.ContainerNumber
                        where ihc.HandlingCode == "ES" && !Db.IHCs.Any(es => (es.HandlingCode == "EM") && (es.ContainerNumber == gateIn.ContainerNo))
                        select new CargoInformationViewModel
                        {
                            CotnainerId = cyContainer.CYContainerId,
                            AgentName = shippingAgent.Name,
                            IGM = cyContainer.VIRNo,
                            IndexNo = cyContainer.IndexNo,
                            CBM = cyContainer.CBM,
                            ContainerNo = cyContainer.ContainerNo,
                            Size = cyContainer.Size,
                            ManifestedWeight = cyContainer.BLGrossWeight,
                            FoundWeight = cyContainer.BLGrossWeight,
                            ManifestedQTY = cyContainer.NoOfPackages,
                            ManifestedUOP = cyContainer.PackageType,
                            FoundQTY = cyContainer.NoOfPackages,
                            FoundUOP = cyContainer.PackageType,
                            Description = cyContainer.Description,
                            MarksAndNumber = cyContainer.MarksAndNumber,
                            Location = "",
                            GateInDate = gateIn.GateInDateTime
                        }).Distinct().ToList();

            return data;
        }



        public IEnumerable<CargoInformationViewModel> Section79CycontainersCFS()
        {
            var data = (from containerIndex in Db.ContainerIndices
                        join shippingAgent in Db.ShippingAgents on containerIndex.ShippingAgentId equals shippingAgent.ShippingAgentId
                        join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                        //join gateIn in Db.GateIns on containerIndex.ContainerNo equals gateIn.ContainerNo
                        join ihco in Db.IHCOs on containerIndex.BLNo equals ihco.BLNumber
                        where ihco.HandlingCode == "ES"
                         && !Db.IHCOs.Any(es => (es.HandlingCode == "EM")
                        && (es.BLNumber == containerIndex.BLNo))
                        select new CargoInformationViewModel
                        {
                            AgentName = shippingAgent.Name,
                            IGM = voyage.VIRNo,
                            BLNo = containerIndex.BLNo,
                            IndexNo = containerIndex.IndexNo,
                            CBM = containerIndex.CBM,
                            Size = containerIndex.Size,
                            ManifestedWeight = Db.ContainerIndices.Where(x => x.BLNo == containerIndex.BLNo).Sum(s => s.BLGrossWeight),
                            ManifestedQTY = containerIndex.NoOfPackages,
                            ManifestedUOP = containerIndex.PackageType,
                            FoundQTY = containerIndex.NoOfPackages,
                            FoundUOP = containerIndex.PackageType,
                            Description = containerIndex.Description,
                            MarksAndNumber = containerIndex.MarksAndNumber,
                            Location = "",
                            //GateInDate = gateIn.GateInDateTime
                        }).Distinct().ToList();

            return data;
        }


        public IEnumerable<ContainerIndexViewModel> GetContainerIndexBYVoyageId(long voyageid)
        {
            var data = (from Containerindex in Db.ContainerIndices
                        where Containerindex.VoyageId == voyageid
                        select new ContainerIndexViewModel
                        {
                            IndexNo = Containerindex.IndexNo,
                            ContainerIndexId = Containerindex.ContainerIndexId
                        }).ToList();

            return data;



        }

        public string GetDocumentCodeCFS(long ContainerIndexId)
        {
            var code = (from containerIndex in Db.ContainerIndices
                        join crlo in Db.CRLOs on containerIndex.BLNo equals crlo.BLNumber
                        select crlo.DocumentCodes).FirstOrDefault();

            return code;
        }


        public string GetDocumentCodeCY(long CyContainerId)
        {
            var code = (from container in Db.CYContainers
                        join crl in Db.CRLs on container.ContainerNo equals crl.ContainerNumber
                        select crl.DocumentCodes).FirstOrDefault();

            return code;
        }

        public void UpdateAuctionContainersCY()
        {
            List<CYContainer> tempList = new List<CYContainer>();

            var auctionLot = Db.Dictionaries.FirstOrDefault(s => s.Key == "AuctionCounterCY");

            if (auctionLot != null)
            {
                var serial = Convert.ToInt32(auctionLot.Value);

                var cyContainers = GetCyAuctionContainers();

                foreach (var container in cyContainers)
                {
                    if (tempList.FirstOrDefault(c => c.CYContainerId == container.CYContainerId) == null)
                    {
                        serial = serial + 1;

                        var lotNo = GetLotNoCY(serial);

                        var updateContainers = cyContainers
                            .Where(c => c.VIRNo == container.VIRNo && c.IndexNo == container.IndexNo)
                            .ToList();

                        foreach (var updateContainer in updateContainers)
                        {
                            updateContainer.AuctionLotNo = lotNo;

                            updateContainer.IsHold = true;

                            _cycontainerRepo.Update(updateContainer);

                            tempList.Add(updateContainer);
                        }

                        auctionLot.Value = serial.ToString();

                        _dictionaryRepo.Update(auctionLot);
                    }

                }
            }
        }

        internal string GetLotNoCY(int serial)
        {
            if (serial < 10)
                return $"BWPAUCTION-CY-000{serial}-{DateTime.Now.ToString("dd/MM/yyyy")}";
            else if (serial < 100)
                return $"BWPAUCTION-CY-00{serial}-{DateTime.Now.ToString("dd/MM/yyyy")}";
            else if (serial < 1000)
                return $"BWPAUCTION-CY-0{serial}-{DateTime.Now.ToString("dd/MM/yyyy")}";
            else if (serial < 10000)
                return $"BWPAUCTION-CY-{serial}-{DateTime.Now.ToString("dd/MM/yyyy")}";

            return "";
        }



        public List<CCMO> GetccmoContainers()
        {
            var data = (from ccmo in Db.CCMOs
                        where !Db.SCMOs.Any(x => x.ContainerNo == ccmo.ContainerNo && x.VIRNo == ccmo.VIRNo)
                        select new CCMO
                        {
                            VIRNo = ccmo.VIRNo,
                            ContainerNo = ccmo.ContainerNo,
                            CCMOId = ccmo.CCMOId
                        }).ToList();
            return data;

        }


        public List<ContainerIndexDetail> GetIGMDetailIndexWise(string igm, string IndexNo)
        {

            var data = (from containerIndex in Db.ContainerIndices
                        from isocode in Db.ISOCodes.Where(x => x.Code == containerIndex.ISOCode).DefaultIfEmpty()
                        from viro in Db.VIROs.Where(x => x.VIRNumber == containerIndex.VirNo).DefaultIfEmpty()
                            // from shipingline in Db.ShippingLines.Where(x=> x.ShippingLineName.Trim().ToUpper() ==  containerIndex.ShippingLine.Trim().ToUpper()).DefaultIfEmpty()

                        where
                        containerIndex.IndexNo == Convert.ToInt32(IndexNo) && containerIndex.VirNo == igm
                        select new ContainerIndexDetail
                        {
                            ContainerIndexId = containerIndex.ContainerIndexId,
                            MeasurementCBM = containerIndex.MeasurementCBM,
                            Description = containerIndex.Description,
                            VirNo = containerIndex != null ? containerIndex.VirNo : "",
                            ContainerNo = containerIndex != null ? containerIndex.ContainerNo : "",
                            Size = isocode != null ? isocode.ContainerSize : "",
                            IsoCodeType = isocode != null ? isocode.Descrition : "",
                            //ManifestedWeight = containerIndex != null ? containerIndex.ManifestedWeight : 0,
                            ManifestedWeight = containerIndex != null ? containerIndex.ManifestedWeight > 0 ? containerIndex.ManifestedWeight : (containerIndex.BLGrossWeight / 1000) ?? 0 : 0,
                            Status = containerIndex != null ? containerIndex.Status : "",
                            IndexNo = containerIndex != null ? containerIndex.IndexNo : null,
                            ManifestedSealNumber = containerIndex != null ? containerIndex.ManifestedSealNumber : "",
                            PackageType = containerIndex != null ? containerIndex.PackageType : "",
                            NoOfPackages = containerIndex != null ? Convert.ToInt32(containerIndex.NoOfPackages) : 0,
                            BLGrossWeight = containerIndex != null ? containerIndex.BLGrossWeight : 0,
                            WeightIntan = containerIndex != null ? (containerIndex.BLGrossWeight / 1000) : 0,
                            MarksAndNumber = containerIndex != null ? containerIndex.MarksAndNumber : "",
                            //ShippingLineId = shipingline != null ? shipingline.ShippingLineId : 0,
                            ShippingLineId = containerIndex != null ? containerIndex.ShippingLineId : null,
                            BLNo = containerIndex != null ? containerIndex.BLNo : "",
                            CBM = containerIndex != null ? containerIndex.CBM : 0,
                            CargoType = containerIndex != null ? containerIndex.CargoType : "",
                            ConsigneId = containerIndex != null ? containerIndex.ConsigneId : null,
                            GoodsHeadId = containerIndex != null ? containerIndex.GoodsHeadId : null,
                            PortAndTerminalId = containerIndex != null ? containerIndex.PortAndTerminalId : null,
                            PortOfLoadingId = containerIndex != null ? containerIndex.PortOfLoadingId : null,
                            ShipperId = containerIndex != null ? containerIndex.ShipperId : null,
                            ShippingAgentId = containerIndex != null ? containerIndex.ShippingAgentId : null,
                            IsPartialContainer = containerIndex != null ? containerIndex.IsPartialContainer : null,
                            IsDGCargo = containerIndex.IsDGCargo,
                            VesselName = viro != null ? viro.VesselName : "",
                            VoyageNo = viro != null ? viro.Voyage : "",
                            ETA = viro != null ? viro.BerthOn : null,
                            DeliveryDate = containerIndex.DeliveryDate
                        }).ToList();



            var serialno = 0;
            foreach (var item in data)
            {
                serialno += 1;
                item.SerialNo = serialno;
            }

            return data;

        }

        public List<ContainerIndexDetail> GetIGMDetailContainerWise(string igm, string ContainerNo)
        {

            var data = (from containerIndex in Db.ContainerIndices
                        from isocode in Db.ISOCodes.Where(x => x.Code == containerIndex.ISOCode).DefaultIfEmpty()
                        from viro in Db.VIROs.Where(x => x.VIRNumber == containerIndex.VirNo).DefaultIfEmpty()
                            //from shipingline in Db.ShippingLines.Where(x => x.ShippingLineName.Trim().ToUpper() == containerIndex.ShippingLine.Trim().ToUpper()).DefaultIfEmpty()
                        where
                        containerIndex.ContainerNo == ContainerNo && containerIndex.VirNo == igm
                        select new ContainerIndexDetail
                        {

                            ContainerIndexId = containerIndex.ContainerIndexId,
                            MeasurementCBM = containerIndex.MeasurementCBM,
                            Description = containerIndex.Description,
                            VirNo = containerIndex != null ? containerIndex.VirNo : "",
                            ContainerNo = containerIndex != null ? containerIndex.ContainerNo : "",
                            Size = isocode != null ? isocode.ContainerSize : "",
                            IsoCodeType = isocode != null ? isocode.Descrition : "",
                            ManifestedWeight = containerIndex != null ? containerIndex.ManifestedWeight > 0 ? containerIndex.ManifestedWeight : (containerIndex.BLGrossWeight / 1000) ?? 0 : 0,
                            Status = containerIndex != null ? containerIndex.Status : "",
                            IndexNo = containerIndex != null ? containerIndex.IndexNo : null,
                            ManifestedSealNumber = containerIndex != null ? containerIndex.ManifestedSealNumber : "",
                            PackageType = containerIndex != null ? containerIndex.PackageType : "",
                            NoOfPackages = containerIndex != null ? Convert.ToInt32(containerIndex.NoOfPackages) : 0,
                            BLGrossWeight = containerIndex != null ? containerIndex.BLGrossWeight : 0,
                            WeightIntan = containerIndex != null ? (containerIndex.BLGrossWeight / 1000) : 0,
                            MarksAndNumber = containerIndex != null ? containerIndex.MarksAndNumber : "",
                            //ShippingLineId = shipingline != null ? shipingline.ShippingLineId : 0,
                            ShippingLineId = containerIndex != null ? containerIndex.ShippingLineId : null,
                            BLNo = containerIndex != null ? containerIndex.BLNo : "",
                            CBM = containerIndex != null ? containerIndex.CBM : 0,
                            CargoType = containerIndex != null ? containerIndex.CargoType : "",
                            ConsigneId = containerIndex != null ? containerIndex.ConsigneId : null,
                            GoodsHeadId = containerIndex != null ? containerIndex.GoodsHeadId : null,
                            PortAndTerminalId = containerIndex != null ? containerIndex.PortAndTerminalId : null,
                            PortOfLoadingId = containerIndex != null ? containerIndex.PortOfLoadingId : null,
                            ShipperId = containerIndex != null ? containerIndex.ShipperId : null,
                            ShippingAgentId = containerIndex != null ? containerIndex.ShippingAgentId : null,
                            IsPartialContainer = containerIndex != null ? containerIndex.IsPartialContainer : null,
                            IsDGCargo = containerIndex.IsDGCargo,
                            VesselName = viro != null ? viro.VesselName : "",
                            VoyageNo = viro != null ? viro.Voyage : "",
                            ETA = viro != null ? viro.BerthOn : null,
                            DeliveryDate = containerIndex.DeliveryDate


                        }).ToList();

            var serialno = 0;
            foreach (var item in data)
            {
                serialno += 1;
                item.SerialNo = serialno;
            }

            return data;

        }

        public List<ContainerIndexDetail> GetIGMDetailCYContainerIndexWise(string igm, string IndexNo)
        {
            var data = (from cycontainer in Db.CYContainers
                        from isocode in Db.ISOCodes.Where(x => x.Code == cycontainer.ISOCode).DefaultIfEmpty()
                            //from shipingline in Db.ShippingLines.Where(x => x.ShippingLineName.Trim().ToUpper() == cycontainer.ShippingLine.Trim().ToUpper()).DefaultIfEmpty()

                        where
                        cycontainer.IndexNo == Convert.ToInt32(IndexNo) && cycontainer.VIRNo == igm
                        select new ContainerIndexDetail
                        {

                            ContainerIndexId = cycontainer.CYContainerId,
                            Description = cycontainer.Description,
                            VirNo = cycontainer != null ? cycontainer.VIRNo : "",
                            ContainerNo = cycontainer != null ? cycontainer.ContainerNo : "",
                            Size = isocode != null ? isocode.ContainerSize : "",
                            IsoCodeType = isocode != null ? isocode.Descrition : "",
                            Status = cycontainer != null ? cycontainer.Status : "",
                            IndexNo = cycontainer != null ? cycontainer.IndexNo : null,
                            ManifestedSealNumber = cycontainer != null ? cycontainer.ManifestedSealNumber : "",
                            PackageType = cycontainer != null ? cycontainer.PackageType : "",
                            NoOfPackages = cycontainer != null ? Convert.ToInt32(cycontainer.NoOfPackages) : 0,
                            BLGrossWeight = cycontainer != null ? cycontainer.BLGrossWeight : 0,
                            WeightIntan = cycontainer != null ? (cycontainer.BLGrossWeight / 1000) : 0,
                            MarksAndNumber = cycontainer != null ? cycontainer.MarksAndNumber : "",
                            //ShippingLineId = shipingline != null ? shipingline.ShippingLineId : 0,
                            ShippingLineId = cycontainer != null ? cycontainer.ShippingLineId : null,
                            BLNo = cycontainer != null ? cycontainer.BLNo : "",
                            CBM = cycontainer.CBM ?? 0,
                            CargoType = cycontainer != null ? cycontainer.CargoType : "",
                            ConsigneId = cycontainer != null ? cycontainer.ConsigneId : null,
                            GoodsHeadId = cycontainer != null ? cycontainer.GoodsHeadId : null,
                            PortAndTerminalId = cycontainer != null ? cycontainer.PortAndTerminalId : null,
                            PortOfLoadingId = cycontainer != null ? cycontainer.PortOfLoadingId : null,
                            ShipperId = cycontainer != null ? cycontainer.ShipperId : null,
                            ShippingAgentId = cycontainer != null ? cycontainer.ShippingAgentId : null,
                            IsPartialContainer = cycontainer != null ? cycontainer.IsPartialContainer : null,
                            IsDGCargo = cycontainer.IsDGCargo,
                            VesselName = "",
                            ETA = cycontainer.BerthOn != null ? cycontainer.BerthOn : null,
                            DeliveryDate = cycontainer.DeliveryDate
                        }).ToList();

            var serialno = 0;
            foreach (var item in data)
            {
                serialno += 1;
                item.SerialNo = serialno;
            }

            return data;

        }


        public List<CargoInformationViewModel> GetCargodetailCFSOrCYConatiner(string virno, string containerno)
        {

            var cfsdata = (from containerIndex in Db.ContainerIndices
                           from destuffContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           from tellySheet in Db.TellySheets.Where(x => x.TellySheetId == destuffContainer.TellySheetId).DefaultIfEmpty()
                           from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                           from invoice in Db.Invoices.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           from deliveryorder in Db.DeliveryOrders.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           from gatePass in Db.OrderDetails.Where(x => x.DeliveryOrderId == deliveryorder.DeliveryOrderId).DefaultIfEmpty()
                           from gatein in Db.GateIns.Where(x => x.ContainerNo == containerIndex.ContainerNo && x.VIRNo == containerIndex.VirNo).DefaultIfEmpty()
                           from brt in Db.BRTs.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           where
                           (containerIndex.VirNo == virno || virno == null || virno == "")
                             &&
                           (containerIndex.ContainerNo == containerno || containerno == null || containerno == "")
                           select new CargoInformationViewModel
                           {
                               CotnainerId = containerIndex.ContainerIndexId,
                               VirNo = containerIndex.VirNo,
                               AgentName = shippingAgent.Name,
                               IndexNo = containerIndex.IndexNo,
                               CBM = containerIndex.CBM,
                               Status = containerIndex.Status,
                               ContainerNo = containerIndex.ContainerNo,
                               BLNo = containerIndex.BLNo,
                               Size = containerIndex.Size,
                               ManifestedWeight = destuffContainer != null ? destuffContainer.ManifestWeight : null,
                               FoundWeight = destuffContainer != null ? destuffContainer.FoundWeight : null,
                               ManifestedQTY = destuffContainer != null ? destuffContainer.ManifestQuantity : null,
                               ManifestedUOP = destuffContainer != null ? destuffContainer.PackageType : null,
                               FoundQTY = destuffContainer != null ? destuffContainer.PackageQuantity : null,
                               FoundUOP = destuffContainer != null ? destuffContainer.FoundPackageType : null,
                               Description = containerIndex != null ? containerIndex.Description : null,
                               MarksAndNumber = destuffContainer != null ? destuffContainer.MarksAndNumber : null,
                               Location = brt != null ? brt.Location : "",
                               GateInDate = containerIndex != null ? containerIndex.CFSContainerGateInDate : null,
                               FoundSealNumber = gatein != null ? gatein.FoundSealNumber : null,
                               DestuffDate = tellySheet != null ? tellySheet.DestuffDate : null,
                               DestuffedRemark = destuffContainer != null ? destuffContainer.Remarks : null,
                               InvoiceNo = invoice != null ? invoice.InvoiceNo : null,
                               InvoiceDate = invoice != null ? invoice.InvoiceDate : null,
                               DoNo = deliveryorder != null ? deliveryorder.DONumber : 0,
                               DoDate = deliveryorder != null ? deliveryorder.Date : null,
                               GatePassNo = gatePass != null ? gatePass.GatePassNumber : "",
                               GatePassDate = gatePass != null ? gatePass.GatePassDate : null
                           }).Distinct().ToList();


            var cydata = (from cycontainer in Db.CYContainers
                          from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == cycontainer.ShippingAgentId).DefaultIfEmpty()
                          from gatein in Db.GateIns.Where(x => x.ContainerNo == cycontainer.ContainerNo && x.VIRNo == cycontainer.VIRNo).DefaultIfEmpty()
                          from invoice in Db.Invoices.Where(x => x.CYContainerId == cycontainer.CYContainerId).DefaultIfEmpty()
                          from deliveryorder in Db.DeliveryOrders.Where(x => x.CYContainerId == cycontainer.CYContainerId).DefaultIfEmpty()
                          from gatePass in Db.OrderDetails.Where(x => x.DeliveryOrderId == deliveryorder.DeliveryOrderId).DefaultIfEmpty()
                          from brt in Db.BRTs.Where(x => x.CyContainerId == cycontainer.CYContainerId).DefaultIfEmpty()
                          where
                           (cycontainer.VIRNo == virno || virno == null || virno == "")
                            &&
                          (cycontainer.ContainerNo == containerno || containerno == null || containerno == "")

                          select new CargoInformationViewModel
                          {
                              CotnainerId = cycontainer.CYContainerId,
                              VirNo = cycontainer.VIRNo,
                              Status = cycontainer.Status,
                              AgentName = shippingAgent != null ? shippingAgent.Name : null,
                              IndexNo = cycontainer != null ? cycontainer.IndexNo : null,
                              CBM = cycontainer != null ? cycontainer.CBM : null,
                              ContainerNo = cycontainer != null ? cycontainer.ContainerNo : null,
                              BLNo = cycontainer != null ? cycontainer.BLNo : null,
                              Size = cycontainer.Size,
                              ManifestedWeight = cycontainer != null ? cycontainer.BLGrossWeight : null,
                              FoundWeight = cycontainer != null ? cycontainer.BLGrossWeight : null,
                              ManifestedQTY = cycontainer.NoOfPackages,
                              ManifestedUOP = cycontainer != null ? cycontainer.PackageType : null,
                              FoundQTY = cycontainer.NoOfPackages,
                              FoundUOP = cycontainer != null ? cycontainer.PackageType : null,
                              Description = cycontainer != null ? cycontainer.Description : null,
                              MarksAndNumber = cycontainer != null ? cycontainer.MarksAndNumber : null,
                              Location = brt != null ? brt.Location : "",
                              GateInDate = cycontainer != null ? cycontainer.CYContainerGateInDate : null,
                              FoundSealNumber = gatein != null ? gatein.FoundSealNumber : null,
                              InvoiceNo = invoice != null ? invoice.InvoiceNo : null,
                              InvoiceDate = invoice != null ? invoice.InvoiceDate : null,
                              DoNo = deliveryorder != null ? deliveryorder.DONumber : 0,
                              DoDate = deliveryorder != null ? deliveryorder.Date : null,
                              GatePassNo = gatePass != null ? gatePass.GatePassNumber : "",
                              GatePassDate = gatePass != null ? gatePass.GatePassDate : null
                          }).ToList();


            cfsdata.AddRange(cydata);

            return cfsdata;
        }


        public List<ContainerIndex> GetCargodetailCFSUnDeliveredCargo(string type, DateTime frmodate, DateTime todate)
        {
            var datetime = DateTime.Now;

            var cargotype = "";

            if (type == "LCL Vehicle")
            {
                cargotype = "CR";
            }
            else
            {
                cargotype = "CF";
            }

            var result = (from containerIndex in Db.ContainerIndices
                          join destuff in Db.DestuffedContainers on containerIndex.ContainerIndexId equals destuff.ContainerIndexId
                          join tellysheet in Db.TellySheets on destuff.TellySheetId equals tellysheet.TellySheetId
                          where
                          containerIndex.IsGateIn == true
                          &&
                          containerIndex.IsGateOut == false
                          &&
                          containerIndex.IsAuction == false
                          &&
                          Convert.ToDateTime(containerIndex.CFSContainerGateInDate.Value.ToShortDateString()) >= Convert.ToDateTime(frmodate.ToShortDateString())
                          &&
                          Convert.ToDateTime(containerIndex.CFSContainerGateInDate.Value.ToShortDateString()) <= Convert.ToDateTime(todate.ToShortDateString())
                          &&
                          tellysheet.ContainerType == type
                          select new ContainerIndex
                          {

                              //ContainerIndexId = containerIndex.ContainerIndexId,
                              VirNo = containerIndex.VirNo,
                              //ContainerNo = containerIndex.ContainerNo,
                              BLNo = containerIndex.VirNo + "-" + containerIndex.IndexNo,
                              IndexNo = containerIndex.IndexNo,
                              //AuctionLotNo = cargotype + "-" +  + datetime.Month  + "-" + datetime.Year ,
                              CFSContainerGateInDate = containerIndex.CFSContainerGateInDate,
                              Size = EF.Functions.DateDiffDay(containerIndex.CFSContainerGateInDate, DateTime.Now).Value,
                              IsAuction = containerIndex.IsAuction
                          }).Distinct().ToList();



            var containerMerged = (from container in result
                                   group container by container.BLNo into g
                                   select new ContainerIndex
                                   {
                                       IndexNo = g.FirstOrDefault().IndexNo,
                                       VirNo = g.FirstOrDefault().VirNo,
                                       CFSContainerGateInDate = g.FirstOrDefault().CFSContainerGateInDate,
                                       Size = g.FirstOrDefault().Size,
                                       BLNo = g.FirstOrDefault().BLNo,
                                       IsAuction = g.FirstOrDefault().IsAuction
                                   }).ToList();

            return containerMerged;

        }
        public List<CYContainer> GetCargodetailCYUnDeliveredCargo(DateTime frmodate, DateTime todate)
        {
            var datetime = DateTime.Now;

            var result = (from cycontainer in Db.CYContainers
                          where
                          cycontainer.IsGateIn == true
                          &&
                          cycontainer.IsGateOut == false
                           &&
                          cycontainer.IsAuction == false
                          &&
                          Convert.ToDateTime(cycontainer.CYContainerGateInDate.Value.ToShortDateString()) >= Convert.ToDateTime(frmodate.ToShortDateString())
                          &&
                          Convert.ToDateTime(cycontainer.CYContainerGateInDate.Value.ToShortDateString()) <= Convert.ToDateTime(todate.ToShortDateString())

                          select new CYContainer
                          {

                              IndexNo = cycontainer.IndexNo,
                              VIRNo = cycontainer.VIRNo,
                              CYContainerGateInDate = cycontainer.CYContainerGateInDate,
                              Size = EF.Functions.DateDiffDay(cycontainer.CYContainerGateInDate, DateTime.Now).Value,
                              ContainerNo = cycontainer.VIRNo + "-" + cycontainer.IndexNo,
                              IsAuction = cycontainer.IsAuction

                          }).ToList();


            var containerMerged = (from container in result
                                   group container by container.ContainerNo into g
                                   select new CYContainer
                                   {
                                       IndexNo = g.FirstOrDefault().IndexNo,
                                       VIRNo = g.FirstOrDefault().VIRNo,
                                       CYContainerGateInDate = g.FirstOrDefault().CYContainerGateInDate,
                                       Size = g.FirstOrDefault().Size,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       IsAuction = g.FirstOrDefault().IsAuction
                                   }).ToList();

            return containerMerged;

        }


        public List<CargoInformationViewModel> GetIGMCFSOrCYConatiner(string voyageNo, string IGM)
        {

            var cfsdata = (from containerIndex in Db.ContainerIndices
                           from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                           from ipao in Db.IPAOs.Where(x => x.VIRNumber == containerIndex.VirNo && x.ContainerNumber == containerIndex.ContainerNo).DefaultIfEmpty()
                           where
                          (containerIndex.VirNo == IGM)
                           select new CargoInformationViewModel
                           {
                               AgentName = shippingAgent.Name,
                               LineName = containerIndex.ShippingLine,
                               FoundSealNumber = ipao.PCCSSSealNumber,
                               Status = containerIndex.Status,
                               ContainerNo = containerIndex.ContainerNo,
                               Size = containerIndex.Size,
                               GateInDate = containerIndex != null ? containerIndex.CFSContainerGateInDate : null,

                           }).ToList();


            var containerMerged = (from container in cfsdata
                                   group container by container.ContainerNo into g
                                   select new CargoInformationViewModel
                                   {
                                       AgentName = g.FirstOrDefault().AgentName,
                                       LineName = g.FirstOrDefault().LineName,
                                       FoundSealNumber = g.FirstOrDefault().FoundSealNumber,
                                       Status = g.FirstOrDefault().Status,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       Size = g.FirstOrDefault().Size,
                                       GateInDate = g.FirstOrDefault().GateInDate,
                                   }).ToList();

            var cydata = (from cycontainer in Db.CYContainers
                          from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == cycontainer.ShippingAgentId).DefaultIfEmpty()
                          from ipao in Db.IPAOs.Where(x => x.VIRNumber == cycontainer.VIRNo && x.ContainerNumber == cycontainer.ContainerNo).DefaultIfEmpty()

                          where
                       (cycontainer.VIRNo == IGM)
                            &&
                          (cycontainer.VoyageNo == voyageNo)

                          select new CargoInformationViewModel
                          {
                              VirNo = cycontainer.VIRNo,
                              AgentName = shippingAgent.Name,
                              LineName = cycontainer.ShippingLine,
                              FoundSealNumber = ipao.PCCSSSealNumber,
                              Status = cycontainer.Status,
                              ContainerNo = cycontainer.ContainerNo,
                              Size = cycontainer.Size,
                              GateInDate = cycontainer != null ? cycontainer.CYContainerGateInDate : null,
                          }).Distinct().ToList();

            var cycontainerMerged = (from container in cydata
                                     group container by container.ContainerNo into g
                                     select new CargoInformationViewModel
                                     {
                                         AgentName = g.FirstOrDefault().AgentName,
                                         LineName = g.FirstOrDefault().LineName,
                                         FoundSealNumber = g.FirstOrDefault().FoundSealNumber,
                                         Status = g.FirstOrDefault().Status,
                                         ContainerNo = g.FirstOrDefault().ContainerNo,
                                         Size = g.FirstOrDefault().Size,
                                         GateInDate = g.FirstOrDefault().GateInDate,
                                     }).ToList();



            containerMerged.AddRange(cycontainerMerged);

            return containerMerged;
        }



        public List<TransporterViewModel> GetTransportAssigningContainers(string virno, string blnumber)
        {
            var containers = (from container in Db.ContainerIndices
                              from isocode in Db.ISOCodes.Where(x => x.Code == container.ISOCode).DefaultIfEmpty()
                              where
                              container.IsGateIn == false
                              &&
                              container.Status.ToUpper() == "CFS"
                              && container.IsDeleted == false
                              && (container.VirNo == virno || virno == null || virno == "")
                             && (container.BLNo == blnumber || blnumber == null || blnumber == "")
                              select new TransporterViewModel
                              {
                                  ContainerId = container.ContainerIndexId,
                                  ContainerNo = container.ContainerNo,
                                  VIRNo = container.VirNo,
                                  BlNumber = container.BLNo,
                                  ContainerSize = isocode != null ? isocode.ContainerSize : "",
                                  Status = container.Status,
                                  TransporterId = container.TransporterId,
                                  IsGateIn = false
                              }).ToList();

            var containerMerged = (from container in containers
                                   group container by container.ContainerNo into g
                                   select new TransporterViewModel
                                   {
                                       ContainerId = g.FirstOrDefault().ContainerId,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       BlNumber = g.FirstOrDefault().BlNumber,
                                       VIRNo = g.FirstOrDefault().VIRNo,
                                       ContainerSize = g.FirstOrDefault().ContainerSize,
                                       Status = g.FirstOrDefault().Status,
                                       TransporterId = g.FirstOrDefault().TransporterId,
                                       IsGateIn = g.FirstOrDefault().IsGateIn
                                   }).ToList();



            var cycontainers = (from cyContainer in Db.CYContainers
                                from isocode in Db.ISOCodes.Where(x => x.Code == cyContainer.ISOCode).DefaultIfEmpty()

                                where cyContainer.IsGateIn == false
                                && cyContainer.Status.ToUpper() == "CY"
                                && cyContainer.IsDeleted == false
                                && (cyContainer.VIRNo == virno || virno == null || virno == "")
                                && (cyContainer.BLNo == blnumber || blnumber == null || blnumber == "")
                                select new TransporterViewModel
                                {
                                    ContainerId = cyContainer.CYContainerId,
                                    ContainerNo = cyContainer.ContainerNo,
                                    BlNumber = cyContainer.BLNo,
                                    VIRNo = cyContainer.VIRNo,
                                    ContainerSize = isocode != null ? isocode.ContainerSize : "",
                                    Status = cyContainer.Status,
                                    TransporterId = cyContainer.TransporterId,
                                    IsGateIn = false
                                }).ToList();



            containerMerged.AddRange(cycontainers);



            return containerMerged;
        }


        public List<CYContainerViewModel> GetCYContainerListBYIGM(string igm, long indexNo)
        {
            var data = (from cycontainer in Db.CYContainers
                        from exchangerate in Db.ExchangeRates
                        where
                        cycontainer.VIRNo == igm && cycontainer.IndexNo == indexNo
                        select new CYContainerViewModel
                        {
                            Virno = cycontainer.VIRNo,
                            ContainerNo = cycontainer.ContainerNo,
                            ContainerSize = cycontainer.Size,
                            ContainerAmount = cycontainer.Size == 20 ? exchangerate.RateAmount20 * exchangerate.ExchangeRateAmount : cycontainer.Size == 40 ? exchangerate.RateAmount40 * exchangerate.ExchangeRateAmount : cycontainer.Size == 45 ? exchangerate.RateAmount45 * exchangerate.ExchangeRateAmount : 0,
                            Stauts = cycontainer.IsDelivered == true ? "Delivered" : "UnDelivered",
                            CountOfPart = Db.CYContainers.Where(s => s.VIRNo == cycontainer.VIRNo && s.ContainerNo == cycontainer.ContainerNo).Count(),
                        }).ToList();

            data.ForEach(x => x.ContainerAmount = (x.ContainerAmount / x.CountOfPart));

            data.ForEach(x => x.ContainerAmount = Math.Round(x.ContainerAmount ?? 0, 2));
            return data;
        }



        public ContainerInfoViewModel GetContainerDeatilInfoCFS(string igm, long indexnumber)
        {
            var res = (from containerindex in Db.ContainerIndices
                       from viro in Db.VIROs.Where(x => x.VIRNumber == igm).DefaultIfEmpty()
                       from goodhead in Db.GoodsHeads.Where(x => x.GoodsHeadId == containerindex.GoodsHeadId).DefaultIfEmpty()
                       from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerindex.ShippingAgentId).DefaultIfEmpty()
                       from examinationRequest in Db.ExaminationRequests.Where(x => x.ContainerIndexId == containerindex.ContainerIndexId).DefaultIfEmpty()
                       from clearingAgent in Db.ClearingAgents.Where(x => x.ClearingAgentId == examinationRequest.ClearingAgentId).DefaultIfEmpty()
                       from deliveryorder in Db.DeliveryOrders.Where(x => x.ContainerIndexId == containerindex.ContainerIndexId).DefaultIfEmpty()
                       where
                       containerindex.IndexNo == indexnumber
                       &&
                       containerindex.VirNo == igm
                       select new ContainerInfoViewModel
                       {
                           ContainerNo = containerindex.ContainerNo,
                           Cycfs = containerindex.Status,
                           Packages = containerindex.NoOfPackages,
                           Size = containerindex.Size,
                           Vessel = viro != null ? viro.VesselName : "",
                           Voyage = viro != null ? viro.Voyage : "",
                           GoodsDesc = goodhead != null ? goodhead.GoodsHeadName : "",
                           AictDoNo = deliveryorder.DONumber,
                           ArrivalDate = containerindex.CFSContainerGateInDate != null ? containerindex.CFSContainerGateInDate : null,
                           CashRegDateCBE = examinationRequest != null ? examinationRequest.PresentationDate.ToString() : "",
                           CashRegNoCBE = examinationRequest != null ? examinationRequest.BECashNo : "",
                           ChlicenceNo = examinationRequest != null ? examinationRequest.LineDoNumber.ToString() : "",
                           ClearingAgent = clearingAgent != null ? clearingAgent.Name : "",
                           CustomHouse = examinationRequest != null ? examinationRequest.CustomRegNo : "",
                           MarksNo = containerindex.MarksAndNumber,
                           Nicno = examinationRequest != null ? examinationRequest.CNIC : "",
                           RegNoConsignee = examinationRequest != null ? examinationRequest.PhoneNumber : "",
                           LineName = shippingAgent != null ? shippingAgent.Name : "",
                           AgentRep = examinationRequest != null ? examinationRequest.ClearingAgentRep : "",
                           CustomPerNo = "",




                       }).LastOrDefault();

            return res;
        }


        public ContainerInfoViewModel GetContainerDeatilInfoCY(string igm, long indexnumber)
        {
            var res = (from cycontainer in Db.CYContainers
                       from goodhead in Db.GoodsHeads.Where(x => x.GoodsHeadId == cycontainer.GoodsHeadId).DefaultIfEmpty()
                       from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == cycontainer.ShippingAgentId).DefaultIfEmpty()
                       from examinationRequest in Db.ExaminationRequests.Where(x => x.CYContainerId == cycontainer.CYContainerId).DefaultIfEmpty()
                       from clearingAgent in Db.ClearingAgents.Where(x => x.ClearingAgentId == examinationRequest.ClearingAgentId).DefaultIfEmpty()
                       from deliveryorder in Db.DeliveryOrders.Where(x => x.CYContainerId == cycontainer.CYContainerId).DefaultIfEmpty()
                       where
                       cycontainer.IndexNo == indexnumber
                       &&
                       cycontainer.VIRNo == igm
                       select new ContainerInfoViewModel
                       {
                           ContainerNo = cycontainer.ContainerNo,
                           Cycfs = cycontainer.Status,
                           Packages = cycontainer.NoOfPackages,
                           Size = cycontainer.Size,
                           Vessel = cycontainer.VesselName,
                           Voyage = cycontainer.VoyageNo,
                           GoodsDesc = goodhead != null ? goodhead.GoodsHeadName : "",
                           AictDoNo = deliveryorder.DONumber,
                           ArrivalDate = cycontainer.CYContainerGateInDate != null ? cycontainer.CYContainerGateInDate : null,
                           CashRegDateCBE = examinationRequest != null ? examinationRequest.PresentationDate.ToString() : "",
                           CashRegNoCBE = examinationRequest != null ? examinationRequest.BECashNo : "",
                           ChlicenceNo = examinationRequest != null ? examinationRequest.LineDoNumber.ToString() : "",
                           ClearingAgent = clearingAgent != null ? clearingAgent.Name : "",
                           CustomHouse = examinationRequest != null ? examinationRequest.CustomRegNo : "",
                           MarksNo = cycontainer.MarksAndNumber,
                           Nicno = examinationRequest != null ? examinationRequest.CNIC : "",
                           RegNoConsignee = examinationRequest != null ? examinationRequest.PhoneNumber : "",
                           LineName = shippingAgent != null ? shippingAgent.Name : "",
                           AgentRep = examinationRequest != null ? examinationRequest.ClearingAgentRep : "",
                           CustomPerNo = "",




                       }).LastOrDefault();
            return res;
        }




        public List<UnLockGeneratBillViewModel> GetUnlockGenerateContainers()
        {
            var containers = (from container in Db.ContainerIndices
                              from agent in Db.ShippingAgents.Where(c => c.ShippingAgentId == container.ShippingAgentId).DefaultIfEmpty()
                              from goodhead in Db.GoodsHeads.Where(c => c.GoodsHeadId == container.GoodsHeadId).DefaultIfEmpty()
                              from portAndTerminal in Db.PortAndTerminals.Where(c => c.PortAndTerminalId == container.PortAndTerminalId).DefaultIfEmpty()
                              from aictAndLineIndexTariff in Db.AICTAndLineIndexTariffs.Where(c => c.VirNumber == container.VirNo && c.IndexNumber == container.IndexNo).DefaultIfEmpty()

                              where
                            container.IsGateIn == true && container.IsGateOut == false && container.InvoiceLocked == true
                              && container.Status.ToUpper() == "CFS" && container.IsDeleted == false && container.CargoType == "LCL"
                              select new UnLockGeneratBillViewModel
                              {
                                  Key = container.VirNo + "-" + container.ContainerNo,
                                  ContainerNo = container.ContainerNo,
                                  GateInDate = container.CFSContainerGateInDate,
                                  ShippingAgent = agent != null ? agent.Name : "",
                                  PortName = portAndTerminal != null ? portAndTerminal.PortName : "",
                                  CargoType = container.CargoType,
                                  GoodName = goodhead != null ? goodhead.GoodsHeadName : "",
                                  ContainerSize = container.Size,
                                  InvoiceLocked = container.InvoiceLocked != false ? true : false,
                                  ShippingLine = container.ShippingLine,
                                  Status = container.Status,
                                  VIRNo = container.VirNo,
                                  UpdationDate = aictAndLineIndexTariff != null ? aictAndLineIndexTariff.CreatedDate.ToString("MM/dd/yyyy") : "",
                                  CountofIndexes = Db.ContainerIndices.Where(s => s.VirNo == container.VirNo && s.ContainerNo == container.ContainerNo).Count(),


                              }).ToList();

            var containerMerged = (from container in containers

                                   group container by container.ContainerNo into g
                                   select new UnLockGeneratBillViewModel
                                   {
                                       ContainerNo = g.Key,
                                       Key = g.FirstOrDefault().Key,
                                       GateInDate = g.FirstOrDefault().GateInDate,
                                       CargoType = g.FirstOrDefault().CargoType,
                                       InvoiceLocked = g.FirstOrDefault().InvoiceLocked,
                                       GoodName = g.FirstOrDefault().GoodName,
                                       PortName = g.FirstOrDefault().PortName,
                                       ShippingAgent = g.FirstOrDefault().ShippingAgent,
                                       ShippingLine = g.FirstOrDefault().ShippingLine,
                                       ContainerSize = g.FirstOrDefault().ContainerSize,
                                       Status = g.FirstOrDefault().Status,
                                       VIRNo = g.FirstOrDefault().VIRNo,
                                       UpdationDate = g.FirstOrDefault().UpdationDate,
                                       CountofIndexes = g.FirstOrDefault().CountofIndexes,


                                   }).ToList();



            foreach (var item in containerMerged)
            {
                var asd = Db.UnlockGenerateBillRemarks.FromSql($"select * from UnlockGenerateBillRemark  where VirNo = {item.VIRNo} and ContainerNo = {item.ContainerNo} and IsDeleted = 0 ").LastOrDefault();
                if (asd != null)
                {
                    item.Remarks = asd.Remarks;
                }
            }

            return containerMerged;
        }


        public List<GDCR> GetUnCrossStuffingDetailBL()
        {
            var containers = (from container in Db.CYContainers
                              join ihc in Db.IHCs on container.ContainerNo equals ihc.ContainerNumber
                              where container.IsCrossStuffed == false
                             && container.IsGateIn == true
                             && ihc.HandlingCode == "CS"
                             && container.VIRNo == ihc.VIRNumber
                              select new GDCR
                              {
                                  BLNumber = container.BLNo,
                              }).Distinct().ToList();

            return containers;
        }




        public List<GDCR> GetUnCrossStuffingDetail(string blno)
        {
            var containers = (from container in Db.CYContainers
                              from gdcr in Db.GDCRs.Where(c => c.VirNumber == container.VIRNo && c.OldContainerNumber == container.ContainerNo && c.BLNumber == container.BLNo).DefaultIfEmpty()

                              join ihc in Db.IHCs on container.ContainerNo equals ihc.ContainerNumber
                              where container.IsCrossStuffed == false
                             && container.IsGateIn == true
                             && ihc.HandlingCode == "CS"
                             && ihc.VIRNumber == container.VIRNo
                             && container.BLNo == blno
                              select new GDCR
                              {
                                  VirNumber = container.VIRNo,
                                  OldContainerNumber = container.ContainerNo,
                                  BLNumber = container.BLNo,
                                  GDNumber = "",
                                  Flag1 = gdcr != null ? gdcr.Flag1 : "I",
                                  Flag2 = gdcr != null ? gdcr.Flag2 : "",
                                  Flag3 = gdcr != null ? gdcr.Flag3 : "",
                                  NewContainerNumber = container.CSContainerNumber,
                                  OperationType = gdcr != null ? gdcr.OperationType : "",
                                  //Status = gdcr != null ? gdcr.Status : "",                                  
                                  IsContainerStuffed = gdcr != null ? gdcr.IsContainerStuffed : false,
                                  OrderNumber = gdcr.OrderNumber != null ? gdcr.OrderNumber : null,
                              }).ToList();

            var containerMerged = (from container in containers
                                   group container by container.OldContainerNumber into g

                                   select new GDCR
                                   {
                                       VirNumber = g.FirstOrDefault().VirNumber,
                                       OldContainerNumber = g.FirstOrDefault().OldContainerNumber,
                                       BLNumber = g.FirstOrDefault().BLNumber,
                                       GDNumber = g.FirstOrDefault().GDNumber,
                                       Flag1 = g.FirstOrDefault().Flag1,
                                       Flag2 = g.FirstOrDefault().Flag2,
                                       Flag3 = g.FirstOrDefault().Flag3,
                                       NewContainerNumber = g.FirstOrDefault().NewContainerNumber,
                                       OperationType = g.FirstOrDefault().OperationType,
                                       IsContainerStuffed = g.FirstOrDefault().IsContainerStuffed,
                                       OrderNumber = g.FirstOrDefault().OrderNumber,

                                   }).ToList();

            return containerMerged;
        }


        public List<CargoInformationViewModel> GetInquiryFromData(string IGM, long index)
        {

            var containerList = new List<CargoInformationViewModel>();

            var res = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {IGM} and IndexNo = {index} and IsDeleted = 0").FirstOrDefault();
            if (res != null)
            {
                DataTable dt = new DataTable();

                DataSet ds = new DataSet();
                string conString = Con;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("InquiryFormDetails", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@igm", IGM);
                        command.Parameters.AddWithValue("@indexnumber", index);
                        command.Parameters.AddWithValue("@type", "CFS");

                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(command);

                        sda.Fill(ds);

                        containerList = ds.Tables[0].AsEnumerable()
                            .Select(datarow => new CargoInformationViewModel()
                            {

                                ManifestedQTY = datarow.Field<int?>("ManifestedQTY"),
                                ManifestedUOP = datarow.Field<string>("PackageType"),
                                Description = datarow.Field<string>("Description"),
                                CRNumber = datarow.Field<string>("CRNumber"),
                                DoNo = datarow.Field<long>("DONumber"),
                                VesselArrivalDate = datarow.Field<DateTime?>("VesselArrivalDate"),
                                AgentName = datarow.Field<string>("AgentName"),
                                VoyageNumber = datarow.Field<string>("VoyageNumber"),
                                VesselName = datarow.Field<string>("VesselName"),
                                IGMDate = datarow.Field<DateTime?>("IGMDate"),
                                GateInDate = datarow.Field<DateTime?>("GateInDate"),
                                TruckIn = datarow.Field<string>("TruckIn"),
                                Status = datarow.Field<string>("Status"),
                                FCLLCL = datarow.Field<string>("FCLLCL"),
                                Size = datarow.Field<int>("Size"),
                                GoodType = datarow.Field<string>("GoodType"),
                                DestuffDate = datarow.Field<DateTime?>("DestuffDate"),
                                ContainerNo = datarow.Field<string>("ContainerNo"),
                                TruckOut = datarow.Field<string>("TruckOut"),
                                DeliveryDate = datarow.Field<DateTime?>("DeliveryDate"),
                                ConsigneeName = datarow.Field<string>("ConsigneeName"),
                                BLNo = datarow.Field<string>("BLNo"),
                                ClearingAgentName = datarow.Field<string>("ClearingAgentName"),
                                IndexNo = datarow.Field<int?>("IndexNo"),
                                ExaminationStatus = datarow.Field<string>("ExaminationStatus"),
                                ReleaseStatus = datarow.Field<string>("ReleaseStatus"),
                                GatePassDate = datarow.Field<DateTime?>("GatePassDate"),
                                GatePassNo = datarow.Field<string>("GatePassNo"),
                                DGCargo = datarow.Field<string>("IsDGCargo"),
                                ActualQty = datarow.Field<double>("ActualQty"),
                                Balance = datarow.Field<double>("BalanceQty"),
                                ShippingLineName = datarow.Field<string>("ShippingLineName"),
                                DisPackages = datarow.Field<double>("DisPackages"),
                                DisQty = datarow.Field<double>("DisQty"),
                                Brtlocation = datarow.Field<string>("Brtlocation"),
                                LineDoNumber = datarow.Field<string>("LineDoNumber"),
                                ExamDate = datarow.Field<DateTime?>("ExamDate"),

                            }).ToList();

                        con.Close();
                    }
                }


            }

            var rescy = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {IGM} and IndexNo = {index} and IsDeleted = 0").FirstOrDefault();
            if (rescy != null)
            {
                DataTable dt = new DataTable();

                DataSet ds = new DataSet();
                string conString = Con;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("InquiryFormDetails", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@igm", IGM);
                        command.Parameters.AddWithValue("@indexnumber", index);
                        command.Parameters.AddWithValue("@type", "CY");

                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(command);

                        sda.Fill(ds);

                        containerList = ds.Tables[0].AsEnumerable()
                            .Select(datarow => new CargoInformationViewModel()
                            {

                                ManifestedQTY = datarow.Field<int?>("ManifestedQTY"),
                                ManifestedUOP = datarow.Field<string>("PackageType"),
                                Description = datarow.Field<string>("Description"),
                                CRNumber = datarow.Field<string>("CRNumber"),
                                DoNo = datarow.Field<long>("DONumber"),
                                VesselArrivalDate = datarow.Field<DateTime?>("VesselArrivalDate"),
                                AgentName = datarow.Field<string>("AgentName"),
                                VoyageNumber = datarow.Field<string>("VoyageNumber"),
                                VesselName = datarow.Field<string>("VesselName"),
                                IGMDate = datarow.Field<DateTime?>("IGMDate"),
                                GateInDate = datarow.Field<DateTime?>("GateInDate"),
                                TruckIn = datarow.Field<string>("TruckIn"),
                                Status = datarow.Field<string>("Status"),
                                FCLLCL = datarow.Field<string>("FCLLCL"),
                                Size = datarow.Field<int>("Size"),
                                GoodType = datarow.Field<string>("GoodType"),
                                DestuffDate = datarow.Field<DateTime?>("DestuffDate"),
                                ContainerNo = datarow.Field<string>("ContainerNo"),
                                TruckOut = datarow.Field<string>("TruckOut"),
                                DeliveryDate = datarow.Field<DateTime?>("DeliveryDate"),
                                ConsigneeName = datarow.Field<string>("ConsigneeName"),
                                BLNo = datarow.Field<string>("BLNo"),
                                ClearingAgentName = datarow.Field<string>("ClearingAgentName"),
                                IndexNo = datarow.Field<int?>("IndexNo"),
                                ExaminationStatus = datarow.Field<string>("ExaminationStatus"),
                                ReleaseStatus = datarow.Field<string>("ReleaseStatus"),
                                GatePassDate = datarow.Field<DateTime?>("GatePassDate"),
                                ExamDate = datarow.Field<DateTime?>("ExamDate"),
                                GatePassNo = datarow.Field<string>("GatePassNo"),
                                DGCargo = datarow.Field<string>("IsDGCargo"),
                                ActualQty = datarow.Field<double>("ActualQty"),
                                Balance = datarow.Field<double>("BalanceQty"),
                                ShippingLineName = datarow.Field<string>("ShippingLineName"),
                                DisPackages = datarow.Field<double>("DisPackages"),
                                DisQty = datarow.Field<double>("DisQty"),
                                Brtlocation = datarow.Field<string>("Brtlocation"),
                                LineDoNumber = datarow.Field<string>("LineDoNumber"),

                            }).ToList();

                        con.Close();
                    }
                }

            }

            return containerList;

            //var res = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {IGM} and IndexNo = {index} and IsDeleted = 0").FirstOrDefault();
            //if(res != null)
            //{
            //    var data = (from containerIndex in Db.ContainerIndices
            //                from destuffContainer in Db.DestuffedContainers.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
            //                from tellySheet in Db.TellySheets.Where(x => x.TellySheetId == destuffContainer.TellySheetId).DefaultIfEmpty()
            //                from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
            //                from deliveryOrder in Db.DeliveryOrders.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
            //                from viro in Db.VIROs.Where(x => x.VIRNumber == containerIndex.VirNo).DefaultIfEmpty()
            //                from ipao in Db.IPAOs.Where(x => x.VIRNumber == containerIndex.VirNo && x.ContainerNumber == containerIndex.ContainerNo).DefaultIfEmpty()
            //                from gtoo in Db.GTOOs.Where(x => x.VIRNumber == containerIndex.VirNo && x.ContainerNumber == containerIndex.ContainerNo && x.BLNumber == containerIndex.BLNo).DefaultIfEmpty()
            //                from consignee in Db.Consignes.Where(x => x.ConsigneId == containerIndex.ConsigneId ).DefaultIfEmpty()
            //                from examinationrequest in Db.ExaminationRequests.Where(x => x.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
            //                from clearingAgent in Db.ClearingAgents.Where(x => x.ClearingAgentId == examinationrequest.ClearingAgentId ).DefaultIfEmpty()
            //                from gatepass in Db.OrderDetails.Where(x => x.DeliveryOrderId == deliveryOrder.DeliveryOrderId).DefaultIfEmpty()
            //                from crlo in Db.CRLOs.Where(x => x.VIRNumber == containerIndex.VirNo && x.IndexNumber == containerIndex.IndexNo && x.BLNumber == containerIndex.BLNo).DefaultIfEmpty()
            //                from goodsHead in Db.GoodsHeads.Where(x => x.GoodsHeadId == containerIndex.GoodsHeadId).DefaultIfEmpty()

            //                where containerIndex.ContainerIndexId == res.ContainerIndexId
            //                select new CargoInformationViewModel
            //                {
            //                    ManifestedQTY = destuffContainer != null ? destuffContainer.ManifestQuantity : null,
            //                    Description = destuffContainer != null ? destuffContainer.Description : null,
            //                    CRNumber = "",
            //                    DeliveryOrderNumber = deliveryOrder != null ? Convert.ToString( deliveryOrder.DONumber) : "",
            //                    VesselArrivalDate = viro != null ? viro.Performed : null,
            //                    AgentName = shippingAgent.Name,
            //                    VoyageNumber = viro != null ? viro.Voyage : "",
            //                    VesselName = viro != null ? viro.VesselName : "",
            //                    IGMDate =  containerIndex.DeleteDate ,
            //                    CotnainerId = containerIndex.ContainerIndexId,
            //                    IndexNo = containerIndex.IndexNo,
            //                    GateInDate = GetCFSContainerGateInDate(res.ContainerIndexId), 
            //                    TruckIn = ipao != null ? ipao.VehicleNumber : "",
            //                    Status = "CFS",
            //                    FCLLCL = containerIndex.CargoType,
            //                    Size = containerIndex.Size,
            //                    GoodType = goodsHead != null ? goodsHead.GoodsHeadName : "",
            //                    DestuffDate = destuffContainer != null ? tellySheet.DestuffDate : null,
            //                    TruckOut = gtoo != null ? gtoo.Truck : null,
            //                    DeliveryDate = gtoo != null ? gtoo.GateOutTime : null,
            //                    ConsigneeName = consignee != null ? consignee.ConsigneName : "",
            //                    BLNo = containerIndex.BLNo,
            //                    ClearingAgentName = clearingAgent != null ? clearingAgent.Name : "",
            //                    ManifestedUOP = containerIndex.PackageType,
            //                    NumberOFPackages = GetCFSNumberofPackages(res.ContainerIndexId),                                
            //                    FoundUOP = destuffContainer != null ? destuffContainer.FoundPackageType : null,
            //                    ExaminationStatus = examinationrequest != null ? "YES" : "NO",
            //                    GatePassDate = gatepass != null ? gatepass.GatePassDate : null,
            //                    GatePassNo = gatepass != null ? gatepass.GatePassNumber : "",
            //                    CrloStatus = crlo !=  null ? "MESSAGE  RECEIVED" : "NON-WEBOC GD OR RELEASE MESSAGE NOT RECEIVED",
            //                }).LastOrDefault();

            //    resdata = data;
            //}


            //var rescy = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {IGM} and IndexNo = {index} and IsDeleted = 0").FirstOrDefault();
            //if (rescy != null)
            //{
            //    var data = (from CYContainer in Db.CYContainers
            //                from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == CYContainer.ShippingAgentId).DefaultIfEmpty()
            //                from deliveryOrder in Db.DeliveryOrders.Where(x => x.ContainerIndexId == CYContainer.ContainerIndexId).DefaultIfEmpty()
            //                from viro in Db.VIROs.Where(x => x.VIRNumber == CYContainer.VirNo).DefaultIfEmpty()
            //                from ipao in Db.IPAOs.Where(x => x.VIRNumber == CYContainer.VirNo && x.ContainerNumber == CYContainer.ContainerNo).DefaultIfEmpty()
            //                from gatepass in Db.OrderDetails.Where(x => x.DeliveryOrderId == deliveryOrder.DeliveryOrderId).DefaultIfEmpty()

            //                where CYContainer.CYContainerId == res.ContainerIndexId
            //                select new CargoInformationViewModel
            //                {
            //                    ManifestedQTY = destuffContainer != null ? destuffContainer.ManifestQuantity : null,
            //                    Description = destuffContainer != null ? destuffContainer.Description : null,
            //                    CRNumber = "",
            //                    DeliveryOrderNumber = deliveryOrder != null ? Convert.ToString(deliveryOrder.DONumber) : "",
            //                    VesselArrivalDate = viro != null ? viro.Performed : null,
            //                    AgentName = shippingAgent.Name,
            //                    VoyageNumber = viro != null ? viro.Voyage : "",
            //                    VesselName = viro != null ? viro.VesselName : "",
            //                    IGMDate = containerIndex.DeleteDate,
            //                    CotnainerId = containerIndex.ContainerIndexId,
            //                    IndexNo = containerIndex.IndexNo,
            //                    GateInDate = containerIndex.CFSContainerGateInDate,
            //                    CBM = containerIndex.CBM,
            //                    ContainerNo = containerIndex.ContainerNo,
            //                    BLNo = containerIndex.BLNo,
            //                    Size = containerIndex.Size,
            //                    ManifestedWeight = destuffContainer != null ? destuffContainer.ManifestWeight : null,
            //                    FoundWeight = destuffContainer != null ? destuffContainer.FoundWeight : null,
            //                    ManifestedUOP = destuffContainer != null ? destuffContainer.PackageType : null,
            //                    FoundQTY = destuffContainer != null ? destuffContainer.PackageQuantity : null,
            //                    FoundUOP = destuffContainer != null ? destuffContainer.FoundPackageType : null,
            //                    MarksAndNumber = destuffContainer != null ? destuffContainer.MarksAndNumber : null,
            //                    Location = destuffContainer != null ? destuffContainer.Location : null,
            //                    DestuffDate = destuffContainer != null ? tellySheet.DestuffDate : null,
            //                    DestuffedRemark = destuffContainer != null ? destuffContainer.Remarks : null,
            //                    VirNo = containerIndex.VirNo
            //                }).LastOrDefault();

            //    resdata = data;
            //}

        }


        public List<CargoInformationViewModel> GetInquiryFromDatabyblnumber(string blnumber)
        {

            var containerList = new List<CargoInformationViewModel>();

            var res = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where BLNo = {blnumber}  and IsDeleted = 0").LastOrDefault();
            if (res != null)
            {
                DataTable dt = new DataTable();

                DataSet ds = new DataSet();
                string conString = Con;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("InquiryFormDetails", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@igm", res.VirNo);
                        command.Parameters.AddWithValue("@indexnumber", res.IndexNo);
                        command.Parameters.AddWithValue("@type", "CFS");

                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(command);

                        sda.Fill(ds);

                        containerList = ds.Tables[0].AsEnumerable()
                            .Select(datarow => new CargoInformationViewModel()
                            {

                                ManifestedQTY = datarow.Field<int?>("ManifestedQTY"),
                                ManifestedUOP = datarow.Field<string>("PackageType"),
                                Description = datarow.Field<string>("Description"),
                                CRNumber = datarow.Field<string>("CRNumber"),
                                DoNo = datarow.Field<long>("DONumber"),
                                VesselArrivalDate = datarow.Field<DateTime?>("VesselArrivalDate"),
                                AgentName = datarow.Field<string>("AgentName"),
                                VoyageNumber = datarow.Field<string>("VoyageNumber"),
                                VesselName = datarow.Field<string>("VesselName"),
                                IGMDate = datarow.Field<DateTime?>("IGMDate"),
                                GateInDate = datarow.Field<DateTime?>("GateInDate"),
                                TruckIn = datarow.Field<string>("TruckIn"),
                                Status = datarow.Field<string>("Status"),
                                FCLLCL = datarow.Field<string>("FCLLCL"),
                                Size = datarow.Field<int>("Size"),
                                GoodType = datarow.Field<string>("GoodType"),
                                DestuffDate = datarow.Field<DateTime?>("DestuffDate"),
                                ContainerNo = datarow.Field<string>("ContainerNo"),
                                TruckOut = datarow.Field<string>("TruckOut"),
                                DeliveryDate = datarow.Field<DateTime?>("DeliveryDate"),
                                ConsigneeName = datarow.Field<string>("ConsigneeName"),
                                BLNo = datarow.Field<string>("BLNo"),
                                ClearingAgentName = datarow.Field<string>("ClearingAgentName"),
                                IndexNo = datarow.Field<int?>("IndexNo"),
                                ExaminationStatus = datarow.Field<string>("ExaminationStatus"),
                                ReleaseStatus = datarow.Field<string>("ReleaseStatus"),
                                GatePassDate = datarow.Field<DateTime?>("GatePassDate"),
                                GatePassNo = datarow.Field<string>("GatePassNo"),
                                DGCargo = datarow.Field<string>("IsDGCargo"),
                                ActualQty = datarow.Field<double>("ActualQty"),
                                Balance = datarow.Field<double>("BalanceQty"),
                                ShippingLineName = datarow.Field<string>("ShippingLineName"),
                                DisPackages = datarow.Field<double>("DisPackages"),
                                DisQty = datarow.Field<double>("DisQty"),
                                Brtlocation = datarow.Field<string>("Brtlocation"),
                                LineDoNumber = datarow.Field<string>("LineDoNumber"),
                                ExamDate = datarow.Field<DateTime?>("ExamDate"),

                            }).ToList();

                        con.Close();
                    }
                }


            }

            var rescy = Db.CYContainers.FromSql($"SELECT * From CYContainer Where BLNo = {blnumber}   and IsDeleted = 0").LastOrDefault();
            if (rescy != null)
            {
                DataTable dt = new DataTable();

                DataSet ds = new DataSet();
                string conString = Con;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("InquiryFormDetails", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@igm", rescy.VIRNo);
                        command.Parameters.AddWithValue("@indexnumber", rescy.IndexNo);
                        command.Parameters.AddWithValue("@type", "CY");

                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(command);

                        sda.Fill(ds);

                        containerList = ds.Tables[0].AsEnumerable()
                            .Select(datarow => new CargoInformationViewModel()
                            {

                                ManifestedQTY = datarow.Field<int?>("ManifestedQTY"),
                                ManifestedUOP = datarow.Field<string>("PackageType"),
                                Description = datarow.Field<string>("Description"),
                                CRNumber = datarow.Field<string>("CRNumber"),
                                DoNo = datarow.Field<long>("DONumber"),
                                VesselArrivalDate = datarow.Field<DateTime?>("VesselArrivalDate"),
                                AgentName = datarow.Field<string>("AgentName"),
                                VoyageNumber = datarow.Field<string>("VoyageNumber"),
                                VesselName = datarow.Field<string>("VesselName"),
                                IGMDate = datarow.Field<DateTime?>("IGMDate"),
                                GateInDate = datarow.Field<DateTime?>("GateInDate"),
                                TruckIn = datarow.Field<string>("TruckIn"),
                                Status = datarow.Field<string>("Status"),
                                FCLLCL = datarow.Field<string>("FCLLCL"),
                                Size = datarow.Field<int>("Size"),
                                GoodType = datarow.Field<string>("GoodType"),
                                DestuffDate = datarow.Field<DateTime?>("DestuffDate"),
                                ContainerNo = datarow.Field<string>("ContainerNo"),
                                TruckOut = datarow.Field<string>("TruckOut"),
                                DeliveryDate = datarow.Field<DateTime?>("DeliveryDate"),
                                ConsigneeName = datarow.Field<string>("ConsigneeName"),
                                BLNo = datarow.Field<string>("BLNo"),
                                ClearingAgentName = datarow.Field<string>("ClearingAgentName"),
                                IndexNo = datarow.Field<int?>("IndexNo"),
                                ExaminationStatus = datarow.Field<string>("ExaminationStatus"),
                                ReleaseStatus = datarow.Field<string>("ReleaseStatus"),
                                GatePassDate = datarow.Field<DateTime?>("GatePassDate"),
                                ExamDate = datarow.Field<DateTime?>("ExamDate"),
                                GatePassNo = datarow.Field<string>("GatePassNo"),
                                DGCargo = datarow.Field<string>("IsDGCargo"),
                                ActualQty = datarow.Field<double>("ActualQty"),
                                Balance = datarow.Field<double>("BalanceQty"),
                                ShippingLineName = datarow.Field<string>("ShippingLineName"),
                                DisPackages = datarow.Field<double>("DisPackages"),
                                DisQty = datarow.Field<double>("DisQty"),
                                Brtlocation = datarow.Field<string>("Brtlocation"),
                                LineDoNumber = datarow.Field<string>("LineDoNumber"),

                            }).ToList();

                        con.Close();
                    }
                }

            }

            return containerList;


        }

        public long GetCFSNumberofPackages(long ContainerIndexId)
        {

            var numberofpackages = 0;
            var containerindex = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  ContainerIndexId = {ContainerIndexId} and IsDeleted = 0 ").FirstOrDefault();

            if (containerindex != null)
            {
                var containerindexes = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  VirNo = {containerindex.VirNo} and IndexNo = {containerindex.IndexNo} and IsDeleted = 0 ").ToList();

                if (containerindexes.Count() > 0)
                {
                    numberofpackages = containerindexes.Sum(x => x.NoOfPackages);

                    return numberofpackages;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;

            }



            return 0;

        }

        public long GetCFSGatePassDate(long ContainerIndexId)
        {

            var numberofpackages = 0;
            var containerindex = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  ContainerIndexId = {ContainerIndexId} and IsDeleted = 0 ").FirstOrDefault();

            if (containerindex != null)
            {
                var containerindexes = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  VirNo = {containerindex.VirNo} and IndexNo = {containerindex.IndexNo} and IsDeleted = 0 ").ToList();

                if (containerindexes.Count() > 0)
                {
                    numberofpackages = containerindexes.Sum(x => x.NoOfPackages);

                    return numberofpackages;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;

            }



            return 0;

        }

        public DateTime? GetCFSContainerGateInDate(long ContainerIndexId)
        {


            var containerindex = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  ContainerIndexId = {ContainerIndexId} and IsDeleted = 0 ").FirstOrDefault();



            if (containerindex != null)
            {
                var res = (from indexres in Db.ContainerIndices
                           where
                           indexres.VirNo == containerindex.VirNo && indexres.IndexNo == containerindex.IndexNo
                           && indexres.CFSContainerGateInDate != null && indexres.IsGateIn == true
                           orderby indexres.CFSContainerGateInDate descending
                           select indexres.CFSContainerGateInDate).FirstOrDefault();


                if (res != null)
                {

                    return res;
                }
            }
            else
            {
                return null;

            }



            return null;

        }


        public DateTime? GetCYContainerGateInDate(long CyContainerId)
        {

            var container = Db.CYContainers.FromSql($"select * from CYContainer   where  CYContainerId = {CyContainerId} and IsDeleted = 0 ").FirstOrDefault();

            if (container != null)
            {
                var res = (from cycontainerres in Db.CYContainers
                           where
                           cycontainerres.VIRNo == container.VIRNo && cycontainerres.IndexNo == container.IndexNo
                           && cycontainerres.CYContainerGateInDate != null && cycontainerres.IsGateIn == true
                           orderby cycontainerres.CYContainerGateInDate descending
                           select cycontainerres.CYContainerGateInDate).FirstOrDefault();

                if (res != null)
                {

                    return res;
                }
            }
            else
            {
                return null;
            }



            return null;

        }

        public ExaminationRequestViewModel GetExaminationRequestCFS(long? containerId)
        {
            var er = new ExaminationRequestViewModel();

            var line = "";
            var principal = "";
            var consignee = "";
            var consigneNTN = "";
            var consigneId = "";
            var stRegistrationNo = "";
            var commodity = "";

            var container = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where  ContainerIndexId = {containerId} and IsDeleted = 0 ").LastOrDefault();



            if (container != null)
            {
                if (container.ShippingAgentId != null)
                {
                    line = Db.ShippingAgents.FromSql($"select * from ShippingAgent   where  ShippingAgentId = {container.ShippingAgentId} and IsDeleted = 0 ").LastOrDefault().Name;

                }

                if (container.ShippingLineId != null)
                {
                    principal = Db.ShippingLines.FromSql($"select * from ShippingLine   where  ShippingLineId = {container.ShippingLineId} and IsDeleted = 0 ").LastOrDefault().ShippingLineName;

                }
                if (container.ConsigneId != null)
                {
                    consignee = Db.Consignes.FromSql($"select * from Consigne   where  ConsigneId = {container.ConsigneId} and IsDeleted = 0 ").LastOrDefault().ConsigneName;
                    consigneNTN = Db.Consignes.FromSql($"select * from Consigne   where  ConsigneId = {container.ConsigneId} and IsDeleted = 0 ").LastOrDefault().ConsigneNTN;
                    stRegistrationNo = Db.Consignes.FromSql($"select * from Consigne   where  ConsigneId = {container.ConsigneId} and IsDeleted = 0 ").LastOrDefault().STRegistrationNo;
                    consigneId = container.ConsigneId.ToString();
                }
                if (container.GoodsHeadId != null)
                {
                    commodity = Db.GoodsHeads.FromSql($"select * from GoodsHead   where  GoodsHeadId = {container.GoodsHeadId} and IsDeleted = 0 ").LastOrDefault().GoodsHeadName;

                }


                var crlo = Db.CRLOs.FromSql($"select * from CRLO   where  VIRNumber = {container.VirNo} and BLNumber = {container.BLNo} and IndexNumber = {container.IndexNo}  and IsDeleted = 0 ").LastOrDefault();


                var ExaminationRequest = Db.ExaminationRequests.FromSql($"select * from ExaminationRequest   where  ContainerIndexId = {containerId} and IsDeleted = 0 ").LastOrDefault();
                if (ExaminationRequest != null)
                {

                    var resdat = new ExaminationRequestViewModel
                    {
                        BECashNo = ExaminationRequest.BECashNo,
                        ClearingAgentId = ExaminationRequest.ClearingAgentId,
                        ClearingAgentRep = ExaminationRequest.ClearingAgentRep,
                        CNIC = ExaminationRequest.CNIC,
                        Commodity = commodity,
                        Consignee = consignee,
                        ConsigneNTN = consigneNTN,
                        STRegistrationNo = stRegistrationNo,
                        ConsigneId = consigneId,
                        ContainerIndexId = ExaminationRequest.ContainerIndexId,
                        CustomOutChargeDate = ExaminationRequest.CustomOutChargeDate,
                        CustomRegDate = ExaminationRequest.CustomRegDate,
                        CustomRegNo = ExaminationRequest.CustomRegNo,
                        CYContainerId = null,
                        DestuffCBM = container.CBM,
                        ExaminationRequestCBM = ExaminationRequest.ExaminationRequestCBM,
                        ExaminationRequestId = ExaminationRequest.ExaminationRequestId,
                        IgmDetilCBM = container.MeasurementCBM,
                        Line = line,
                        Principal = principal,
                        LineDoNumber = ExaminationRequest.LineDoNumber,
                        PhoneNumber = ExaminationRequest.PhoneNumber,
                        PresentationDate = ExaminationRequest.PresentationDate,
                        ExaminationDate = ExaminationRequest.ExaminationDate,
                        GroundingDate = ExaminationRequest.GroundingDate,
                        IsTPCargo = ExaminationRequest.IsTPCargo,
                        IsEPZCargo = ExaminationRequest.IsEPZCargo,
                        BLNumber = container.BLNo,
                        Email = ExaminationRequest.Email,
                        DeliveryStatus = ExaminationRequest.DeliveryStatus


                    };

                    return resdat;
                }
                else
                {
                    if (crlo != null)
                    {

                        er.Line = line;
                        er.Principal = principal;
                        er.Commodity = commodity;
                        er.Consignee = consignee;
                        er.ConsigneNTN = consigneNTN;
                        er.STRegistrationNo = stRegistrationNo;
                        er.ConsigneId = consigneId;
                        er.DestuffCBM = container.CBM;
                        er.IgmDetilCBM = container.MeasurementCBM;
                        er.CustomOutChargeDate = DateTime.Now;
                        er.CustomRegDate = Convert.ToDateTime(DateTime.ParseExact(crlo.GDNumber.Trim().ToUpper().Substring(crlo.GDNumber.Trim().ToUpper().Length - 10).ToString(), "dd-MM-yyyy", null).ToString("MM-dd-yyyy"));
                        er.PresentationDate = Convert.ToDateTime(DateTime.ParseExact(crlo.GDNumber.Trim().ToUpper().Substring(crlo.GDNumber.Trim().ToUpper().Length - 10).ToString(), "dd-MM-yyyy", null).ToString("MM-dd-yyyy"));
                        er.CustomRegNo = crlo.GDNumber;
                        er.BLNumber = container.BLNo;
                        er.Email = "";


                    }
                    else
                    {

                        er.Line = line;
                        er.Principal = principal;
                        er.Commodity = commodity;
                        er.Consignee = consignee;
                        er.ConsigneNTN = consigneNTN;
                        er.STRegistrationNo = stRegistrationNo;
                        er.ConsigneId = consigneId;
                        er.DestuffCBM = container.CBM;
                        er.IgmDetilCBM = container.MeasurementCBM;
                        er.CustomOutChargeDate = DateTime.Now;
                        er.BLNumber = container.BLNo;
                        er.Email = "";

                    }

                    return er;
                }

            }

            return er;

        }
        public ExaminationRequestViewModel GetExaminationRequestCY(string igm, int? indexno)
        {
            var line = "";
            var principal = "";
            var consignee = "";
            var consigneNTN = "";
            var stRegistrationNo = "";
            var consigneId = "";
            var commodity = "";


            var er = new ExaminationRequestViewModel();

            var container = Db.CYContainers.FromSql($"select * from CYContainer   where  VIRNo = {igm} and IndexNo = {indexno} and IsDeleted = 0 ").FirstOrDefault();

            if (container != null)
            {

                if (container.ShippingAgentId != null)
                {
                    line = Db.ShippingAgents.FromSql($"select * from ShippingAgent   where  ShippingAgentId = {container.ShippingAgentId} and IsDeleted = 0 ").LastOrDefault().Name;

                }

                if (container.ShippingLineId != null)
                {
                    principal = Db.ShippingLines.FromSql($"select * from ShippingLine   where  ShippingLineId = {container.ShippingLineId} and IsDeleted = 0 ").LastOrDefault().ShippingLineName;

                }
                if (container.ConsigneId != null)
                {
                    consignee = Db.Consignes.FromSql($"select * from Consigne   where  ConsigneId = {container.ConsigneId} and IsDeleted = 0 ").LastOrDefault().ConsigneName;
                    consigneNTN = Db.Consignes.FromSql($"select * from Consigne   where  ConsigneId = {container.ConsigneId} and IsDeleted = 0 ").LastOrDefault().ConsigneNTN;
                    stRegistrationNo = Db.Consignes.FromSql($"select * from Consigne   where  ConsigneId = {container.ConsigneId} and IsDeleted = 0 ").LastOrDefault().STRegistrationNo;
                    consigneId = container.ConsigneId.ToString();

                }
                if (container.GoodsHeadId != null)
                {
                    commodity = Db.GoodsHeads.FromSql($"select * from GoodsHead   where  GoodsHeadId = {container.GoodsHeadId} and IsDeleted = 0 ").LastOrDefault().GoodsHeadName;

                }


                var crlo = Db.CRLs.FromSql($"select * from CRL   where  VIRNumber = {container.VIRNo} and ContainerNumber = {container.ContainerNo}   and IsDeleted = 0 ").LastOrDefault();


                var ExaminationRequest = Db.ExaminationRequests.FromSql($"select * from ExaminationRequest   where  CYContainerId = {container.CYContainerId} and IsDeleted = 0 ").LastOrDefault();
                if (ExaminationRequest != null)
                {

                    var resdat = new ExaminationRequestViewModel
                    {
                        BECashNo = ExaminationRequest.BECashNo,
                        ClearingAgentId = ExaminationRequest.ClearingAgentId,
                        ClearingAgentRep = ExaminationRequest.ClearingAgentRep,
                        CNIC = ExaminationRequest.CNIC,
                        Commodity = commodity,
                        Consignee = consignee,
                        ConsigneNTN = consigneNTN,
                        STRegistrationNo = stRegistrationNo,
                        ConsigneId = consigneId,
                        ContainerIndexId = null,
                        CustomOutChargeDate = ExaminationRequest.CustomOutChargeDate,
                        CustomRegDate = ExaminationRequest.CustomRegDate,
                        CustomRegNo = ExaminationRequest.CustomRegNo,
                        CYContainerId = ExaminationRequest.CYContainerId,
                        DestuffCBM = 0,
                        ExaminationRequestCBM = ExaminationRequest.ExaminationRequestCBM,
                        ExaminationRequestId = ExaminationRequest.ExaminationRequestId,
                        IgmDetilCBM = 0,
                        Line = line,
                        Principal = principal,
                        LineDoNumber = ExaminationRequest.LineDoNumber,
                        PhoneNumber = ExaminationRequest.PhoneNumber,
                        PresentationDate = ExaminationRequest.PresentationDate,
                        ExaminationDate = ExaminationRequest.ExaminationDate,
                        GroundingDate = ExaminationRequest.GroundingDate,
                        IsTPCargo = ExaminationRequest.IsTPCargo,
                        IsEPZCargo = ExaminationRequest.IsEPZCargo,
                        BLNumber = container.BLNo,
                        Email = ExaminationRequest.Email,
                        DeliveryStatus = ExaminationRequest.DeliveryStatus





                    };

                    return resdat;
                }
                else
                {
                    if (crlo != null)
                    {


                        er.Line = line;
                        er.Principal = principal;
                        er.Commodity = commodity;
                        er.Consignee = consignee;
                        er.ConsigneNTN = consigneNTN;
                        er.STRegistrationNo = stRegistrationNo;
                        er.ConsigneId = consigneId;
                        er.DestuffCBM = 0;
                        er.IgmDetilCBM = 0;
                        er.CustomOutChargeDate = DateTime.Now;
                        er.CustomRegDate = Convert.ToDateTime(DateTime.ParseExact(crlo.GDNumber.Trim().ToUpper().Substring(crlo.GDNumber.Trim().ToUpper().Length - 10).ToString(), "dd-MM-yyyy", null).ToString("MM-dd-yyyy"));
                        er.PresentationDate = Convert.ToDateTime(DateTime.ParseExact(crlo.GDNumber.Trim().ToUpper().Substring(crlo.GDNumber.Trim().ToUpper().Length - 10).ToString(), "dd-MM-yyyy", null).ToString("MM-dd-yyyy"));
                        er.CustomRegNo = crlo.GDNumber;
                        er.BLNumber = container.BLNo;
                        er.Email = "";


                    }
                    else
                    {
                        er.Line = line;
                        er.Principal = principal;
                        er.Commodity = commodity;
                        er.Consignee = consignee;
                        er.ConsigneNTN = consigneNTN;
                        er.STRegistrationNo = stRegistrationNo;
                        er.ConsigneId = consigneId;
                        er.DestuffCBM = 0;
                        er.IgmDetilCBM = 0;
                        er.CustomOutChargeDate = DateTime.Now;
                        er.BLNumber = container.BLNo;
                        er.Email = "";

                    }

                    return er;
                }



            }


            return er;

        }


        public bool GetDeliveredStatus(string virno, long indexno)
        {
            var gatepss = (from gatepass in Db.OrderDetails
                           join doitem in Db.DOItems on gatepass.GatePassId equals doitem.GatePassId
                           where
                           gatepass.VirNumber == virno
                           &&
                           gatepass.IndexNo == indexno
                           &&
                           doitem.Status == "F"
                           select doitem.Status
                           ).ToList();

            if (gatepss.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<TruckInOut> GetTruckInOuts(string igm, long index, string type)
        {

            var res = Db.TruckInOuts.FromSql($" select * from TruckInOut where VirNumber  = {igm} and IndexNumber = {index} and  isdeleted = 0").ToList();

            if (type == "CFS")
            {
                var container = Db.ContainerIndices.FromSql($" select * from ContainerIndex where VirNo  = {igm} and IndexNo = {index} and  isdeleted = 0").ToList();

                var containerindexidlist = string.Join(",", container.Select(n => n.ContainerIndexId.ToString()).ToArray());

                var dodetail = Db.DeliveryOrders.FromSql($"select * from DeliveryOrder where  ContainerIndexId  in (select data from [dbo].[Split]( {containerindexidlist}, ',') )   and IsDeleted = 0   ").LastOrDefault();

                if (dodetail != null && res.Count() > 0)
                {
                    res.Where(x => x.ValidTo == null).ToList().ForEach(x => x.ValidTo = dodetail.ValidTo);
                }
            }

            if (type == "CY")
            {
                var container = Db.CYContainers.FromSql($" select * from CYContainer where VirNo  = {igm} and IndexNo = {index} and  isdeleted = 0").ToList();

                var containerindexidlist = string.Join(",", container.Select(n => n.CYContainerId.ToString()).ToArray());

                var dodetail = Db.DeliveryOrders.FromSql($"select * from DeliveryOrder where  CYContainerId  in (select data from [dbo].[Split]( {containerindexidlist}, ',') )   and IsDeleted = 0   ").LastOrDefault();

                if (dodetail != null && res.Count() > 0)
                {
                    res.Where(x => x.ValidTo == null).ToList().ForEach(x => x.ValidTo = dodetail.ValidTo);
                }
            }

            return res;
        }

        public List<IGMO> GetContainerList(string igm, long index)
        {
            var res = Db.IGMOs.FromSql($" select * from IGMO where VIRNumber  = {igm} and IndexNumber = {index} and  isdeleted = 0").ToList();

            return res;
        }


        public List<TruckInOut> GetDOIfno(long dono)
        {
            var resdata = new List<TruckInOut>();

            var res = Db.DeliveryOrders.FromSql($" select * from DeliveryOrder where DONumber  = {dono}  and  isdeleted = 0").LastOrDefault();

            if (res != null)
            {

                if (res.ContainerIndexId != null)
                {
                    var contaienindex = Db.ContainerIndices.FromSql($" select * from containerindex where ContainerIndexId  = {res.ContainerIndexId}  and  isdeleted = 0").LastOrDefault();

                    if (contaienindex != null)
                    {

                        var truckInOuts = Db.TruckInOuts.FromSql($" select * from TruckInOut where VirNumber  = {contaienindex.VirNo} and IndexNumber = {contaienindex.IndexNo} and  isdeleted = 0").ToList();

                        return truckInOuts;
                    }
                    else
                    {
                        return resdata;
                    }


                }

                if (res.CYContainerId != null)
                {

                    var contaienindex = Db.CYContainers.FromSql($" select * from CYContainer where CYContainerId  = {res.CYContainerId}  and  isdeleted = 0").LastOrDefault();

                    if (contaienindex != null)
                    {
                        var truckInOuts = Db.TruckInOuts.FromSql($" select * from TruckInOut where VirNumber  = {contaienindex.VIRNo} and IndexNumber = {contaienindex.IndexNo} and  isdeleted = 0").ToList();

                        return truckInOuts;
                    }
                    else
                    {
                        return resdata;
                    }
                }
                return resdata;
            }

            return resdata;

        }

        public ImportFielsViewModel Getigmindexdetail(long dono)
        {

            var res = Db.DeliveryOrders.FromSql($" select * from DeliveryOrder where DONumber  = {dono}  and  isdeleted = 0").LastOrDefault();

            if (res != null)
            {

                if (res.ContainerIndexId != null)
                {
                    var contaienindex = Db.ContainerIndices.FromSql($" select * from containerindex where ContainerIndexId  = {res.ContainerIndexId}  and  isdeleted = 0").LastOrDefault();


                    return new ImportFielsViewModel { VirNumber = contaienindex.VirNo, IndexNo = contaienindex.IndexNo, Type = "CFS" };



                }

                if (res.CYContainerId != null)
                {

                    var contaienindex = Db.CYContainers.FromSql($" select * from CYContainer where CYContainerId  = {res.CYContainerId}  and  isdeleted = 0").LastOrDefault();


                    return new ImportFielsViewModel { VirNumber = contaienindex.VIRNo, IndexNo = contaienindex.IndexNo, Type = "CY" };

                }
                return null;
            }

            return null;
        }


        public DeliveryOrder GetDODetail(string igm, long index)
        {

            var res = Db.IGMOs.FromSql($" select * from IGMO where VIRNumber  = {igm} and IndexNumber = {index} and  isdeleted = 0").LastOrDefault();

            if (res != null)
            {

                if (res.Status == "CFS")
                {
                    var contaienindex = Db.ContainerIndices.FromSql($" select * from containerindex where VirNo  = {igm}  and IndexNo = {index} and  isdeleted = 0").LastOrDefault();

                    if (contaienindex != null)
                    {

                        var data = Db.DeliveryOrders.FromSql($" select * from DeliveryOrder where containerindexId  = {contaienindex.ContainerIndexId}  and  isdeleted = 0").LastOrDefault();

                        return data;
                    }
                    else
                    {
                        return null;
                    }


                }

                if (res.Status == "CY")
                {

                    var contaienindex = Db.CYContainers.FromSql($" select * from CYContainer where VIRNo  = {igm}  and IndexNo = {index} and  isdeleted = 0").FirstOrDefault();

                    if (contaienindex != null)
                    {

                        var data = Db.DeliveryOrders.FromSql($" select * from DeliveryOrder where CYContainerId  = {contaienindex.CYContainerId}  and  isdeleted = 0").LastOrDefault();

                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }

            return null;

        }

        public List<EmptyReceivingCYContainerViewModel> GetEmptyReveivingCrossStuffing()
        {
            var res = (from cycontainer in Db.CYContainers
                       where
                       cycontainer.CSCountryCode == "UZ"
                       &&
                        !Db.GDCRs.Any(es => (es.VirNumber == cycontainer.VIRNo && es.BLNumber == cycontainer.BLNo && es.OldContainerNumber == cycontainer.ContainerNo && es.NewContainerNumber == cycontainer.CSContainerNumber && es.IsSubmit == true))

                       select cycontainer).ToList();



            var containerMerged = (from container in res
                                   group container by container.ContainerNo into g

                                   select new EmptyReceivingCYContainerViewModel
                                   {
                                       VIRNo = g.FirstOrDefault().VIRNo,
                                       ContainerNo = g.FirstOrDefault().ContainerNo,
                                       Size = g.FirstOrDefault().Size,
                                       ShippingLineName = g.FirstOrDefault().ShippingLine,
                                       BLNo = g.FirstOrDefault().BLNo,
                                       CSContainerNumber = g.FirstOrDefault().CSContainerNumber,
                                       CSSize = g.FirstOrDefault().CSSize,
                                       EmptyReceivedShippingLineId = g.FirstOrDefault().EmptyReceivedShippingLineId,
                                       CSVehicleNumer = g.FirstOrDefault().CSVehicleNumer,
                                       CSCondition = g.FirstOrDefault().CSCondition,
                                       CSTireWeight = g.FirstOrDefault().CSTireWeight,
                                       CSEmptyContainerReceiveId = g.FirstOrDefault().CSEmptyContainerReceiveId,
                                   }).ToList();

            return containerMerged;

        }

        public List<PGO> GetPreGateOutCrossStuffing()
        {

            var res = (from cycontainer in Db.CYContainers
                       join crl in Db.CRLs on cycontainer.CSContainerNumber equals crl.ContainerNumber
                       join gdcr in Db.GDCRs on cycontainer.CSContainerNumber equals gdcr.NewContainerNumber
                       where
                       cycontainer.CSCountryCode == "UZ"
                       && crl.VIRNumber == cycontainer.VIRNo
                       && gdcr.VirNumber == cycontainer.VIRNo
                       && gdcr.BLNumber == cycontainer.BLNo
                       && cycontainer.IsCSEmtyptyRecieved == true
                       && cycontainer.IsCrossStuffed == true
                       && !Db.PGOs.Any(es => (es.VIRNumber == cycontainer.VIRNo && es.ContainerNumber == cycontainer.CSContainerNumber))

                       select new PGO
                       {

                           VIRNumber = crl.VIRNumber,
                           ContainerNumber = crl.ContainerNumber,
                           BondedCarrierId = "",
                           BondedCarrierName = "",
                           VehicleNumber = "",
                           IsProcessed = false

                       }).ToList();

            var datares = (from re in res
                           group re by re.ContainerNumber into g
                           select new PGO
                           {
                               ContainerNumber = g.LastOrDefault().ContainerNumber,
                               VIRNumber = g.LastOrDefault().VIRNumber,
                               IsProcessed = g.FirstOrDefault().IsProcessed,
                               BondedCarrierId = g.FirstOrDefault().BondedCarrierId,
                               BondedCarrierName = g.FirstOrDefault().BondedCarrierName,
                               VehicleNumber = g.FirstOrDefault().VehicleNumber,
                           }).ToList();

            return datares;

        }


        public UnLockGeneratBillViewModel GetFFAndGoodsInfo(string virno, long indexno, string type)
        {
            var resdt = new UnLockGeneratBillViewModel();
            if (type == "CFS")
            {

                var data = Db.ContainerIndices.FromSql($" select * from ContainerIndex where VirNo  = {virno}  and IndexNo = {indexno} and  isdeleted = 0").LastOrDefault();

                if (data != null && data.ShippingAgentId != null)
                {
                    var ff = Db.ShippingAgents.FromSql($" select * from ShippingAgent where ShippingAgentId  = {data.ShippingAgentId}  and  isdeleted = 0").LastOrDefault();
                    if (ff != null)
                    {
                        resdt.ShippingAgent = ff.Name;
                    }


                }

                if (data != null && data.GoodsHeadId != null)
                {
                    var goodsHead = Db.GoodsHeads.FromSql($" select * from GoodsHead where GoodsHeadId  = {data.GoodsHeadId}  and  isdeleted = 0").LastOrDefault();
                    if (goodsHead != null)
                    {
                        resdt.GoodName = goodsHead.GoodsHeadName;
                    }


                }

                return resdt;
            }
            else
            {
                var data = Db.CYContainers.FromSql($" select * from CYContainer where VirNo  = {virno}  and IndexNo = {indexno} and  isdeleted = 0").LastOrDefault();

                if (data != null && data.ShippingAgentId != null)
                {
                    var ff = Db.ShippingAgents.FromSql($" select * from ShippingAgent where ShippingAgentId  = {data.ShippingAgentId}  and  isdeleted = 0").LastOrDefault();
                    if (ff != null)
                    {
                        resdt.ShippingAgent = ff.Name;
                    }


                }

                if (data != null && data.GoodsHeadId != null)
                {
                    var goodsHead = Db.GoodsHeads.FromSql($" select * from GoodsHead where GoodsHeadId  = {data.GoodsHeadId}  and  isdeleted = 0").LastOrDefault();
                    if (goodsHead != null)
                    {
                        resdt.GoodName = goodsHead.GoodsHeadName;
                    }


                }

                return resdt;

            }
        }


        public List<DOItem> GetDOItems(string virno, int indexno)
        {
            var res = Db.DOItems.FromSql($"select DOItem.* from gatepass join DOItem on gatepass.GatePassId =DOItem.GatePassId and DOItem.IsDeleted = 0 where gatepass.VirNumber  = {virno} and gatepass.IndexNo = {indexno}  and gatepass.CargoType in ('LCL', 'FCL') and  gatepass.isdeleted = 0 ").ToList();

            return res;
        }


        public ImportContainerLocation GetcontainerLocation(string virno, string containerno)
        {
            var resdata = new ImportContainerLocation();

            var type = "";
            var containerindex = Db.ContainerIndices.FromSql($" select * from ContainerIndex where VirNo  = {virno}  and  ContainerNo  = {containerno}  and  isdeleted = 0").LastOrDefault();

            if (containerindex != null)
            {
                type = "CFS";
            }


            var cycontainer = Db.CYContainers.FromSql($" select * from CYContainer where VirNo  = {virno}  and  ContainerNo  = {containerno}  and  isdeleted = 0").LastOrDefault();

            if (cycontainer != null)
            {
                type = "CY";
            }

            if (type != null || type != "")
            {

                var res = Db.ImportContainerLocations.FromSql($" select * from ImportContainerLocation where Virno  = {virno}  and  ContainerNo  = {containerno}  and  isdeleted = 0").LastOrDefault();

                if (res != null)
                {

                    resdata.Location = res.Location;

                    resdata.Type = type;

                    return resdata;

                }
                else
                {


                    resdata.Location = "";

                    resdata.Type = type;

                    return resdata;
                }

            }
            else
            {

                resdata.Location = "";

                resdata.Type = "";

                return resdata;
            }

        }

        public string GetCFSCargoStatus(long containerId)
        {
            var cfscontainer = Db.ContainerIndices.FromSql($"select * from ContainerIndex  where  ContainerIndexId = {containerId} and   IsDeleted = 0 ").LastOrDefault();

            if (cfscontainer != null)
            {
                var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={cfscontainer.VirNo} and  GatePass.IndexNo = {cfscontainer.IndexNo} and  DOItem.Status = 'F' and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

                if (DeliveredCargogatepass.Count() > 0)
                {
                    return "index is already delivered on Dated: " + DeliveredCargogatepass[0].DeleteDate;
                }
                else
                {
                    return "OK";
                }

            }
            return "OK";

        }

        public string GetStatusCYCargo(string virno, long indexno)
        {


            var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={virno} and  GatePass.IndexNo = {indexno} and  DOItem.Status = 'F' and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

            if (DeliveredCargogatepass.Count() > 0)
            {
                return "index is already delivered on Dated: " + DeliveredCargogatepass[0].DeleteDate;
            }
            else
            {
                return "OK";
            }


        }


        public string GetStatusCYCargoByCycontainerId(long cycontainerid)
        {

            var cycontainer = Db.CYContainers.FromSql($"select  * from  CYContainer where CYContainerId = {cycontainerid} and  IsDeleted = 0 ").LastOrDefault();

            if (cycontainer != null)
            {
                var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={cycontainer.VIRNo} and  GatePass.IndexNo = {cycontainer.IndexNo} and  DOItem.Status = 'F' and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

                if (DeliveredCargogatepass.Count() > 0)
                {
                    return "index is already delivered on Dated: " + DeliveredCargogatepass[0].DeleteDate;
                }
                else
                {
                    return "OK";
                }
            }
            else
            {
                return "index not found ";
            }




        }

        public string GetWaiverIfnoByWaiverno(string waiverno)
        {

            var waiverres = Db.Waivers.FromSql($"select  * from  Waiver where WaiverNumber = {waiverno} and  IsDeleted = 0 ").ToList().Count();
            if (waiverres > 0)
            {
                var waiverresdata = Db.Waivers.FromSql($"select  * from  Waiver where WaiverNumber = {waiverno} and  IsDeleted = 0 and InvoiceCreated = 1 ").ToList().Count();

                if (waiverresdata > 0)
                {
                    return "can't update and delete due to invoice created";
                }
                else
                {
                    return "OK";
                }
            }
            else
            {
                return "no waiver found against";
            }


        }


        public bool CheckDeliveryStatus(string virno, long index)
        {

            var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={virno} and  GatePass.IndexNo = {index} and  DOItem.Status = 'F' and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

            if (DeliveredCargogatepass.Count() > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }


        public ContainerIndex GetLastContainerIndexByIGMAndContainerNo(string Virno, string Containerno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno}  and ContainerNo = {Containerno} and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {


                var idofc = Db.ImportDropOfTickets.FromSql($"SELECT * From ImportDropOfTicket Where VirNumber = {Virno}  and ContainerNo = {Containerno} and IsDeleted = 0").LastOrDefault();

                if (idofc != null)
                {
                    asd.ManifestedSealNumber = idofc.ManifestedSealNumber;
                }

                return asd;

            }
            return null;
        }

        public CYContainer GetLastContainerCYByIGMAndContainerNo(string Virno, string containerno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and ContainerNo = {containerno} and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {

                var idofc = Db.ImportDropOfTickets.FromSql($"SELECT * From ImportDropOfTicket Where VirNumber = {Virno}  and ContainerNo = {containerno} and IsDeleted = 0").LastOrDefault();

                if (idofc != null)
                {
                    asd.ManifestedSealNumber = idofc.ManifestedSealNumber;
                }

                return asd;
            }
            return null;
        }


        public List<GIIO> GetReProcessGateIn()
        {
            var datetime = DateTime.Now;
            datetime = datetime.AddDays(-2);

            var containers = (from ipao in Db.IPAOs
                              join giio in Db.GIIOs on ipao.ContainerNumber equals giio.ContainerNumber
                              where
                               giio.VIRNumber == ipao.VIRNumber
                               &&
                               Convert.ToDateTime(ipao.Performed.Value.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy"))
                              select new GIIO
                              {
                                  ContainerNumber = giio.ContainerNumber,
                                  VIRNumber = giio.VIRNumber,
                                  PCCSSSealNumber = giio.PCCSSSealNumber,
                                  VehicleNumber = giio.VehicleNumber,
                                  Weight = giio.Weight,
                                  //CreateDT = null,
                                  //GateInTime = null,
                                  //Performed = null,
                                  //DeleteDate = DateTime.Now,
                                  //FileName = null,
                                  //IsDeleted = false,
                                  //GIIOId = 0,
                                  //MessageFrom = null,
                                  //MessageTo = null,
                                  //RecordSequence = null,
                                  //TotalRecords = null,                                  
                                  IsProcessed = false
                              }).ToList();

            var containerMerged = (from container in containers
                                   group container by container.ContainerNumber into g
                                   select new GIIO
                                   {
                                       ContainerNumber = g.FirstOrDefault().ContainerNumber,
                                       VIRNumber = g.FirstOrDefault().VIRNumber,
                                       PCCSSSealNumber = g.FirstOrDefault().PCCSSSealNumber,
                                       VehicleNumber = g.FirstOrDefault().VehicleNumber,
                                       Weight = g.FirstOrDefault().Weight,
                                       //CreateDT = g.FirstOrDefault().CreateDT,
                                       //GateInTime = g.FirstOrDefault().GateInTime,
                                       //Performed = g.FirstOrDefault().Performed,
                                       //DeleteDate = g.FirstOrDefault().DeleteDate,
                                       //FileName = g.FirstOrDefault().FileName,
                                       //IsDeleted = g.FirstOrDefault().IsDeleted,
                                       //GIIOId = g.FirstOrDefault().GIIOId,
                                       //MessageFrom = g.FirstOrDefault().MessageFrom,
                                       //MessageTo = g.FirstOrDefault().MessageTo,
                                       //RecordSequence = g.FirstOrDefault().RecordSequence,
                                       //TotalRecords = g.FirstOrDefault().TotalRecords,
                                       IsProcessed = g.FirstOrDefault().IsProcessed
                                   }).ToList();


            return containerMerged;
        }


        public bool GetFileNameGIIO(string FileName)
        {
            var res = Db.GIIOs.FromSql($"select  * from GIIO where IsDeleted = 0  and FileName = {FileName} ").ToList();

            if (res.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckDeliveryStatusForPart(string virno, long index, string containerno)
        {

            var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={virno} and  GatePass.IndexNo = {index} and  GatePass.GatePasscontainerNumber = {containerno}  and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

            if (DeliveredCargogatepass.Count() > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<DOItem> Checkcontainerwisegp(string virno, string containerno)
        {

            var DeliveredCargogatepass = Db.DOItems.FromSql($"select DOItem.* from GatePass join DOItem  on GatePass.GatePassId = DOItem.GatePassId where GatePass.VirNumber ={virno}   and  GatePass.GatePasscontainerNumber = {containerno}  and DOItem.IsDeleted =0 and GatePass.IsDeleted = 0 ").ToList();

            return DeliveredCargogatepass;


        }


        public List<OnlineTrackingViewModel> GetOnlineTracking(string number, string type)
        {

            var resdata = new List<OnlineTrackingViewModel>();
            if (type == "Container")
            {
                var containerno = number.Trim().ToUpper();
                var igmores = Db.IGMOs.FromSql($"select * from IGMO  where   ContainerNumber = {containerno}  and  IsDeleted =0  and OpType = 'A' ").LastOrDefault();
                if (igmores != null)
                {
                    if (igmores.Status == "CFS")
                    {
                        var res = (from containerindex in Db.ContainerIndices
                                   from viro in Db.VIROs.Where(x => x.VIRNumber == containerindex.VirNo).DefaultIfEmpty()
                                   where
                                    containerindex.VirNo == igmores.VIRNumber
                                    && containerindex.ContainerNo == igmores.ContainerNumber

                                   select new OnlineTrackingViewModel
                                   {
                                       VirNo = containerindex.VirNo,
                                       BLNo = containerindex.BLNo,
                                       IndexNo = Convert.ToString(containerindex.IndexNo),
                                       ContainerNo = containerindex.ContainerNo,
                                       Category = "Import - CFS",
                                       LotNo = containerindex.AuctionLotNo,
                                       ContainerSize = Convert.ToString(containerindex.Size),
                                       Weight = Convert.ToString(containerindex.ManifestedWeight),
                                       NoOfPackages = Convert.ToString(containerindex.NoOfPackages),
                                       PackageType = containerindex.PackageType,
                                       BLGrossWeight = Convert.ToString(containerindex.BLGrossWeight),
                                       Description = containerindex.Description,
                                       Status = containerindex.CargoType,
                                       MarksAndNumber = containerindex.MarksAndNumber,
                                       ShippingLineName = containerindex.ShippingLine,
                                       VesselName = viro != null ? viro.VesselName : "",
                                       VoyageNo = viro != null ? viro.Voyage : "",
                                       GateInDate = containerindex.CFSContainerGateInDate,
                                       GateOutDate = containerindex.CFSContainerGateOutDate
                                   }).ToList();
                        resdata.AddRange(res);

                        foreach (var item in resdata)
                        {
                            var crlo = Db.CRLOs.FromSql($"SELECT * From CRLO Where VIRNumber = {item.VirNo}  and BLNumber = {item.BLNo} and IndexNumber = {item.IndexNo} and IsDeleted = 0").LastOrDefault();
                            if (crlo != null)
                            {
                                item.GDNo = crlo.GDNumber;
                            }

                            var srco = Db.SRCOs.FromSql($"SELECT * From SRCO Where VIRNumber = {item.VirNo}  and BLNumber = {item.BLNo} and IndexNumber = {item.IndexNo} and IsDeleted = 0").LastOrDefault();
                            if (srco != null)
                            {
                                item.GroundingDate = srco.Performed;
                            }

                        }

                        return resdata;
                    }
                    if (igmores.Status == "CY")
                    {
                        var res = (from containerindex in Db.CYContainers
                                   from viro in Db.VIROs.Where(x => x.VIRNumber == containerindex.VIRNo).DefaultIfEmpty()
                                   where
                                    containerindex.VIRNo == igmores.VIRNumber
                                    && containerindex.ContainerNo == igmores.ContainerNumber

                                   select new OnlineTrackingViewModel
                                   {
                                       VirNo = containerindex.VIRNo,
                                       BLNo = containerindex.BLNo,
                                       IndexNo = Convert.ToString(containerindex.IndexNo),
                                       ContainerNo = containerindex.ContainerNo,
                                       Category = "Import - CY",
                                       LotNo = containerindex.AuctionLotNo,
                                       ContainerSize = Convert.ToString(containerindex.Size),
                                       Weight = Convert.ToString(containerindex.ContainerGrossWeight),
                                       NoOfPackages = Convert.ToString(containerindex.NoOfPackages),
                                       PackageType = containerindex.PackageType,
                                       BLGrossWeight = Convert.ToString(containerindex.BLGrossWeight),
                                       Description = containerindex.Description,
                                       Status = containerindex.CargoType,
                                       MarksAndNumber = containerindex.MarksAndNumber,
                                       ShippingLineName = containerindex.ShippingLine,
                                       VesselName = viro != null ? viro.VesselName : "",
                                       VoyageNo = viro != null ? viro.Voyage : "",
                                       GateInDate = containerindex.CYContainerGateInDate,
                                       GateOutDate = containerindex.CYContainerGateOutDate
                                   }).ToList();
                        resdata.AddRange(res);

                        foreach (var item in resdata)
                        {
                            var crlo = Db.CRLs.FromSql($"SELECT * From CRL Where VIRNumber = {item.VirNo}  and ContainerNumber = {item.ContainerNo}  and IsDeleted = 0").LastOrDefault();
                            if (crlo != null)
                            {
                                item.GDNo = crlo.GDNumber;
                            }

                            var srco = Db.SRCs.FromSql($"SELECT * From SRC Where VIRNumber = {item.VirNo}  and ContainerNumber = {item.ContainerNo} and IsDeleted = 0").LastOrDefault();
                            if (srco != null)
                            {
                                item.GroundingDate = srco.Performed;
                            }

                        }

                        return resdata;
                    }
                }
            }

            if (type == "BL")
            {
                var blno = number.Trim().ToUpper();
                var igmores = Db.IGMOs.FromSql($"select * from IGMO  where   BLNumber = {blno}  and  IsDeleted =0  and OpType = 'A' ").LastOrDefault();

                if (igmores != null)
                {

                    if (igmores.Status == "CFS")
                    {
                        var res = (from containerindex in Db.ContainerIndices
                                   from viro in Db.VIROs.Where(x => x.VIRNumber == containerindex.VirNo).DefaultIfEmpty()
                                   where
                                    containerindex.VirNo == igmores.VIRNumber
                                    && containerindex.BLNo == igmores.BLNumber

                                   select new OnlineTrackingViewModel
                                   {
                                       VirNo = containerindex.VirNo,
                                       BLNo = containerindex.BLNo,
                                       IndexNo = Convert.ToString(containerindex.IndexNo),
                                       ContainerNo = containerindex.ContainerNo,
                                       Category = "Import - CFS",
                                       LotNo = containerindex.AuctionLotNo,
                                       ContainerSize = Convert.ToString(containerindex.Size),
                                       Weight = Convert.ToString(containerindex.ManifestedWeight),
                                       NoOfPackages = Convert.ToString(containerindex.NoOfPackages),
                                       PackageType = containerindex.PackageType,
                                       BLGrossWeight = Convert.ToString(containerindex.BLGrossWeight),
                                       Description = containerindex.Description,
                                       Status = containerindex.CargoType,
                                       MarksAndNumber = containerindex.MarksAndNumber,
                                       ShippingLineName = containerindex.ShippingLine,
                                       VesselName = viro != null ? viro.VesselName : "",
                                       VoyageNo = viro != null ? viro.Voyage : "",
                                       GateInDate = containerindex.CFSContainerGateInDate,
                                       GateOutDate = containerindex.CFSContainerGateOutDate
                                   }).ToList();
                        resdata.AddRange(res);

                        foreach (var item in resdata)
                        {
                            var crlo = Db.CRLOs.FromSql($"SELECT * From CRLO Where VIRNumber = {item.VirNo}  and BLNumber = {item.BLNo} and IndexNumber = {item.IndexNo} and IsDeleted = 0").LastOrDefault();
                            if (crlo != null)
                            {
                                item.GDNo = crlo.GDNumber;
                            }

                            var srco = Db.SRCOs.FromSql($"SELECT * From SRCO Where VIRNumber = {item.VirNo}  and BLNumber = {item.BLNo} and IndexNumber = {item.IndexNo} and IsDeleted = 0").LastOrDefault();
                            if (srco != null)
                            {
                                item.GroundingDate = srco.Performed;
                            }

                        }


                        return resdata;
                    }
                    if (igmores.Status == "CY")
                    {
                        var res = (from containerindex in Db.CYContainers
                                   from viro in Db.VIROs.Where(x => x.VIRNumber == containerindex.VIRNo).DefaultIfEmpty()
                                   where
                                    containerindex.VIRNo == igmores.VIRNumber
                                    && containerindex.BLNo == igmores.BLNumber

                                   select new OnlineTrackingViewModel
                                   {
                                       VirNo = containerindex.VIRNo,
                                       BLNo = containerindex.BLNo,
                                       IndexNo = Convert.ToString(containerindex.IndexNo),
                                       ContainerNo = containerindex.ContainerNo,
                                       Category = "Import - CY",
                                       LotNo = containerindex.AuctionLotNo,
                                       ContainerSize = Convert.ToString(containerindex.Size),
                                       Weight = Convert.ToString(containerindex.ContainerGrossWeight),
                                       NoOfPackages = Convert.ToString(containerindex.NoOfPackages),
                                       PackageType = containerindex.PackageType,
                                       BLGrossWeight = Convert.ToString(containerindex.BLGrossWeight),
                                       Description = containerindex.Description,
                                       Status = containerindex.CargoType,
                                       MarksAndNumber = containerindex.MarksAndNumber,
                                       ShippingLineName = containerindex.ShippingLine,
                                       VesselName = viro != null ? viro.VesselName : "",
                                       VoyageNo = viro != null ? viro.Voyage : "",
                                       GateInDate = containerindex.CYContainerGateInDate,
                                       GateOutDate = containerindex.CYContainerGateOutDate
                                   }).ToList();
                        resdata.AddRange(res);


                        foreach (var item in resdata)
                        {
                            var crlo = Db.CRLs.FromSql($"SELECT * From CRL Where VIRNumber = {item.VirNo}  and ContainerNumber = {item.ContainerNo}  and IsDeleted = 0").LastOrDefault();
                            if (crlo != null)
                            {
                                item.GDNo = crlo.GDNumber;
                            }

                            var srco = Db.SRCs.FromSql($"SELECT * From SRC Where VIRNumber = {item.VirNo}  and ContainerNumber = {item.ContainerNo} and IsDeleted = 0").LastOrDefault();
                            if (srco != null)
                            {
                                item.GroundingDate = srco.Performed;
                            }

                        }

                        return resdata;
                    }
                }
            }

            return resdata;

        }


        public CSEmptyContainerReceive GetCsContainerbycontainerno(long CSEmptyContainerReceiveId)
        {

            var res = Db.CSEmptyContainerReceives.FromSql($"select   * from CSEmptyContainerReceive  where CSEmptyContainerReceiveId ={CSEmptyContainerReceiveId} and  IsDeleted =0   ").LastOrDefault();

            return res;


        }

        public bool GetInvoiceInfo(string igm, long index, string type)
        {



            if (type == "CFS")
            {
                var contaienindex = Db.ContainerIndices.FromSql($" select * from containerindex where VirNo  = {igm}  and IndexNo = {index} and  isdeleted = 0").ToList();

                if (contaienindex.Count() > 0)
                {

                    var containerindexidlist = string.Join(",", contaienindex.Select(n => n.ContainerIndexId.ToString()).ToArray());

                    var invoicesdetail = Db.Invoices.FromSql($"select * from Invoice where  ContainerIndexId  in (select data from [dbo].[Split]( {containerindexidlist}, ',') )   and IsDeleted = 0   ").LastOrDefault();
                    if (invoicesdetail != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }


            }

            if (type == "CY")
            {

                var contaienindex = Db.CYContainers.FromSql($" select * from CYContainer where VIRNo  = {igm}  and IndexNo = {index} and  isdeleted = 0").ToList();

                if (contaienindex.Count() > 0)
                {

                    var containerindexidlist = string.Join(",", contaienindex.Select(n => n.CYContainerId.ToString()).ToArray());

                    var invoicesdetail = Db.Invoices.FromSql($"select * from Invoice where  CYContainerId  in (select data from [dbo].[Split]( {containerindexidlist}, ',') )   and IsDeleted = 0   ").LastOrDefault();
                    if (invoicesdetail != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;



        }


        public Consigne GetConsigneById(long? id)
        {
            var res = Db.Consignes.FromSql($"select * from Consigne where  ConsigneId  = {id}   and IsDeleted = 0   ").LastOrDefault();

            return res;
        }

        public ShippingAgent GetShippingAgentId(long? id)
        {
            var res = Db.ShippingAgents.FromSql($"select * from ShippingAgent where  ShippingAgentId  = {id}   and IsDeleted = 0   ").LastOrDefault();
            return res;
        }
        public GoodsHead GetGoodsheadId(long? id)
        {
            var res = Db.GoodsHeads.FromSql($"select * from Goodshead where  GoodsheadId  = {id}   and IsDeleted = 0   ").LastOrDefault();
            return res;
        }

        public ShippingLine GetShippingLineId(long? id)
        {
            var res = Db.ShippingLines.FromSql($"select * from ShippingLine where  ShippingLineId  = {id}   and IsDeleted = 0   ").LastOrDefault();
            return res;
        }

        public ExaminationRequest GetExaminationRequestContainerIndexId(long id)
        {
            var res = Db.ExaminationRequests.FromSql($"select * from ExaminationRequest where  ContainerIndexId  = {id}   and IsDeleted = 0   ").LastOrDefault();
            return res;
        }

        public ExaminationRequest GetExaminationRequestCYContainerId(long id)
        {
            var res = Db.ExaminationRequests.FromSql($"select * from ExaminationRequest where  CYContainerId  = {id}   and IsDeleted = 0   ").LastOrDefault();
            return res;
        }

        public ClearingAgent GetClearingAgentId(long? id)
        {
            var res = Db.ClearingAgents.FromSql($"select * from ClearingAgent where  ClearingAgentId  = {id}   and IsDeleted = 0   ").LastOrDefault();
            return res;
        }

        public Voyage GetVoyageId(long? id)
        {
            var res = Db.Voyages.FromSql($"select * from Voyage where  VoyageId  = {id}   and IsDeleted = 0   ").LastOrDefault();
            return res;
        }

        public Vessel GetVesselByVoyageId(long? id)
        {
            var res = Db.Vessels.FromSql($"select * from Vessel where  VesselId  = {id}   and IsDeleted = 0   ").LastOrDefault();
            return res;
        }



    }
}
