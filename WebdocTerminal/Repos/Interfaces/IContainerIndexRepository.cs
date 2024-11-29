using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IContainerIndexRepository : IRepo<ContainerIndex>
    {   
        ContainerIndex GetContainerIndexWithShippingAgent(long ContainerIndexId);
        ContainerIndex GetContainerIndexCFS(string ContainerNumber, string BlNumber, int IndexNo, string VirNumber);
        List<ContainerIndex> GetContainerIndexInfo(string blNumber, int indexNumber, string igm);
        List<ContainerIndex> GetContainerIndexBYigmIndex(int indexNumber, string igm);

        List<ContainerIndex> GetNotGeneratedDOIndexes(string containerNo);

        void ExecuteNonSQL(string sql, params object[] param);
        ContainerIndex GetContainerIndexById(long containerinexid);

        List<ContainerIndex> GetContainerIndexByIGMAndContainerNo(string Virno, string Containerno);

        List<ContainerIndex> GetContainerIndexByIGMAndBLNo(string Virno, string blno);

        ContainerIndex GetContainerIndexByIGMIndexAndContainer(string Virno, string containerno, long indexno);

        BillToLine GetBillToLineInfo(string Virno, long indexno, string IndexType);

        List<Waiver> GetWaiverInfo(long ContainerIndexId);

        List<ContainerIndex> GetIndexInfo(string Virno, long indexno);

        ContainerIndex GetSingleIndexInfo(string Virno, long indexno);


        ContainerIndex GetLastContainerIndexByIGMAndContainerNo(string Virno, string Containerno);

        List<ContainerIndex> GetIndexesByVirno(string Virno);

        ContainerIndex GetLastContainerIndexById(long containerinexid);


        List<ContainerIndex> GetontainerIndexByIGMandContainerNumber(string VIRNumber, string ContainerNumber);

        List<ContainerIndex> GetIndexInfoUndelivered(string Virno, long indexno);

        ContainerIndex GetHoldContainerIndexById(long containerinexid);

        ContainerIndex GetAuctionContainerIndexById(long containerinexid);

        List<ContainerIndex> GetCFSIGMsByContainerNumber(string containernumber);

        bool GetEDOInfo(long containerindexid);

        DestuffedContainer GetDestuffedIndexDetail(long containerinexid);

        List<GatePass> GetGatePassInfo(string Virno, string containerno, long indexno);

        List<DestuffedContainer> GetDestuffedList(string containerinexids);

        List<ContainerIndex> GetContainerIndexByIGMContainerNo(string Virno, string Containerno);

        List<CYContainer> GetCYContainerByIGMContainerNo(string Virno, string Containerno);

        List<ContainerIndex> GetContainerIndexByIgmAndIndex(string Virno, long indexno);

        List<IGMO> GetVirNoListBycontainerNumber(string containernumber);

        AICTAndLineIndexTariff GetLastAICTAndLineIndexTariff(string virno, long indexno);

        ShortExcessWeigth GetShortExcessWeigth(string virno, long indexno, string containertype);
        ContainerIndex GetContainerindexByIgmIndexContainer(string virno, long indexno, string containerno);

        List<ContainerIndex> GetContainerIndexByIds(string containerindexid);

        IndexWeighmentViewModel GetIndexInfoForWeighment(string Virno, long indexno);
      }
}
