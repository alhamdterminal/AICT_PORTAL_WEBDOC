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
    public class TerminalAndFFShareRateRepository : RepoBase<TerminalAndFFShareRate> , ITerminalAndFFShareRateRepository
    {
        public TerminalAndFFShareRateRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        //public TerminalAndFFShareRate GetShareRateByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? ContainerType, long? portAndTerminalId , string indexCargoType)
        //{


        //    var tariffs = (from terminalAndFFShareRate in Db.TerminalAndFFShareRates 
        //                   where

        //                   (terminalAndFFShareRate.ShippingAgentId == ShippingAgentId)
        //                    && (terminalAndFFShareRate.ConsigneId == ConsigneId)
        //                    && (terminalAndFFShareRate.ClearingAgentId == ClearingAgentId)
        //                    && (terminalAndFFShareRate.GoodsHeadId == GoodId)
        //                    && (terminalAndFFShareRate.ShipperId == ShipperId)
        //                    && (terminalAndFFShareRate.ISOCodeHeadId == ContainerType)
        //                    && (terminalAndFFShareRate.PortAndTerminalId == portAndTerminalId)
        //                    && (terminalAndFFShareRate.IndexCargoType == indexCargoType)

        //                   select terminalAndFFShareRate).FirstOrDefault();


        //    return tariffs;

        //}

        public TerminalAndFFShareRate GetShareRateByPerametersId(long? ShippingAgentId,   long? GoodId,   string indexCargoType)
        {

            var res = new TerminalAndFFShareRate();
            if (ShippingAgentId != null && GoodId != null)
            {
                res =  Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and GoodsHeadId = {GoodId}  and IndexCargoType = 'LCL'  and IsDeleted = 0 ").LastOrDefault();

                if (res != null  )
                { 
                    return res;
                }
            }

            if (ShippingAgentId != null && GoodId == null)
            {
                res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId = {ShippingAgentId}  and GoodsHeadId  is null  and IndexCargoType = 'LCL'   and IsDeleted = 0 ").LastOrDefault();
 
                if (res != null)
                {
                    return res;
                }
            }
            if (ShippingAgentId == null && GoodId != null)
            {
                res = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId   is null  and GoodsHeadId = { GoodId}  and IndexCargoType = 'LCL'  and IsDeleted = 0 ").LastOrDefault();


                if (res != null)
                {
                    return res;
                }
            }
            
            return res;

        }

    }
}
