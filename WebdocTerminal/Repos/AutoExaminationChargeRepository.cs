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
    public class AutoExaminationChargeRepository : RepoBase<AutoExaminationCharge> , IAutoExaminationChargeRepository
    {
        public AutoExaminationChargeRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
    }
}
