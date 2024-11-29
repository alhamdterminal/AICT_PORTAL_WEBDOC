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
    public class DeliveryOrderRepository : RepoBase<DeliveryOrder>, IDeliveryOrderRepository
    {
        public DeliveryOrderRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public IEnumerable<DOItemViewModel> GetBLNumberConsigneename(long indexnumber, string containernumber)
        {
            var data = (from igmo in Db.IGMOs
                        from gdio in Db.GDIOs.Where(g => g.BLNumber == igmo.BLNumber).DefaultIfEmpty()
                        from gdi in Db.GDIs.Where(g => g.BLNumber == igmo.BLNumber).DefaultIfEmpty()
                        from CRL in Db.CRLs.Where(g => g.ContainerNumber == igmo.ContainerNumber).DefaultIfEmpty()
                        from CRLO in Db.CRLOs.Where(g => g.VIRNumber == igmo.VIRNumber && g.IndexNumber == igmo.IndexNumber).DefaultIfEmpty()
                        where igmo.IndexNumber == indexnumber && igmo.ContainerNumber == containernumber
                        select new DOItemViewModel
                        {
                            BLNumber = gdi != null ? gdi.BLNumber : gdio.BLNumber,
                            ConsigneeName = gdi != null ? gdi.ConsigneeName : gdio.ConsigneeName,
                            ConsigneeNTN = gdi != null ? gdi.ConsigneeNTN : gdio.ConsigneeNTN,
                            DONumber = CRL != null ? CRL.DocumentCodes : CRLO.DocumentCodes,
                            GDNumber = CRL != null ? CRL.GDNumber : CRLO.GDNumber,

                        }).ToList();


            return data;

        }

        public IEnumerable<DOItemViewModel> GetBLNumberConsigneenameCY(long indexnumber, string containernumber, string igm)
        {
            var data = (from igmo in Db.IGMOs
                        from gdio in Db.GDIOs.Where(g => g.BLNumber == igmo.BLNumber && g.VIRNumber == igmo.VIRNumber && g.IndexNumber == igmo.IndexNumber).DefaultIfEmpty()
                        from gdi in Db.GDIs.Where(g => g.BLNumber == igmo.BLNumber && g.VIRNumber == igmo.VIRNumber && g.ContainerNumber == igmo.ContainerNumber).DefaultIfEmpty()
                        from CRL in Db.CRLs.Where(g => g.ContainerNumber == igmo.ContainerNumber && g.VIRNumber == igmo.VIRNumber).DefaultIfEmpty()
                        from CRLO in Db.CRLOs.Where(g => g.VIRNumber == igmo.VIRNumber && g.IndexNumber == igmo.IndexNumber && g.BLNumber == igmo.BLNumber).DefaultIfEmpty()
                        where igmo.IndexNumber == indexnumber && igmo.ContainerNumber == containernumber && igmo.VIRNumber == igm
                        select new DOItemViewModel
                        {
                            BLNumber = gdi != null ? gdi.BLNumber : gdio.BLNumber,
                            ConsigneeName = gdi != null ? gdi.ConsigneeName : gdio.ConsigneeName,
                            ConsigneeNTN = gdi != null ? gdi.ConsigneeNTN : gdio.ConsigneeNTN,
                            DONumber = CRL != null ? CRL.DocumentCodes : CRLO.DocumentCodes,
                            GDNumber = CRL != null ? CRL.GDNumber : CRLO.GDNumber,
                            Status = CRL != null ? CRL.Status : CRLO.Status,
                            DocumentCode = CRL != null ? CRL.DocumentCodes : CRLO.DocumentCodes,

                        }).ToList();


            return data;

        }
        public IEnumerable<DOItemViewModel> GetBLNumberConsigneenameCFS(long Index, string blNumbr, string igm)
        {
            var data = (from igmo in Db.IGMOs
                        from gdio in Db.GDIOs.Where(g => g.BLNumber == igmo.BLNumber && g.VIRNumber == igmo.VIRNumber && g.IndexNumber == igmo.IndexNumber).DefaultIfEmpty()
                        from gdi in Db.GDIs.Where(g => g.BLNumber == igmo.BLNumber && g.VIRNumber == igmo.VIRNumber && g.ContainerNumber == igmo.ContainerNumber).DefaultIfEmpty()
                        from CRL in Db.CRLs.Where(g => g.ContainerNumber == igmo.ContainerNumber && g.VIRNumber == igmo.VIRNumber).DefaultIfEmpty()
                        from CRLO in Db.CRLOs.Where(g => g.VIRNumber == igmo.VIRNumber && g.IndexNumber == igmo.IndexNumber && g.BLNumber == igmo.BLNumber).DefaultIfEmpty()
                        where igmo.IndexNumber == Index && igmo.BLNumber == blNumbr && igmo.VIRNumber == igm
                        select new DOItemViewModel
                        {
                            BLNumber = gdi != null ? gdi.BLNumber : gdio.BLNumber,
                            ConsigneeName = gdi != null ? gdi.ConsigneeName : gdio.ConsigneeName,
                            ConsigneeNTN = gdi != null ? gdi.ConsigneeNTN : gdio.ConsigneeNTN,
                            DONumber = CRL != null ? CRL.DocumentCodes : CRLO.DocumentCodes,
                            GDNumber = CRL != null ? CRL.GDNumber : CRLO.GDNumber,
                            Status = CRL != null ? CRL.Status : CRLO.Status,
                            DocumentCode = CRL != null ? CRL.DocumentCodes : CRLO.DocumentCodes,

                        }).ToList();


            return data;

        }


        public GDI GetBLNumberConsigneeNameForCY(string virNo, int IndexNo)
        {
            var data = (from gdio in Db.GDIOs
                        where gdio.VIRNumber == virNo && gdio.IndexNumber == IndexNo
                        select new GDI
                        {
                            BLNumber = gdio.BLNumber,
                            ConsigneeName = gdio.ConsigneeName,
                            ConsigneeNTN = gdio.ConsigneeNTN,

                        }).FirstOrDefault();


            return data;

        }

        public BillDetailsViewModel GetDeliveryOrderBill(long DONumber)
        {
            var invoiceDetails = new BillDetailsViewModel();
            var doDetails = (from dordemr in Db.DeliveryOrders
                             where
                                 dordemr.DONumber == DONumber
                             select dordemr).LastOrDefault();
            if (doDetails.ContainerIndexId != null)
            {
                invoiceDetails = (from invoice in Db.Invoices
                                  where
                                      invoice.ContainerIndexId == doDetails.ContainerIndexId
                                  select new BillDetailsViewModel
                                  {
                                      BillDate = invoice.InvoiceDate,
                                      BillNo = invoice.InvoiceNo 
                                  }).OrderByDescending(d => d.BillNo).FirstOrDefault();
                return invoiceDetails;


            }
            if (doDetails.CYContainerId != null)
            {
                invoiceDetails = (from invoice in Db.Invoices
                                  where
                                      invoice.CYContainerId == doDetails.CYContainerId
                                  select new BillDetailsViewModel
                                  {
                                      BillDate = invoice.InvoiceDate,
                                      BillNo = invoice.InvoiceNo 
                                  }).OrderByDescending(d => d.BillNo).FirstOrDefault();
                return invoiceDetails;
            }


            return invoiceDetails;

        }

        public DeliveryOrder GetDeliveryOrderByContainerIndexId(long containerinexid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.DeliveryOrders.FromSql($"SELECT * From DeliveryOrder Where ContainerIndexId = {containerinexid} and IsDeleted = 0 ").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }

        public List<GatePass> GatePasses(long dono)
        {
            var totalItems = Db.OrderDetails.FromSql($"select * from GatePass where   IsDeleted = 0  and DONumber = {dono}").ToList();

            return totalItems;
        }

    }
}
