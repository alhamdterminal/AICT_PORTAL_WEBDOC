using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;
using WebdocTerminal.Data;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Areas.Import.Models;
using System.Data;

namespace WebdocTerminal.Helpers
{
    public class EmailAlerts
    {
        private ApplicationDbContext _appicationDbContext;
        private IEmailSender _emailSender;
        private IConfiguration Configuration;
        private IEmailAlertRepository _emailAlertRepository;
        public string Con { get; set; }

        public EmailAlerts(ApplicationDbContext appicationDbContext,
                                        IEmailSender emailSender,
                                        IConfiguration _configuration,
                                        IEmailAlertRepository emailAlertRepository)
        {
            _appicationDbContext = appicationDbContext;
            _emailSender = emailSender;
            Configuration = _configuration;
            _emailAlertRepository = emailAlertRepository;
            Con = _configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }


        public List<EmailAlert> GetRes()
        {
            var containerList = new List<EmailAlert>();

            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            string conString = Con;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("EmailAlerts", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 360;
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(command);

                    sda.Fill(ds);

                    containerList = ds.Tables[0].AsEnumerable()
                        .Select(datarow => new EmailAlert()
                        {
                            VIRNumber = datarow.Field<string>("VIRNumber"),
                            ContainerNumber = datarow.Field<string>("ContainerNumber"),
                            CreateDT = datarow.Field<DateTime>("CreateDT"),
                            Diff = datarow.Field<long>("Diff"),
                            Remarks = datarow.Field<string>("Remarks")

                        }).ToList();

                    con.Close();
                }
            }

            return containerList;

        }

        public void EmailsAltersForOperation()
        {
            var users = _appicationDbContext.UsersEmails.FromSql($"select * from UsersEmail where Department = 'MOVEMENTALTERS' and IsDeleted = 0").ToList();

            if (users.Count() > 0)
            {


                var res = GetRes();

                #region IPAO Not Arrive
                var IPAONotArriveres = res.Where(x => x.Remarks == "IPAONotArrive").ToList();

                if (IPAONotArriveres.Count() > 0)
                {
                    try
                    {

                        var subject = "Container not Gate-In at the AICT within 12 hours after the arrival IPAO EDI message from WEBOC";

                        var html = GetMyTable(IPAONotArriveres, x => x.VIRNumber, x => x.ContainerNumber, x => x.CreateDT, x => x.Diff);

                        var body = " Container not Gate-In at the AICT within 12 hours after the arrival IPAO EDI message from WEBOC : " + html;

                        _emailSender.UsersSendBulkEmailAsync(users, subject, body);


                        _emailAlertRepository.AddRange(IPAONotArriveres);

                    }
                    catch (Exception e)
                    {

                    }

                }
                #endregion

                #region IGMO Not Arrive

                var IGMONotArriveres = res.Where(x => x.Remarks == "IGMONotArrive").ToList();

                if (IGMONotArriveres.Count() > 0)
                {
                    try
                    {

                        var subject = "Container manifested to the AICT but not arrived at the AICT after 12 days.";

                        var html = GetMyTable(IGMONotArriveres, x => x.VIRNumber, x => x.ContainerNumber, x => x.CreateDT, x => x.Diff);

                        var body = "Container manifested to the AICT but not arrived at the AICT after 12 days. : " + html;

                        _emailSender.UsersSendBulkEmailAsync(users, subject, body);

                        _emailAlertRepository.AddRange(IGMONotArriveres);

                    }
                    catch (Exception e)
                    {

                    }

                }

                #endregion

                #region Empty Out

                var EmptyOutres = res.Where(x => x.Remarks == "EmptyOut").ToList();

                if (EmptyOutres.Count() > 0)
                {
                    try
                    {

                        var subject = "CFS containers empty off-hire intimation not submitted after destuffing at AICT within 12 hours.";

                        var html = GetMyTable(EmptyOutres, x => x.VIRNumber, x => x.ContainerNumber, x => x.CreateDT, x => x.Diff);

                        var body = "CFS containers empty off-hire intimation not submitted after destuffing at AICT within 12 hours " + html;

                        _emailSender.UsersSendBulkEmailAsync(users, subject, body);

                        _emailAlertRepository.AddRange(EmptyOutres);

                    }
                    catch (Exception e)
                    {

                    }

                }

                #endregion


                #region SIM After GatePass

                var SIMAfterGatePassres = res.Where(x => x.Remarks == "SIMAfterGatePass").ToList();

                if (SIMAfterGatePassres.Count() > 0)
                {
                    try
                    {

                        var subject = "CY container SIMO EDI hold message arrived after issuance of Gate-Pass.";

                        var html = GetMyTable(SIMAfterGatePassres, x => x.VIRNumber, x => x.ContainerNumber, x => x.CreateDT, x => x.Diff);

                        var body = "CY container SIMO EDI hold message arrived after issuance of Gate-Pass." + html;

                        _emailSender.UsersSendBulkEmailAsync(users, subject, body);

                        _emailAlertRepository.AddRange(SIMAfterGatePassres);

                    }
                    catch (Exception e)
                    {

                    }

                }

                #endregion

                #region Pendding Destuffing

                var PenddingDestuffingres = res.Where(x => x.Remarks == "PenddingDestuffing").ToList();

                if (PenddingDestuffingres.Count() > 0)
                {
                    try
                    {

                        var subject = "CFS containers not destuffed within 6 hours after Gate-In at the AICT.";

                        var html = GetMyTable(PenddingDestuffingres, x => x.VIRNumber, x => x.ContainerNumber, x => x.CreateDT, x => x.Diff);

                        var body = "CFS containers not destuffed within 6 hours after Gate-In at the AICT." + html;

                        _emailSender.UsersSendBulkEmailAsync(users, subject, body);

                        _emailAlertRepository.AddRange(PenddingDestuffingres);
                    }
                    catch (Exception e)
                    {

                    }

                }

                #endregion


            }


        }



        public static string GetMyTable<T>(IEnumerable<T> list, params Func<T, object>[] fxns)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            sb.Append("<table style='table-layout: fixed;width: 100%;border-collapse: collapse;border: 3px solid purple;'>\n");
            sb.Append("<thead style='background: #86D6FB;border: 3px solid #009eef;'>\n");
            sb.Append("<tr>");

            sb.Append("<th> VirNo  </th>");
            sb.Append("<th> ContainerNo  </th>");
            sb.Append("<th> CreatedDate  </th>");
            sb.Append("<th> Diff </th>");

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

        public void EmailAlertAutoReport()
        {
            var users = _appicationDbContext.UsersEmails.FromSql($"select * from UsersEmail where Department = 'CONTAINERMOVEMENT' and IsDeleted = 0").ToList();

            if (users.Count() > 0)
            {
                var eres = GetResEmailAlertAutoReport();
                if (eres.Count() > 0)
                {
                    try
                    {

                        var subject = "Email Alert Auto Report Containers Intimation";

                        var html = GetMyTableEmailAlertAutoReport(eres, x => x.SerialNo, x => x.Line, x => x.ContainerNo, x => x.Size, x => x.Status
                                                                      , x => x.DischargePort, x => x.VesselArrivalDate, x => x.AICTArrivalDate
                                                                      , x => x.EmptyGateOutDate);

                        var body = "Email Alert Auto Report Containers Intimation  : " + html;

                        _emailSender.UsersSendBulkEmailAsync(users, subject, body);

                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        public List<EmailAlertAutoReportViewModel> GetResEmailAlertAutoReport()
        {
            var containerList = new List<EmailAlertAutoReportViewModel>();

            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            string conString = Con;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("EmailAlertAutoReport", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 360;
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(command);

                    sda.Fill(ds);

                    containerList = ds.Tables[0].AsEnumerable()
                        .Select(datarow => new EmailAlertAutoReportViewModel()
                        {
                            SerialNo = datarow.Field<long>("SerialNo"),
                            Line = datarow.Field<string>("Line"),
                            ContainerNo = datarow.Field<string>("ContainerNo"),
                            Size = datarow.Field<string>("Size"),
                            Status = datarow.Field<string>("Status"),
                            DischargePort = datarow.Field<string>("DischargePort"),
                            VesselArrivalDate = datarow.Field<string>("VesselArrivalDate"),
                            AICTArrivalDate = datarow.Field<string>("AICTArrivalDate"),
                            EmptyGateOutDate = datarow.Field<string>("EmptyGateOutDate")

                        }).ToList();

                    con.Close();
                }
            }

            return containerList;

        }

        public static string GetMyTableEmailAlertAutoReport<T>(IEnumerable<T> list, params Func<T, object>[] fxns)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            sb.Append("<table style='table-layout: fixed;width: 100%;border-collapse: collapse;border: 3px solid purple;'>\n");
            sb.Append("<thead style='background: #86D6FB;border: 3px solid #009eef;'>\n");
            sb.Append("<tr>");

            sb.Append("<th> SerialNo  </th>");
            sb.Append("<th> Line </th>");
            sb.Append("<th> ContainerNo  </th>");
            sb.Append("<th> Size </th>");
            sb.Append("<th> Status </th>");
            sb.Append("<th> DischargePort </th>");
            sb.Append("<th> VesselArrivalDate </th>");
            sb.Append("<th> AICTArrivalDate </th>");
            sb.Append("<th> EmptyGateOutDate </th>");

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

    }
}
