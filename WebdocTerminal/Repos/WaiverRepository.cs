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
    public class WaiverRepository : RepoBase<Waiver> , IWaiverRepository
    {
        public WaiverRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public long GenerateWaiverNumber()
        {

            var lastWaiverNumber = "0001";

            var lastGeneratedWaiverNumber = GetAll().OrderByDescending(l => l.WaiverId).FirstOrDefault();

            if (lastGeneratedWaiverNumber != null)
            {
                lastWaiverNumber = Regex.Match(lastGeneratedWaiverNumber.WaiverNumber, @"\d+").Value;
                return Int32.Parse(lastWaiverNumber) + 1;

            }
            else
            {
                lastWaiverNumber = "0001";
                return Int32.Parse(lastWaiverNumber);

            }

        }

    }
}
