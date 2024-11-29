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
    public class CRLORepository : RepoBase<CRLO>, ICRLORepository
    {
        public CRLORepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<CRLO> GetMANUALCRLOList()
        {
            var res = Db.CRLOs.FromSql($"SELECT * From CRLO where  MessageFrom = 'MANUAL' and IsDeleted = 0  ").ToList();

            return res;
        }

        public List<CRLO> GetMANUALICRLObyigmindexbl(string virno, int indeno , string blno)
        {
            var res = Db.CRLOs.FromSql($"SELECT * From CRLO  where VIRNumber = {virno} and IndexNumber = {indeno} and BLNumber = {blno} and IsDeleted = 0  ").ToList();

            return res;
        }


        public CRLO GetManualCrloInfo(string virno, int indeno, string blno)
        {
            var res = Db.CRLOs.FromSql($"SELECT * From CRLO  where VIRNumber = {virno} and IndexNumber = {indeno} and BLNumber = {blno} and IsDeleted = 0 and MessageFrom = 'MANUAL' ").LastOrDefault();

            return res;
        }
    }
}
