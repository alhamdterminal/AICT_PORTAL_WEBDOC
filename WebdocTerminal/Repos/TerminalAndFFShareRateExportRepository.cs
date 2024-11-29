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
    public class TerminalAndFFShareRateExportRepository : RepoBase<TerminalAndFFShareRateExport> , ITerminalAndFFShareRateExportRepository
    {
        public TerminalAndFFShareRateExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public TerminalAndFFShareRateExport GetShareRateByPerametersId(long? ShippingAgentId, long? GoodId, string indexCargoType)
        {

            var res = new TerminalAndFFShareRateExport();
            if (ShippingAgentId != null && GoodId != null)
            {
                res = Db.TerminalAndFFShareRateExports.FromSql($"SELECT * From TerminalAndFFShareRateExport Where ShippingAgentExportId = {ShippingAgentId}  and GoodsHeadExportId = {GoodId}  and IndexCargoType = 'LCL'  and IsDeleted = 0 ").LastOrDefault();

                if (res != null)
                {
                    return res;
                }
            }

            if (ShippingAgentId != null && GoodId == null)
            {
                res = Db.TerminalAndFFShareRateExports.FromSql($"SELECT * From TerminalAndFFShareRateExport Where ShippingAgentExportId = {ShippingAgentId}  and GoodsHeadExportId  is null  and IndexCargoType = 'LCL'   and IsDeleted = 0 ").LastOrDefault();

                if (res != null)
                {
                    return res;
                }
            }
            if (ShippingAgentId == null && GoodId != null)
            {
                res = Db.TerminalAndFFShareRateExports.FromSql($"SELECT * From TerminalAndFFShareRateExport Where ShippingAgentExportId   is null  and GoodsHeadExportId = { GoodId}  and IndexCargoType = 'LCL'  and IsDeleted = 0 ").LastOrDefault();


                if (res != null)
                {
                    return res;
                }
            }

            return res;

        }

    }
}
