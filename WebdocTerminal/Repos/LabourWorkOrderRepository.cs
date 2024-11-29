using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class LabourWorkOrderRepository : RepoBase<LabourWorkOrder> , ILabourWorkOrderRepository
    {
        public LabourWorkOrderRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        //public long GenerateNumber()
        //{

        //    long lastNumber = 1;

        //    var lastGeneratedNumber = GetAll().OrderByDescending(l => l.LabourWorkOrderId).FirstOrDefault();

        //    if (lastGeneratedNumber != null)
        //    {
        //        lastNumber = lastGeneratedNumber.WorkOrderNumber;
        //        return lastNumber + 1;

        //    }
        //    else
        //    {
        //        lastNumber =  1;
        //        return  lastNumber;

        //    }

        //}
    }
}
