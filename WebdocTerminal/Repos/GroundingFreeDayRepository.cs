using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class GroundingFreeDayRepository : RepoBase<GroundingFreeDay> , IGroundingFreeDayRepository
    {

        public GroundingFreeDayRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<GroundingFreeDaysViewModel> GetGroundingFreedays()
        {
            var res = (from groundingFreeDay in Db.GroundingFreeDays
                       from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == groundingFreeDay.ShippingAgentId).DefaultIfEmpty()
                       from consigne in Db.Consignes.Where(x => x.ConsigneId == groundingFreeDay.ConsigneId).DefaultIfEmpty()
                       from clearingAgent in Db.ClearingAgents.Where(x => x.ClearingAgentId == groundingFreeDay.ClearingAgentId).DefaultIfEmpty()
                       from goodsHead in Db.GoodsHeads.Where(x => x.GoodsHeadId == groundingFreeDay.GoodsHeadId).DefaultIfEmpty()

                       select new GroundingFreeDaysViewModel
                       {
                           ConsigneeName = consigne != null ? consigne.ConsigneName : "",
                           GroundingFreeDayId = groundingFreeDay.GroundingFreeDayId,
                           GroundingFreeDays = groundingFreeDay.GroundingFreeDays,
                           StorageFreeFreeDays = groundingFreeDay.StorageFreeFreeDays,
                           ShippingAgentName = shippingAgent != null ? shippingAgent.Name : "",
                           ClearingAgentName = clearingAgent != null ? clearingAgent.Name : "",
                           GoodHeadName = goodsHead != null ? goodsHead.GoodsHeadName : "",
                           CargoType = groundingFreeDay != null ? groundingFreeDay.CargoType : "",
                       }).ToList();

            return res;
        }


    }
}
