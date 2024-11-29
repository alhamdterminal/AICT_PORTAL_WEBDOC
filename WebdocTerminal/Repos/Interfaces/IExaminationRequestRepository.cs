using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IExaminationRequestRepository : IRepo<ExaminationRequest>
    {
        ExaminationRequest GetExaminationRequestByClearingAgnetAndCNIC(string cninc, long clearingAgentId);
        ExaminationRequest GetExaminationRequestByContainerindexId(long ContainerIndexId);
        ExaminationRequest GetExaminationRequestByCYContainerId(long CYContainerId);
    }
}
