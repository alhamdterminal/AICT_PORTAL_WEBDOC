using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface ISealPurchaseRepository : IRepo<SealPurchase>
    {
        long GenerateSealNumber();
        List<SealPurchase> GetSealsByNo(long fromSeal, long toSeal, string batch);
        List<SealPurchase> GetSealsByBatchCode(string batch);

    }
}
