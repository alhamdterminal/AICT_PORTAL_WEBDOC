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
    public class EDI_Tradelens_LookupRepository : RepoBase<EDI_Tradelens_Lookup> , IEDI_Tradelens_LookupRepository
    {
        public EDI_Tradelens_LookupRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
    }
}
