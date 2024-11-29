using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class GateoutExportRepository : RepoBase<GateoutExport> , IGateoutExportRepository
    {
        public GateoutExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public long GateOutExportCount()
        {
            var data = (from gateoutExport in Db.GateoutExports
                        select gateoutExport).ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }
    }
}
