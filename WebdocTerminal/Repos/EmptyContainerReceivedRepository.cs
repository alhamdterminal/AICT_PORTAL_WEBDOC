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
    public class EmptyContainerReceivedRepository : RepoBase<EmptyContainerReceived> , IEmptyContainerReceivedRepository
    {
        public EmptyContainerReceivedRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public EmptyContainerReceivedViewModel GetEmptyReceiveContainerData(long EmptyContainerReceivedId)
        {
            var res = (from emptyContainerReceived in Db.EmptyContainerReceiveds
                       from isocode in Db.ISOCodeHeads.Where(x => x.ISOCodeHeadId == emptyContainerReceived.ISOCodeHeadId).DefaultIfEmpty()
                       from shippingagnet in Db.ShippingAgents.Where(x => x.ShippingAgentId == emptyContainerReceived.ShippingAgentId).DefaultIfEmpty()
                       from shippingline in Db.ShippingLines.Where(x => x.ShippingLineId == emptyContainerReceived.ShippingLineId).DefaultIfEmpty()
                       from consignee in Db.Consignes.Where(x => x.ConsigneId == emptyContainerReceived.ConsigneId).DefaultIfEmpty()
                       from clearingAgent in Db.ClearingAgents.Where(x => x.ClearingAgentId == emptyContainerReceived.ClearingAgentId).DefaultIfEmpty()
                       where
                       emptyContainerReceived.EmptyContainerReceivedId == EmptyContainerReceivedId
                       select new EmptyContainerReceivedViewModel
                       {
                           ActualArrive = emptyContainerReceived.ActualArrive,
                           EmptyContainerReceivedId = emptyContainerReceived.EmptyContainerReceivedId,
                           Type = isocode != null ? isocode.ISOCodeHeadDescription: "",
                           Size = emptyContainerReceived.Size,
                           Line = shippingagnet != null ? shippingagnet.Name : "",
                           Principal = shippingline != null ? shippingline.ShippingLineName : "",
                           ShippinAgentId = shippingagnet != null ? shippingagnet.ShippingAgentId : 0,
                           ConsigneeName = consignee != null ? consignee.ConsigneName : "",
                           ClearningAgentName = clearingAgent != null ? clearingAgent.Name : "",
                           PortOfLoading =  "AICT",
                           Vessel = emptyContainerReceived.Vessel,
                           Voyage = emptyContainerReceived.Voyage,
                             

                       }).LastOrDefault();

            return res;
        }
    }
}
