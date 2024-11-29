using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IChequePrintingRepository : IRepo<ChequePrinting>
    {
        List<ChequePrinting> GetChequePrintingList(long? companyid, long? userid, DateTime fromdate, DateTime todate, string search);
    }
}
