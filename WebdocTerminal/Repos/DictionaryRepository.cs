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
    public class DictionaryRepository : RepoBase<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public long GenerateTRNumber()
        {
            var dictionaries = Table.ToList();
            if (dictionaries.Count == 0)
            {
                return 1;
            }

            else
            {
                return dictionaries.Count + 1;
            }

        }


    }
}
