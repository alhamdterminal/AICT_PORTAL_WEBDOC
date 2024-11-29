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
    public class HoldRepository : RepoBase<Hold> , IHoldRepository
    {
        public HoldRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public long HoldContainersCountCFS()
        {
            //var holdCountainer = (from hold in Db.Holds
            //                      join containerIndex in Db.ContainerIndices on hold.ContainerIndexId equals containerIndex.ContainerIndexId
            //                     where containerIndex.IsHold == true
            //                     select hold
            //             ).ToList();
            //if (holdCountainer.Count == 0)
            //{
            //    return 0;
            //}

            //else
            //{
            //    return holdCountainer.Count;
            //}

            return 0;
        }

        public long HoldContainersCountCY()
        {
            var holdCountainer = (from CYContainer in Db.CYContainers
                                  where CYContainer.IsHold == true
                                  select CYContainer
                         ).Count();

            return holdCountainer;
        }



        public Hold GetHoldContainerIndexById(long containerinexid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.Holds.FromSql($"SELECT * From Hold Where ContainerIndexId = {containerinexid} and IsDeleted = 0 and IsHold = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public Hold GetHoldContainerById(long containerid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.Holds.FromSql($"SELECT * From Hold Where CYContainerId = {containerid} and IsDeleted = 0 and IsHold = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


        public List<Hold> GetHoldDetail(string virno , long indexno , string type)
        { 
            var asd = Db.Holds.FromSql($"SELECT * From Hold Where VirNo = {virno} and IndexNo = {indexno} and Type = {type} and IsDeleted = 0 and IsHold = 1").ToList();
            
            return asd;
            
        }
    }
}
