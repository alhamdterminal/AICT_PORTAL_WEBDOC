using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Data;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class PreAlertRepository : RepoBase<PreAlert>, IPreAlertRepository
    {

        private static IConfiguration _configuration;
        public string Con { get; set; }

        public PreAlertRepository(IUserResolveService userResolveService, IConfiguration configuration) : base(userResolveService)
        {
            _configuration = configuration;
            Con =   _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }

        public long GeneratePreAlterNumber()
        {

            var lastPreAlertNumber = "0001";

            var lastGeneratedPreAlertNumber = GetAll().OrderByDescending(l => l.PreAlertId).FirstOrDefault();

            if (lastGeneratedPreAlertNumber != null)
            {
                lastPreAlertNumber = Regex.Match(lastGeneratedPreAlertNumber.PreAlertNumber, @"\d+").Value;
                return Int32.Parse(lastPreAlertNumber) + 1;

            }
            else
            {
                lastPreAlertNumber = "0001";
                return Int32.Parse(lastPreAlertNumber);

            }

        }


        public List<PreAlert> GetPreAlertNumber()
        {
            var data = (from prealert in Db.PreAlerts
                        where !Db.PaymentUpdates.Any(x => x.PreAlertNumber == prealert.PreAlertNumber)
                        select new PreAlert
                        {
                            PreAlertNumber = prealert.PreAlertNumber,
                            PreAlertId = prealert.PreAlertId
                        }).ToList();
            return data;

        }


        public void GetAlert(string alterno)
        {
            var res = Db.PreAlerts.FromSql($"SELECT * From PreAlert Where PreAlertNumber   = {alterno}   and IsDeleted = 0 ").LastOrDefault();

            if (res != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    res.AlertStatus = true;
                    db.PreAlerts.Update(res);
                    db.SaveChanges();
                }
            }


        }


        public void updateAlertStaus(string alterno)
        {
            var res = Db.PreAlerts.FromSql($"SELECT * From PreAlert Where PreAlertNumber   = {alterno}   and IsDeleted = 0 ").LastOrDefault();

            if (res != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    res.AlertStatus = false;
                    db.PreAlerts.Update(res);
                    db.SaveChanges();
                }
            }


        }

        public bool GetPayOrderStatusByPrealertid(long prealertid)
        {
            var res = (from peralter in Db.PreAlerts
                       join preAlterDetail in Db.PreAlterDetails on peralter.PreAlertId equals preAlterDetail.PreAlertId
                       join paymentUpdateDetail in Db.PaymentUpdateDetails on preAlterDetail.PreAlterDetailId equals paymentUpdateDetail.PreAlterDetailId
                       join paymentUpdate in Db.PaymentUpdates on paymentUpdateDetail.PaymentUpdateId equals paymentUpdate.PaymentUpdateId
                       join preAlertPayOrder in Db.PreAlertPayOrders on paymentUpdate.RequestNumber equals preAlertPayOrder.RequestNumber
                       where
                       peralter.PreAlertId == prealertid
                       select peralter).ToList();

            if (res.Count() > 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }


        public bool GetPaymentUpdateStatusByPrealertid(long prealertid)
        {
            var res = (from peralter in Db.PreAlerts
                       join preAlterDetail in Db.PreAlterDetails on peralter.PreAlertId equals preAlterDetail.PreAlertId
                       join paymentUpdateDetail in Db.PaymentUpdateDetails on preAlterDetail.PreAlterDetailId equals paymentUpdateDetail.PreAlterDetailId
                       join paymentUpdate in Db.PaymentUpdates on paymentUpdateDetail.PaymentUpdateId equals paymentUpdate.PaymentUpdateId
                       where
                       peralter.PreAlertId == prealertid
                       select peralter).ToList();

            if (res.Count() > 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }


        public IEnumerable<PreAlertForWebocViewModel> GetUnBindPreAlert(long? shippingAgent, long? line, long? portAndTerminal, string vessel, string voyage, DateTime? from, DateTime? to)
        {



            var listItems = new List<PreAlertForWebocViewModel>();



            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string conString = Con;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("UnBindAlerts", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 360;

                    command.Parameters.AddWithValue("@shippingAgent", shippingAgent);
                    command.Parameters.AddWithValue("@line", line);
                    command.Parameters.AddWithValue("@portAndTerminal", portAndTerminal);
                    command.Parameters.AddWithValue("@vessel", vessel);
                    command.Parameters.AddWithValue("@voyage", voyage);
                    command.Parameters.AddWithValue("@fromdate", from?.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@todate", to?.ToString("MM/dd/yyyy"));


                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(command);

                    sda.Fill(ds);

                    listItems = ds.Tables[0].AsEnumerable()
                        .Select(datarow => new PreAlertForWebocViewModel()
                        {
                            PreAlterDetailId = datarow.Field<long>("PreAlterDetailId"),
                            ContainerNo = datarow.Field<string>("ContainerNumber"),
                            MasterBLNumber = datarow.Field<string>("MasterBLNumber"),
                            Size = datarow.Field<long>("Size"),
                            Type = datarow.Field<string>("Type"),
                            CargoType = datarow.Field<string>("CargoType"),
                            LineFFName = datarow.Field<string>("LineFFName"),
                            SOCCOC = datarow.Field<string>("SOCCOC"),
                            PortAndTerminalName = datarow.Field<string>("PortAndTerminalName"),
                            PreAlertDate = datarow.Field<DateTime>("PreAlertDate"),
                            VirNumber = datarow.Field<string>("VirNumber"),
                            IsCheck = false

                        }).ToList();

                    con.Close();

                }
            }
            return listItems;
        }


        public List<PreAlterDetail> GetpreAlterDetails(string PreAlterDetailId)
        {
            var res = Db.PreAlterDetails.FromSql($"select * from PreAlterDetail where  PreAlterDetailId  in (select data from [dbo].[Split]( {PreAlterDetailId}, ',') )   and IsDeleted = 0   ").ToList();

            return res;

        }
    }
}
