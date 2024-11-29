using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
  public  interface IPreAlertPayOrderRepository : IRepo<PreAlertPayOrder>
    {
        List<PreAlertPayOrderViewModel> GetPreAlertPayOrders(long RequestNumber);
        List<PreAlertPayOrderViewModel> GetPreAlertPayOrdersCount(long RequestNumber);

        long GeneratePayOrderNumber();
    }
}
