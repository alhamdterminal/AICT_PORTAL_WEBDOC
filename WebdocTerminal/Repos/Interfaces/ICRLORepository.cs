using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface ICRLORepository : IRepo<CRLO>
    {
        List<CRLO> GetMANUALCRLOList();
        List<CRLO> GetMANUALICRLObyigmindexbl(string virno, int indeno, string blno);

        CRLO GetManualCrloInfo(string virno, int indeno, string blno);
    }
}
