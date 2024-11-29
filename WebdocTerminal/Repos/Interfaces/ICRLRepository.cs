using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
  public  interface ICRLRepository : IRepo<CRL>
    {

        List<CRL> GetMANUALCRLList();
        List<CRL> GetMANUALICRLbyigmcontainer(string virno, string containerNumber);

        CRL GetManualCrlInfo(string virno, string containerNumber);
    }
}
