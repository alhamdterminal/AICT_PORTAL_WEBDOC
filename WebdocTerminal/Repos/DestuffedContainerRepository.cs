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
    public class DestuffedContainerRepository : RepoBase<DestuffedContainer>, IDestuffedContainerRepository
    {
        public DestuffedContainerRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }
        public long DestuffedContainersCountCFS()
        {
            var destuffedCountainer = (from containerIndex in Db.ContainerIndices 
                                       where containerIndex.IsDestuffed == true  
                                     select new ContainerIndex {
                                    ContainerNo = containerIndex.ContainerNo,
                                         VoyageId = containerIndex.VoyageId
                                     }).Distinct().ToList();
            if (destuffedCountainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return destuffedCountainer.Count;
            }
        }


        public long DestuffedContainersIndexCountCFS()
        {
            var destuffedCountainer = (from containerIndex in Db.ContainerIndices
                                       where containerIndex.IsDestuffed == true
                                       select containerIndex
                         ).ToList();
            if (destuffedCountainer.Count == 0)
            {
                return 0;
            }

            else
            {
                return destuffedCountainer.Count;
            }
        }

        public List<ContainerIndex> getContainerIndex(string containerNumber , string virNo)
        {
            var data = (from containerIndex in Db.ContainerIndices
                        join voyage in Db.Voyages on containerIndex.VoyageId equals voyage.VoyageId
                        where containerIndex.ContainerNo == containerNumber && voyage.VIRNo == virNo
                        select new ContainerIndex {
                            ContainerIndexId = containerIndex.ContainerIndexId
                        }).Distinct().ToList();

            return data;
        }



        public DestuffedContainer GetDestuffByContainerIndexId(long containerinexid)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.DestuffedContainers.FromSql($"SELECT * From DestuffedContainer Where ContainerIndexId = {containerinexid} and IsDeleted = 0").FirstOrDefault();
            if (asd != null)
            {
                return asd;
            }
            return null;
        }
    }
}
