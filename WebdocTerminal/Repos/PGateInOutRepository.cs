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
    public class PGateInOutRepository : RepoBase<PGateInOut> , IPGateInOutRepository
    {
        public PGateInOutRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public PGateInOut GetPGateInOutDetailBydCNIC(string cninc)
        {
            var res = Db.PGateInOuts.FromSql($"select * from  PGateInOut  where IsDeleted = 0 and  CNIC = {cninc}   ").LastOrDefault();

            return res;
        }

        public PGateInOut GetPGateInOutDetailByImageUrl(string ImageUrl)
        {
            var res = Db.PGateInOuts.FromSql($"select * from  PGateInOut  where IsDeleted = 0 and  ImageUrl = {ImageUrl}    ").LastOrDefault();

            return res;
        }

    }
}
