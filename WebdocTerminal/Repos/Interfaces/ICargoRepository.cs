using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface ICargoRepository : IRepo<Cargo>
    {
        CargoViewModel GetCargoInformation(string gdNumber , string lpNumber);

        CargoViewModel GetCargoInfo(string gdNumber, string lpNumber , long lpDetailId);

        List<string> GetGDsForTerminalReceipt(long vesselId, long voyageId);

    }
}
