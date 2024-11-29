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
    public class ShippingLineRepository : RepoBase<ShippingLine> , IShippingLineRepository
    {
        public ShippingLineRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public ShippingLine GetShippingLineById(long ShippingLineId)
        {

            var data = Db.ShippingLines.FromSql($" select * from ShippingLine   where   ShippingLineId  = {ShippingLineId}  and  IsDeleted = 0 ").LastOrDefault();

            return data;

        }
    }
}
