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
    public class AICTAndLineIndexTariffRepository : RepoBase<AICTAndLineIndexTariff> , IAICTAndLineIndexTariffRepository
    {

        public AICTAndLineIndexTariffRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public AICTAndLineIndexTariff GetIGMIndexInfo(string Virno, long indexno)
        {

            //return Db.Database.SqlQuery<TSet>(sql, param);

            var res = Db.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {Virno} and IndexNo = {indexno} and IsDeleted = 0 ").LastOrDefault();

            if(res != null)
            {
                var lineindex = new AICTAndLineIndexTariff();

                 string indexcargotype = "LCL";


                if (res.ShippingAgentId != null && res.GoodsHeadId != null)
                {
                    var resdata = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId = {res.ShippingAgentId} and GoodsHeadId = {res.GoodsHeadId}  and IndexCargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                    if (resdata != null)
                    {
                        return lineindex;
                    }
                }

                if (res.ShippingAgentId != null)
                {
                    var resdata = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId = {res.ShippingAgentId}  and GoodsHeadId is null  and IndexCargoType = {indexcargotype} and IsDeleted = 0  ").LastOrDefault();

                    if (resdata != null)
                    {
                        return lineindex;
                    }
                }
                if (res.GoodsHeadId != null)
                {
                    var resdata = Db.TerminalAndFFShareRates.FromSql($"SELECT * From TerminalAndFFShareRate Where ShippingAgentId is null and GoodsHeadId = {res.GoodsHeadId} and IndexCargoType = {indexcargotype}  and IsDeleted = 0 ").LastOrDefault();

                    if (resdata != null)
                    {
                        return lineindex;
                    }
                }

                return null;

            }
            //var asd = Db.AICTAndLineIndexTariffs.FromSql($"SELECT * From AICTAndLineIndexTariff Where VirNumber = {Virno} and IndexNumber = {indexno} and IsDeleted = 0 ").LastOrDefault();
            //if (asd != null)
            //{
            //    return asd;
            //}
            return null;
        }

        public bool GetIGMIndexRates(string Virno, long indexno)
        {

            //return Db.Database.SqlQuery<TSet>(sql, param);

            
                var asd = Db.AICTAndLineIndexTariffs.FromSql($"SELECT * From AICTAndLineIndexTariff Where VirNumber = {Virno} and IndexNumber = {indexno} and IsDeleted = 0  and status = 'UnDelivered' ").LastOrDefault();
                if (asd != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
             
        }


        public AICTAndLineIndexTariff GetLineIndexRates(string Virno, long indexno)
        {

            //return Db.Database.SqlQuery<TSet>(sql, param);


            var asd = Db.AICTAndLineIndexTariffs.FromSql($"SELECT * From AICTAndLineIndexTariff Where VirNumber = {Virno} and IndexNumber = {indexno} and IsDeleted = 0  and status = 'UnDelivered' ").LastOrDefault();
            if (asd != null)
            {
                return asd;
            }
            else
            {
                return null;
            }

        }
    }
}
