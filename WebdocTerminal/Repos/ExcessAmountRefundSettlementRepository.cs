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
    public class ExcessAmountRefundSettlementRepository : RepoBase<ExcessAmountRefundSettlement> , IExcessAmountRefundSettlementRepository
    {
        public ExcessAmountRefundSettlementRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<ExcessAmountRefundSettlement> GetexcessRefundDetail(string invoiceno)
        {
            var res = Db.ExcessAmountRefundSettlements.FromSql($"SELECT * From ExcessAmountRefundSettlement Where InvoiceNo  = {invoiceno}  and IsDeleted = 0  ").ToList();
             
            return res;

        }


    }
}
