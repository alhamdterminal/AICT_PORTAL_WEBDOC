using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class ElectronicDeliveryOrderRepository : RepoBase<ElectronicDeliveryOrder>, IElectronicDeliveryOrderRepository
    {
        public ElectronicDeliveryOrderRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public IEnumerable<ElectronicDeliveryOrderViewModel> GetElectronicDODetail(string virno, string blno , string indexno , long shippingagnetId)
        { 
            var BillDate = DateTime.Now;

            var listItems = new List<ElectronicDeliveryOrderViewModel>();


            if(virno == null && blno == null  && indexno == null)
            {
                return listItems;
            }
            var edores = (from igmo in Db.ElectronicDeliveryOrders
                                  
                                 where
                                (igmo.IGM_No == virno || virno == null || virno == "")
                                  &&
                                (igmo.BL_No == blno || blno == null || blno == "")
                                  &&
                                (igmo.Index_No == indexno || indexno == null || indexno == "")
                                 

                                 select igmo).LastOrDefault();



            //foreach (var item in containerList)
            //{

            //  var edores = Db.ElectronicDeliveryOrders.FromSql($"SELECT * From ElectronicDeliveryOrder Where IGM_No = {virno} and BL_No = {blno} and Index_No  = {indexno}   and IsDeleted = 0   ").LastOrDefault();


            if (edores != null)
                {
                    var resdata = new ElectronicDeliveryOrderViewModel
                    {
                        
                        ElectronicDeliveryOrderId = edores.ElectronicDeliveryOrderId,
                        Agent_Name = edores.Agent_Name,
                        BL_No = edores.BL_No,
                        //Container_No = item.ContainerNo,
                        //CONT_Size = isocoderes != null ? isocoderes.ContainerSize : "",
                        Doc_Tran_No = edores.Doc_Tran_No,
                        Do_Date = edores.Do_Date,
                        Goods_Desc = edores.Goods_Desc,
                        Gross_Weight = edores.Gross_Weight  ,
                        IGM_No = edores.IGM_No,
                        Index_No = edores.Index_No,
                        Marks_No = edores.Marks_No,
                        NetWeight = edores.NetWeight,
                        No_Of_Pkgs = edores.No_Of_Pkgs,
                        PKg_Description = edores.PKg_Description,
                        Port_of_Arrival = edores.Port_of_Arrival,
                        Ref_No = edores.Ref_No,
                        Valid_Date = edores.Valid_Date,
                        Vessel_Name = edores.Vessel_Name,
                        Voyage = edores.Voyage,
                        IsEdo = false,
                    };

                    listItems.Add(resdata);

                }
              


            //}



            return listItems;



        }


    }
}
