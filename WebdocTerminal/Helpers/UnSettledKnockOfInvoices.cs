using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebdocTerminal.Data;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Helpers
{
    public class UnSettledKnockOfInvoices
    {

        private ApplicationDbContext _appicationDbContext;
        private IEmailSender _emailSender;
        private ICreditAllowedRepository _creditAllowedRepository;
        private IConfiguration Configuration;
        private IPortChargeRepository _portChargeRepository;
        private IContainerIndexRepository _containerIndexRepository;
        private IInvoiceRepository _invoiceRepository;


        public UnSettledKnockOfInvoices(ApplicationDbContext appicationDbContext,
                                        IEmailSender emailSender,
                                        ICreditAllowedRepository creditAllowedRepository,
                                        IConfiguration _configuration,
                                        IPortChargeRepository portChargeRepository,
                                        IContainerIndexRepository containerIndexRepository,
                                        IInvoiceRepository invoiceRepository)
        {
            _appicationDbContext = appicationDbContext;
            _emailSender = emailSender;
            _creditAllowedRepository = creditAllowedRepository;
            Configuration = _configuration;
            _portChargeRepository = portChargeRepository;
            _containerIndexRepository = containerIndexRepository;
            _invoiceRepository = invoiceRepository;
        }


        public void GetUnSettledExcessAmountInvoices()
        {
            var users = _appicationDbContext.UsersEmails.FromSql($"select * from UsersEmail where Department = 'FINANCE' and IsDeleted = 0").ToList();

            if (users.Count() > 0)
            {
                var res = _appicationDbContext.Invoices.FromSql($"select   * from invoice where  ExcessAmount > 0 and IsDeleted = 0").ToList();

                if (res.Count() > 0)
                {
                    foreach (var item in res)
                    {
                        DateTime currentDate = DateTime.Now;

                        DateTime InvoiceDate = item.InvoiceDate ?? DateTime.Now;

                        TimeSpan tt = currentDate - InvoiceDate;

                        int NrOftDays = Convert.ToInt32(tt.TotalDays);

                        if (NrOftDays >= 15)
                        {
                            try
                            {
                                var subject = "Excess Amount !";
                                var body = "please Settled Excess Amount of Invoice No : " + item.InvoiceNo + " And  the Excess Amount is " + item.ExcessAmount;
                                _emailSender.UsersSendBulkEmailAsync(users, subject, body);
                            }
                            catch (Exception e)
                            {

                            }


                        }

                    }
                }


            }




        }


        public void UpdateUnSettelCreditAllow()
        {
            var resdata = _appicationDbContext.CreditAlloweds.FromSql($"select * from creditallowed where cast( CreatedDate as date) < cast( Getdate() as date) and IsSettled = 0   and IsDeleted = 0  and IsCancel = 0  and InvoiceNo  is null").ToList();

            if (resdata.Count() > 0)
            {

                resdata.ToList().ForEach(i => i.IsCancel = true);

                _creditAllowedRepository.UpdateRange(resdata);


            }

        }


        public void OutstandingSpecialHandling()
        {

            var users = _appicationDbContext.UsersEmails.FromSql($"select * from UsersEmail where Department = 'FINANCE' and IsDeleted = 0").ToList();

            if (users.Count() > 0)
            {
                var res = GetOutstandingSpecialHandling();

                if (res.Count() > 0)
                {



                    try
                    {
                        var subject = "OutStanding Special Handling";

                        var html = GetMyTable(res, x => x.VirNo, x => x.IndexNo, x => x.ContainerNo, x => x.UserName, x => x.CreatedDate, x => x.AgingDays, x => x.ClearingAgentName
                        , x => x.ConsigneName, x => x.SpecialTotalBalance);

                        var body = "Please see List Of  OutStanding Special Handling : " + html;



                        _emailSender.UsersSendBulkEmailAsync(users, subject, body);
                    }
                    catch (Exception e)
                    {

                    }



                }
            }
        }

        internal List<OutstandingSpecialHandlingViewModel> GetOutstandingSpecialHandling()
        {

            var connectionString =  Configuration.GetConnectionString("DefaultConnection");
            var queryString = "select * from  fn_OutstandingSpecialHandling ()";

            List<OutstandingSpecialHandlingViewModel> messages = new List<OutstandingSpecialHandlingViewModel>();

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var codecoMessage = new OutstandingSpecialHandlingViewModel
                        {

                            VirNo = reader.GetString(0),
                            IndexNo = Convert.ToInt64(reader[1]),
                            ContainerNo = reader.GetString(2),
                            UserName = reader.GetString(3),
                            CreatedDate = reader.GetDateTime(4),
                            AgingDays = Convert.ToInt64(reader[5]),
                            ClearingAgentName = reader.GetString(6),
                            ConsigneName = reader.GetString(7),
                            SpecialTotalBalance = Convert.ToInt64(reader[8]),
                        };


                        messages.Add(codecoMessage);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {

                }
            }

            return messages;
        }



        public static string GetMyTable<T>(IEnumerable<T> list, params Func<T, object>[] fxns)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            sb.Append("<table style='table-layout: fixed;width: 100%;border-collapse: collapse;border: 3px solid purple;'>\n");
            sb.Append("<thead style='background: #86D6FB;border: 3px solid #009eef;'>\n");
            sb.Append("<tr>");

            sb.Append("<th> VirNo  </th>");
            sb.Append("<th> IndexNo  </th>");
            sb.Append("<th> ContainerNo  </th>");
            sb.Append("<th> UserName  </th>");
            sb.Append("<th> CreatedDate  </th>");
            sb.Append("<th> AgingDays  </th>");
            sb.Append("<th> ClearingAgentName  </th>");
            sb.Append("<th> ConsigneName  </th>");
            sb.Append("<th> SpecialTotalBalance  </th>");

            sb.Append("</tr>");
            sb.Append("</thead>\n");

            foreach (var item in list)
            {
                sb.Append("<TR style='border: 1px solid black;padding: 5px;'>\n");
                foreach (var fxn in fxns)
                {
                    sb.Append("<TD style='border: 1px solid black;' >");
                    sb.Append(fxn(item));
                    sb.Append("</TD>");
                }
                sb.Append("</TR>\n");
            }



            sb.Append("</table>");

            return sb.ToString();
        }


        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void UpdateProtcharges()
        {

            var resdata = _portChargeRepository.GetAutoUpdatePortcharges();

            if (resdata.Count() > 0)
            {

                var indexeslist = new List<ContainerIndex>();
                var portchargeslist = new List<PortCharge>();
                var updateportchargeslist = new List<PortCharge>();
                foreach (var model in resdata)
                {
                    var res = _containerIndexRepository.GetIndexInfo(model.VirNo, model.IndexNo ?? 0).ToList();

                    var containerindexidlist = string.Join(",", res.Select(n => n.ContainerIndexId.ToString()).ToArray());

                    var invoiceres = _invoiceRepository.GetCfsInvoices(containerindexidlist).ToList();

                    if (invoiceres.Count() <= 0)
                    {
                        var cfslclindexes = _appicationDbContext.ContainerIndices.FromSql($"select * from ContainerIndex where VirNo = {model.VirNo} and ContainerNo = {model.ContainerNo}  and IndexNo = {model.IndexNo} and IsDeleted = 0").ToList();


                        var charges = _appicationDbContext.PortCharges.FromSql($"select * from PortCharge where VirNumber = {model.VirNo} and ContainerNumber = {model.ContainerNo}  and IndexNumber = {model.IndexNo} and IsDeleted = 0").LastOrDefault();

                        if (charges != null)
                        {

                            charges.DetentionChargesOrBulletSeal = charges.DetentionChargesOrBulletSeal + 1500;
                            charges.TotalCharges = charges.DemurrageCharges + charges.WeighmentCharges + charges.OverWeightPenalty + charges.DetentionChargesOrBulletSeal + charges.ThcPhcKdlpCharges
                                                                          + charges.LoloCharges + charges.QictCharges + model.OtherCharges + charges.WashAndCleanCharges + charges.ANF + charges.Pallet + charges.Recover
                                                                          + charges.TransportCharges;

                            updateportchargeslist.Add(charges);

                        }
                        else
                        {
                            var charge = new PortCharge
                            {
                                DemurrageCharges = 0,
                                WeighmentCharges = 0,
                                OverWeightPenalty = 0,
                                DetentionChargesOrBulletSeal = 1500,
                                ThcPhcKdlpCharges = 0,
                                LoloCharges = 0,
                                QictCharges = 0,
                                OtherCharges = 0,
                                WashAndCleanCharges = 0,
                                TotalCharges = 1500,
                                VirNumber = model.VirNo,
                                IndexNumber = model.IndexNo ?? 0,
                                ContainerNumber = model.ContainerNo,
                                ANF = 0,
                                Pallet = 0,
                                Recover = 0,
                                TransportCharges = 0

                            };
                            portchargeslist.Add(charge);
                        }
                         
                         

                    }


                }

                _portChargeRepository.AddRange(portchargeslist);
                _portChargeRepository.UpdateRange(updateportchargeslist);
                 
                _appicationDbContext.SaveChanges();


            }

        }



    }
}
