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
    public class EDI_Maersk_MessageRepository : RepoBase<EDI_Maersk_Message> , IEDI_Maersk_MessageRepository
    {
        public EDI_Maersk_MessageRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
    }
}
