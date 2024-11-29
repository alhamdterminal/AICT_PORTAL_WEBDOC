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
    public class ContainerIndexRepository : RepoBase<ContainerIndex> , IContainerIndexRepository
    {
        public ContainerIndexRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public ContainerIndex GetContainerIndexWithShippingAgent(long ContainerIndexId)
        {
            var index = (from containerIndex in Db.ContainerIndices
                         join container in Db.ContainerIndices on containerIndex.VoyageId equals container.VoyageId
                         where
                         containerIndex.ContainerIndexId == ContainerIndexId
                         && containerIndex.IndexNo == container.IndexNo
                         && container.ShippingAgentId != null
                         select container).FirstOrDefault();

            return index;
        }


        public ContainerIndex GetContainerIndexCFS(string ContainerNumber , string BlNumber , int IndexNo , string VirNumber)
        {
            var index = (from containerIndex in Db.ContainerIndices
                         join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                         where
                         containerIndex.ContainerNo == ContainerNumber
                         && containerIndex.BLNo == BlNumber
                         && containerIndex.IndexNo == IndexNo
                         && voyage.VIRNo == VirNumber
                         select containerIndex).LastOrDefault() ;

            return index;
        }

        public  List<ContainerIndex> GetContainerIndexInfo(string blNumber, int indexNumber, string igm)
        {
            var data = (from containerIndex in Db.ContainerIndices
                        //join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                        where
                        containerIndex.VirNo == igm && containerIndex.BLNo == blNumber && containerIndex.IndexNo == indexNumber
                        select containerIndex).ToList();
            if(data != null)
            {
                return data;
            }

            return data;

        }
        public List<ContainerIndex> GetContainerIndexBYigmIndex(int indexNumber, string igm)
        {
            var data = (from containerIndex in Db.ContainerIndices
                        join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                        where
                        voyage.VIRNo == igm  && containerIndex.IndexNo == indexNumber
                        select containerIndex).ToList();
            if(data != null)
            {
                return data;
            }

            return data;

        }



        public List<ContainerIndex> GetNotGeneratedDOIndexes(string containerNo)
        {
            var ccmo = (from containerIndex in Db.ContainerIndices
                        
                        where
                        containerIndex.ContainerNo == containerNo
                         &&
                        !Db.DeliveryOrders.Any(x => x.ContainerIndexId == containerIndex.ContainerIndexId)

                        select new ContainerIndex
                        {
                            ContainerIndexId = containerIndex.ContainerIndexId,
                            IndexNo = containerIndex.IndexNo,
                            ContainerNo = containerIndex.ContainerNo,
                             
                        }).Distinct().ToList();

            return ccmo;
        }

        void IContainerIndexRepository.ExecuteNonSQL(string sql, params object[] param)
        {
            Db.Database.ExecuteSqlCommand(sql, param);
        }

        public ContainerIndex GetContainerIndexById(long containerinexid )
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

             var asd =  Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where ContainerIndexId = {containerinexid} and IsDeleted = 0").FirstOrDefault();
            if(asd != null)
            {
                return asd;
            }
            return null;
        }


        public List<ContainerIndex> GetContainerIndexByIGMAndContainerNo(string Virno , string Containerno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno}  and ContainerNo = {Containerno} and IsDeleted = 0").ToList();
            if (asd.Count() > 0)
            {
                return asd;
            }
            return null;
        }


        public  ContainerIndex GetLastContainerIndexByIGMAndContainerNo(string Virno, string Containerno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno}  and ContainerNo = {Containerno} and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<ContainerIndex> GetContainerIndexByIGMAndBLNo(string Virno, string blno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno}  and BLNo = {blno} and IsDeleted = 0").ToList();
            if (asd.Count() > 0)
            {
                return asd;
            }
            return null;
        }

        public ContainerIndex GetContainerIndexByIGMIndexAndContainer(string Virno, string containerno , long indexno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno}  and ContainerNo = {containerno} and IndexNo = {indexno} and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public BillToLine GetBillToLineInfo(string Virno ,   long indexno , string IndexType)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.BillToLines.FromSql($"SELECT * From BillToLine Where VirNo = {Virno} and IndexNo = {indexno} and IndexType = {IndexType} and InoviceCreated = 0 and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
        
        public List<Waiver> GetWaiverInfo(  long ContainerIndexId)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.Waivers.FromSql($"SELECT * From Waiver Where ContainerIndexId = {ContainerIndexId} and IsWaive = 1 and InvoiceCreated =  0 and IsDeleted = 0 ").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
         

        public List<ContainerIndex>  GetIndexInfo(string Virno,   long indexno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno} and IndexNo = {indexno} and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public ContainerIndex GetSingleIndexInfo(string Virno, long indexno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno} and IndexNo = {indexno} and IsDeleted = 0").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<ContainerIndex> GetIndexesByVirno(string Virno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where VirNo = {Virno } and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public ContainerIndex GetLastContainerIndexById(long containerinexid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where ContainerIndexId = {containerinexid} and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }



        public List<ContainerIndex> GetontainerIndexByIGMandContainerNumber(string VIRNumber, string ContainerNumber)
        {
            var igmores = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {VIRNumber} and ContainerNo = { ContainerNumber } ").ToList();
 
           return igmores;
    
        }

        public List<ContainerIndex> GetIndexInfoUndelivered(string Virno, long indexno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno} and IndexNo = {indexno} and IsDelivered = 0 and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public ContainerIndex GetHoldContainerIndexById(long containerinexid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where ContainerIndexId = {containerinexid} and IsDeleted = 0 and IsHold = 1").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public ContainerIndex GetAuctionContainerIndexById(long containerinexid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where ContainerIndexId = {containerinexid} and IsDeleted = 0 and IsAuction = 1").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public List<ContainerIndex> GetCFSIGMsByContainerNumber(string containernumber)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where ContainerNo = {containernumber } and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public  bool GetEDOInfo(long containerindexid)
        {
             
            var conatiner = Db.ContainerIndices.FromSql($"select * from ContainerIndex   where ContainerIndexId = {containerindexid } and IsDeleted = 0").LastOrDefault();
            if (conatiner != null && conatiner.ShippingAgentId != null && conatiner.CargoType == "LCL")
            {

                var edohold = Db.EDOHolds.FromSql($"select * from EDOHold   where VirNo = {conatiner.VirNo } and IndexNo = {conatiner.IndexNo } and IsDeleted = 0").LastOrDefault();

                if(edohold != null)
                {
                    return false;
                }

                var agent = Db.ShippingAgents.FromSql($"select * from ShippingAgent   where ShippingAgentId = {conatiner.ShippingAgentId } and IsDeleted = 0").LastOrDefault();

                if(agent != null && agent.MasterShippingAgentId == 30)
                {
                    var edo = Db.ElectronicDeliveryOrders.FromSql($"select * from ElectronicDeliveryOrder where BL_No = {conatiner.BLNo } and Container_No = {conatiner.ContainerNo } and Index_No = {conatiner.IndexNo }  and IsDeleted = 0").LastOrDefault();

                    if(edo != null && edo.OpType == "A")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                  
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


        public DestuffedContainer GetDestuffedIndexDetail(long containerinexid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.DestuffedContainers.FromSql($"SELECT * From DestuffedContainer Where ContainerIndexId = {containerinexid} and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<GatePass> GetGatePassInfo(string Virno, string containerno, long indexno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.OrderDetails.FromSql($"SELECT * From GatePass Where VirNumber = {Virno}  and GatePasscontainerNumber = {containerno} and IndexNo = {indexno} and IsDeleted = 0").ToList();
            
            return asd;
             
        }


        public List<DestuffedContainer> GetDestuffedList(string containerinexids)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);
            var resdestuff = new List<DestuffedContainer>();

            var iNo = containerinexids.Split(",").ToList();

            foreach (var item in iNo)
            {
                var asd = Db.DestuffedContainers.FromSql($"SELECT * From DestuffedContainer Where ContainerIndexId  = {item} and IsDeleted = 0 ").LastOrDefault();

                if (asd != null)
                {
                    resdestuff.Add(asd);
                }
            }
             
            return resdestuff;
             
        }

        public List<ContainerIndex> GetContainerIndexByIGMContainerNo(string Virno, string Containerno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno}  and ContainerNo = {Containerno} and IsDeleted = 0").ToList();
          
            return asd;
           
        }

        public List<CYContainer> GetCYContainerByIGMContainerNo(string Virno, string Containerno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and ContainerNo = {Containerno} and IsDeleted = 0").ToList();

            return asd;

        }


        public List<ContainerIndex> GetContainerIndexByIgmAndIndex(string Virno, long indexno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno} and IndexNo = {indexno} and IsDeleted = 0").ToList();
            
            return asd;
             
             
        }
        public List<IGMO> GetVirNoListBycontainerNumber(string containernumber)
        {
             
            var asd = Db.IGMOs.FromSql($"select * from IGMO   where ContainerNumber = {containernumber } and IsDeleted = 0").ToList();
             
            return asd;
        }


        public AICTAndLineIndexTariff GetLastAICTAndLineIndexTariff(string virno , long indexno)
        {

            var aictindextariff = Db.AICTAndLineIndexTariffs.FromSql($"SELECT * From AICTAndLineIndexTariff Where VirNumber = {virno} and IndexNumber = {indexno} and IsDeleted = 0 and Status = 'UnDelivered' ").LastOrDefault();

            return aictindextariff;
        }


        public ShortExcessWeigth GetShortExcessWeigth(string virno, long indexno , string containertype)
        {

            var resdata = Db.ShortExcessWeigths.FromSql($"SELECT * From ShortExcessWeigth Where VirNumber = {virno} and IndexNumber = {indexno} and IsDeleted = 0 and ContainerType = {containertype} ").LastOrDefault();

            if(resdata != null)
            {
                return resdata;
            }
            return null;
        }

        public ContainerIndex GetContainerindexByIgmIndexContainer(string virno, long indexno, string containerno)
        {
            var res = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {virno} and IndexNo = {indexno}  and ContainerNo = {containerno}  and IsDeleted = 0   ").LastOrDefault();

            return res;
        }

        public List<ContainerIndex> GetContainerIndexByIds(string containerindexid)
        {
            var res = Db.ContainerIndices.FromSql($"select * from ContainerIndex where  ContainerIndexId  in   (select data from [dbo].[Split]( {containerindexid}, ',') )     and IsDeleted = 0   ").ToList();

            return res;

        }


        public IndexWeighmentViewModel GetIndexInfoForWeighment(string Virno, long indexno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);
            var resdata = new IndexWeighmentViewModel();

            var asd = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno} and IndexNo = {indexno} and IsDeleted = 0").FirstOrDefault();
            if (asd != null)
            {

                resdata.NumberOfPackages = asd.NoOfPackages;
                resdata.DischargeQTY = asd.NoOfPackages;
                resdata.BLNo = asd.BLNo;
                resdata.ManifestedWeight = asd.BLGrossWeight ?? 0;
                 
                var checkcontainerdate = Db.ContainerIndices.FromSql($"SELECT top 1 * From ContainerIndex Where VirNo = {Virno} and IndexNo = {indexno} and IsDeleted = 0 order by CFSContainerGateInDate desc ").LastOrDefault();
                  
                if(checkcontainerdate != null)
                {
                    resdata.GateInDate = checkcontainerdate.CFSContainerGateOutDate;
                }

                var checkcontainers = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno} and IndexNo = {indexno} and IsDeleted = 0").ToList();

                if(checkcontainers.Count() > 1)
                {
                    resdata.ContainerNo = "Multiple" + asd.IndexNo;
                }
                else
                {
                    resdata.ContainerNo =  asd.ContainerNo;
                }

                return resdata;
            }
            return resdata;
        }
    }
}
