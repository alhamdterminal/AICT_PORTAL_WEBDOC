using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IGDHoldRepository : IRepo<GDHold>
    {
        GDHoldDetailViewModel GetCargoDetail(string gdnumber);

        long GetGDHold(string gdNumber);
        long GetGDHoldByEnteringCargoId(long EnteringCargoId);

        long GDHoldCount();

        GDHoldDetailViewModel GetCargoDetailByGD(string gdnumber);
    }
}
