using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class ElectronicDeliveryOrderController : ParentController
    { 
        public IElectronicDeliveryOrderRepository  _electronicDeliveryOrderRepository;
         private UserManager<IdentityUser> _userManager;
        private IUsersRepository _userRepo;
        private IShippingAgentRepository _agentRepo;

        public ElectronicDeliveryOrderController(IElectronicDeliveryOrderRepository electronicDeliveryOrderRepository,
            UserManager<IdentityUser> userManager,
            IUsersRepository userRepo,
            IShippingAgentRepository agentRepo)
        {
            _electronicDeliveryOrderRepository = electronicDeliveryOrderRepository;
            _userManager = userManager;
            _userRepo = userRepo;
            _agentRepo = agentRepo;

        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ElectronicDeliveryOrderView()
        {
   
            return View();
        }



        public IActionResult UploadEDOView()
        {

            return View();
        }


        internal string CreateQuery
        {
            get
            {
                return @"INSERT INTO DSA_EDO
                               (IGM_NO
                               ,INDEX_NO
                               ,BL_NO
                               ,CONTAINER_NO
                               ,CONT_SIZE
                               ,AGENT_NAME
                               ,VESSEL_NAME
                               ,VOYAGE
                               ,DO_DATE
                               ,PORT_OF_ARRIVAL
                               ,NET_WEIGHT
                               ,NO_OF_PKGS
                               ,MARKS_NO
                               ,DOC_TRAN_NO
                               ,VALID_DATE
                               ,REF_NO
                               ,GROSS_WEIGHT
                               ,PKG_DESC
                               ,GOODS_DESC )
                                VALUES
                               ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')";
            }
        }


        internal string updateQuery
        {
            get
            {
                return @"INSERT INTO DSA_EDO
                               (IGM_NO
                               ,INDEX_NO
                               ,BL_NO
                               ,CONTAINER_NO
                               ,CONT_SIZE
                               ,AGENT_NAME
                               ,VESSEL_NAME
                               ,VOYAGE
                               ,DO_DATE
                               ,PORT_OF_ARRIVAL
                               ,NET_WEIGHT
                               ,NO_OF_PKGS
                               ,MARKS_NO
                               ,DOC_TRAN_NO
                               ,VALID_DATE
                               ,REF_NO
                               ,GROSS_WEIGHT
                               ,PKG_DESC
                               ,GOODS_DESC )
                                VALUES
                               ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')";
            }
        }

        public IActionResult SaveEDO( ElectronicDeliveryOrder model)
        {
            try
            {

              

                ////string conString = "User Id=aictops;Password=aictaictops;Data Source=192.168.1.19:1521/ORC1;";
                //string conStringedo = "User Id=AICTWEB;Password=aictweb;Data Source=192.168.1.19:1521/orc1;";
                ////  OdbcConnection conn = new OdbcConnection(connString);
                //using (OracleConnection con = new OracleConnection(conStringedo))
                //{
                //    using (OracleCommand cmd = con.CreateCommand())
                //    {
                //        con.Open();
                //        cmd.BindByName = true;

                //        try
                //        {
                //            //foreach (var item in model)
                //            //{
                                  
                //                var dodate =   model.Do_Date?.ToString("dd-MMM-yy");
                //                var validdate = model.Valid_Date?.ToString("dd-MMM-yy");


                //                var insertquery = string.Format(CreateQuery, model.IGM_No, model.Index_No, model.BL_No, model.Container_No, model.CONT_Size, model.Agent_Name, model.Vessel_Name, model.Voyage, dodate, model.Port_of_Arrival, model.NetWeight
                //                    , model.No_Of_Pkgs, model.Marks_No , model.Doc_Tran_No , validdate , model.Ref_No, model.Gross_Weight , model.PKg_Description, model.Goods_Desc);
                //                cmd.CommandText = insertquery;
                                 
                //                cmd.ExecuteNonQuery();
                //            //}


                //        }
                //        catch (Exception ex)
                //        {
                //            return new JsonResult(new { error = true, message = ex.InnerException.InnerException.Message });
                //        }
                //    }
                //}


                _electronicDeliveryOrderRepository.Update(model);

                return new JsonResult(new { error = false, message = "Save" });

            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }


        public IActionResult SaveEDOFromExcel( ElectronicDeliveryOrder model)
        {
            try
            {

                var res = _electronicDeliveryOrderRepository.GetAll().Where(x => x.BL_No == model.BL_No && x.Index_No == model.Index_No).LastOrDefault();

                if(res != null)
                {
                    return new JsonResult(new { error = true, message = "already updated against bl " + model.BL_No + " and index "+ model.Index_No });
                }
                else
                {
                    //string conStringedo = "User Id=AICTWEB;Password=aictweb;Data Source=192.168.1.19:1521/orc1;";

                    //using (OracleConnection con = new OracleConnection(conStringedo))
                    //{
                    //    using (OracleCommand cmd = con.CreateCommand())
                    //    {
                    //        con.Open();
                    //        cmd.BindByName = true;

                    //        try
                    //        {
                                 

                    //                var dodate = model.Do_Date?.ToString("dd-MMM-yy");
                    //                var validdate = model.Valid_Date?.ToString("dd-MMM-yy");


                    //                var insertquery = string.Format(CreateQuery, model.IGM_No, model.Index_No, model.BL_No, model.Container_No, model.CONT_Size, model.Agent_Name, model.Vessel_Name, model.Voyage, dodate, model.Port_of_Arrival, model.NetWeight
                    //                    , model.No_Of_Pkgs, model.Marks_No, model.Doc_Tran_No, validdate, model.Ref_No, model.Gross_Weight, model.PKg_Description, model.Goods_Desc);
                    //                cmd.CommandText = insertquery;

                    //                cmd.ExecuteNonQuery();
                                


                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            return new JsonResult(new { error = true, message = ex.InnerException.InnerException.Message });
                    //        }
                    //    }
                    //}


                    _electronicDeliveryOrderRepository.Add(model);

                    return new JsonResult(new { error = false, message = "Save" });
                }
                //string conString = "User Id=aictops;Password=aictaictops;Data Source=192.168.1.19:1521/ORC1;";
                
                //  OdbcConnection conn = new OdbcConnection(connString);
                

            }
            catch (Exception e)
            {

                return new JsonResult(new { error = true, message = e.InnerException.InnerException.Message });
            }
        }



    }
}
