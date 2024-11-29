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
    public class ScheduleOfChargeRepository : RepoBase<ScheduleOfCharge> , IScheduleOfChargeRepository
    {
        public ScheduleOfChargeRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public  List<ScheduleOfCharge> GetSOCBYIGMIndex(string VirNo , int IndexNo )
        {
            var listres = new List<ScheduleOfCharge>();
            //return Db.Database.SqlQuery<TSet>(sql, param);

            //var asd = Db.ScheduleOfCharges.FromSql($"SELECT * From ScheduleOfCharge Where IGMNo = {VirNo} and Indexno = {IndexNo} and IsDeleted = 0").ToList();

            var soc = Db.ScheduleOfCharges.FromSql($"select top 1 * from ScheduleOfCharge where IGMNo  = {VirNo} and Indexno = {IndexNo}  and IsDeleted = 0 order by ScheduleOfChargeId desc").LastOrDefault();
            if(soc != null)
            {
                if (Convert.ToDateTime(soc.AdvanceDate.Value.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(DateTime.Now.Date.ToString("MM/dd/yyyy")))
                {
                    listres.Add(soc);
                }
                    
            }
            
            return listres;
             
        }
    }
}
