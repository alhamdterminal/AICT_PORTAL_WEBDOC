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
    public class EdiMessageRepository : RepoBase<EdiMessage>, IEdiMessageRepository
    {
        public EdiMessageRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<EdiMessage> GetBackInTimeMessages(int mins)
        {
            var backintime = DateTime.Now.AddMinutes(mins);

            var edi_messages = (from msg in Db.EdiMessages
                                where msg.CreateDate > backintime
                                select msg).ToList();

            return edi_messages;
        }
    }
}
