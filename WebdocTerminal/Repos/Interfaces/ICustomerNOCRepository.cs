using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface ICustomerNOCRepository : IRepo<CustomerNOC>
    {
        List<CustomerNOCViewModel> GetCargoDetail(string gdnumber );
        List<CustomerNOCViewModel> GetCustomerNOCbyGDnumber(string gdnumber);
        List<GDListViewModel> GetCustomerNocAndCargoRollOver();
        List<ExportContainer> GetCustomerNocAndCargoRollOverContainers();

        ExportContainerItem GetGDIfo(string gdnumber);
    }
}
