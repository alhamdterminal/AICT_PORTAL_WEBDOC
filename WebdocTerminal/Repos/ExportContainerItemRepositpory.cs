using Microsoft.EntityFrameworkCore;
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
    public class ExportContainerItemRepositpory : RepoBase<ExportContainerItem> , IExportContainerItemRepositpory
    {
        public ExportContainerItemRepositpory(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<ExportContainerItem> GetUnSubbmitedGDs(long exportContainerId)
        {
            var asd = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem where ExportContainerId = {exportContainerId} and IsSubmitted = 0 and IsDeleted = 0").ToList();

            return asd;
        }
    }
}
