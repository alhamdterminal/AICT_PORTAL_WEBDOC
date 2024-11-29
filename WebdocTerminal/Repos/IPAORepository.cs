using Microsoft.EntityFrameworkCore;
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
    public class IPAORepository : RepoBase<IPAO>, IIPAORepository
    {
        public IPAORepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<IPAO> GetIPAOSByDateCY(DateTime fromdate)
        {
            var ipaoscy = (from ipao in Db.IPAOs
                              join container in Db.ContainerIndices on ipao.ContainerNumber equals container.ContainerNo
                              where ipao.Performed.Value.Date  == fromdate .Date && container.Status.ToUpper() == "CY" && container.IsDeleted == false
                              
                              select new IPAO
                              {
                                  TotalRecords = ipao.TotalRecords,
                                  RecordSequence = ipao.RecordSequence,
                                  VIRNumber = ipao.VIRNumber,
                                  ContainerNumber =  ipao.ContainerNumber,
                                  BondedCarrierId = ipao.BondedCarrierId,
                                  BondedCarrierName = ipao.BondedCarrierName,
                                  VehicleNumber= ipao.VehicleNumber
                                   
                              }).ToList();

            return ipaoscy;
        }

        public List<IPAO> GetIPAOSByDateCFS(DateTime fromdate)
        {
            var ipaoscfs = (from ipao in Db.IPAOs
                           //join container in Db.ContainerIndices on ipao.ContainerNumber equals container.ContainerNo
                           where ipao.Performed.Value.Date == fromdate.Date
                           //&& container.Status.ToUpper() == "CFS" && container.IsDeleted == false
                           select new IPAO
                           {
                               TotalRecords = ipao.TotalRecords,
                               RecordSequence = ipao.RecordSequence,
                               VIRNumber = ipao.VIRNumber,
                               ContainerNumber = ipao.ContainerNumber,
                               BondedCarrierId = ipao.BondedCarrierId,
                               BondedCarrierName = ipao.BondedCarrierName,
                               VehicleNumber = ipao.VehicleNumber

                           }).ToList();

            return ipaoscfs;
        }


        public List<IPAO> GetMANUALIPAOsList()
        {
            var res = Db.IPAOs.FromSql($"SELECT * From IPAOs where  MessageFrom = 'MANUAL' and IsDeleted = 0  ").ToList();

            return res;
        }


        public List<IPAO> GetMANUALIPAOsByIGMandContainer(string virno , string containernumber)
        {
            var res = Db.IPAOs.FromSql($"SELECT * From IPAOs  where VIRNumber = {virno} and ContainerNumber = {containernumber} and MessageFrom = 'MANUAL' and IsDeleted = 0  ").ToList();

            return res;
        }

    }
}
