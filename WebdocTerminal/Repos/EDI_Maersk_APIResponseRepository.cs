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
    public class EDI_Maersk_APIResponseRepository : RepoBase<EDI_Maersk_APIResponse> , IEDI_Maersk_APIResponseRepository
    {
        public EDI_Maersk_APIResponseRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
    }
}
