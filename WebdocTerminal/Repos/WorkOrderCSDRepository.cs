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
    public class WorkOrderCSDRepository : RepoBase<WorkOrderCSD> , IWorkOrderCSDRepository
    {
        public WorkOrderCSDRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public long GenerateNumber()
        {

            long lastNumber = 1;

            var lastGeneratedNumber = GetAll().OrderByDescending(l => l.WorkOrderCSDId).FirstOrDefault();

            if (lastGeneratedNumber != null)
            {
                lastNumber = lastGeneratedNumber.WorkOrderNumber;
                return lastNumber + 1;

            }
            else
            {
                lastNumber = 1;
                return lastNumber;

            }

        }
    }
}
