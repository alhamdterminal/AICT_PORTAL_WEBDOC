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
    public class CRLRepository : RepoBase<CRL> , ICRLRepository
    {
        public CRLRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<CRL> GetMANUALCRLList()
        {
            var res = Db.CRLs.FromSql($"SELECT * From CRL where  FileName = 'MANUALData' and IsDeleted = 0  ").ToList();

            return res;
        }

        public List<CRL> GetMANUALICRLbyigmcontainer(string virno,  string containerNumber)
        {
            var res = Db.CRLs.FromSql($"SELECT * From CRL  where VIRNumber = {virno} and ContainerNumber = {containerNumber}  and IsDeleted = 0  ").ToList();

            return res;
        }

        public CRL GetManualCrlInfo(string virno, string containerNumber)
        {
            var res = Db.CRLs.FromSql($"SELECT * From CRL  where VIRNumber = {virno} and ContainerNumber = {containerNumber}  and IsDeleted = 0 and  FileName = 'MANUALData' ").LastOrDefault();

            return res;
        }

    }
}
