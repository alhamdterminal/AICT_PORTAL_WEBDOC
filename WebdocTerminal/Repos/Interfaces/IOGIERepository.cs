using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IOGIERepository : IRepo<OGIE>
    {
        long CargoReceivedCount();

        List<GateInExportGDsViewModel> GetGateInGDs();

        List<OPIA> GetUnSetteledGDNumbers();

        OPIA GetGDDetailGdnumber(string res);
        string GetAgentNameByCNIC(string res);
        string GetDriverNameByCNIC(string res);

        GDInfoViewModel GetGDDetailingo(string res);
    }
}
