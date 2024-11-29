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
    public class WeighmentRepository : RepoBase<Weighment>, IWeighmentRepository
    {
        public WeighmentRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public List<CFSWeighmentViewModel> GetCFSWeightmentContainers()
        {
            var containers = (from containerindex in Db.ContainerIndices
                              join ihco in Db.IHCOs on containerindex.BLNo equals ihco.BLNumber
                              where containerindex.IsWeighed == false && containerindex.IndexNo == ihco.IndexNumber

                              select new CFSWeighmentViewModel
                              {
                                  ContainerIndexId = containerindex.ContainerIndexId,
                                  BLGrossWeight = containerindex.BLGrossWeight,
                                  BLNo = containerindex.BLNo,
                                  ContainerNo = containerindex.ContainerNo,
                                  HandlingCode = ihco.HandlingCode,
                                  indexNo = containerindex.IndexNo.ToString(),
                                  VIRNo = ihco.VIRNumber
                              }).ToList();

            return containers;
        }


        public List<CFSWeighmentViewModel> GetCYWeightmentContainers()
        {
            var containers = (from CYContainer in Db.CYContainers
                               join ihco in Db.IHCs on CYContainer.ContainerNo equals ihco.ContainerNumber
                              where CYContainer.IsWeighed == false
                              select new CFSWeighmentViewModel
                              {
                                  CYContainerId = CYContainer.CYContainerId,
                                  BLGrossWeight = CYContainer.BLGrossWeight,
                                  BLNo = CYContainer.BLNo,
                                  ContainerNo = CYContainer.ContainerNo,
                                  HandlingCode = ihco.HandlingCode,
                                  indexNo = CYContainer.IndexNo.ToString(),
                                  VIRNo = CYContainer.VIRNo
                              }).ToList();

            return containers;
        }

        public long CountCYWeighment()
        {
            var data = (from CYContainer in Db.CYContainers
                        join Weighent in Db.Weighments on CYContainer.CYContainerId equals Weighent.CyContainerId
                        where CYContainer.IsWeighed == true
                        select Weighent


                ).ToList();
            if (data.Count == null)
            {
                return 0;
            }
            else
            {
                return data.Count;
            }

        }
    }
}
