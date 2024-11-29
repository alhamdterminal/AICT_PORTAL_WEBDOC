using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface ICargoRollOverRepository : IRepo<CargoRollOver>
    {
        CargoRollOverViewModel GetGDDeatil(string gdnumber);
        List<CargoRollOverViewModel> GetUnAssignGDNumbers();


        List<CargoRollOverViewModel> GetUnAssignGDNumbersForDO();

        CargoRollOverViewModel GetGDDeatilbygdnumber(string gdnumber);
     }
}
