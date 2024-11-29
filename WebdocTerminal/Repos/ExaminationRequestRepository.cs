using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class ExaminationRequestRepository : RepoBase<ExaminationRequest> , IExaminationRequestRepository
    {
        public ExaminationRequestRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public ExaminationRequest GetExaminationRequestByClearingAgnetAndCNIC(string cninc, long clearingAgentId)
        {
            var res = Db.ExaminationRequests.FromSql($"select * from  ExaminationRequest  where IsDeleted = 0 and  ClearingAgentId = {clearingAgentId} and  CNIC = {cninc}  ").LastOrDefault();

            return res;
        }


        public ExaminationRequest GetExaminationRequestByContainerindexId(long ContainerIndexId)
        {
            var res = Db.ExaminationRequests.FromSql($"select * from  ExaminationRequest  where IsDeleted = 0 and  ContainerIndexId = {ContainerIndexId} ").LastOrDefault();

            return res;
        }

        public ExaminationRequest GetExaminationRequestByCYContainerId(long CYContainerId)
        {
            var res = Db.ExaminationRequests.FromSql($"select * from  ExaminationRequest  where IsDeleted = 0 and  CYContainerId = {CYContainerId} ").LastOrDefault();

            return res;
        }
         
    }
}
