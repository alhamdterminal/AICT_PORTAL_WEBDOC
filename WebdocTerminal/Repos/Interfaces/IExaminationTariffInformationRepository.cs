using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IExaminationTariffInformationRepository : IRepo<ExaminationTariffInformation>
    {
        List<ExaminationTariffInfoViewModel> GetExaminationTariffsByPerametersId(long? ShippingAgentId, long? GoodId, string indexCargoType, string examinationType);
    }
}
