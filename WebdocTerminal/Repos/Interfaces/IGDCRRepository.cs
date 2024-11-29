using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IGDCRRepository : IRepo<GDCR>
    {
        GDCR GetInfo(string Virno, string containerno, string blnumber);

        List<CYContainer> GetContainerInfo(string Virno, string containerno, string blnumber);
    }
}
