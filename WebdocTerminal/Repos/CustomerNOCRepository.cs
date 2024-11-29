using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class CustomerNOCRepository : RepoBase<CustomerNOC> , ICustomerNOCRepository
    {
        public CustomerNOCRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<CustomerNOCViewModel> GetCargoDetail(string gdnumber)
        {
            var data = (from opia in Db.OPIAs
                        join cargo  in Db.Cargos on opia.GDNumber equals cargo.GDNumber
                        join commodity in Db.Commodities on cargo.CommodityId equals commodity.CommodityId
                        join vessel in Db.VesselExports on cargo.VesselExportId equals vessel.VesselExportId
                        join voyage in Db.VoyageExports on cargo.VoyageExportId equals voyage.VoyageExportId
                        where cargo.GDNumber == gdnumber
                        select new CustomerNOCViewModel
                        {

                            CommodityName = commodity.CommodityName,
                            CBM = cargo.CBM,
                            Weight = cargo.Weight,
                            Vessel = vessel.VesselName,
                            Voyage = voyage.VoyageNumber,
                            PackagesReceived = cargo.PackageReceived,
                            PackageType = opia.PackageType
                        }).ToList();
            
                return data;
             
        }

        public List<CustomerNOCViewModel> GetCustomerNOCbyGDnumber(string gdnumber)
        {
            var data = (from customerNOCs in Db.CustomerNOCs
                        join loadingProgram in Db.LoadingPrograms on customerNOCs.GDNumber equals loadingProgram.GDNumber
                        join shippingAgentExportsA in Db.ShippingAgentExports on customerNOCs.ShippingAgentExportId equals shippingAgentExportsA.ShippingAgentExportId
                        join shippingAgentExportsB in Db.ShippingAgentExports on customerNOCs.ShippingAgentExportBId equals shippingAgentExportsB.ShippingAgentExportId
                        where customerNOCs.GDNumber == gdnumber
                        select new CustomerNOCViewModel
                        {
                            ShippingAgentName = shippingAgentExportsA.ShippingAgentName,
                            ShippingAgentNameB = shippingAgentExportsB.ShippingAgentName,
                            NameOfPerson = customerNOCs.NameOfPerson,
                            EmailReceived = customerNOCs.EmailReceived,
                            ContactNumber = customerNOCs.ContactNumber,


                        }).ToList();

            return data;

        }
        public List<GDListViewModel> GetCustomerNocAndCargoRollOver()
        {

            var gdList = (from gatein in Db.GateInExports
                          where
                            !Db.ExportContainerItems.Any(s => s.GDNumber == gatein.GDNumber && s.IsOvams == true)
                            &&
                            !Db.OCRLs.Any(s => s.GDNumber == gatein.GDNumber && s.Status == "OD")
                          select new GDListViewModel
                          {
                              GDNumber = gatein.GDNumber,
                          }).ToList();

            return gdList;
        }

        public List<ExportContainer> GetCustomerNocAndCargoRollOverContainers()
        {

            var gdList = (from exportContainerItems in Db.ExportContainerItems
                          where
                           exportContainerItems.IsOvams == false
                            &&
                            !Db.OCRLs.Any(s => s.GDNumber == exportContainerItems.GDNumber && s.Status == "OD")
                          select new ExportContainer
                          {
                              ExportContainerId = exportContainerItems.ExportContainerId ?? 0,
                              ContainerNumber = exportContainerItems.ContainerNumber,
                          }).ToList();




            var containerMerged = (from res in gdList
                                   group res by res.ExportContainerId into g
                                   select new ExportContainer
                                   {
                                       ExportContainerId = g.FirstOrDefault().ExportContainerId,
                                       ContainerNumber = g.FirstOrDefault().ContainerNumber
                                   }).ToList();


            return containerMerged;
        }

        public ExportContainerItem GetGDIfo(string gdnumber)
        {
            var asd = Db.ExportContainerItems.FromSql($"select * from ExportContainerItem  where GDNumber = {gdnumber} and IsDeleted = 0 and IsOvams  = 1").LastOrDefault();

            return asd;

        }
    }
}
