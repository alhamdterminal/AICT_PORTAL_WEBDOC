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
    public class CYContainerRepository : RepoBase<CYContainer> , ICYContainerRepository
    {
        public CYContainerRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        void ICYContainerRepository.ExecuteNonSQL(string sql, params object[] param)
        {
            Db.Database.ExecuteSqlCommand(sql, param);
        }


        public CYContainer GetContainerById(long containerid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where CYContainerId = {containerid} and IsDeleted = 0 ").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public List<CYContainer> GetContainerIndexByIGMAndContainerNo(string Virno, int indexno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and IndexNo = {indexno} and IsDeleted = 0 ").ToList();
            if (asd.Count()  >0)
            {
                return asd;
            }
            return null;
        }


        public List<CYContainer> GetContainerIndexByIGMAndContainerNo(string Virno, string containerno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and ContainerNo = {containerno} and IsDeleted = 0 ").ToList();
            if (asd.Count() > 0)
            {
                return asd;
            }
            return null;
        }


        public List<CYContainer> GetCYContainerByIGMAndContainerNo(string Virno, string containerno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and ContainerNo = {containerno} and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                var res = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and IndexNo = {asd.IndexNo} and IsDeleted = 0 ").ToList();

                if(res.Count() > 0)
                {
                    return res;
                }
            }
            return null;
        }


        public  CYContainer GetContainerCYByIGMAndIndex(string Virno, int indexno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and IndexNo = {indexno} and IsDeleted = 0 ").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public CYContainer  GetLastContainerIndexByIGMAndContainerNo(string Virno, string containerno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and ContainerNo = {containerno} and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public CYContainer GetLastContainerByIGMIndexAndContainerNo(string Virno, int indexno , string containerno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and ContainerNo = {containerno} and IndexNo = {indexno} and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public List<CYContainer> GetIndexInfoUndelivered(string Virno, long indexno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno} and IndexNo = {indexno} and IsDelivered = 0 and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public List<CYContainer> GetUndeliveredIndexbyigmindex(string Virno, long indexno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno} and IndexNo = {indexno} and IsDeleted = 0").ToList();
             
            return asd;
             
        }

        public CYContainer GetLastContainerById(long containerid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where CYContainerId = {containerid} and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
         

        public List<CYContainer> GetHoldContainersById(string Virno, long indexno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno} and IndexNo = {indexno} and IsHold = 1 and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<CYContainer> GetAuctionContainersById(string Virno, long indexno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno} and IndexNo = {indexno} and IsAuction = 1 and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<CYContainer> GetCYIndexesByVirno(string Virno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.CYContainers.FromSql($"select * from CYContainer   where VIRNo = {Virno } and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<CYContainer> GetCYIGMsByContainerNumber(string containernumber)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.CYContainers.FromSql($"select * from CYContainer   where ContainerNo = {containernumber } and IsDeleted = 0").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<CYContainer> GetCYContainersByIGMContainer(string virNo, string containerNo)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.CYContainers.FromSql($"select * from CYContainer where    VIRNo  = {virNo} and  ContainerNo = {containerNo} and IsDeleted = 0").ToList();

            return asd;

        }

        public List<CYContainer> GetCYContainersByBL(string blnumber)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.CYContainers.FromSql($"select * from CYContainer where    BLNo  = {blnumber} and IsDeleted = 0").ToList();

            return asd;

        }


        public List<CYContainer> GetContainercydetilByIGMAndContainerNo(string Virno, string containerno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and ContainerNo = {containerno} and IsDeleted = 0 ").ToList();
            if (asd.Count() > 0)
            {
                return asd;
            }
            return null;
        }


        public List<CYContainer> GetCSContainerIndexByIGMAndContainerNo(string Virno, string containerno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and CSContainerNumber = {containerno} and IsDeleted = 0 ").ToList();

            return asd;

        }

        public CYContainer GetLastCSContainerByIGMIndexAndContainerNo(string Virno, int indexno, string containerno)
        {

            var asd = Db.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {Virno}  and CSContainerNumber = {containerno} and IndexNo = {indexno} and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public  CYContainer GetFirstCYContainerByIGMIndex(string Virno, long indexno)
        {

           var asd = Db.CYContainers.FromSql($"SELECT   * From CYContainer Where VIRNo = {Virno}  and IndexNo = {indexno} and IsDeleted = 0   ").FirstOrDefault();
            
           return asd;
            
        }

        
        public BillToLine GetCYContainerBillToLineInfo(string Virno, long indexno)
        {
            var asd = Db.BillToLines.FromSql($"SELECT   * From BillToLine Where VirNo = {Virno}  and IndexNo = {indexno} and IsDeleted = 0   and IndexType = 'CY' and InoviceCreated = 0 ").LastOrDefault();

            return asd;

        }
    }
}
