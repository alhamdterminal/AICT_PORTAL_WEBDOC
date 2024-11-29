using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IPreAlertRepository : IRepo<PreAlert>
    {
        long GeneratePreAlterNumber();
        List<PreAlert> GetPreAlertNumber();
        void GetAlert(string alterno);
        bool GetPayOrderStatusByPrealertid(long prealertid);
        bool GetPaymentUpdateStatusByPrealertid(long prealertid);

        void updateAlertStaus(string alterno);

        IEnumerable<PreAlertForWebocViewModel> GetUnBindPreAlert(long? shippingAgent, long? line, long? portAndTerminal, string vessel, string voyage, DateTime? from, DateTime? to);

        List<PreAlterDetail> GetpreAlterDetails(string PreAlterDetailId);

    }
}
