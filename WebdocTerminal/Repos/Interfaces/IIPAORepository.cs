using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IIPAORepository : IRepo<IPAO>
    {
        List<IPAO> GetIPAOSByDateCY(DateTime fromdate);
        List<IPAO> GetIPAOSByDateCFS(DateTime fromdate);
        List<IPAO> GetMANUALIPAOsList();
        List<IPAO> GetMANUALIPAOsByIGMandContainer(string virno, string containernumber);

    }
}
