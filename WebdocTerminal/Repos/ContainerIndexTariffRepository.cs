using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using Microsoft.EntityFrameworkCore;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class ContainerIndexTariffRepository : RepoBase<ContainerIndexTariff>, IContainerIndexTariffRepository
    {
        public ContainerIndexTariffRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<TariffViewModel> GetContainerIndexTariffs(long ContainerIndexId)
        {
            var InvoiceData = false;

            var invoice = (from Invoice in Db.Invoices
                           where Invoice.ContainerIndexId == ContainerIndexId
                           select Invoice).FirstOrDefault();
            if (invoice != null)
            {

                InvoiceData = true;
            }

            var tariffs = (from tariff in Db.Tariffs
                           join containerIndexTariff in Db.ContainerIndexTariffs on tariff.TariffId equals containerIndexTariff.TariffId
                           join containerIndex in Db.ContainerIndices on containerIndexTariff.ContainerIndexId equals containerIndex.ContainerIndexId
                           //from invoice in Db.Invoices.Where(c => c.ContainerIndexId == containerIndex.ContainerIndexId).DefaultIfEmpty()
                           where containerIndex.ContainerIndexId == ContainerIndexId
                           select new TariffViewModel
                           {
                               Type = "IndexTariff",
                               CBMRate = tariff.CBMRate,
                               DailyCharges = tariff.DailyCharges,
                               ImplementFrom = tariff.ImplementFrom,
                               IsActive = tariff.IsActive,
                               PerIndexRate = tariff.PerIndexRate,
                               Rate20 = tariff.Rate20,
                               Rate40 = tariff.Rate40,
                               Rate45 = tariff.Rate45,
                               DevededIndexAmount = tariff.DevededIndexAmount,
                               RoundCBMCode = tariff.RoundCBMCode,
                               TariffDate = tariff.TariffDate,
                               TariffHead = tariff.TariffHead,
                               TariffHeadId = tariff.TariffHeadId,
                               TariffId = tariff.TariffId,
                               WeightRate = tariff.WeightRate,
                               InvoiceCreated = InvoiceData
                                 
                           }).Include(t => t.TariffHead).ToList();
   
            return tariffs;
        }



        public List<TariffViewModel> GetContainerIndexTariffsCY(string igm, long indexNo)
        {
            var InvoiceData = false;

            var cycontainer = (from cyContainer in Db.CYContainers
                               where cyContainer.VIRNo == igm && cyContainer.IndexNo == indexNo
                               select cyContainer.CYContainerId).FirstOrDefault();

            if (cycontainer >= 0)
            {

                var invoice = (from Invoice in Db.Invoices
                               where Invoice.CYContainerId == cycontainer
                               select Invoice).FirstOrDefault();
                if (invoice != null)
                {

                    InvoiceData = true;
                }
            }

            var tariffs = (from tariff in Db.Tariffs
                           join containerIndexTariff in Db.ContainerIndexTariffs on tariff.TariffId equals containerIndexTariff.TariffId
                           join CYContainer in Db.CYContainers on containerIndexTariff.CYContainerId equals CYContainer.CYContainerId
                           //from invoice in Db.Invoices.Where(c => c.CYContainerId == CYContainer.CYContainerId).DefaultIfEmpty()
                           where CYContainer.VIRNo == igm && CYContainer.IndexNo == indexNo
                           select new TariffViewModel
                           {    
                               Type = "IndexTariff",
                               CBMRate = tariff.CBMRate,
                               DailyCharges = tariff.DailyCharges,
                               ImplementFrom = tariff.ImplementFrom,
                               IsActive = tariff.IsActive,
                               PerIndexRate = tariff.PerIndexRate,
                               Rate20 = tariff.Rate20,
                               Rate40 = tariff.Rate40,
                               Rate45 = tariff.Rate45,
                               DevededIndexAmount = tariff.DevededIndexAmount,
                               RoundCBMCode = tariff.RoundCBMCode,
                               TariffDate = tariff.TariffDate,
                               TariffHead = tariff.TariffHead,
                               TariffHeadId = tariff.TariffHeadId,
                               TariffId = tariff.TariffId,
                               WeightRate = tariff.WeightRate,
                               InvoiceCreated = InvoiceData

                           }).Include(t => t.TariffHead).ToList();
             
            return tariffs;
        }

        public List<Tariff> GetUnassignedontainerIndexTariffs(long ContainerIndexId)
        {
            if (ContainerIndexId > 0)
            {
                var tariffs = (from tariff in Db.Tariffs
                               from containerIndexTariff in Db.ContainerIndexTariffs.Where(c => c.TariffId == tariff.TariffId).DefaultIfEmpty()
                               where (!Db.ContainerIndexTariffs.Any(es => (es.TariffId == tariff.TariffId) && (es.ContainerIndexId == ContainerIndexId)) &&
                                      !Db.ShippingAgentTariffs.Any(s => s.TariffId == tariff.TariffId))
                               select tariff
                               ).Include(t => t.TariffHead).Distinct().ToList();


                return tariffs;
            }

            return Db.Tariffs.Include(s => s.TariffHead).ToList();
        }


        //public List<Tariff> GetUnassignedExportContainerGDTariffs(long EnteringCargoId)
        //{
        //    if (EnteringCargoId > 0)
        //    {
        //        var tariffs = (from tariff in Db.Tariffs
        //                       from containerIndexTariff in Db.ContainerIndexTariffs.Where(c => c.TariffId == tariff.TariffId).DefaultIfEmpty()
        //                       where (!Db.ContainerIndexTariffs.Any(es => (es.TariffId == tariff.TariffId) && (es.EnteringCargoId == EnteringCargoId)) &&
        //                              !Db.ShippingAgentTariffs.Any(s => s.TariffId == tariff.TariffId))
        //                       select tariff
        //                       ).Include(t => t.TariffHead).Distinct().ToList();


        //        return tariffs;
        //    }

        //    return Db.Tariffs.Include(s => s.TariffHead).ToList();
        //}

        public List<Tariff> GetUnassignedContainerTariffsCY(string igm, long indexNo)
        {
            var tariffs = (from tariff in Db.Tariffs
                           from containerIndexTariff in Db.ContainerIndexTariffs.Where(c => c.TariffId == tariff.TariffId).DefaultIfEmpty()
                           where (!Db.ContainerIndexTariffs.Any(es => (es.TariffId == tariff.TariffId) && (es.CYContainer.VIRNo == igm && es.CYContainer.IndexNo == indexNo)) &&
                                      !Db.ShippingAgentTariffs.Any(s => s.TariffId == tariff.TariffId))
                           select tariff).Include(t => t.TariffHead).Distinct().ToList();


            return tariffs;
        }

        //public List<TariffViewModel> GetCargoTariffs(long EnteringCargoId)
        //{
        //    var InvoiceData = false;

        //    var enteringCargoId = (from cargo in Db.EnteringCargos
        //                           where cargo.EnteringCargoId == EnteringCargoId
        //                           select cargo.EnteringCargoId).FirstOrDefault();

        //    if (enteringCargoId >= 0)
        //    {

        //        var invoice = (from Invoice in Db.Invoices
        //                       where Invoice.EnteringCargoId == enteringCargoId
        //                       select Invoice).FirstOrDefault();
        //        if (invoice != null)
        //        {

        //            InvoiceData = true;
        //        }
        //    }


        //    var tariffs = (from tariff in Db.Tariffs
        //                   join containerIndexTariff in Db.ContainerIndexTariffs on tariff.TariffId equals containerIndexTariff.TariffId
        //                   join enteringCargo in Db.EnteringCargos on containerIndexTariff.EnteringCargoId equals enteringCargo.EnteringCargoId
        //                   //from invoice in Db.Invoices.Where(c => c.CYContainerId == CYContainer.CYContainerId).DefaultIfEmpty()
        //                   where enteringCargo.EnteringCargoId == enteringCargoId
        //                   select new TariffViewModel
        //                   {
        //                       CBMRate = tariff.CBMRate,
        //                       DailyCharges = tariff.DailyCharges,
        //                       ImplementFrom = tariff.ImplementFrom,
        //                       IsActive = tariff.IsActive,
        //                       PerIndexRate = tariff.PerIndexRate,
        //                       Rate20 = tariff.Rate20,
        //                       Rate40 = tariff.Rate40,
        //                       Rate45 = tariff.Rate45,
        //                       RoundCBMCode = tariff.RoundCBMCode,
        //                       TariffDate = tariff.TariffDate,
        //                       TariffHead = tariff.TariffHead,
        //                       TariffHeadId = tariff.TariffHeadId,
        //                       TariffId = tariff.TariffId,
        //                       WeightRate = tariff.WeightRate,
        //                       InvoiceCreated = InvoiceData

        //                   }).Include(t => t.TariffHead).ToList();

        //    return tariffs;

        //}


    }
}
