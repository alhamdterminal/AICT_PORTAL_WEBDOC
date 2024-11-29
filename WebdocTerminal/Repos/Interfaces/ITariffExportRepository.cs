using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface ITariffExportRepository : IRepo<TariffExport>
    {
        List<TariffChargesExportViewModel> GetExportTariffsByPerametersId(long ShippingAgentId);

        string GetShippingAgentBygd(string gdnumber);

        string GetTariffTypeBygd(string gdnumber);

 
        List<EnteringCargo> GetUnDeliveredGDS();
    }
}
