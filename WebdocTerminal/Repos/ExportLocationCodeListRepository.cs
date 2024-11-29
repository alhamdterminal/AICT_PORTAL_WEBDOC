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
    public class ExportLocationCodeListRepository : RepoBase<ExportLocationCodeList>, IExportLocationCodeListRepository
    {
        public ExportLocationCodeListRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<string> GetCountries()
        {
            var countries = (from codeList in Db.ExportLocationCodeLists
                             select codeList.CountryName).Distinct().ToList();

            return countries;
        }

        public List<string> GetDestinationPorts()
        {
            var ports = (from codeList in Db.ExportLocationCodeLists
                             select codeList.PortName).Distinct().ToList();

            return ports;
        }

        public List<string> GetDestinationPortsByCountry(string country)
        {
            var ports = (from codeList in Db.ExportLocationCodeLists
                         where codeList.CountryName == country
                         select codeList.PortName).Distinct().ToList();

            return ports;
        }


    }
}
