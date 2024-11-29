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
    public class OtherChargeAssigningRepository : RepoBase<OtherChargeAssigning> , IOtherChargeAssigningRepository
    {
        public OtherChargeAssigningRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public List<OtherChargeAssigning> GetUnPaidOtherChargeAssigning(string type , long containerId)
        {
            var resdata = new List<OtherChargeAssigning>();
            if(type == "CFS")
            {
                var res = Db.ChargeAssignings.FromSql($"select * From OtherChargeAssigning Where  ContainerIndexId = {containerId} and InvoiceCreated = 0 and IsDeleted = 0 ").ToList();
                if (res.Count() > 0)
                {
                    resdata.AddRange(res);
                }

            }

            if (type == "CY")
            {
                var res = Db.ChargeAssignings.FromSql($"select * From OtherChargeAssigning Where  CyContainerId = {containerId}  and InvoiceCreated = 0 and IsDeleted = 0 ").ToList();
                if (res.Count() > 0)
                {
                    resdata.AddRange(res);
                }

            }

            return resdata;

        }


    }
}
