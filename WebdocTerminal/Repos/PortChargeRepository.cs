using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class PortChargeRepository : RepoBase<PortCharge>, IPortChargeRepository
    {


        private static IConfiguration _configuration;
        public string Con { get; set; }
        public PortChargeRepository(IUserResolveService userResolveService, IConfiguration configuration) : base(userResolveService)
        {
            _configuration = configuration;
            Con =  _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

        }



        public IEnumerable<PortChargesViewModel> GetPortChargesDetail(string virno, string containerno, string indexno, string type)
        {

            var result = new List<PortChargesViewModel>();

            if (type == "CFS")
            {

                var containerList = (from containerIndex in Db.ContainerIndices
                                     from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                                     from portcharg in Db.PortCharges.Where(x => x.IndexNumber == containerIndex.IndexNo && x.VirNumber == containerIndex.VirNo && x.ContainerNumber == containerIndex.ContainerNo).DefaultIfEmpty()
                                     where
                                    (containerIndex.VirNo == virno || virno == null || virno == "")
                                      &&
                                    (containerIndex.ContainerNo == containerno || containerno == null || containerno == "")
                                      &&
                                    (containerIndex.IndexNo == Convert.ToInt64(indexno) || indexno == null || indexno == "")

                                     select new PortChargesViewModel
                                     {
                                         ContainerId = containerIndex.ContainerIndexId,
                                         Type = type,
                                         ContainerNumber = containerIndex.ContainerNo,
                                         IsDelivered = containerIndex.IsDelivered,
                                         shippingAgent = shippingAgent != null ? shippingAgent.Name : "",
                                         IndexNumber = containerIndex.IndexNo ?? 0,
                                         VirNumber = containerIndex.VirNo,
                                         DemurrageCharges = portcharg != null ? portcharg.DemurrageCharges : 0,
                                         WeighmentCharges = portcharg != null ? portcharg.WeighmentCharges : 0,
                                         OverWeightPenalty = portcharg != null ? portcharg.OverWeightPenalty : 0,
                                         DetentionChargesOrBulletSeal = portcharg != null ? portcharg.DetentionChargesOrBulletSeal : 0,
                                         ThcPhcKdlpCharges = portcharg != null ? portcharg.ThcPhcKdlpCharges : 0,
                                         LoloCharges = portcharg != null ? portcharg.LoloCharges : 0,
                                         QictCharges = portcharg != null ? portcharg.QictCharges : 0,
                                         OtherCharges = portcharg != null ? portcharg.OtherCharges : 0,
                                         WashAndCleanCharges = portcharg != null ? portcharg.WashAndCleanCharges : 0,
                                         ANF = portcharg != null ? portcharg.ANF : 0,
                                         Pallet = portcharg != null ? portcharg.Pallet : 0,
                                         Recover = portcharg != null ? portcharg.Recover : 0,
                                         TransportCharges = portcharg != null ? portcharg.TransportCharges : 0,
                                         TotalCharges = portcharg != null ? portcharg.TotalCharges : 0,
                                     }).ToList();

                result.AddRange(containerList);

            }


            if (type == "CY")
            {


                var containerList = (from cycontainer in Db.CYContainers
                                     from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == cycontainer.ShippingAgentId).DefaultIfEmpty()
                                     from portcharg in Db.PortCharges.Where(x => x.IndexNumber == cycontainer.IndexNo && x.VirNumber == cycontainer.VIRNo && x.ContainerNumber == cycontainer.ContainerNo).DefaultIfEmpty()
                                     where
                                    (cycontainer.VIRNo == virno || virno == null || virno == "")
                                      &&
                                    (cycontainer.ContainerNo == containerno || containerno == null || containerno == "")
                                      &&
                                    (cycontainer.IndexNo == Convert.ToInt64(indexno) || indexno == null || indexno == "")

                                     select new PortChargesViewModel
                                     {
                                         ContainerId = cycontainer.CYContainerId,
                                         Type = type,
                                         ContainerNumber = cycontainer.ContainerNo,
                                         IsDelivered = cycontainer.IsDelivered == true ? true : false,
                                         IndexNumber = cycontainer.IndexNo ?? 0,
                                         VirNumber = cycontainer.VIRNo,
                                         shippingAgent = shippingAgent != null ? shippingAgent.Name : "",
                                         DemurrageCharges = portcharg != null ? portcharg.DemurrageCharges : 0,
                                         WeighmentCharges = portcharg != null ? portcharg.WeighmentCharges : 0,
                                         OverWeightPenalty = portcharg != null ? portcharg.OverWeightPenalty : 0,
                                         DetentionChargesOrBulletSeal = portcharg != null ? portcharg.DetentionChargesOrBulletSeal : 0,
                                         ThcPhcKdlpCharges = portcharg != null ? portcharg.ThcPhcKdlpCharges : 0,
                                         LoloCharges = portcharg != null ? portcharg.LoloCharges : 0,
                                         QictCharges = portcharg != null ? portcharg.QictCharges : 0,
                                         OtherCharges = portcharg != null ? portcharg.OtherCharges : 0,
                                         WashAndCleanCharges = portcharg != null ? portcharg.WashAndCleanCharges : 0,
                                         ANF = portcharg != null ? portcharg.ANF : 0,
                                         Pallet = portcharg != null ? portcharg.Pallet : 0,
                                         Recover = portcharg != null ? portcharg.Recover : 0,
                                         TransportCharges = portcharg != null ? portcharg.TransportCharges : 0,
                                         TotalCharges = portcharg != null ? portcharg.TotalCharges : 0,
                                     }).ToList();

                result.AddRange(containerList);


            }


            return result;
        }
        public IEnumerable<PortChargesViewModel> GetPortChargesDetailContainerWise(string virno, string containerno, string indexno, string type)
        {

            var result = new List<PortChargesViewModel>();

            if (type == "CFS")
            {

                var containers = (from containerIndex in Db.ContainerIndices
                                  where
                                     (containerIndex.VirNo == virno || virno == null || virno == "")
                                       &&
                                     (containerIndex.ContainerNo == containerno || containerno == null || containerno == "")
                                  select containerIndex).ToList();


                if (containers.Count() > 0)
                {
                    var containerMerged = (from container in containers
                                           group container by container.ContainerNo into g
                                           select new GateInViewModel
                                           {
                                               ContainerNo = g.Key,
                                               Key = g.FirstOrDefault().ContainerNo + "-" + g.FirstOrDefault().VirNo,
                                               VIRNo = g.FirstOrDefault().VirNo,
                                               ShippingAgentId = g.FirstOrDefault().ShippingAgentId,
                                           }).ToList();


                    var containerList = (from containerIndex in containerMerged
                                         from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                                         where
                                        (containerIndex.VIRNo == virno || virno == null || virno == "")
                                          &&
                                        (containerIndex.ContainerNo == containerno || containerno == null || containerno == "")

                                         select new PortChargesViewModel
                                         {
                                             Key = containerIndex.VIRNo + "-" + containerIndex.ContainerNo,
                                             Type = type,
                                             ContainerNumber = containerIndex.ContainerNo,
                                             shippingAgent = shippingAgent != null ? shippingAgent.Name : "",
                                             VirNumber = containerIndex.VIRNo,
                                             DemurrageCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.DemurrageCharges),
                                             WeighmentCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.WeighmentCharges),
                                             OverWeightPenalty = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.OverWeightPenalty),
                                             DetentionChargesOrBulletSeal = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.DetentionChargesOrBulletSeal),
                                             ThcPhcKdlpCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.ThcPhcKdlpCharges),
                                             LoloCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.LoloCharges),
                                             QictCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.QictCharges),
                                             OtherCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.OtherCharges),
                                             WashAndCleanCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.WashAndCleanCharges),
                                             ANF = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.ANF),
                                             Pallet = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.Pallet),
                                             Recover = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.Recover),
                                             TransportCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.TransportCharges),
                                             TotalCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.TotalCharges),
                                         }).ToList();

                    result.AddRange(containerList);
                }





            }


            if (type == "CY")
            {

                var containers = (from containerIndex in Db.CYContainers
                                  where
                                     (containerIndex.VIRNo == virno || virno == null || virno == "")
                                       &&
                                     (containerIndex.IndexNo == Convert.ToInt64(indexno) || indexno == null || indexno == "")
                                  select containerIndex).ToList();




                if (containers.Count() > 0)
                {
                    var containerMerged = (from container in containers
                                           group container by container.ContainerNo into g
                                           select new GateInViewModel
                                           {
                                               ContainerNo = g.Key,
                                               Key = g.FirstOrDefault().ContainerNo + "-" + g.FirstOrDefault().VIRNo,
                                               VIRNo = g.FirstOrDefault().VIRNo,
                                               ShippingAgentId = g.FirstOrDefault().ShippingAgentId,
                                           }).ToList();


                    var containerList = (from containerIndex in containerMerged
                                         from shippingAgent in Db.ShippingAgents.Where(x => x.ShippingAgentId == containerIndex.ShippingAgentId).DefaultIfEmpty()
                                         where
                                        (containerIndex.VIRNo == virno || virno == null || virno == "")
                                          &&
                                        (containerIndex.ContainerNo == containerno || containerno == null || containerno == "")

                                         select new PortChargesViewModel
                                         {
                                             Key = containerIndex.VIRNo + "-" + containerIndex.ContainerNo,
                                             Type = type,
                                             ContainerNumber = containerIndex.ContainerNo,
                                             shippingAgent = shippingAgent != null ? shippingAgent.Name : "",
                                             VirNumber = containerIndex.VIRNo,
                                             DemurrageCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.DemurrageCharges),
                                             WeighmentCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.WeighmentCharges),
                                             OverWeightPenalty = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.OverWeightPenalty),
                                             DetentionChargesOrBulletSeal = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.DetentionChargesOrBulletSeal),
                                             ThcPhcKdlpCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.ThcPhcKdlpCharges),
                                             LoloCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.LoloCharges),
                                             QictCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.QictCharges),
                                             OtherCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.OtherCharges),
                                             WashAndCleanCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.WashAndCleanCharges),
                                             ANF = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.ANF),
                                             Pallet = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.Pallet),
                                             Recover = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.Recover),
                                             TransportCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.TransportCharges),
                                             TotalCharges = Db.PortCharges.Where(s => s.VirNumber == containerIndex.VIRNo && s.ContainerNumber == containerIndex.ContainerNo).Sum(s => s.TotalCharges),
                                         }).ToList();

                    result.AddRange(containerList);
                }


            }


            return result;
        }

        public List<ContainerIndex> GetPortchargesForVechile()
        {

            var resdata = (from containerIndex in Db.ContainerIndices
                           where
                           containerIndex.CargoType == "LCL"
                           && containerIndex.GoodsHeadId == 123
                           && containerIndex.IsDelivered == false
                           && containerIndex.ShippingAgentId != 100999
                           && containerIndex.ShippingAgentId != 101000
                           && containerIndex.ShippingAgentId != 101061
                           && containerIndex.ShippingAgentId != null
                           && !Db.PortCharges.Any(s => s.VirNumber == containerIndex.VirNo && s.IndexNumber == containerIndex.IndexNo)
                           select containerIndex).ToList();

            return resdata;
        }

        public PortCharge GetPortChargesByIgmIndexContainer(string virno, long indexno, string containerno)
        {
            var res = Db.PortCharges.FromSql($"SELECT * From PortCharge Where VirNumber = {virno} and IndexNumber = {indexno}  and ContainerNumber = {containerno}  and IsDeleted = 0   ").LastOrDefault();

            return res;
        }

        public IEnumerable<PortChargesRatesViewModel> GetPortChargesRates(string virno, string containerno, string indexno, string type, long? agent, long? goodhead, DateTime? fromdate, DateTime? todate, string status)
        {

            var listItems = new List<PortChargesRatesViewModel>();


            if (type != "CY")
            {

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string conString = Con;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("GetCFSPortChargesRates", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 360;

                        command.Parameters.AddWithValue("@virnumber", virno);
                        command.Parameters.AddWithValue("@indexnumber", indexno);
                        command.Parameters.AddWithValue("@containernumber", containerno);
                        command.Parameters.AddWithValue("@shippingAgentId", agent);
                        command.Parameters.AddWithValue("@goodheadId", goodhead);
                        command.Parameters.AddWithValue("@status", status);
                        command.Parameters.AddWithValue("@fromdate", fromdate?.ToString("MM/dd/yyyy"));
                        command.Parameters.AddWithValue("@todate", todate?.ToString("MM/dd/yyyy"));
                        command.Parameters.AddWithValue("@type", type);

                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(command);

                        sda.Fill(ds);

                        listItems = ds.Tables[0].AsEnumerable()
                            .Select(datarow => new PortChargesRatesViewModel()
                            {
                                ContainerId = datarow.Field<long>("IndexId"),
                                Key = datarow.Field<string>("key"),
                                VirNumber = datarow.Field<string>("VirNo"),
                                ContainerNumber = datarow.Field<string>("ContainerNo"),
                                IndexNumber = datarow.Field<int>("IndexNo"),
                                ArrivalDate = datarow.Field<DateTime?>("ArrivalDate"),
                                Size = datarow.Field<int>("Size"),
                                ShippingAgent = datarow.Field<string>("ShippingAgentName"),
                                GoodsHeadName = datarow.Field<string>("GoodsHeadName"),
                                IsDelivered = datarow.Field<int>("IsDelivered") == 1 ? true : false,
                                DeliveryDate = datarow.Field<DateTime?>("DeliveryDate"),
                                Type = "CFS",
                                DemurrageCharges = datarow.Field<double>("DemurrageCharges"),
                                WeighmentCharges = datarow.Field<double>("WeighmentCharges"),
                                OverWeightPenalty = datarow.Field<double>("OverWeightPenalty"),
                                DetentionChargesOrBulletSeal = datarow.Field<double>("DetentionChargesOrBulletSeal"),
                                ThcPhcKdlpCharges = datarow.Field<double>("ThcPhcKdlpCharges"),
                                LoloCharges = datarow.Field<double>("LoloCharges"),
                                QictCharges = datarow.Field<double>("QictCharges"),
                                OtherCharges = datarow.Field<double>("OtherCharges"),
                                WashAndCleanCharges = datarow.Field<double>("WashAndCleanCharges"),
                                ANF = datarow.Field<double>("ANF"),
                                Pallet = datarow.Field<double>("Pallet"),
                                Recover = datarow.Field<double>("Recover"),
                                TransportCharges = datarow.Field<double>("TransportCharges"),
                                TotalCharges = datarow.Field<double>("TotalCharges"),

                            }).ToList();

                        con.Close();

                    }
                }
            }
            else
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string conString = Con;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("GetCYPortChargesRates", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 360;

                        command.Parameters.AddWithValue("@virnumber", virno);
                        command.Parameters.AddWithValue("@indexnumber", indexno);
                        command.Parameters.AddWithValue("@containernumber", containerno);
                        command.Parameters.AddWithValue("@shippingAgentId", agent);
                        command.Parameters.AddWithValue("@goodheadId", goodhead);
                        command.Parameters.AddWithValue("@status", status);
                        command.Parameters.AddWithValue("@fromdate", fromdate?.ToString("MM/dd/yyyy"));
                        command.Parameters.AddWithValue("@todate", todate?.ToString("MM/dd/yyyy"));

                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(command);

                        sda.Fill(ds);

                        listItems = ds.Tables[0].AsEnumerable()
                            .Select(datarow => new PortChargesRatesViewModel()
                            {
                                ContainerId = datarow.Field<long>("IndexId"),
                                Key = datarow.Field<string>("key"),
                                VirNumber = datarow.Field<string>("VirNo"),
                                ContainerNumber = datarow.Field<string>("ContainerNo"),
                                IndexNumber = datarow.Field<int>("IndexNo"),
                                ArrivalDate = datarow.Field<DateTime?>("ArrivalDate"),
                                Size = datarow.Field<int>("Size"),
                                ShippingAgent = datarow.Field<string>("ShippingAgentName"),
                                GoodsHeadName = datarow.Field<string>("GoodsHeadName"),
                                IsDelivered = datarow.Field<int>("IsDelivered") == 1 ? true : false,
                                DeliveryDate = datarow.Field<DateTime?>("DeliveryDate"),
                                Type = "CY",
                                DemurrageCharges = datarow.Field<double>("DemurrageCharges"),
                                WeighmentCharges = datarow.Field<double>("WeighmentCharges"),
                                OverWeightPenalty = datarow.Field<double>("OverWeightPenalty"),
                                DetentionChargesOrBulletSeal = datarow.Field<double>("DetentionChargesOrBulletSeal"),
                                ThcPhcKdlpCharges = datarow.Field<double>("ThcPhcKdlpCharges"),
                                LoloCharges = datarow.Field<double>("LoloCharges"),
                                QictCharges = datarow.Field<double>("QictCharges"),
                                OtherCharges = datarow.Field<double>("OtherCharges"),
                                WashAndCleanCharges = datarow.Field<double>("WashAndCleanCharges"),
                                ANF = datarow.Field<double>("ANF"),
                                Pallet = datarow.Field<double>("Pallet"),
                                Recover = datarow.Field<double>("Recover"),
                                TransportCharges = datarow.Field<double>("TransportCharges"),
                                TotalCharges = datarow.Field<double>("TotalCharges"),

                            }).ToList();

                        con.Close();

                    }
                }
            }

            return listItems;
        }


        public List<ContainerIndex> GetAutoUpdatePortcharges()
        {

            var resdata = (from containerIndex in Db.ContainerIndices
                           where
                           containerIndex.CargoType == "LCL"
                           && containerIndex.GoodsHeadId == 123
                           && containerIndex.IsDelivered == false
                           && containerIndex.ShippingAgentId != 100999
                           && containerIndex.ShippingAgentId != 101000
                           && containerIndex.ShippingAgentId != 101061
                           && containerIndex.ShippingAgentId != null
                           && containerIndex.IsPortChargesAssign == false
                           select containerIndex).ToList();

            return resdata;
        }
    }
}
