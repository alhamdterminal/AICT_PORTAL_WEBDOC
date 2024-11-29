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
    public class ServicesInfoRepository : RepoBase<ServicesInfo> , IServicesInfoRepository
    {
        public ServicesInfoRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<ServiceInfoViewModel> GetServicesInfo()
        {
            var data = (from servicesInfo in Db.ServicesInfos
                        join natureOfPayment in Db.NatureOfPayments on servicesInfo.NatureOfPaymentId equals natureOfPayment.NatureOfPaymentId
                        join servicesSection in Db.ServicesSections on servicesInfo.ServicesSectionId equals servicesSection.ServicesSectionId
                        join servicesType in Db.ServicesTypes on servicesInfo.ServicesTypeId equals servicesType.ServicesTypeId
                        select new ServiceInfoViewModel
                        {
                            ServiceInfoId = servicesInfo.ServicesInfoId,
                            Code = servicesInfo.ServicesInfoCode,
                            Name = servicesInfo.ServicesInfoName,
                            NatureOfPayment = natureOfPayment.NatureOfPaymentName,
                            Section = servicesSection.ServicesSectionCode,
                            Type = servicesType.ServicesTypeName,
                            Rate = servicesInfo.Rate

                        }).ToList();

            return data;
        }
    }
}
