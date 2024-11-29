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
    public class EDIMaerskService
    {

        private readonly IOptions<AppConfig> config;
        private ApplicationDbContext _appicationDbContext;
        private IEmailSender _emailSender;
        private IConfiguration Configuration;

        public string UpdateCODECOMessageStatusQuery
        {
            get
            {
                return @"UPDATE EDIMaerskMessageTOS SET IS_PROCESSED=0, PROCESSING_COMMENTS='message file generated successfully by Service.' WHERE EDIMaerskMessageTOSId IN ({ 1  })";
            }
        }



        public EDIMaerskService(ApplicationDbContext appicationDbContext,
                                        IEmailSender emailSender,
                                         IConfiguration _configuration,
                                         IOptions<AppConfig> config)
        {
            _appicationDbContext = appicationDbContext;
            _emailSender = emailSender;
            Configuration = _configuration;
            this.config = config;
        }



        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void SaveCODECOMessages()
        {
            var datetime = DateTime.Now;
            DateTime dt1 = _appicationDbContext.EDI_Maersk_Lookups.FirstOrDefault()?.ModifyPoint ?? DateTime.Now;

            var cfs_codeco = GetCfsCodecoMessages(dt1);

            var cy_codeco = GetCyCodecoMessages(dt1);

            var allMessages = cfs_codeco.Concat(cy_codeco);

            foreach (var containerModel in allMessages)
            {

                if (!_appicationDbContext.EDIMaerskMessageTOs.Any(s => s.CONTAINER_NO == containerModel.CONTAINER_NO && s.BL_NO == containerModel.BL_NO && s.EVENT_ID == "0"))
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
                           (container.ShippingAgentId == 101021)
                           select container.ContainerNo).Distinct().ToList();

            return string.Join(",", resdata);
        }

        public string GetCFSEmptyOutContainers(DateTime dt)
        {

            var resdata = (from emptycontainer in _appicationDbContext.EmptyGateOutContainers
                           join contaner in _appicationDbContext.ContainerIndices on emptycontainer.ContainerNo equals contaner.ContainerNo
                           where
                           emptycontainer.VirNo == contaner.VirNo
                           &&
                           Convert.ToDateTime(emptycontainer.CreatedDate.Value.Date.ToString("MM/dd/yyyy")) == Convert.ToDateTime(dt.ToString("MM/dd/yyyy"))
                           &&
                           (contaner.ShippingAgentId == 101033)
                           select emptycontainer.VirNo).Distinct().ToList();

            return string.Join(",", resdata);
        }

        public List<EDIMaerskMessageTOS> GetCfsCodecoMessages(DateTime dt)
        {
            var atlasdestuffcontainers = GetDestuffContainer(dt);


            var gateinCodeco = GetCfsMaerskContainers("GATE_IN", "", dt);


            var destuffCodeco = GetCfsMaerskContainers("DE_STUFF", atlasdestuffcontainers, dt);


            var concatCodeco = gateinCodeco.Concat(destuffCodeco);

            return concatCodeco.ToList();

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
                return @"INSERT INTO  [dbo].[EDIMaerskMessageTOS]
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
                               ,[IsDeleted]
                               ,[DeleteDate])
                                VALUES
                               ({0},'{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}',{16},{17},'{18}','{19}','{20}','{21}','{22}','{23}','{24}', '{25}','{26}','{27}','{28}')";
            }
        }
        internal string UpdateLookup(string column)
        {
            return @"Update [Tosdb].[dbo].[EDI_Maersk_Lookup] set " + column + " = '{0}'";
        }

        internal List<EDIMaerskMessageTOS> GetCfsMaerskContainers(string optype, string containers, DateTime date)
        {
            string sequenceMessageNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"));
            string sendDateTime = string.Format("{0}:{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.ToString("HHmm"));

            var connectionString =   Configuration.GetConnectionString("DefaultConnection");

            var queryString = "";

            if (optype == "DE_STUFF")
                queryString = $"select * FROM [dbo].[fn_cfs_mskcontainers]('{containers}')";
            else
                queryString = $"select * FROM fn_cfs_gatein_mskcontainers('{date.ToString("yyyy-MM-dd")}')";

            List<EDIMaerskMessageTOS> messages = new List<EDIMaerskMessageTOS>();

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
                        var codecoMessage = new EDIMaerskMessageTOS
                        {
                            CONTAINER_ID = 0,
                            SHIPPING_LINE_ID = 0,
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
                            CONTAINER_LINE_CODE = reader.GetString(11),
                            CARRIER_ID = "945555",
                            CONTAINER_COUNT = 1,
                            CONTAINER_HISTORY_DATE = reader.GetDateTime(0).ToString(),
                            CONTAINER_STATUS = optype,
                            CONTAINER_TYPE = "CFS",
                            EVENT_ID = eventID,
                            IS_MARSK = true,
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


        public List<EDIMaerskMessageTOS> GetCyCodecoMessages(DateTime dt)
        {
            var gateinCodeco = GetCyMaerskContainers("GATE_IN", dt.ToString("yyyy-MM-dd"));

            var gateoutCodeco = GetCyMaerskContainers("GATE_OUT", dt.ToString("yyyy-MM-dd"));

            var concatCodeco = gateinCodeco.Concat(gateoutCodeco);

            return concatCodeco.ToList();

        }

        internal List<EDIMaerskMessageTOS> GetCyMaerskContainers(string optype, string date)
        {
            string sequenceMessageNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"));
            string sendDateTime = string.Format("{0}:{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.ToString("HHmm"));

            var connectionString =  Configuration.GetConnectionString("DefaultConnection");
            var queryString = optype == "GATE_IN" ? $"select * FROM fn_cy_gatein_mskcontainers('{date}')" : $"select * FROM fn_cy_gateout_mskcontainers('{date}')";

            List<EDIMaerskMessageTOS> messages = new List<EDIMaerskMessageTOS>();
            var eventID = optype == "GATE_IN" ? "CY_CONTAINER_GATEIN" : "CY_CONTAINER_GATEOUT";

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
                        var codecoMessage = new EDIMaerskMessageTOS
                        {
                            CONTAINER_ID = 0,
                            SHIPPING_LINE_ID = 0,
                            VOYAGE_NO = reader.GetString(2),
                            VESSEL_CODE = reader.GetString(3),
                            CONTAINER_NO = reader.GetString(4),
                            CN_ISO_CODE = reader.GetString(5),
                            BL_NO = reader.GetString(6),
                            GATE_IN_DATE = optype == "GATE_IN" ? reader[7] as string ?? default(string) : null,
                            GATE_OUT_DATE = optype == "GATE_OUT" ? reader[7] as string ?? default(string) : null,
                            BL_GROSS_WEIGHT = Convert.ToDecimal(reader.GetString(8)),
                            MAN_SEAL_NO = reader.GetString(9),
                            CONTAINER_LINE_CODE = reader.GetString(11),
                            TRUCK_NO = reader[10] as string ?? default(string),
                            CARRIER_ID = "945555",
                            CONTAINER_COUNT = 1,
                            CONTAINER_HISTORY_DATE = reader.GetDateTime(0).ToString(),
                            CONTAINER_STATUS = optype,
                            CONTAINER_TYPE = "FCL",
                            EVENT_ID = eventID,
                            IS_MARSK = true,
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


        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void GenerateCODECOMessageFile(string rootpath)
        {
            string sCODECO_Files_Path = "YES";
            List<EDIMaerskMessageTOS> listCodecoMessage = new List<EDIMaerskMessageTOS>();
            listCodecoMessage = _appicationDbContext.EDIMaerskMessageTOs.Select(x => x).Where(x => x.IS_PROCESSED == false || x.IS_PROCESSED == null).ToList();
            string sendDateTime = string.Format("{0}:{1}", DateTime.Now.ToString("yyMMdd"), DateTime.Now.ToString("HHmm"));
            string sequenceMessageNo = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"));
            string cy_gate_in_hamb_template = "UNB+UNOA:1+{0}+MAEU+{1}+{2}'@UNH+{3}+CODECO:D:95B:UN:ITG13'@BGM+34+{4}+9'@TDT+20+{5}+1++MAEU:172:166+++:103::{6}'@NAD+CF+HAS:160:166'@GID+1'@SGP+{7}'@EQD+CN+{8}+{9}:102:5++3+5'@DTM+7:{11}:203'@LOC+165+{12}:139:6'@LOC+9+{13}:139:6'@LOC+11+{14}:139:6'@MEA+AAE+G+KGM:{15}'@SEL+{16}+CA'@DAM+{17}'@TDT+1++3++{18}:172:87'@CNT+16:{19}'@UNT+17+{21}'@UNZ+1+{23}'";
            string cfs_gateout_hamb_template = "UNB+UNOA:1+{0}+MAEU+{1}+{2}'@UNH+{3}+CODECO:D:95B:UN:ITG12'@BGM+36+{4}+9'@NAD+CF+HAS:160:166'@EQD+CN+{5}+{6}:102:5++8+4'@DTM+7:{7}:203'@LOC+165+{8}:139:6'@DAM+{9}'@CNT+16:{10}'@UNT+9+{12}'@UNZ+1+{14}'";
            string Location2 = "PKKHIAH";
            string codecoContent = string.Empty;
            string containerStatusDate = string.Empty;
            var ci = new CultureInfo("en-US");

            ArrayList lInProcessMessages = new ArrayList();
            string sInProcessMessageID = "0,";
            using (WebClient webClient = new WebClient())
            {

                try
                {

                    if (listCodecoMessage.Count > 0)
                    {
                        foreach (var codecoMessage in listCodecoMessage)
                        {

                            if (codecoMessage.CONTAINER_STATUS == "GATE_IN" && codecoMessage.CONTAINER_LINE_CODE == "101144")
                            {

                                sequenceMessageNo = Convert.ToDateTime(codecoMessage.GATE_IN_DATE, ci).ToString("yyyyMMddHHmmss");
                                sendDateTime = string.Format("{0}:{1}", Convert.ToDateTime(codecoMessage.GATE_IN_DATE, ci).ToString("yyMMdd"), Convert.ToDateTime(codecoMessage.GATE_IN_DATE, ci).ToString("HHmm"));
                                codecoContent = string.Format(cy_gate_in_hamb_template, codecoMessage.MAERSKPARTNERID, sendDateTime, sequenceMessageNo, sequenceMessageNo, sequenceMessageNo, codecoMessage.VOYAGE_NO, codecoMessage.VESSEL_CODE, codecoMessage.CONTAINER_NO, codecoMessage.CONTAINER_NO, codecoMessage.CN_ISO_CODE, codecoMessage.BL_NO, Convert.ToDateTime(codecoMessage.GATE_IN_DATE, ci).ToString("yyyyMMddHHmm"), "PKKHIAH", Location2, Location2, codecoMessage.BL_GROSS_WEIGHT, codecoMessage.MAN_SEAL_NO, codecoMessage.REMARKS == "" ? '2' : '6', codecoMessage.TRUCK_NO, codecoMessage.CONTAINER_COUNT, codecoMessage.SEGMENT_NO, sequenceMessageNo, sequenceMessageNo, sequenceMessageNo);
                            }

                            if (codecoMessage.CONTAINER_STATUS == "GATE_OUT" && codecoMessage.CONTAINER_LINE_CODE == "101146")
                            {
                                sequenceMessageNo = Convert.ToDateTime(codecoMessage.GATE_OUT_DATE, ci).ToString("yyyyMMddHHmmss");
                                sendDateTime = string.Format("{0}:{1}", Convert.ToDateTime(codecoMessage.GATE_OUT_DATE, ci).ToString("yyMMdd"), Convert.ToDateTime(codecoMessage.GATE_OUT_DATE, ci).ToString("HHmm"));
                                codecoContent = string.Format(cfs_gateout_hamb_template, codecoMessage.MAERSKPARTNERID, sendDateTime, sequenceMessageNo, sequenceMessageNo, sequenceMessageNo, codecoMessage.CONTAINER_NO, codecoMessage.CN_ISO_CODE, Convert.ToDateTime(codecoMessage.GATE_OUT_DATE, ci).ToString("yyyyMMddHHmm"), "PKKHIAH", codecoMessage.REMARKS == "" ? '2' : '6', codecoMessage.CONTAINER_COUNT, codecoMessage.SEGMENT_NO, sequenceMessageNo, sequenceMessageNo, sequenceMessageNo);
                            }

                            codecoContent = codecoContent.Replace("@", "" + System.Environment.NewLine);


                            string fileName = string.Empty;
                            if (Convert.ToBoolean(codecoMessage.IS_MARSK))
                            { fileName = string.Format(" {0}-{1}-{2}-{3}.edi", (codecoMessage.CONTAINER_STATUS).ToString(), (codecoMessage.CONTAINER_TYPE).ToString(), codecoMessage.CONTAINER_NO, sequenceMessageNo); }
                            else
                            { fileName = string.Format(" {0}-{1}-{2}-{3}.txt", (codecoMessage.CONTAINER_STATUS).ToString(), (codecoMessage.CONTAINER_TYPE).ToString(), codecoMessage.CONTAINER_NO, sequenceMessageNo); }


                            sCODECO_Files_Path = @"";
                            File.Create(sCODECO_Files_Path).Dispose();
                            StringBuilder sb = new StringBuilder();
                            sb.Append(codecoContent);

                            File.WriteAllText(sCODECO_Files_Path, codecoContent);

                            if (Convert.ToBoolean(codecoMessage.IS_MARSK))
                            {
                                var sCODECO_Files_Path_messages = @"C:\maersk\CODECO_MESSAGE\";

                                sCODECO_Files_Path_messages = sCODECO_Files_Path_messages + fileName;
                                File.Create(sCODECO_Files_Path_messages).Dispose();
                                StringBuilder sb_messages = new StringBuilder();
                                sb_messages.Append(codecoContent);

                                File.WriteAllText(sCODECO_Files_Path_messages, codecoContent);

                            }


                            lInProcessMessages.Add(codecoMessage.EDIMaerskMessageTOSId);
                        }


                        if (lInProcessMessages.Count > 0)
                        {
                            for (int i = 0; i < lInProcessMessages.Count; i++)
                            {
                                sInProcessMessageID += lInProcessMessages[i] + ",";
                            }
                            UpdateCODECOMessageStatus(sInProcessMessageID);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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


        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void MaerskFileTransFer()
        {
            var path = @"C:\maersk\CODECO_MESSAGE\";

            DirectoryInfo d = new DirectoryInfo(path);

            FileInfo[] Files = d.GetFiles();


            foreach (FileInfo file in Files)
            {
                UploadEdiFiles(path, file.Name);

            }



        }

        public void UploadEdiFiles(string localPath, string fileName)
        {

            try
            {

                String Host =   config.Value.MaerskFTPHost;
                String Username = config.Value.MaerskUsername;
                String Password = config.Value.MaerskPassword;

                using (WebClient client = new WebClient())
                {
                    var filePath = Path.Combine(localPath, fileName);

                    var remoteFilePath = string.Concat(Host, "/", "CODECO_MESSAGE", "/", fileName);

                    if (File.Exists(filePath))
                    {

                        client.Credentials = new NetworkCredential(Username, Password);

                        client.UploadFile(remoteFilePath, WebRequestMethods.Ftp.UploadFile, filePath);

                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }


    }
}
