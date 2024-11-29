using FluentFTP;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebdocTerminal.Data;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Helpers
{
    public class EDIDynamicEDO
    {
        private ApplicationDbContext _appicationDbContext;
        private IEdiMessageRepository _ediMessageRepo;
        private IElectronicDeliveryOrderRepository _electronicDeliveryOrderRepository;
        private IEmailSender _emailSender;
        private readonly IOptions<AppConfig> config;
        private IConfiguration Configuration;
        public EDIDynamicEDO(ApplicationDbContext appicationDbContext
                            , IEdiMessageRepository ediMessageRepo
                            , IElectronicDeliveryOrderRepository electronicDeliveryOrderRepository
                            , IEmailSender emailSender
                            , IOptions<AppConfig> config
                            , IConfiguration _configuration)
        {
            _appicationDbContext = appicationDbContext;
            _ediMessageRepo = ediMessageRepo;
            _electronicDeliveryOrderRepository = electronicDeliveryOrderRepository;
            _emailSender = emailSender;
            this.config = config;
            Configuration = _configuration;

        }


        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void PollEDOFiles(string rootPath)
        {
            var ediPath = Path.Combine(rootPath, "EDIFilesEDO");

            try
            {
                DownloadEDOFiles(ediPath);

                AddFilesRecords(ediPath);

            }
            catch (Exception e)
            {
                throw;
            }

        }


        public void DownloadEDOFiles(string localPath)
        {


            var edoPath = "DGP";


            String Host =  config.Value.EDOFTPHost;
            String Username =  config.Value.EDOUsername;
            String Password =   config.Value.EDOPassword;


            using (var ftp = new FtpClient(Host, Username, Password))
            {


                var edofiles = ftp.GetListing(edoPath);
                CopyEDOFilesToLocal("txt", edofiles, localPath, ftp);


            }
        }


        public void CopyEDOFilesToLocal(string extension, IEnumerable<FtpListItem> files, string localPath, FtpClient ftp)
        {
            var currentFile = "";
            bool exception = false;

            try
            {
                foreach (var file in files)
                {
                    {
                        currentFile = file.Name;

                        try
                        {

                            var ediMessages = _appicationDbContext.EdiMessages.FromSql($"Select * from EdiMessages where FileName = '{currentFile}'").ToList();

                            ftp.DownloadFile(Path.Combine(localPath, file.Name), file.FullName);

                            ftp.DeleteFile(file.FullName);

                            SaveEdiMessage(file.Name);

                        }
                        catch (Exception e)
                        {
                            exception = true;
                        }

                        exception = false;

                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void SaveEdiMessage(string filename)
        {

            var ediMessages = _appicationDbContext.EdiMessages.FromSql($"Select * from EdiMessages where FileName = '{filename}'").ToList();

            if (ediMessages.Count() == 0)
            {
                using (var db = new ApplicationDbContext())
                {
                    var local = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                    var datetime = TimeZoneInfo.ConvertTime(DateTime.Now, local);

                    var newMessage = new EdiMessage
                    {
                        CreateDate = datetime,
                        Exception = false,
                        FileName = filename
                    };

                    db.EdiMessages.Add(newMessage);
                    db.SaveChanges();
                }
            }

        }


        public void AddFilesRecords(string localPath)
        {
            #region edo

            try
            {
                var edolist = EDOReadFile(localPath);

                foreach (var item in edolist)
                {
                    _electronicDeliveryOrderRepository.Add(item);

                }

            }
            catch (Exception e)
            {

            }

            #endregion

        }



        public List<ElectronicDeliveryOrder> EDOReadFile(string localPath)
        {

            var _ElectronicDeliveryOrderFiles = new List<ElectronicDeliveryOrder>();

            DirectoryInfo d = new DirectoryInfo(localPath);

            FileInfo[] Files = d.GetFiles("*DGP"); //Getting JSON files

            foreach (FileInfo file in Files)
            {
                List<string> list = new List<string>();

                for (int i = 0; i < list.Count; i++)
                {
                    string[] strlist = list[i].Split('-');

                    _ElectronicDeliveryOrderFiles.Add(new ElectronicDeliveryOrder
                    {
                        TotalRecords = Convert.ToInt32(strlist[1]),
                        RecordSequence = Convert.ToInt32(strlist[2]),
                        Vessel_Name = strlist[3],
                        Voyage = strlist[4],
                        ShippingLineCode = strlist[5],
                        BL_No = strlist[6],
                        Container_No = strlist[7],
                        CONT_Size = strlist[8],
                        Index_No = strlist[9],
                        IGM_No = strlist[10],
                        ConsigneeName = strlist[11],
                        ConsigneeNTN = strlist[12],
                        ConsigneeAddress = strlist[13],
                        AgentsChalNo = strlist[14],
                        Agent_Name = strlist[15],
                        Doc_Tran_No = strlist[16],
                        Do_Date = DateFormatter.ConvertToEdiFormat(strlist[17]),
                        Valid_Date = DateFormatter.ConvertToEdiFormat(strlist[18]),
                        Port_of_Arrival = strlist[19],
                        NetWeight = Convert.ToDouble(strlist[20]),
                        Gross_Weight = Convert.ToDouble(strlist[21]),
                        No_Of_Pkgs = strlist[22],
                        PackagesUnit = strlist[23],
                        Marks_No = strlist[24],
                        Goods_Desc = strlist[25],
                        OpType = strlist[26],
                        Performedby = strlist[27],
                        PerformedDate = DateFormatter.ConvertToEdiFormat(strlist[28]),
                        FileName = file.Name

                    });


                }


            }


            foreach (var file in Files)
            {
                Files = Files.Except(new FileInfo[] { file }).ToArray();

                var path = Path.Combine(localPath, "Processed", file.Name);

            }


            return _ElectronicDeliveryOrderFiles;
        }




    }
}

