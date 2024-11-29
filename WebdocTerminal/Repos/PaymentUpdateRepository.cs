using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class PaymentUpdateRepository : RepoBase<PaymentUpdate> , IPaymentUpdateRepository
    {
        public PaymentUpdateRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public List<PreAlertViewModel> GetUnAssignPaymentUpdate(string prealertNo)
        {
            var PaymentUpdates = (from preAlert in Db.PreAlerts
                        join preAlertDetail in Db.PreAlterDetails on preAlert.PreAlertId equals preAlertDetail.PreAlertId
                        join shippingLine in Db.ShippingLines on preAlertDetail.ShippingLineId equals shippingLine.ShippingLineId
                        where
                       preAlert.PreAlertNumber == prealertNo
                        
                        select new PreAlertViewModel
                        {
                           ShippingLineIdForSD = shippingLine.ShippingLineId, 
                           ShippingLineIdForTHC = shippingLine.ShippingLineId, 
                           ShippingLineIdForLOLO = shippingLine.ShippingLineId, 
                            CargoType = preAlertDetail.CargoType,
                           ContainerNumber = preAlertDetail.ContainerNumber,
                           PreAlterDetailId = preAlertDetail.PreAlterDetailId,
                           Size = preAlertDetail.Size,
                           SOCCOC = preAlertDetail.SOCCOC,
                           Type = preAlertDetail.Type
                        }).ToList();

            return PaymentUpdates;
        }



        public List<PreAlertViewModel> GetUnAssignPaymentUpdateMultiple(string prealertNo)
        {

            var resdata = new List<PreAlertViewModel>();

            var iNo = prealertNo.Split(",").ToList();

            foreach (var itmIndex in iNo)
            {
                var PaymentUpdates = (from preAlert in Db.PreAlerts
                                      join preAlertDetail in Db.PreAlterDetails on preAlert.PreAlertId equals preAlertDetail.PreAlertId
                                      join shippingLine in Db.ShippingLines on preAlertDetail.ShippingLineId equals shippingLine.ShippingLineId
                                      where
                                     preAlert.PreAlertNumber == itmIndex

                                      select new PreAlertViewModel
                                      {
                                          ShippingLineIdForSD = shippingLine.ShippingLineId,
                                          ShippingLineIdForTHC = shippingLine.ShippingLineId,
                                          ShippingLineIdForLOLO = shippingLine.ShippingLineId,
                                          CargoType = preAlertDetail.CargoType,
                                          ContainerNumber = preAlertDetail.ContainerNumber,
                                          PreAlterDetailId = preAlertDetail.PreAlterDetailId,
                                          Size = preAlertDetail.Size,
                                          SOCCOC = preAlertDetail.SOCCOC,
                                          Type = preAlertDetail.Type
                                      }).ToList();


                resdata.AddRange(PaymentUpdates);
            }

                

            return resdata;
        }

        public long GeneratePaymentUpdateNumber()
        {

 
            var paymentUpdates = ( from paymentUpdate in Db.PaymentUpdates
                                                     select paymentUpdate).ToList();

            if (paymentUpdates.Count() > 0)
            {
               var lastPaymentUpdateNumber = paymentUpdates.Count() ;
                 return  lastPaymentUpdateNumber + 1;

            }
            else
            {
                
                return 1;

            }

        }



        public List<PaymentUpdateViewModel> GetRequestNumbers()
        {  
            var data = (from paymentUpdate in Db.PaymentUpdates 
                        where paymentUpdate.Status == false
                         select new PaymentUpdateViewModel
                         {
                            RequestNo = paymentUpdate.RequestNumber ,
                            RequestNoWithDateTime  = paymentUpdate.RequestNumber + "   "+ paymentUpdate.RequestDate.ToString("dd-MMM-yy hh:mm tt")
                         }).ToList();
            return data;

        }


    }
}
