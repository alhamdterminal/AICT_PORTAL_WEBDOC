using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface ICCMORepository : IRepo<CCMO>
    {
      CCMO GetCCMOsByIgmContainer(string virno, string containerno);
      List<CCMO> GetCCMOsByContainerAndGd(string containerno, string gdnumber);

      List<CCMO> ccmodatewiselist(string containerno);
    }
}
