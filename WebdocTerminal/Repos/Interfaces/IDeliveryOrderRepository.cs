using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IDeliveryOrderRepository : IRepo<DeliveryOrder>
    {
        IEnumerable<DOItemViewModel> GetBLNumberConsigneename(long  indexnumber , string containernumber);
        IEnumerable<DOItemViewModel> GetBLNumberConsigneenameCY(long  indexnumber , string containernumber , string igm);
        IEnumerable<DOItemViewModel> GetBLNumberConsigneenameCFS(long Index, string blNumbr, string igm);
        GDI GetBLNumberConsigneeNameForCY(string virNo, int IndexNo);

        BillDetailsViewModel GetDeliveryOrderBill(long DONumber);
        DeliveryOrder GetDeliveryOrderByContainerIndexId(long containerinexid);

        List<GatePass> GatePasses(long dono);
    }
}
