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
    public class EDI_Maersk_LookupRepository : RepoBase<EDI_Maersk_Lookup> , IEDI_Maersk_LookupRepository
    {
        public EDI_Maersk_LookupRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
    }
}
