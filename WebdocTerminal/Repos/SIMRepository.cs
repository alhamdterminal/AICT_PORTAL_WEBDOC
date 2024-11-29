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
    public class SIMRepository : RepoBase<SIM>, ISIMRepository
    {
        public SIMRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }



        public SIM GetSIMInfo(string ContainerNo,  string Virno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.SIMs.FromSql($"SELECT * From SIM Where VIRNumber = {Virno} and ContainerNumber = {ContainerNo}    and IsDeleted = 0 ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
    }
}
