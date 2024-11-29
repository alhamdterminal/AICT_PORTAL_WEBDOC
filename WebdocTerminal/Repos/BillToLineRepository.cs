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
    public class BillToLineRepository : RepoBase<BillToLine> , IBillToLineRepository
    {

        public BillToLineRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public long GenerateBillToLineNumber()
        {

            var lastBillToLineNumber = "0001";

            //var lastGeneratedBillToLineNumber = GetAll().OrderByDescending(l => l.BillToLineId).FirstOrDefault();

            //if (lastGeneratedBillToLineNumber != null)
            //{
            //    lastBillToLineNumber = Regex.Match(lastGeneratedBillToLineNumber.BillToLineNumber, @"\d+").Value;
            //    return Int32.Parse(lastBillToLineNumber) + 1;

            //}
            //else
            //{
            //    lastBillToLineNumber = "0001";
            //    return Int32.Parse(lastBillToLineNumber);

            //}

            return Int32.Parse(lastBillToLineNumber);
        }

    }
}
