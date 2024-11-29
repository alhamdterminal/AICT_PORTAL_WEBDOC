using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IOPIARepository : IRepo<OPIA>
    {
        void AddAllEDIMessages(OPIA model);

        long UpcomingGDsCount();

        List<OPIA> GetUnAssociatedGds();

        OPIA GetOPIAByGD(string gdnumber);

        ExportContainer GetExportContainerById(long exportcontainerid);

        List<OPIA> GetOpiaslist();
        List<OGDE> Getogdelist();
        List<OEHC> Getoehcslist();
        List<OECM> Getoecmslist();
        List<OCRL> Getocrlslist();
        List<OSVM> Getosvmslist();


        List<OPIA> GetNotGateInOPIAsList();

        List<OPIA> GetOpiaByGdnumber(string gdnumber);

        OPIA GeLastGdnumberInfo(string gdnumber);

        List<OSVM> GetPenddingosvms();
    }
}
