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
    public class OEHCRepository : RepoBase<OEHC> , IOEHCRepository
    {
        public OEHCRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public OEHC GetOEHCInfo(string gdnumber)
        {

            var asd = Db.OEHCs.FromSql($"select * from OEHCs where GDNumber = {gdnumber}  and  MessageFrom != 'Manual' and IsDeleted = 0").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
    }
}
