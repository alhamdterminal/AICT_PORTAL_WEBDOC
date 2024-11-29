using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface ISIMORepository : IRepo<SIMO>
    {
        SIMO GetSIMOInfo(string BLNo, long indexno, string Virno);

        SIMO GetSIMOInfoByIGMIndex(string Virno, long indexno);
    }
}
