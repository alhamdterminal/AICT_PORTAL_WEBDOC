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
    public class ReturnToYardRepository : RepoBase<ReturnToYard> , IReturnToYardRepository
    {
        public ReturnToYardRepository(IUserResolveService userResolveService) : base(userResolveService)
        {


        }


       

    }
}


