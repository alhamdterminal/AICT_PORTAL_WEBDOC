using Microsoft.EntityFrameworkCore;
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
    public class TariffInformationRepository : RepoBase<TariffInformation> , ITariffInformationRepository
    {
        public TariffInformationRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }



        public List<Tariff>GetTariffs()
        {  
                var tariffs = (from tariff in Db.Tariffs 
                               select tariff

                               ).Include(t => t.TariffHead).Distinct().ToList();


                return tariffs;
           
        }


        //public List<Tariff> GetTariffsByPerametersId(long? ShippingAgentId , long? ConsigneId , long? ClearingAgentId , long? GoodId , long? ShipperId , string ContainerType , long percentAgeShippingAgentId , long? portAndTerminalId)
        //{

        //    //var generaltariffs = (from tariffinfo in Db.TariffInformations
        //    //                      join tariff in Db.Tariffs on tariffinfo.TariffId equals tariff.TariffId
        //    //                      where tariffinfo.TariffType == "GeneralTariff"
        //    //                      select tariff).Include(t => t.TariffHead).Distinct().ToList();

        //    var tariffs = (from  tariffinfo in Db.TariffInformations 
        //                       join tariff in Db.Tariffs on tariffinfo.TariffId equals tariff.TariffId
        //                       where



        //                       (tariffinfo.ShippingAgentId == ShippingAgentId  )
        //                        && (tariffinfo.ConsigneId == ConsigneId  )
        //                        && (tariffinfo.ClearingAgentId == ClearingAgentId  )
        //                        && (tariffinfo.GoodsHeadId == GoodId  )
        //                        && (tariffinfo.ShipperId == ShipperId  )
        //                        && (tariffinfo.ContainerType == ContainerType )
        //                        && (tariffinfo.PortAndTerminalId == portAndTerminalId)

        //                        //(tariffinfo.ShippingAgentId == ShippingAgentId  || tariffinfo.ShippingAgentId ==  null)
        //                        // && (tariffinfo.ConsigneId == ConsigneId || tariffinfo.ConsigneId == null)
        //                        // &&( tariffinfo.ClearingAgentId == ClearingAgentId || tariffinfo.ClearingAgentId == null)
        //                        // && (tariffinfo.GoodId == GoodId || tariffinfo.GoodId == null)
        //                        // && (tariffinfo.ShipperId == ShipperId || tariffinfo.ShipperId == null)
        //                        // && (tariffinfo.ContainerType == ContainerType || tariffinfo.ContainerType == null)
        //                        && (tariffinfo.PercentAgeShippingAgentId == percentAgeShippingAgentId)
        //                   //  && tariffinfo.TariffType != "GeneralTariff"
        //                   select tariff).Include(t => t.TariffHead).Distinct().ToList();


        //    //generaltariffs.AddRange(tariffs);

        //    return tariffs;



        //}


        public List<TariffInfoViewModel> GetTariffsByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? ContainerType
                                                    ,   long? portAndTerminalId ,  string indexCargoType)
        {


            var tariffs = (from tariffinfo in Db.TariffInformations

                           join tariffTariffInofrmationTariff in Db.TariffInofrmationTariffs on tariffinfo.TariffInformationId equals tariffTariffInofrmationTariff.TariffInformationId
                           join tariff in Db.Tariffs on tariffTariffInofrmationTariff.TariffId equals tariff.TariffId
                           join tariffHead  in Db.TariffHeads on tariff.TariffHeadId equals tariffHead.TariffHeadId
                         // from portandterminal in Db.PortAndTerminals.Where(x=> x.PortAndTerminalId == tariff.PortAndTerminalId).DefaultIfEmpty()
                           where
                             
                           (tariffinfo.ShippingAgentId == ShippingAgentId)
                            && (tariffinfo.ConsigneId == ConsigneId)
                            && (tariffinfo.ClearingAgentId == ClearingAgentId)
                            && (tariffinfo.GoodsHeadId == GoodId)
                            && (tariffinfo.ShipperId == ShipperId)
                            && (tariffinfo.ISOCodeHeadId == ContainerType)
                            && (tariffinfo.IndexCargoType == indexCargoType)
                            && (tariffinfo.PortAndTerminalId == portAndTerminalId)


                           //select tariff).Include(t => t.TariffHead).ToList();

                           select new  TariffInfoViewModel
                           {
                               CBMRate = tariff.CBMRate,
                               DailyCharges = tariff.DailyCharges,
                               Description = tariffHead.Description,
                               DevededIndexAmount = tariff.DevededIndexAmount,
                               ImplementFrom = tariff.ImplementFrom,
                               ImplementTo = tariff.ImplementTo,
                               IsActive = tariff.IsActive,
                               IsCBMorRate =tariff.IsCBMorRate,
                               IsFixedRate = tariff.IsFixedRate,
                               IsDollerRate = tariff.IsDollerRate,
                               IsGeneral = tariff.IsGeneral,
                               TypeOfTariff = tariff.TypeOfTarifff,
                               Name = tariffHead.Name,
                               //portname = portandterminal.PortName, 
                               PerIndexRate = tariff.PerIndexRate,
                               PortAndTerminalId = tariff.PortAndTerminalId,
                               Rate20 = tariff.Rate20,
                               Rate40 = tariff.Rate40,
                               Rate45 = tariff.Rate45,
                               RoundCBMCode = tariff.RoundCBMCode,
                               TariffDate = tariff.TariffDate,
                               TariffHeadId = tariffHead.TariffHeadId,
                               TariffHeadType = tariffHead.TariffHeadType,
                               TariffId = tariff.TariffId,
                               TariffInofrmationTariffId  = tariffTariffInofrmationTariff.TariffInofrmationTariffId,
                               WeightRate = tariff.WeightRate,
                               IsEnableDisabled = tariffinfo.IsEnableDisabled,
                               TariffCode = tariffinfo.TariffInformationId,
                               StorageFreeDays = tariffinfo.StorageFreeDays,
                               DGFreeDays = tariffinfo.DGFreeDays,
                               TypeOfImplementation = tariff.TypeOfImplementation,

                           }).ToList();


            return tariffs;






        }
          public List<TariffInfoViewModel> GetTariffsByCollectionId(long? collectionid)
        {


            var tariffs = (from transportCollection in Db.TransportCollections
                           join transportCollectionTariff in Db.TransportCollectionTariffs on transportCollection.TransportCollectionId equals transportCollectionTariff.TransportCollectionId
                           join tariff in Db.Tariffs on transportCollectionTariff.TariffId equals tariff.TariffId
                           join tariffHead  in Db.TariffHeads on tariff.TariffHeadId equals tariffHead.TariffHeadId
                           where

                           (transportCollection.TransportCollectionId == collectionid)

                           select new  TariffInfoViewModel
                           {
                               CBMRate = tariff.CBMRate,
                               DailyCharges = tariff.DailyCharges,
                               Description = tariffHead.Description,
                               DevededIndexAmount = tariff.DevededIndexAmount,
                               ImplementFrom = tariff.ImplementFrom,
                               ImplementTo = tariff.ImplementTo,
                               IsActive = tariff.IsActive,
                               IsCBMorRate =tariff.IsCBMorRate,
                               TypeOfTariff = tariff.TypeOfTarifff,
                               IsDollerRate = tariff.IsDollerRate,
                               IsGeneral = tariff.IsGeneral,
                               Name = tariffHead.Name,
                               PerIndexRate = tariff.PerIndexRate,
                               PortAndTerminalId = tariff.PortAndTerminalId,
                               Rate20 = tariff.Rate20,
                               Rate40 = tariff.Rate40,
                               Rate45 = tariff.Rate45,
                               RoundCBMCode = tariff.RoundCBMCode,
                               TariffDate = tariff.TariffDate,
                               TariffHeadId = tariffHead.TariffHeadId,
                               TariffHeadType = tariffHead.TariffHeadType,
                               TariffId = tariff.TariffId,
                               TariffInofrmationTariffId  = transportCollectionTariff.TransportCollectionTariffId,
                               WeightRate = tariff.WeightRate


                           }).ToList();


            return tariffs;






        }


        public long  GetStorageFreeDaysByPerametersId(long? ShippingAgentId, long? ConsigneId, long? ClearingAgentId, long? GoodId, long? ShipperId, long? ContainerType, long? portAndTerminalId , string indexCargoTypeid)
        {
            var data = (from storageFreeDays in Db.StorageFreeDays                           
                           where                            
                           (storageFreeDays.ShippingAgentId == ShippingAgentId)
                            && (storageFreeDays.ConsigneId == ConsigneId)
                            && (storageFreeDays.ClearingAgentId == ClearingAgentId)
                            && (storageFreeDays.GoodsHeadId == GoodId)
                            && (storageFreeDays.ShipperId == ShipperId)
                            && (storageFreeDays.ISOCodeHeadId == ContainerType)
                            && (storageFreeDays.PortAndTerminalId == portAndTerminalId) 
                            && (storageFreeDays.IndexCargoType == indexCargoTypeid) 
                           select storageFreeDays ).FirstOrDefault();
            if(data != null)
            {
                 
                return data.StorageFreeDays ?? 0;
            }


            return 0;
        
        }

        public List<ShippingAgent> GetShippingAgentFromPercentAgeTariff()
        {
            var data = (from tariffPercentage in Db.TariffPercentages
                        join shippingAgent in Db.ShippingAgents on tariffPercentage.ShippingAgentId equals shippingAgent.ShippingAgentId
                        select new  ShippingAgent
                        {
                            ShippingAgentId = shippingAgent.ShippingAgentId,
                            Name = shippingAgent.Name
                        }).Distinct().ToList();

            return data;
        }


        void ITariffInformationRepository.ExecuteNonSQL(string sql, params object[] param)
        {
            Db.Database.ExecuteSqlCommand(sql, param);
        }




        public List<TariffInformation> GetTariffInformations(long? shippingAgentID, long? ConsigneId, long? ClearingAgentId, long? GoodsHeadId, long? ShipperId, long? ISOCodeHeadId, long? PortAndTerminalId, string IndexCargoType)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);
             

            var asd = Db.TariffInformations.FromSql($"SELECT * From TariffInformation Where ( IndexCargoType = {IndexCargoType} ) and ( ShippingAgentId = {shippingAgentID} or {shippingAgentID} is null ) and ( ConsigneId = {ConsigneId} or {ConsigneId} is null ) and  ( ClearingAgentId  = {ClearingAgentId} or {ClearingAgentId} is null )  and  ( GoodsHeadId  = {GoodsHeadId} or {GoodsHeadId} is null ) and ( ShipperId  = {ShipperId} or {ShipperId} is null ) and ( ISOCodeHeadId  = {ISOCodeHeadId} or {ISOCodeHeadId} is null ) and ( PortAndTerminalId = {PortAndTerminalId} or {PortAndTerminalId} is null )  and IsDeleted = 0  ").ToList();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }


    }
}
