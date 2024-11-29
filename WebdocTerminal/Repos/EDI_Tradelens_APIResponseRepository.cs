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
    public class EDI_Tradelens_APIResponseRepository : RepoBase<EDI_Tradelens_APIResponse> , IEDI_Tradelens_APIResponseRepository
    {
        public EDI_Tradelens_APIResponseRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
    }
}
