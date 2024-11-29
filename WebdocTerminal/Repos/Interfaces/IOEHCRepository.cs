using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IOEHCRepository : IRepo<OEHC>
    {
        OEHC GetOEHCInfo(string gdnumber);
    }
}
