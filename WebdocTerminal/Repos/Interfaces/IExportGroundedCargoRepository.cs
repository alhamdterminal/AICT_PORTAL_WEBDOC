using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IExportGroundedCargoRepository : IRepo<ExportGroundedCargo>
    {
        List<ExportGroundingCargoViewModel> GetUngroundedCargos();

        EnteringCargo GetEnteringCargoInfo(string gdnumber);

    }
}
