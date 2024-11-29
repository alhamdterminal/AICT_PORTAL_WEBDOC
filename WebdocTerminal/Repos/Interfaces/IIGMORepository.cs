using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IIGMORepository : IRepo<IGMO>
    {
        List<IGMO> GetIndexInfo(string Virno);
        IGMO GetIndexDetail(string Virno , int indexno);

        IGMO GetIGMODetailBYBLnumber(string blnumber);

        IGMO GetManualIgmo(string virno);

        List<IGMO> GetManualIgmoList();

        IGMO GetIGMOInfo(string VIRNumber, string ContainerNumber, long IndexNumber, string BLNumber);


        ISOCode GetISOCodeBySize(string size);

        IGMO GetMANUALIGMOByVirAndContainerno(string VIRNumber, string ContainerNumber);
    }
}
