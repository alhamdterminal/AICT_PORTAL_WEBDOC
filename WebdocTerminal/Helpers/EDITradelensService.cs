using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using WebdocTerminal.AuthTradelens;
using WebdocTerminal.Data;
using WebdocTerminal.Models;
using System.IO;
using System.Text;
using System.Net;
using Microsoft.Extensions.Options;


namespace WebdocTerminal.Helpers
{
    public class EDITradelensService
    {
        private ApplicationDbContext _appicationDbContext;
        private IEmailSender _emailSender;
        private IConfiguration Configuration;


        public EDITradelensService(ApplicationDbContext appicationDbContext,
                                        IEmailSender emailSender,
                                        IConfiguration _configuration)
        {
            _appicationDbContext = appicationDbContext;
            _emailSender = emailSender;
            Configuration = _configuration;
        }

        public string UpdateCODECOMessageStatusQuery
        {
            get
            {
                return @"UPDATE EDIMaerskMessageTOS SET IS_PROCESSED=0, PROCESSING_COMMENTS='message file generated successfully by Service.' WHERE EDIMaerskMessageTOSId IN ({ 1  })";
            }
        }


        #region Get Tradelens Messages
        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void GetTradelensMessages()
        {

            DateTime dt1 = _appicationDbContext.EDI_Tradelens_Lookups.FirstOrDefault()?.ModifyPoint ?? DateTime.Now;

            var cfs_codeco = GetCfsCodecoMessages(dt1);

            var cy_codeco = GetCyCodecoMessages(dt1);

            var allMessages = cfs_codeco.Concat(cy_codeco);


            foreach (var containerModel in allMessages)
            {

                if (!_appicationDbContext.EDI_Tradelens_Messages.Any(s => s.CONTAINER_NO == containerModel.CONTAINER_NO && s.BL_NO == containerModel.BL_NO && s.EVENT_ID == "0"))
                {
                    var insertquery = string.Format(CreateCODECOQuery, containerModel.EVENT_ID, containerModel.CONTAINER_ID, containerModel.SHIPPING_LINE_ID, containerModel.MAERSKPARTNERID, containerModel.VOYAGE_NO, containerModel.VESSEL_CODE, containerModel.CONTAINER_NO, containerModel.CN_ISO_CODE, containerModel.BL_NO, containerModel.GATE_OUT_DATE, containerModel.GATE_IN_DATE, containerModel.DESTUFF_DATE, containerModel.MAN_SEAL_NO, containerModel.BL_GROSS_WEIGHT, containerModel.REMARKS, containerModel.CONTAINER_COUNT, containerModel.TRUCK_NO, 10, containerModel.SEQUENCE_NO, null, containerModel.CARRIER_ID, 0, containerModel.CONTAINER_TYPE, containerModel.IS_MARSK, containerModel.CONTAINER_STATUS, containerModel.CONTAINER_LINE_CODE, containerModel.CONTAINER_HISTORY_DATE, 0, null);

                    _appicationDbContext.Database.ExecuteSqlCommand(insertquery);
                }
            }

            var modifyQuery = string.Format(UpdateLookup("ModifyPoint"), DateTime.Now.ToString());

            _appicationDbContext.Database.ExecuteSqlCommand(modifyQuery);


        }

        public string GetDestuffContainer(DateTime dt)
        {

            var resdata = (from container in _appicationDbContext.ContainerIndices
                           join destuffed in _appicationDbContext.DestuffedContainers on container.ContainerIndexId equals destuffed.ContainerIndexId
                           join tellysheet in _appicationDbContext.TellySheets on destuffed.TellySheetId equals tellysheet.TellySheetId
                           where
                           Convert.ToDateTime(tellysheet.DestuffDate.Value.Date.ToString("MM/dd/yyyy")) == Convert.ToDateTime(dt.ToString("MM/dd/yyyy"))
                           &&
                           (container.ShippingAgentId == 101033)
                           select container.ContainerNo).Distinct().ToList();

            return string.Join(",", resdata);
        }

        public string GetCFSEmptyOutContainers(DateTime dt)
        {

            var resdata = (from emptycontainer in _appicationDbContext.EmptyGateOutContainers
                           join contaner in _appicationDbContext.ContainerIndices on emptycontainer.ContainerNo equals contaner.ContainerNo
                           //join tellysheet in _appicationDbContext.TellySheets on destuffed.TellySheetId equals tellysheet.TellySheetId
                           where
                           emptycontainer.VirNo == contaner.VirNo
                           &&
                           Convert.ToDateTime(emptycontainer.CreatedDate.Value.Date.ToString("MM/dd/yyyy")) == Convert.ToDateTime(dt.ToString("MM/dd/yyyy"))
                           &&
                           (contaner.ShippingAgentId == 101033)
                           select emptycontainer.ContainerNo).Distinct().ToList();

            return string.Join(",", resdata);
        }

        public List<EDI_Tradelens_Message> GetCfsCodecoMessages(DateTime dt)
        {

            var atlasdestuffcontainers = GetDestuffContainer(dt);



            var gateinCodeco = GetCfsMaerskContainers("GATE_IN", "", dt);

            var destuffCodeco = GetCfsMaerskContainers("DE_STUFF", atlasdestuffcontainers, dt);

            var concatCodeco = gateinCodeco.Concat(destuffCodeco);

            return concatCodeco.ToList();

        }

        public List<EDI_Tradelens_Message> GetCyCodecoMessages(DateTime dt)
        {
            var gateinCodeco = GetCyMaerskContainers("GATE_IN", dt.ToString("yyyy-MM-dd"));

            var gateoutCodeco = GetCyMaerskContainers("GATE_OUT", dt.ToString("yyyy-MM-dd"));

            var concatCodeco = gateinCodeco.Concat(gateoutCodeco);

            return concatCodeco.ToList();

        }

        internal string CreateOracleDestuffQuery(DateTime date)
        {
            string query = "select distinct t.container_no, t.destuffing_date " +
                "from container_list t, container_to_handle y, edi.app_igm z " +
                "where t.chr_no = y.chr_no " +
                "and t.container_no = z.container_no " +
                "and t.container_no not like '%MULTI%' " +
                "AND Y.LINE_CODE IN('46','145','66','655','656','290','170', '334','420','573','633','536') " +
                $"and to_Char(t.destuffing_date,'DD-MON-YY') = TO_DATE('{date.ToString("dd-MMM-yy")}', 'DD-MON-YY')";

            return query;
        }

        internal string CreateOracleContainerLineCodeQuery(string containerno)
        {
            string query = "SELECT distinct cth.line_code " +
                            "FROM edi.App_GII T, aictops.container_to_handle cth, aictops.container_list cl " +
                            "where t.igm_no = cth.w_igm_no and cth.chr_no = cl.chr_no " +
                            "and t.container_no = cl.container_no " +
                            $"and cl.container_no = '{containerno}'";

            return query;
        }

        internal string CreateCODECOQuery
        {
            get
            {
                return @"INSERT INTO [Tosdb].[dbo].[EDI_Tradelens_Message]
                               ([CONTAINER_ID]
                               ,[EVENT_ID]
                               ,[SHIPPING_LINE_ID]
                               ,[MAERSKPARTNERID]
                               ,[VOYAGE_NO]
                               ,[VESSEL_CODE]
                               ,[CONTAINER_NO]
                               ,[CN_ISO_CODE]
                               ,[BL_NO]
                               ,[GATE_IN_DATE]
                               ,[GATE_OUT_DATE]
                               ,[DESTUFF_DATE]
                               ,[BL_GROSS_WEIGHT]
                               ,[MAN_SEAL_NO]
                               ,[REMARKS]
                               ,[TRUCK_NO]
                               ,[CONTAINER_COUNT]
                               ,[SEGMENT_NO]
                               ,[SEQUENCE_NO]
                               ,[MESSAGE_SEND_DATETIME]
                               ,[CARRIER_ID]
                               ,[VESSEL_CALL_SIGN]
                               ,[CONTAINER_TYPE]
                               ,[IS_MARSK]
                               ,[CONTAINER_STATUS]
                               ,[CONTAINER_HISTORY_DATE]
                               ,[CONTAINER_LINE_CODE]
                               ,[IsDeleted])
                                VALUES
                               ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}',{16},{17},'{18}','{19}','{20}','{21}','{22}','{23}','{24}', '{25}','{26}','{27}')";
            }
        }
        internal string UpdateLookup(string column)
        {
            return @"Update [Tosdb].[dbo].[EDI_Tradelens_Lookup] set " + column + " = '{0}'";
        }

        internal string CreateOracleEmptyGateoutQuery(DateTime date)
        {
            string query = "select distinct T.CONTAINER_NO, T.DEL_DATE from  EMPTY_CON_DEL T, CONTAINER_TO_HANDLE Y, CONTAINER_LIST Z " +
                "WHERE T.CHR_NO = Y.CHR_NO " +
                "AND T.CHR_NO = Z.CHR_NO " +
                "AND T.CONTAINER_NO = Z.CONTAINER_NO " +
                $"AND to_Char(T.DEL_DATE,'DD-MON-YY') = TO_DATE('{date.ToString("dd-MMM-yy")}', 'DD-MON-YY') " +
                "AND Y.LINE_CODE IN('46','145' ) " +
                "and t.container_no not like '%MULTI%'";

            return query;
        }

        internal List<EDI_Tradelens_Message> GetCfsMaerskContainers(string optype, string containers, DateTime date)
        {
            string sequenceMessageNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"));
            string sendDateTime = string.Format("{0}:{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.ToString("HHmm"));

            var connectionString =  Configuration.GetConnectionString("DefaultConnection");

            var queryString = "";

            if (optype == "DE_STUFF" || optype == "GATE_OUT_EMPTY")
                queryString = $"select * FROM [dbo].[fn_cfs_containers]('{containers}')";
            else
                queryString = $"select * FROM fn_cfs_gatein_containers('{date.ToString("yyyy-MM-dd")}')";

            List<EDI_Tradelens_Message> messages = new List<EDI_Tradelens_Message>();

            var eventID = "";

            if (optype == "DE_STUFF")
                eventID = "CONTAINER_DESTUFF";

            else
                eventID = "CFS_CONTAINER_GATEIN";

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
                        var codecoMessage = new EDI_Tradelens_Message
                        {
                            CONTAINER_ID = 0,
                            SHIPPING_LINE_ID = 0,
                            IS_MARSK = reader.GetString(1) == "MSK" ? true : false,
                            VOYAGE_NO = reader.GetString(2),
                            VESSEL_CODE = reader.GetString(3),
                            CONTAINER_NO = reader.GetString(4),
                            CN_ISO_CODE = reader.GetString(5),
                            BL_NO = reader.GetString(6),
                            GATE_IN_DATE = reader[7] as string ?? default(string),
                            GATE_OUT_DATE = optype == "GATE_OUT_EMPTY" ? date.ToString("yyyy-MM-dd") : null,
                            DESTUFF_DATE = optype == "DE_STUFF" ? date.ToString("yyyy-MM-dd") : null,
                            BL_GROSS_WEIGHT = Convert.ToDecimal(reader.GetDecimal(8)),
                            MAN_SEAL_NO = reader.GetString(9),
                            TRUCK_NO = reader[10] as string ?? default(string),
                            CARRIER_ID = "945555",
                            CONTAINER_COUNT = 1,
                            CONTAINER_HISTORY_DATE = reader.GetDateTime(0).ToString(),
                            CONTAINER_STATUS = optype,
                            CONTAINER_TYPE = "CFS",
                            EVENT_ID = eventID,
                            IS_PROCESSED = false,
                            MAERSKPARTNERID = "PKKHIAH",
                            MESSAGE_SEND_DATETIME = sendDateTime,
                            SEGMENT_NO = 10,
                            SEQUENCE_NO = sequenceMessageNo
                        };

                        if (codecoMessage.GATE_IN_DATE != null)
                            codecoMessage.GATE_IN_DATE = DateTime.ParseExact(codecoMessage.GATE_IN_DATE, "yyyyMMddHHmm", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy hh:mm tt");
                        if (codecoMessage.GATE_OUT_DATE != null)
                            codecoMessage.GATE_OUT_DATE = date.ToString("MM/dd/yyyy hh:mm tt");
                        if (codecoMessage.DESTUFF_DATE != null)
                            codecoMessage.DESTUFF_DATE = date.ToString("MM/dd/yyyy hh:mm tt");

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

        internal List<EDI_Tradelens_Message> GetCyMaerskContainers(string optype, string date)
        {
            string sequenceMessageNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"));
            string sendDateTime = string.Format("{0}:{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.ToString("HHmm"));

            var connectionString =  Configuration.GetConnectionString("DefaultConnection");
            var queryString = optype == "GATE_IN" ? $"select * FROM fn_cy_gatein_containers('{date}')" : $"select * FROM fn_cy_gateout_containers('{date}')";

            List<EDI_Tradelens_Message> messages = new List<EDI_Tradelens_Message>();
            var eventID = optype == "GATE_IN" ? "CY_CONTAINER_GATEIN" : "CY_CONTAINER_GATEOUT";

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var codecoMessage = new EDI_Tradelens_Message
                        {
                            CONTAINER_ID = 0,
                            SHIPPING_LINE_ID = 0,
                            IS_MARSK = reader.GetString(1) == "MSK" ? true : false,
                            VOYAGE_NO = reader.GetString(2),
                            VESSEL_CODE = reader.GetString(3),
                            CONTAINER_NO = reader.GetString(4),
                            CN_ISO_CODE = reader.GetString(5),
                            BL_NO = reader.GetString(6),
                            GATE_IN_DATE = optype == "GATE_IN" ? reader[7] as string ?? default(string) : null,
                            GATE_OUT_DATE = optype == "GATE_OUT" ? reader[7] as string ?? default(string) : null,
                            BL_GROSS_WEIGHT = Convert.ToDecimal(reader.GetString(8)),
                            MAN_SEAL_NO = reader.GetString(9),
                            TRUCK_NO = reader[10] as string ?? default(string),
                            CARRIER_ID = "945555",
                            CONTAINER_COUNT = 1,
                            CONTAINER_HISTORY_DATE = reader.GetDateTime(0).ToString(),
                            CONTAINER_STATUS = optype,
                            CONTAINER_TYPE = "CY",
                            EVENT_ID = eventID,
                            IS_PROCESSED = false,
                            MAERSKPARTNERID = "PKKHIAH",
                            MESSAGE_SEND_DATETIME = sendDateTime,
                            SEGMENT_NO = 10,
                            SEQUENCE_NO = sequenceMessageNo
                        };

                        if (codecoMessage.GATE_IN_DATE != null)
                            codecoMessage.GATE_IN_DATE = DateTime.ParseExact(codecoMessage.GATE_IN_DATE, "yyyyMMddHHmm", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy hh:mm tt");
                        if (codecoMessage.GATE_OUT_DATE != null)
                            codecoMessage.GATE_OUT_DATE = DateTime.ParseExact(codecoMessage.GATE_OUT_DATE, "yyyyMMddHHmm", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy hh:mm tt");

                        if ((optype == "GATE_IN" && codecoMessage.GATE_IN_DATE != null) || (optype == "GATE_OUT" && codecoMessage.GATE_OUT_DATE != null))
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

        public string GetAtlasContainers(string query)
        {
            //Create a connection to Oracle			
            // string conString = ConfigurationManager.ConnectionStrings["ATLAS_DB_Oracle"].ConnectionString;

            //string conString = "User Id=aictops;Password=aictaictops;Data Source=192.168.1.19:1521/orc1;";
            string conString = Configuration.GetConnectionString("OracleConnection");

            var data = new List<string>();

            //How to connect to an Oracle DB with a DB alias.
            //Uncomment below and comment above.
            //"Data Source=<service name alias>;";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = query;

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            data.Add(reader.GetString(0));
                        }

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            return string.Join(",", data);
        }
        #endregion

        #region MyRegion

        public async Task<bool> GenerateCODECOMessageFile()
        {
            string sCODECO_Files_Path = "YES";
            List<EDI_Tradelens_Message> listCodecoMessage = new List<EDI_Tradelens_Message>();
            listCodecoMessage = _appicationDbContext.EDI_Tradelens_Messages.Select(x => x).Where(x => x.IS_PROCESSED == false || x.IS_PROCESSED == null).ToList();

            string Location2 = "PKKHI";
            string Originator = "Al-Hamd International Container Terminal";
            string OriginatorId = "AICT";
            string Terminal = "PKKHICLYX";

            ArrayList lInProcessMessages = new ArrayList();
            string sInProcessMessageID = "0,";

            var apiEvents = new List<EDI_Tradelens_APIResponse>();

            try
            {
                var authToken = await Authenticator.GetAuthToken();
                var apiToken = await Authenticator.GetAPIAuthToken(authToken);

                TimeZoneInfo timeZoneInfo;
                // Set the time zone information from US Mountain Standard Time to Pakistan Standard Time
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

                // Get date and time in US Mountain Standard Time 
                DateTime now = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

                var eventdate = now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz");

                if (listCodecoMessage.Count > 0)
                {
                    foreach (var codecoMessage in listCodecoMessage)
                    {
                        var gateindate = string.IsNullOrEmpty(codecoMessage.GATE_IN_DATE) ? DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") : Convert.ToDateTime(codecoMessage.GATE_IN_DATE).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz");

                        if (codecoMessage.CONTAINER_STATUS == "GATE_IN")
                        {
                            var gatein = new ActualGateIn
                            {
                                billOfLadingNumber = codecoMessage.BL_NO,
                                equipmentNumber = codecoMessage.CONTAINER_NO,
                                originatorName = Originator,
                                originatorId = OriginatorId,
                                eventSubmissionTime8601 = eventdate,
                                eventOccurrenceTime8601 = gateindate,
                                location = new AuthTradelens.Location { unlocode = Location2 },
                                vehicleId = codecoMessage.TRUCK_NO,
                                transportationPhase = "Import",
                                fullStatus = "Full",
                                terminal = Terminal
                            };

                            var resp = await APICalls.ActualGateIn(apiToken.solution_token, gatein);

                            resp.messageId = codecoMessage.EDI_Tradelens_MessageId;
                            apiEvents.Add(resp);

                        }

                        else if (codecoMessage.CONTAINER_STATUS == "GATE_OUT")
                        {
                            var gateoutdate = string.IsNullOrEmpty(codecoMessage.GATE_OUT_DATE) ? DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") : Convert.ToDateTime(codecoMessage.GATE_OUT_DATE).ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz");

                            var gateout = new ActualGateOut
                            {
                                billOfLadingNumber = codecoMessage.BL_NO,
                                equipmentNumber = codecoMessage.CONTAINER_NO,
                                originatorName = Originator,
                                originatorId = OriginatorId,
                                vehicleId = codecoMessage.TRUCK_NO,
                                eventSubmissionTime8601 = eventdate,
                                eventOccurrenceTime8601 = gateoutdate,
                                location = new AuthTradelens.Location { unlocode = Location2 },
                                transportationPhase = "Import",
                                fullStatus = "Full",
                                terminal = Terminal
                            };

                            var resp = await APICalls.ActualGateOut(apiToken.solution_token, gateout);
                            resp.messageId = codecoMessage.EDI_Tradelens_MessageId;
                            apiEvents.Add(resp);
                        }

                        lInProcessMessages.Add(codecoMessage.EDI_Tradelens_MessageId);
                    }


                    if (lInProcessMessages.Count > 0)
                    {
                        for (int i = 0; i < lInProcessMessages.Count; i++)
                        {
                            sInProcessMessageID += lInProcessMessages[i] + ",";
                        }

                        if (sInProcessMessageID.EndsWith(","))
                        {
                            sInProcessMessageID = sInProcessMessageID.Substring(0, sInProcessMessageID.Length - 1);
                        }

                        UpdateCODECOMessageStatus(sInProcessMessageID);
                    }

                    foreach (var item in apiEvents)
                    {
                        InsertEvent(item);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool UpdateCODECOMessageStatus(string messageID)
        {
            try
            {
                string strQuery = string.Format(UpdateCODECOMessageStatusQuery, messageID);
                _appicationDbContext.Database.ExecuteSqlCommand(strQuery);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertEvent(EDI_Tradelens_APIResponse resp)
        {
            try
            {
                string strQuery = $"Insert into EDI_Tradelens_APIResponse (message, messageId, eventTransactionId, code, timestamp ) values ('{resp.message}','{resp.messageId}','{resp.eventTransactionId}','{resp.code}','{resp.timestamp}' )";

                _appicationDbContext.Database.ExecuteSqlCommand(strQuery);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion


    }
}
