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
    public class DisabledAgentTariffExportRepository : RepoBase<DisabledAgentTariffExport>, IDisabledAgentTariffExportRepository
    {
        public DisabledAgentTariffExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public DisabledAgentTariffExport GetDisabledAgentTariffExport(long enteringcargoid, long agentid)
        {
            var data = Db.DisabledAgentTariffExports.FromSql($"select * from DisabledAgentTariffExport   where EnteringCargoId = {enteringcargoid } and ShippingAgentTariffExportId = {agentid} and IsDeleted = 0").LastOrDefault();

            if (data != null)
            {

                return data;
            }
            else
            {
                return null;
            }

        }
    }
}
