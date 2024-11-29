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
    public class ShippingLineExportRepository : RepoBase<ShippingLineExport> , IShippingLineExportRepository
    {
        public ShippingLineExportRepository(IUserResolveService  userResolveService) : base(userResolveService)
        {

        }

        public ShippingLineExport GetShippingLineById(long ShippingLineId)
        {

            var data = Db.ShippingLineExports.FromSql($" select * from ShippingLineExport   where   ShippingLineExportId  = {ShippingLineId}  and  IsDeleted = 0 ").LastOrDefault();

            return data;

        }
    }
}
