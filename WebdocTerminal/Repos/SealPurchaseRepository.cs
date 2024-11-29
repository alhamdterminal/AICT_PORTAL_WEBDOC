using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class SealPurchaseRepository : RepoBase<SealPurchase>, ISealPurchaseRepository
    {
        public SealPurchaseRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public long GenerateSealNumber()
        {

            var lastSealNumber = "0001";

            var sealNumber = GetAll().OrderByDescending(l => l.SealPurchaseId).FirstOrDefault();

            if (sealNumber != null)
            {
                lastSealNumber = Regex.Match(sealNumber.SealPurchaseNo.ToString(), @"\d+").Value;
                return Int32.Parse(lastSealNumber) + 1;

            }
            else
            {
                lastSealNumber = "0001";
                return Int32.Parse(lastSealNumber);

            }

        }

        public List<SealPurchase> GetSealsByNo(long fromSeal, long toSeal, string batch)
        {
            var seals = (from sp in Db.SealPurchases 
                              where 
                              sp.SealFrom >= fromSeal && 
                              sp.SealFrom <= toSeal && 
                              sp.SealPurchaseNo == batch
                              // && sp.Status == false
                              select new SealPurchase
                              { 
                                  //Status = true,
                                  SealFrom = sp.SealFrom, 
                                  PurchaseDate = sp.PurchaseDate,
                                  Rate = sp.Rate,
                                  SealPurchaseId = sp.SealPurchaseId,
                                  SealPurchaseNo = sp.SealPurchaseNo
                              }).ToList();

            return seals;
        }


        public List<SealPurchase> GetSealsByBatchCode(string batch)
        {
            var seals = (from sp in Db.SealPurchases
                         where 
                         sp.SealPurchaseNo == batch &&
                         //sp.Status == true && 
                         sp.AssignStatus == false
                         select new SealPurchase
                         {
                            // Status = true,
                             SealFrom = sp.SealFrom,
                             PurchaseDate = sp.PurchaseDate,
                             Rate = sp.Rate,
                             SealPurchaseId = sp.SealPurchaseId,
                             SealPurchaseNo = sp.SealPurchaseNo
                         }).ToList();

            return seals;
        }

    }
}
