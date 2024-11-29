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
    public class CompanyRepository : RepoBase<Company> , ICompanyRepository
    {
        public CompanyRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public User GetUserByCompanyId(long CompanyId)
        {
            var user = (from usr in Db.Users
                        where usr.CompanyId == CompanyId
                        select usr).FirstOrDefault();
             
            return user;
        }

    }
}
