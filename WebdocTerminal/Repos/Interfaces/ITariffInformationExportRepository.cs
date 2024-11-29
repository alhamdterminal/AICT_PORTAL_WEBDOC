using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface ITariffInformationExportRepository : IRepo<TariffInformationExport>
    {
        List<ShippingAgentExport> GetShippingAgentFromPercentAgeTariff();

        List<TariffInfoExportViewModel> GetTariffsByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId
                                                , long? portAndTerminalId, string indexCargoType);
    }
}
