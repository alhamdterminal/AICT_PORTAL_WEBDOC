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
    public class EmptyGateOutContainerRepository : RepoBase<EmptyGateOutContainer>, IEmptyGateOutContainerRepository
    {
        public EmptyGateOutContainerRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<EmptyGateOutContainerViewModel> GetEmptyGateOutContainers()
        {

            var resofEmptyGateOutContainers = new List<EmptyGateOutContainerViewModel>();
            var resdata = (from containerIndex in Db.ContainerIndices
                           where
                           containerIndex.IsGateIn == true
                           && !Db.EmptyGateOutContainers.Any(s => s.VirNo == containerIndex.VirNo && s.ContainerNo == containerIndex.ContainerNo)
                           select new ImportFielsViewModel {
                             VirNumber =  containerIndex.VirNo,
                             ContainerNumber =   containerIndex.ContainerNo,
                           }).ToList();


            var resdataMerged = (from container in resdata
                                   group container by container.ContainerNumber into g
                                   select new ImportFielsViewModel
                                   {
                                       VirNumber = g.FirstOrDefault().VirNumber,
                                       ContainerNumber = g.FirstOrDefault().ContainerNumber 
                                   }).ToList();

            foreach (var item in resdataMerged)
            {

                var indexes = Db.ContainerIndices.FromSql($"select * from  ContainerIndex where VirNo = {item.VirNumber} and ContainerNo = {item.ContainerNumber}  and  IsDeleted = 0 ").ToList();
                 
                if(indexes.Where(x => x.IsDestuffed == false).Count() == 0)
                {
                    var containerinfo = Db.ContainerIndices.FromSql($"select * from  ContainerIndex where VirNo = {item.VirNumber} and ContainerNo = {item.ContainerNumber}  and  IsDeleted = 0 ").FirstOrDefault();

                    if(containerinfo != null)
                    {
                        var model = new EmptyGateOutContainerViewModel()
                        {
                            Type = "CFS",
                            ContainerNumber = containerinfo.ContainerNo,
                            VirNumber = containerinfo.VirNo,
                            ContainerSize = containerinfo.Size,
                            ShippingAgentId = containerinfo.ShippingAgentId,
                            ShippingLine = containerinfo.ShippingLine,
                            Key = containerinfo.ContainerIndexId,
                            CYContainerId = null,
                            ContainerCondition = "",
                            DeliveredYard = "",
                            DeliveredYardDate = DateTime.Now,
                            VehicleNumber = "",
                            isEmptyOut = false
                        };
                        resofEmptyGateOutContainers.Add(model);
                    }
                   


                }

            }


            //var containers = (from container in Db.ContainerIndices
            //                  join voyage in Db.Voyages on container.VoyageId equals  voyage.VoyageId
            //            where container.IsEmptyGateOut == false && container.IsGateIn == true && container.IsEmptyOut == true

            //            select new EmptyGateOutContainerViewModel
            //            {
            //                ContainerNumber = container.ContainerNo,
            //                VirNumber = voyage.VIRNo,
            //                ContainerSize = container.Size,
            //                ShippingAgentId = container.ShippingAgentId,
            //                ShippingLine = container.ShippingLine,
            //                ContainerIndexId = container.ContainerIndexId,
            //                ContainerCondition = "",
            //                DeliveredYard = "",
            //                DeliveredYardDate = DateTime.Now,
            //                VehicleNumber = "", 
            //                isEmptyOut = false
            //            }).ToList();




            //var containerMerged = (from container in containers
            //                       group container by container.ContainerNumber into g
            //                       select new EmptyGateOutContainerViewModel
            //                       { 
            //                           ContainerNumber = g.Key,
            //                           ContainerSize = g.FirstOrDefault().ContainerSize ,
            //                           VirNumber = g.FirstOrDefault().VirNumber ,
            //                           ShippingAgentId = g.FirstOrDefault().ShippingAgentId,
            //                           ShippingLine = g.FirstOrDefault().ShippingLine,
            //                           ContainerIndexId = g.FirstOrDefault().ContainerIndexId,
            //                           ContainerCondition = g.FirstOrDefault().ContainerCondition,
            //                           DeliveredYardDate = g.FirstOrDefault().DeliveredYardDate,
            //                           DeliveredYard = g.FirstOrDefault().DeliveredYard,
            //                           VehicleNumber = g.FirstOrDefault().VehicleNumber,
            //                            isEmptyOut = g.FirstOrDefault().isEmptyOut
            //                       }).ToList();


            var cyresdata = (from cycontainer in Db.CYContainers
                           where
                           cycontainer.IsGateIn == true
                           && cycontainer.IsCrossStuffed == true
                           && !Db.EmptyGateOutContainers.Any(s => s.VirNo == cycontainer.VIRNo && s.ContainerNo == cycontainer.ContainerNo)
                           select new ImportFielsViewModel
                           {
                               VirNumber = cycontainer.VIRNo,
                               ContainerNumber = cycontainer.ContainerNo,
                           }).ToList();


            var cyresdataMerged = (from container in cyresdata
                                   group container by container.ContainerNumber into g
                                 select new ImportFielsViewModel
                                 {
                                     VirNumber = g.FirstOrDefault().VirNumber,
                                     ContainerNumber = g.FirstOrDefault().ContainerNumber
                                 }).ToList();

            foreach (var item in cyresdataMerged)
            { 
                 
                    var cycontainerinfo = Db.CYContainers.FromSql($"select * from  CYContainer where VIRNo = {item.VirNumber} and ContainerNo = {item.ContainerNumber}  and  IsDeleted = 0 ").FirstOrDefault();

                    if (cycontainerinfo != null)
                    {
                        var cymodel = new EmptyGateOutContainerViewModel()
                        {
                            Type ="CY",
                            ContainerNumber = cycontainerinfo.ContainerNo,
                            VirNumber = cycontainerinfo.VIRNo,
                            ContainerSize = cycontainerinfo.Size,
                            ShippingAgentId = cycontainerinfo.ShippingAgentId,
                            ShippingLine = cycontainerinfo.ShippingLine,
                            ContainerIndexId = null,
                            CYContainerId = cycontainerinfo.CYContainerId,
                            Key = cycontainerinfo.CYContainerId,
                            ContainerCondition = "",
                            DeliveredYard = "",
                            DeliveredYardDate = DateTime.Now,
                            VehicleNumber = "",
                            isEmptyOut = false
                        };
                        resofEmptyGateOutContainers.Add(cymodel);
                    }
  
            }


            return resofEmptyGateOutContainers;
             
        }





        public List<EmptyGateOutContainerViewModel> GetEmptyDeliverContainerGatePass(string container)
        {
            var emptygatePass = (from emptyContainercontainers in Db.EmptyGateOutContainers
                                 join containerindex in Db.ContainerIndices on emptyContainercontainers.ContainerIndexId equals containerindex.ContainerIndexId
                                 join voyage in Db.Voyages on containerindex.VoyageId equals voyage.VoyageId
                                 where
                                 containerindex.ContainerNo == container
                                 select new EmptyGateOutContainerViewModel
                                 {
                                     EmptyGateOutContainerId = emptyContainercontainers.EmptyGateOutContainerId,
                                     ContainerIndexId = emptyContainercontainers.ContainerIndexId,
                                     ContainerCondition = emptyContainercontainers.ContainerCondition,
                                      ContainerSize = containerindex.Size,
                                     CreatedDate = emptyContainercontainers.CreatedDate ?? DateTime.Now,
                                     DeliveredYard = emptyContainercontainers.DeliveredYard,
                                     ContainerNumber = containerindex.ContainerNo,
                                     VirNumber = voyage.VIRNo
                                 }).Distinct().ToList();

            return emptygatePass;
        }


    }
}
