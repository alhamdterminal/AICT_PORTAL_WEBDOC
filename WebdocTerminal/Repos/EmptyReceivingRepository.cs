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
    public class EmptyReceivingRepository : RepoBase<EmptyReceiving> , IEmptyReceivingRepository
    {
        public EmptyReceivingRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public long EmptyReceivingCount()
        {
            var data = (from emptyReceiving in Db.EmptyReceivings
                        select emptyReceiving).ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }
    }
}
