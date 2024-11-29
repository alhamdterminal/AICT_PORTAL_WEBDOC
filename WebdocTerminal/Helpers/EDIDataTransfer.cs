using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevExpress.Xpo;
using FluentFTP;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using WebdocTerminal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebdocTerminal.Helpers
{
    public class EDIDataTransfer
    {

        private ApplicationDbContext _appicationDbContext;
        private readonly IOptions<AppConfig> config;

        public EDIDataTransfer(ApplicationDbContext appicationDbContext
                              , IOptions<AppConfig> config)
        {
            _appicationDbContext = appicationDbContext;
            this.config = config;
        }

        public void DownloadFiles()
        {
            var remoteArchiveDirectory = "/Archive";


            var ackPath = "ACK_RECEIVE";
            var ack_o_path = "ACK_RECEIVE_O";

            //Import Inconming  Messages

            var viroPath = "VIRO";
            var igmoPath = "IGMO";
            var ipaoPath = "IPAO";
            var gdioPath = "GDIO";
            var simPath = "SIM";
            var ecmPath = "ECM";
            var simoPath = "SIMO";
            var ecmoPath = "ECMO";
            var ccmoPath = "CCMO";
            var crloPath = "CRLO";
            var svmoPath = "SVMO";
            var svmPath = "SVM";
            var gdiPath = "GDI";
            var ihcPath = "IHC";
            var ihcoPath = "IHCO";
            var crlPath = "CRL";


            string startupPath = System.IO.Directory.GetCurrentDirectory();
            string localPath = Path.Combine(startupPath, "EDIFilesTransfer");

            String Host =  config.Value.EDIDataTransferFTPHost;
            String Username = config.Value.EDIDataTransferUsername;
            String Password =  config.Value.EDIDataTransferPassword;

            var ftp = new FtpClient(Host, Username, Password);

            var datetime = DateTime.Now.AddDays(-2);

            var ackFiles = ftp.GetListing(ackPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var ackFiles_o = ftp.GetListing(ack_o_path).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            #region import

            var viroFiles = ftp.GetListing(viroPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var igmoFiles = ftp.GetListing(igmoPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var ipaoFiles = ftp.GetListing(ipaoPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var gdiFiles = ftp.GetListing(gdiPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var gdioFiles = ftp.GetListing(gdioPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var simFiles = ftp.GetListing(simPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var simoFiles = ftp.GetListing(simoPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));

            var ihcFiles = ftp.GetListing(ihcPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));

            var ihcoFiles = ftp.GetListing(ihcoPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));

            var ecmFiles = ftp.GetListing(ecmPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));

            var ecmoFiles = ftp.GetListing(ecmoPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var crlFiles = ftp.GetListing(crlPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var crloFiles = ftp.GetListing(crloPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var ccmoFiles = ftp.GetListing(ccmoPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var svmFiles = ftp.GetListing(svmPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            var svmoFiles = ftp.GetListing(svmoPath).Where(x => Convert.ToDateTime(x.Modified.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(datetime.ToString("MM/dd/yyyy")));


            #endregion


        }


    }
}
