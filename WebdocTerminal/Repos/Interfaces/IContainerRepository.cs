using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IContainerRepository
    {
        WebdocTerminal.Models.ContainerIndex GetContainerByVIR(string VIR);
        List<CYGateOutViewModel> GetGateoutCYContainers();

        List<CFSGateOutViewModel> GetGateoutCFS();

        List<CYContainer> GetCyAuctionContainers();

        List<ContainerIndex> GetCFSAuctionIndexes();

        List<CFSGateOutViewModel> GetGateoutCFSContainers(string VIRNumber, int? IndexNo);

        List<CFSGateOutViewModel> GetGateoutCFSIndex();

        List<GateInViewModel> GetGateinCYContainers();

        List<GateInViewModel> GetGateinCFSContainers();

        List<GTTOViewModel> GetTPContainers();

        List<CYGroundingViewModel> GetUngroundedCYContainers();

        List<CFSGroundingViewModel> GetUngroundedCFSContainers();

        List<CFSGroundingViewModel> GetUngroundedCFSContainersByGoodHeadId(long goodheadId);
        List<CFSGroundingViewModel> GetUngroundedCFSContainersVechiles();

        List<DestuffingViewModel> GetDestuffingCFSContainers(long ContainerId, string Orientation);

        DeliveryOrderViewModel GetDeliveryOrderByDONumber(long doNumber);

        DeliveryOrderViewModel GetDeliveryOrderByDONumberForCyConatiner(long doNumber);

        IEnumerable<CargoInformationViewModel> GetCargoInformationCFSConatiner(long containerIndexId);

        IEnumerable<CargoInformationViewModel> GetCargoInformationCYConatiner(long CYContainerId);

        IEnumerable<CargoInformationViewModel> Section79ContainersCY();

        IEnumerable<CargoInformationViewModel> Section79CycontainersCFS();

        IEnumerable<ContainerIndexViewModel> GetContainerIndexBYVoyageId(long voyageid);

        string GetDocumentCodeCFS(long ContainerIndexId);

        string GetDocumentCodeCY(long CyContainerId);

        void UpdateAuctionContainersCY();
        List<ContainerIndex> GetIndexes(string VIR, int IndexNo, string BlNumber);

        DeliveryOrderViewModel GetDeliveryOrderByContainerId(long containerId);

        List<CCMO> GetccmoContainers();

        IEnumerable<CargoInformationViewModel> GetCargodetailCFSConatiner(string virno, string containerno, string indexno, string desc);

        IEnumerable<CargoInformationViewModel> GetCargodetailCYConatiner(string virno, string containerno, string indexno, string desc);

        List<ContainerIndexDetail> GetIGMDetailIndexWise(string igm, string IndexNo);

        List<ContainerIndexDetail> GetIGMDetailContainerWise(string igm, string ContainerNo);

        List<ContainerIndexDetail> GetIGMDetailCYContainerIndexWise(string igm, string IndexNo);

        List<CargoInformationViewModel> GetCargodetailCFSOrCYConatiner(string virno, string containerno);

        List<ContainerIndex> GetCargodetailCFSUnDeliveredCargo(string type, DateTime frmodate, DateTime todate);

        List<CYContainer> GetCargodetailCYUnDeliveredCargo( DateTime frmodate, DateTime todate);

        List<CargoInformationViewModel> GetIGMCFSOrCYConatiner(string voyageNo, string IGM);

        List<TransporterViewModel> GetTransportAssigningContainers(string virno, string blnumber);

        List<CYContainerViewModel> GetCYContainerListBYIGM(string igm, long indexNo);

        ContainerInfoViewModel GetContainerDeatilInfoCFS(string igm, long indexnumber);
        ContainerInfoViewModel GetContainerDeatilInfoCY(string igm, long indexnumber);
        List<UnLockGeneratBillViewModel> GetUnlockGenerateContainers();
        List<GDCR> GetUnCrossStuffingDetailBL();
        List<GDCR> GetUnCrossStuffingDetail(string blno);
        IEnumerable<CFSAndCYCargoInformationViewModel> GetCargoInformationImport(string igm, string container, string index, string blnumberlist);
        List<CargoInformationViewModel> GetInquiryFromData(string IGM, long index);
        ExaminationRequestViewModel GetExaminationRequestCFS(long? containerId);
        ExaminationRequestViewModel GetExaminationRequestCY(string igm, int? indexno);

        List<CYGroundingViewModel> GetUngroundedCYContainersByGoodHeadId(long goodheadid);

        bool GetDeliveredStatus(string virno, long indexno);

        List<TruckInOut> GetTruckInOuts(string igm, long index , string type);

        List<IGMO> GetContainerList(string igm, long index);

        List<TruckInOut> GetDOIfno(long dono);

        ImportFielsViewModel Getigmindexdetail(long dono);

        DeliveryOrder GetDODetail(string igm, long index);

        List<EmptyReceivingCYContainerViewModel> GetEmptyReveivingCrossStuffing();

        List<PGO> GetPreGateOutCrossStuffing();

        UnLockGeneratBillViewModel GetFFAndGoodsInfo(string virno, long indexno, string type);

        List<DOItem> GetDOItems(string virno, int indexno);

        ImportContainerLocation GetcontainerLocation(string virno, string containerno);
         
        string GetCFSCargoStatus(long containerId);
        string GetStatusCYCargo(string virno, long indexno);

        string GetStatusCYCargoByCycontainerId(long cycontainerid);

        string GetWaiverIfnoByWaiverno(string waiverno);

        bool CheckDeliveryStatus(string virno, long index);

        ContainerIndex GetLastContainerIndexByIGMAndContainerNo(string Virno, string Containerno);

        CYContainer GetLastContainerCYByIGMAndContainerNo(string Virno, string containerno);

        List<CFSGroundingViewModel> GetAutoUngroundedCFSContainers();

        List<CYGroundingViewModel> GetAutoUngroundedCYContainers();
        List<CargoInformationViewModel> GetInquiryFromDatabyblnumber(string blnumber);

        List<GIIO> GetReProcessGateIn();
        bool GetFileNameGIIO(string FileName);

        bool CheckDeliveryStatusForPart(string virno, long index, string containerno);

        List<DOItem> Checkcontainerwisegp(string virno, string containerno);

        List<OnlineTrackingViewModel> GetOnlineTracking(string number, string type);

        CSEmptyContainerReceive GetCsContainerbycontainerno(long CSEmptyContainerReceiveId);

        bool GetInvoiceInfo(string igm, long index, string type);

        Consigne GetConsigneById(long? id);
        ShippingAgent GetShippingAgentId(long? id);
        GoodsHead GetGoodsheadId(long? id);
        ShippingLine GetShippingLineId(long? id);
        ExaminationRequest GetExaminationRequestContainerIndexId(long id);
        ExaminationRequest GetExaminationRequestCYContainerId(long id);
        ClearingAgent GetClearingAgentId(long? id);
        Voyage GetVoyageId(long? id);
        Vessel GetVesselByVoyageId(long? id);

    }

}
