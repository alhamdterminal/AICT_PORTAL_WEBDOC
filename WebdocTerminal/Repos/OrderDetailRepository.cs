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
    public class OrderDetailRepository : RepoBase<GatePass>, IOrderDetailRepository
    {
        public OrderDetailRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }



        public long GetOrderDetailNo()
        {
            var gatePasses = Table.ToList();

            if (gatePasses.Count == 0)
            {
                return 1;
            }

            else
            {
                return gatePasses.Count + 1;
                //return gatePasses.Max(d => d.GatePassId) + 1;
            }


        }

        public long NoOfPackages(string blNumber)
        {
            var totalItems = 0.0;

            var doItems = (from doItem in Db.DOItems
                           join gatepass in Db.OrderDetails on doItem.GatePassId equals gatepass.GatePassId
                           join deliveryOrder in Db.DeliveryOrders on gatepass.DeliveryOrderId equals deliveryOrder.DeliveryOrderId
                           join containerIndex in Db.ContainerIndices on deliveryOrder.ContainerIndexId equals containerIndex.ContainerIndexId
                           where containerIndex.BLNo == blNumber
                           select doItem).ToList();

            foreach (var item in doItems)
            {
                totalItems += item.Quantity;
            }

            return  Convert.ToInt64( totalItems);
        }


        public long deliveredCYContsiners(string blNumber)
        {
            var totalItems = 0;

            totalItems = Db.CYContainers.FromSql($"select * from CYContainer where IsDelivered = 1 and IsDeleted = 0  and BLNo = {blNumber}").Count();
             
            return totalItems;
        }


      


    }

}
