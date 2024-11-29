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
    public class IndexWeighmentRepository : RepoBase<IndexWeighment> , IIndexWeighmentRepository
    {
        public IndexWeighmentRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<IndexWeighment> GetIndexWeighmentByIgmIndex(string igm, int indexno)
        {
            var res = Db.IndexWeighments.FromSql($"select * from IndexWeighment where  VirNo = {igm} and Indexno = {indexno} and IsDeleted = 0   ").ToList();

            return res;

        }
    }
}
