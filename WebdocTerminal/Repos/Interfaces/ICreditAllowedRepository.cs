using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface ICreditAllowedRepository : IRepo<CreditAllowed>
    {
        List<CreditAllowed> GetCreditAllowDetail();
        List<CreditAllowed> GetCreditAllowDetail(string virno, long indexno);
        List<UnSettledCreditAllowed> GetUnSettledCreditAllow();
        double GetCreditAllowedAmount(string paymentReceivedIds);
        PaymentReceived GetPaymentById(long id);
        List<PaymentReceived> GetListUnSetteledRefund(string paymentReceivedIds);
        double GetUnSetteledRefund(string paymentReceivedIds);
        Invoice GetInvoiceByInvoiceNo(string invoiceno);
        double GetPaymentReceivedById(long paymentReceivedIds);
        List<CreditAllowRefoundSettlement> GetCreditAllowRefoundSettlement(long paymentReceivedIds);
    }
}