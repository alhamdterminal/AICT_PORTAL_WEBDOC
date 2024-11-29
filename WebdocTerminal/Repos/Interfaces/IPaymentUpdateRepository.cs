using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IPaymentUpdateRepository : IRepo<PaymentUpdate>
    {
        List<PreAlertViewModel> GetUnAssignPaymentUpdate(string prealertNo);

        long GeneratePaymentUpdateNumber();

        List<PaymentUpdateViewModel> GetRequestNumbers();


        List<PreAlertViewModel> GetUnAssignPaymentUpdateMultiple(string prealertNo);

    }
}
