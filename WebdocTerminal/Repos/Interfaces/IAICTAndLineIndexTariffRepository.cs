using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IAICTAndLineIndexTariffRepository : IRepo<AICTAndLineIndexTariff>
    {

        AICTAndLineIndexTariff GetIGMIndexInfo(string Virno, long indexno);

        bool GetIGMIndexRates(string Virno, long indexno);

        AICTAndLineIndexTariff GetLineIndexRates(string Virno, long indexno);

    }
}
