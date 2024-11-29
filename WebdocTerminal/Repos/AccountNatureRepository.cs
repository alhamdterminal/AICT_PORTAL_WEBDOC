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
    public class AccountNatureRepository : RepoBase<AccountNature> , IAccountNatureRepository
    {
        public AccountNatureRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public bool GetAccountNature(string code, string name, long id)
        {
            var res = Db.AccountNatures.FromSql($"select * from AccountNature where   IsDeleted = 0  and( Code = {code} or Name = {name} ) and AccountNatureId != {id} ").FirstOrDefault();

            if (res != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
