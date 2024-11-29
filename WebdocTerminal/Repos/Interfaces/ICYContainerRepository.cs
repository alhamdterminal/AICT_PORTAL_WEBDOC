using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface ICYContainerRepository : IRepo<CYContainer>
    {
        void ExecuteNonSQL(string sql, params object[] param);

        CYContainer GetContainerById(long containerid);

        List<CYContainer> GetContainerIndexByIGMAndContainerNo(string Virno, int indexno);

        List<CYContainer> GetContainerIndexByIGMAndContainerNo(string Virno, string containerno);

        List<CYContainer> GetCYContainerByIGMAndContainerNo(string Virno, string containerno);


        CYContainer GetContainerCYByIGMAndIndex(string Virno, int indexno);

        CYContainer GetLastContainerIndexByIGMAndContainerNo(string Virno, string containerno);

        CYContainer GetLastContainerByIGMIndexAndContainerNo(string Virno, int indexno, string containerno);

        List<CYContainer> GetIndexInfoUndelivered(string Virno, long indexno);

        CYContainer GetLastContainerById(long containerid);

        List<CYContainer> GetHoldContainersById(string Virno, long indexno);

        List<CYContainer> GetAuctionContainersById(string Virno, long indexno);


        List<CYContainer> GetCYIndexesByVirno(string Virno);

        List<CYContainer> GetCYIGMsByContainerNumber(string containernumber);

        List<CYContainer> GetCYContainersByIGMContainer(string virNo, string containerNo);

        List<CYContainer> GetCYContainersByBL(string blnumber);

        List<CYContainer> GetContainercydetilByIGMAndContainerNo(string Virno, string containerno);

        List<CYContainer> GetCSContainerIndexByIGMAndContainerNo(string Virno, string containerno);

        List<CYContainer> GetUndeliveredIndexbyigmindex(string Virno, long indexno);

        CYContainer GetLastCSContainerByIGMIndexAndContainerNo(string Virno, int indexno, string containerno);

        CYContainer GetFirstCYContainerByIGMIndex(string Virno, long indexno);
        BillToLine GetCYContainerBillToLineInfo(string Virno, long indexno);
    }
}
