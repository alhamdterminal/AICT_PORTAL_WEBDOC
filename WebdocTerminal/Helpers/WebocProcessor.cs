using FluentFTP;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebdocTerminal.Data;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace WebdocTerminal.Helpers
{
    public class WebocProcessor
    {

        #region Constructor
        private IConfiguration Configuration;
        private IUsersRepository _userRepo;
        private IUsersEmailRepository _usersEmailRepository;
        private IVIRORepository _viroRepo;
        private IIGMORepository _igmoRepo;
        private IGTORepository _igtoRepo;
        private IIPAORepository _ipaoRepo;
        private IGIIORepository _giioRepo;
        private IGDCRRepository _gdcrrepository;

        private IGTTORepository _gttoRepo;
        private IGDIORepository _gdioRepo;
        private IGDIRepository _gdiRepo;
        private IOGDCRepository _ogdcRepo;
        private IOGTERepository _ogteRepo;
        private IIHCRepository _ihcRepo;
        private IIHCORepository _ihcoRepo;
        private ICCMORepository _ccmoRepo;
        private ISCMORepository _scmoRepo;
        private ISIMRepository _simRepo;
        private ITTSORepository _ttsoRepo;
        private ICRLORepository _crloRepo;
        private IPGOORepository _pgooRepo;
        private IGTOORepository _gtooRepo;
        private IGTORepository _gtoRepo;
        private IOPIARepository _opiaRepo;
        private IOGDERepository _ogdeRepo;
        private IOGIERepository _ogieRepo;
        private IOSVMRepository _osvmRepo;
        private IOSRCRepository _osrcRepo;
        private IOEHCRepository _oehcRepo;
        private IOCRLRepository _ocrlRepo;
        private IOECMRepository _oecmRepo;
        private IACKRepository _ackRepo;
        private IECMRepository _ecmRepo;
        private IECMORepository _ecmoRepo;
        private ISIMORepository _simoRepo;
        ISRCRepository _srcRepo;
        ISRCORepository _srcoRepo;
        private IOSIMRepository _osimRepo;
        private ICRLRepository _crlRepo;
        private ISVMORepository _svmoRepo;
        private ISVMRepository _svmRepo;
        private IOVAMRepository _ovamRepo;
        private IVesselRepository _vesselRepo;
        private IVoyageRepository _voyageRepo;
        private IContainerRepository _containerRepo;
        private IContainerIndexRepository _containerIndexRepo;
        private ICYContainerRepository _cyContainerRepo;
        private IEmailSender _emailSender;
        private readonly IOptions<AppConfig> config;
        private IEdiMessageRepository _ediMessageRepo;
        private IEnteringCargoRepository _enteringCargoRepo;
        private IHostingEnvironment env;
        private IHoldRepository _holdrepo;
        private IGroundedContainerRepository _groundedRepo;
        private ApplicationDbContext _appicationDbContext;
        private IGateInRepository _gateInRepository;
        private ITellySheetRepository _tellySheetRepository;
        private IDestuffedContainerRepository _destuffedContainerRepository;
        private IOtherChargeAssigningRepository _otherChargeAssigningRepo;
        private IBRTRepository _BRTRepository;
        private IBrtItemRepository _BrtItemRepository;
        private IPGORepository _pgoRepository;
        private IPortAndTerminalRepository _portAndTerminalRepository;
        private IExportContainerItemRepositpory _exportContainerItemRepositpory;


        public WebocProcessor(
                              IConfiguration _configuration,
                              IOptions<AppConfig> config,
                              IUsersRepository userRepo,
                              IUsersEmailRepository usersEmailRepository,
                              IEmailSender emailSender,
                              ICYContainerRepository cyContainerRepo,
                              IGTORepository gtoRepo,
                              ISRCRepository srcRepo,
                              ISRCORepository srcoRepo,
                              IOSRCRepository osrcRepo,
                              IOGDCRepository ogdcRepo,
                              IOGTERepository ogteRepo,
                              IOEHCRepository oehcRepo,
                              IGTTORepository gttoRepo,
                              IOCRLRepository ocrlRepo,
                              IVIRORepository viroRepo,
                              IIGMORepository igmoRepo,
                              IIPAORepository ipaoRepo,
                              IGIIORepository giioRepo,
                              IGDCRRepository gdcrrepository,
                              IGDIORepository gdioRepo,
                              ICCMORepository ccmoRepo, ISCMORepository scmoRepo, ITTSORepository ttsoRepo, ISIMRepository simRepo, ICRLRepository crlRepo,
                              ICRLORepository crloRepo, IPGOORepository pgooRepo,
                              IGTOORepository gtooRepo, IACKRepository ackRepo,
                              IOVAMRepository ovamRepo,
                              IVesselRepository vesselRepo, IVoyageRepository voyageRepo,
                              IContainerRepository containerRepo,
                              IContainerIndexRepository containerIndexRepo,
                              IGDIRepository gdiRepo,
                              IOSVMRepository osvmRepo,
                              IECMRepository ecmRepo,
                              IOECMRepository oecmRepo,
                              IIHCRepository ihcRepo,
                              IIHCORepository ihcoRepo,
                              IECMORepository ecmoRepo,
                              ISIMORepository simoRepo,
                              IOSIMRepository osimRepo,
                              ISVMORepository svmoRepo,
                              ISVMRepository svmRepo,
                              IOPIARepository opiaRepo,
                              IOGDERepository ogdeRepo,
                              IOGIERepository ogieRepo,
                              IEdiMessageRepository ediMessageRepo,
                              IEnteringCargoRepository enteringCargoRepo,
                              IHostingEnvironment _env,
                              IHoldRepository holdrepo,
                              IGroundedContainerRepository groundedRepo,
                              ApplicationDbContext appicationDbContext,
                              IGateInRepository gateInRepository,
                              ITellySheetRepository tellySheetRepository,
                              IDestuffedContainerRepository destuffedContainerRepository,
                              IOtherChargeAssigningRepository otherChargeAssigningRepo,
                              IBRTRepository BRTRepository,
                              IBrtItemRepository BrtItemRepository,
                              IPGORepository pgoRepository,
                              IPortAndTerminalRepository portAndTerminalRepository,
                              IExportContainerItemRepositpory exportContainerItemRepositpory)
        {
            Configuration = _configuration;
            _emailSender = emailSender;
            _ediMessageRepo = ediMessageRepo;
            _usersEmailRepository = usersEmailRepository;
            _opiaRepo = opiaRepo;
            _ovamRepo = ovamRepo;
            _ogdeRepo = ogdeRepo;
            _ogieRepo = ogieRepo;
            _oehcRepo = oehcRepo;
            _oecmRepo = oecmRepo;
            _userRepo = userRepo;
            _srcoRepo = srcoRepo;
            _srcRepo = srcRepo;
            _gtoRepo = gtoRepo;
            _ecmRepo = ecmRepo;
            _simRepo = simRepo;
            _viroRepo = viroRepo;
            _igmoRepo = igmoRepo;
            _ipaoRepo = ipaoRepo;
            _giioRepo = giioRepo;
            _gdcrrepository = gdcrrepository;
            _gdioRepo = gdioRepo;
            _ccmoRepo = ccmoRepo;
            _scmoRepo = scmoRepo;
            _ttsoRepo = ttsoRepo;
            _crloRepo = crloRepo;
            _pgooRepo = pgooRepo;
            _gtooRepo = gtooRepo;
            _ogdcRepo = ogdcRepo;
            _ogteRepo = ogteRepo;
            _ackRepo = ackRepo;
            _vesselRepo = vesselRepo;
            _voyageRepo = voyageRepo;
            _containerRepo = containerRepo;
            _containerIndexRepo = containerIndexRepo;
            _osvmRepo = osvmRepo;
            _gdiRepo = gdiRepo;
            _ihcRepo = ihcRepo;
            _crlRepo = crlRepo;
            _gttoRepo = gttoRepo;
            _ocrlRepo = ocrlRepo;
            _ihcoRepo = ihcoRepo;
            _svmoRepo = svmoRepo;
            _svmRepo = svmRepo;
            _ecmoRepo = ecmoRepo;
            _simoRepo = simoRepo;
            _osimRepo = osimRepo;
            _osrcRepo = osrcRepo;
            _enteringCargoRepo = enteringCargoRepo;
            _cyContainerRepo = cyContainerRepo;
            env = _env;
            this.config = config;
            _holdrepo = holdrepo;
            _groundedRepo = groundedRepo;
            _appicationDbContext = appicationDbContext;
            _gateInRepository = gateInRepository;
            _tellySheetRepository = tellySheetRepository;
            _destuffedContainerRepository = destuffedContainerRepository;
            _otherChargeAssigningRepo = otherChargeAssigningRepo;
            _BRTRepository = BRTRepository;
            _BrtItemRepository = BrtItemRepository;
            _pgoRepository = pgoRepository;
            _portAndTerminalRepository = portAndTerminalRepository;
            _exportContainerItemRepositpory = exportContainerItemRepositpory;
        }

        #endregion


        public void sendReportToAgent(string email, string path)
        {

            try
            {
                var subject = "Cargo Along Side Report";
                var body = " ";
                _emailSender.SendEmailWithAttechMentAsync(email, subject, body, path);

            }
            catch (Exception e)
            {

            }

        }



        public void SendAcknowledgement(string FileName, string LocalPath)
        {
            var ackFileName = "ACK_" + FileName;

            var path = Path.Combine(LocalPath, ackFileName);

            var MessageId = FileName.Split("_")[0];
            var performed = DateFormatter.ConvertToyyyyMMddHHmmFormat(DateTime.Now);

            UploadEdiFiles("ACK", LocalPath, ackFileName, true);


        }

        public void UploadEdiFiles(string remoteDocFolder, string localPath, string fileName, bool isAck = false)
        {

            try
            {


                bool exception = false;

                var filePath = localPath.Contains("EDIFiles") ? Path.Combine(localPath, fileName) : Path.Combine(localPath, fileName);

                var remoteFilePath = string.Concat("", remoteDocFolder, "/", fileName);


                DirectoryInfo d = new DirectoryInfo(localPath.Contains("EDIFiles") ? Path.Combine(localPath) : Path.Combine(localPath, "EDIFiles"));

                var files = d.GetFiles();

                var fileNames = "";

                foreach (var item in files)
                {
                    if (_ediMessageRepo.GetFirst(e => e.FileName == item.Name) == null && fileNames.Contains(item.Name))
                        SaveEdiMessage(item.Name);
                }


            }
            catch (Exception e)
            {
                throw;
            }

        }


        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void PollWeboc(string rootPath)
        {

            var ediPath = Path.Combine(rootPath, "EDIFiles");

            EDIFileReader.PATH = ediPath;

            EDIFileReader.CurrentFiles = new FileInfo[] { };

            EDIFileReader.ConnString =  Configuration.GetConnectionString("DefaultConnection");

            try
            {
                var users = _usersEmailRepository.GetAll().ToList();

                #region OPIA

                try
                {
                    var _opia = EDIFileReader.OPIAReadFile();

                    _opiaRepo.Add(_opia.FirstOrDefault());

                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("OPIA", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }


                #endregion

                #region OGDE

                //Process OGDE

                try
                {
                    var _ogde = EDIFileReader.OGDEReadFile();

                    _ogdeRepo.Add(_ogde.FirstOrDefault());

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("OGDE", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }

                #endregion

                #region OEHC

                //Process OEHC
                try
                {
                    var _oehc = EDIFileReader.OEHCReadFile();

                    _oehcRepo.AddRange(_oehc);
                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("OEHC", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }


                #endregion

                #region OECM

                //Process OEHC

                try
                {
                    var _oecm = EDIFileReader.OECMReadFile();

                    _oecmRepo.AddRange(_oecm);
                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("OECM", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }


                #endregion

                #region OCRL

                //Process OCRL

                try
                {
                    var _ocrl = EDIFileReader.OCRLReadFile();

                    _ocrlRepo.AddRange(_ocrl);
                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("OCRL", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }


                #endregion

                #region OSVM

                //Process OSVM

                try
                {
                    var _osvm = EDIFileReader.OSVMReadFile();

                    _osvmRepo.AddRange(_osvm);
                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("OSVM", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }

                #endregion

                #region OVAM

                //Process OVAM

                try
                {
                    var _ovam = EDIFileReader.OVAMReadFile();

                    var exportcontaineritems = new List<ExportContainerItem>();
                    foreach (var item in _ovam)
                    {
                        var exportcontaineritem = _appicationDbContext.ExportContainerItems.FromSql($"SELECT * From ExportContainerItem Where ContainerNumber = {item.ContainerNumber} and IsOvams = 0   and IsDeleted = 0 ").ToList();

                        if (exportcontaineritem.Count() > 0)
                        {
                            exportcontaineritems.AddRange(exportcontaineritem);
                        }
                    }

                    _ovamRepo.AddRange(_ovam);

                    if (exportcontaineritems.Count() > 0)
                    {
                        exportcontaineritems.ToList().ForEach(i => i.IsOvams = true);

                        _exportContainerItemRepositpory.UpdateRange(exportcontaineritems);
                    }
                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("OVAM", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }


                #endregion

                #region ACK
                //Process ACK

                try
                {
                    var _ack = EDIFileReader.ACKReadFile();

                    foreach (var ack in _ack.ToList())
                    {
                        if (_ackRepo.GetFirst(a => a.FileName == ack.FileName) != null)
                        {
                            _ack.Remove(ack);
                        }
                    }

                    _ackRepo.AddRange(_ack);

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("ACK", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }
                #endregion


                #region VIRO

                //Process VIRO

                try
                {
                    var _viro = EDIFileReader.VIROReadFile();

                    long vesselId = 0;

                    foreach (var item in _viro)
                    {
                        try
                        {
                            var igmYear = item.BerthOn != null ? item.BerthOn.Value.Year.ToString() : "";

                            var existingVessel = _vesselRepo.GetFirst(v => v.VesselName?.ToUpper() == item.VesselName?.ToUpper() && v.IGM == item.VIRNumber);

                            if (existingVessel == null)
                            {
                                var vessel = new Vessel
                                {
                                    IGM = item.VIRNumber,
                                    IGMYear = igmYear,
                                    VesselName = item.VesselName,
                                };

                                _vesselRepo.Add(vessel);


                                vesselId = vessel.VesselId;

                            }


                            if (_voyageRepo.GetFirst(v => v.VIRNo == item.VIRNumber && v.VoyageNo == item.Voyage) == null)
                            {
                                var voyage = new Voyage
                                {
                                    VoyageNo = item.Voyage,
                                    VIRNo = item.VIRNumber,
                                    BerthOn = item.BerthOn,
                                    BerthAt = item.BerthAt,
                                    VesselId = vesselId
                                };


                                _voyageRepo.Add(voyage);



                                var containerIndex = _appicationDbContext.ContainerIndices.FromSql($"SELECT * From ContainerIndex where VirNo = { item.VIRNumber} and VoyageId = 0 ").ToList();

                                //   var containerIndex = _containerIndexRepo.GetAll().Where(x => x.VirNo == item.VIRNumber && x.VoyageId == 0).ToList();

                                if (containerIndex != null)
                                {
                                    foreach (var data in containerIndex)
                                    {

                                        var tempUpdateQuery = string.Format(@"
                                                        UPDATE ContainerIndex SET VoyageId = '" + voyage.VoyageId + "'  WHERE  ContainerIndexId  = '" + data.ContainerIndexId + "'  ");
                                        _containerIndexRepo.ExecuteNonSQL(tempUpdateQuery);

                                        //data.VoyageId = voyage.VoyageId;
                                        //_containerIndexRepo.Update(data);

                                    }
                                }

                                var cycontainers = _appicationDbContext.CYContainers.FromSql($"SELECT * From CYContainer where VIRNo = { item.VIRNumber} ").ToList();

                                //  var cycontainers = _cyContainerRepo.GetAll().Where(x => x.VIRNo == item.VIRNumber).ToList();

                                if (cycontainers != null)
                                {
                                    foreach (var data in cycontainers)
                                    {

                                        var IGMYear = item.BerthOn != null ? item.BerthOn.Value.Year.ToString() : "";

                                        var tempUpdateQuery = string.Format(@"UPDATE CYContainer SET 
                                                        BerthAt = '" + item.BerthAt +
                                                                                   "' ,  BerthOn = '" + item.BerthOn +
                                                                                   "' ,  VoyageNo = '" + item.Voyage +
                                                                                   "' ,  VesselName = '" + item.VesselName +
                                                                                   "' ,  IGMYear = '" + IGMYear +
                                                                                   "'  WHERE  CYContainerId  = '" + data.CYContainerId + "'  ");
                                        _cyContainerRepo.ExecuteNonSQL(tempUpdateQuery);


                                        //data.BerthAt = item.BerthAt;
                                        //data.BerthOn = item.BerthOn;
                                        //data.VoyageNo = item.Voyage;
                                        //data.VesselName = item.VesselName;
                                        //data.IGMYear = item.BerthOn != null ? item.BerthOn.Value.Year.ToString() : "";
                                        //_cyContainerRepo.Update(data);

                                    }
                                }


                            }

                        }
                        catch (Exception e)
                        {

                            SaveFailedFileMessage("VIRO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message + item);
                        }


                    }

                    _viroRepo.AddRange(_viro);

                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("VIRO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }

                #endregion

                #region IGMO

                try
                {
                    var _igmo = EDIFileReader.IGMOReadFile();

                    foreach (var item in _igmo)
                    {
                        //var voyage = _voyageRepo.GetFirst(v => v.VIRNo == item.VIRNumber, s => s.Vessel);

                        var voyage = _appicationDbContext.Voyages.FromSql($"SELECT * From Voyage Where VIRNo = {item.VIRNumber} ").Include(s => s.Vessel).FirstOrDefault();


                        var index = _appicationDbContext.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where ContainerNo = {item.ContainerNumber} and IndexNo = { item.IndexNumber }").FirstOrDefault();

                        //var index = _containerIndexRepo.GetFirst(c => c.ContainerNo == item.ContainerNumber && item.IndexNumber == c.IndexNo);

                        //if (voyage != null && index == null)
                        //{
                        if (item.Status == "CFS" && item.OpType == "A")
                        {
                            var cargostas = "A";

                            var containerindex = _appicationDbContext.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VIRNumber} and ContainerNo = { item.ContainerNumber }  and  IndexNo = {item.IndexNumber} and BLNo = {item.BLNumber } and {item.OpType} = {cargostas} ").LastOrDefault();

                            //var containerindex = _containerIndexRepo.GetAll().Where(x => x.VirNo == item.VIRNumber && x.ContainerNo == item.ContainerNumber && x.IndexNo == item.IndexNumber
                            //                                   && x.BLNo == item.BLNumber && item.OpType == "A").LastOrDefault();

                            if (containerindex == null)
                            {

                                var Index = new ContainerIndex
                                {
                                    ContainerNo = item.ContainerNumber,
                                    ContainerGrossWeight = item.ContainerGrossWeight,
                                    NoOfPackages = Convert.ToInt32(item.NumberofPackages),
                                    Status = item.Status,
                                    PackageType = item.PackageType,
                                    VoyageId = voyage != null ? voyage.VoyageId : 0,
                                    VirNo = item.VIRNumber,
                                    ManifestedSealNumber = item.ManifestedSealNumber,
                                    ISOCode = item.ISOCode,
                                    BLGrossWeight = item.BLGrossWeight,
                                    BLNo = item.BLNumber,
                                    Description = item.Description,
                                    IndexNo = item.IndexNumber,
                                    MarksAndNumber = item.MarksAndNumber,
                                    ShippingLine = item.ShippingLine,
                                    IsGateIn = false,
                                    IsHold = false,
                                    IsDeleted = false,
                                    IsDestuffed = false,
                                    IsGrounded = false,
                                    IsGateOut = false
                                };

                                _containerIndexRepo.Add(Index);

                                //var noofpackages = Convert.ToInt32(item.NumberofPackages);
                                //var VoyageId = voyage != null ? voyage.VoyageId : 0;
                                //var datetimenow = DateTime.Now;

                                //var tempUpdateQuery = string.Format(@" INSERT INTO ContainerIndex (
                                //ContainerNo , ContainerGrossWeight , NoOfPackages , Status , PackageType , VoyageId , VirNo ,
                                //ManifestedSealNumber , ISOCode , BLGrossWeight  , BLNo , Description , IndexNo , MarksAndNumber , ShippingLine ,
                                //IsGateIn , IsHold , IsDeleted  , IsDestuffed  , IsGrounded  , IsGateOut , DeleteDate )
                                //VALUES (  " + item.ContainerNumber + "," + item.ContainerGrossWeight + "," + noofpackages + "," + item.Status + "," + item.PackageType + "," + VoyageId + "," +
                                //item.VIRNumber + "," + item.ManifestedSealNumber + "," + item.ISOCode + "," + item.BLGrossWeight + "," + item.BLNumber + "," +
                                //item.Description + "," + item.IndexNumber + "," + item.MarksAndNumber + "," + item.ShippingLine + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," +
                                //datetimenow + ")"); ;

                                //_containerIndexRepo.ExecuteNonSQL(tempUpdateQuery);


                            }
                            else
                            {
                                containerindex.ContainerNo = item.ContainerNumber;
                                containerindex.VirNo = item.VIRNumber;
                                containerindex.IndexNo = item.IndexNumber;
                                containerindex.BLNo = item.BLNumber;
                                containerindex.ContainerGrossWeight = item.ContainerGrossWeight;
                                containerindex.NoOfPackages = Convert.ToInt32(item.NumberofPackages);
                                containerindex.Status = item.Status;
                                containerindex.PackageType = item.PackageType;
                                containerindex.ManifestedSealNumber = item.ManifestedSealNumber;
                                containerindex.ISOCode = item.ISOCode;
                                containerindex.BLGrossWeight = item.BLGrossWeight;
                                containerindex.Description = item.Description;
                                containerindex.MarksAndNumber = item.MarksAndNumber;
                                containerindex.ShippingLine = item.ShippingLine;



                                _containerIndexRepo.Update(containerindex);
                            }


                        }
                        if (item.Status == "CY" && item.OpType == "A")
                        {

                            var cargostas = "A";

                            var cycont = _appicationDbContext.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {item.VIRNumber} and ContainerNo = { item.ContainerNumber }  and  IndexNo = {item.IndexNumber} and BLNo = {item.BLNumber } and {item.OpType} = {cargostas} ").LastOrDefault();



                            //var cycont = _cyContainerRepo.GetAll().Where(x => x.VIRNo == item.VIRNumber && x.ContainerNo == item.ContainerNumber && x.IndexNo == item.IndexNumber
                            //     && x.BLNo == item.BLNumber && item.OpType == "A").LastOrDefault();

                            if (cycont == null)
                            {
                                var cyContainer = new CYContainer
                                {
                                    Status = "CY",
                                    BerthAt = voyage != null ? voyage.BerthAt : null,
                                    BerthOn = voyage != null ? voyage.BerthOn : null,
                                    BLNo = item.BLNumber,
                                    ContainerGrossWeight = item.ContainerGrossWeight,
                                    ContainerNo = item.ContainerNumber,
                                    Description = item.Description,
                                    VoyageNo = voyage != null ? voyage.VoyageNo : null,
                                    ISOCode = item.ISOCode,
                                    VesselName = voyage != null ? voyage.Vessel.VesselName : null,
                                    VIRNo = item.VIRNumber,
                                    ShippingLine = item.ShippingLine,
                                    CargoType = "FCL",
                                    PackageType = item.PackageType,
                                    ManifestedSealNumber = item.ManifestedSealNumber,
                                    NoOfPackages = Convert.ToInt32(item.NumberofPackages),
                                    MarksAndNumber = item.MarksAndNumber,
                                    IGMYear = voyage != null ? voyage.Vessel.IGMYear : null,
                                    BLGrossWeight = item.BLGrossWeight,
                                    IndexNo = item.IndexNumber,
                                    CSCountryCode = item.CountryCode,
                                    IsHold = false,
                                    IsGateIn = false,
                                    IsGateOut = false,
                                    IsGrounded = false,
                                    IsDeleted = false
                                };

                                _cyContainerRepo.Add(cyContainer);
                            }
                            else
                            {
                                cycont.VIRNo = item.VIRNumber;
                                cycont.ContainerNo = item.ContainerNumber;
                                cycont.IndexNo = item.IndexNumber;
                                cycont.BLNo = item.BLNumber;
                                cycont.Description = item.Description;
                                cycont.CargoType = "FCL";
                                cycont.ISOCode = item.ISOCode;
                                cycont.ShippingLine = item.ShippingLine;
                                cycont.PackageType = item.PackageType;
                                cycont.NoOfPackages = Convert.ToInt32(item.NumberofPackages);
                                cycont.ManifestedSealNumber = item.ManifestedSealNumber;
                                cycont.MarksAndNumber = item.MarksAndNumber;
                                cycont.BLGrossWeight = item.BLGrossWeight;
                                cycont.CSCountryCode = item.CountryCode;
                                _cyContainerRepo.Update(cycont);
                            }

                        }

                        // }



                        var cargostatus = "A";

                        var igmores = _appicationDbContext.IGMOs.FromSql($"SELECT * From IGMO Where VIRNumber = {item.VIRNumber} and ContainerNumber = { item.ContainerNumber }  and  IndexNumber = {item.IndexNumber} and BLNumber = {item.BLNumber } and {item.OpType} = {cargostatus} ").LastOrDefault();


                        //var igmores = _igmoRepo.GetAll().Where(x => x.VIRNumber == item.VIRNumber && x.ContainerNumber == item.ContainerNumber && x.IndexNumber == item.IndexNumber
                        //                                       && x.BLNumber == item.BLNumber && item.OpType == "A").LastOrDefault();

                        if (igmores == null)
                        {
                            _igmoRepo.Add(item);
                        }

                        if (igmores != null)
                        {
                            igmores.TotalRecord = item.TotalRecord;
                            igmores.RecordSequence = item.RecordSequence;
                            igmores.VIRNumber = item.VIRNumber;
                            igmores.BLNumber = item.BLNumber;
                            igmores.IndexNumber = item.IndexNumber;
                            igmores.Status = item.Status;
                            igmores.ContainerNumber = item.ContainerNumber;
                            igmores.ContainerGrossWeight = item.ContainerGrossWeight;
                            igmores.BLGrossWeight = item.BLGrossWeight;
                            igmores.HSCode = item.HSCode;
                            igmores.Description = item.Description;
                            igmores.IMOClass = item.IMOClass;
                            igmores.MarksAndNumber = item.MarksAndNumber;
                            igmores.NumberofPackages = item.NumberofPackages;
                            igmores.PackageType = item.PackageType;
                            igmores.ISOCode = item.ISOCode;
                            igmores.CountryCode = item.CountryCode;
                            igmores.DestinationCode = item.DestinationCode;
                            igmores.ManifestedSealNumber = item.ManifestedSealNumber;
                            igmores.ShippingLine = item.ShippingLine;
                            igmores.OpType = item.OpType;
                            igmores.Performed = item.Performed;
                            igmores.MessageFrom = item.MessageFrom;
                            igmores.MessageTo = item.MessageTo;
                            igmores.FileName = item.FileName;
                            igmores.CreateDT = item.CreateDT;

                            _igmoRepo.Update(igmores);
                        }

                    }

                    //_igmoRepo.AddRange(_igmo);

                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("IGMO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }
                #endregion

                #region GDI

                //Process GDI
                try
                {
                    var _gdi = EDIFileReader.GDIReadFile();

                    _gdiRepo.AddRange(_gdi);

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("GDI", e.Message);

                }

                #endregion

                #region IHCO
                //Process IHCO

                try
                {
                    var _ihco = EDIFileReader.IHCOReadFile();

                    foreach (var item in _ihco)
                    {
                        _ihcoRepo.Add(item);

                        //var crlo = _crloRepo.GetFirst(c => c.IndexNumber == item.IndexNumber && c.VIRNumber == item.VIRNumber && c.BLNumber == item.BLNumber);
                        var crlo = _appicationDbContext.CRLOs.FromSql($"SELECT * From CRLO where IndexNumber = { item.IndexNumber} and VIRNumber = { item.VIRNumber} and BLNumber = { item.BLNumber} ").FirstOrDefault();

                        if (crlo != null)
                            _crloRepo.Delete(crlo);

                        //var index = _containerIndexRepo.GetFirst(c => c.IndexNo == item.IndexNumber && c.BLNo == item.BLNumber);
                        var index = _appicationDbContext.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VIRNumber} and IndexNo = {item.IndexNumber } and BLNo = { item.BLNumber }  ").ToList();

                        if (index.Count() > 0)
                        {
                            index.ForEach(x => x.IsGrounded = false);

                            _containerIndexRepo.UpdateRange(index);
                        }


                        if (item.HandlingCode == "ES")
                        {
                            try
                            {
                                var subject = "Section 79B - Containers";
                                var body = "The following BL Number are marked under section 79B " + item.BLNumber;
                                _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                            }
                            catch (Exception e)
                            {

                            }
                        }


                    }

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("IHCO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }
                #endregion

                #region IHC
                //Process IHC
                try
                {
                    var _ihc = EDIFileReader.IHCReadFile();

                    _ihcRepo.AddRange(_ihc);

                    if (_ihc.Count > 0)
                    {
                        var contList = "";

                        for (int i = 0; i < _ihc.Count; i++)
                        {
                            if (_ihc[i].HandlingCode == "ES")
                            {
                                contList += $"{i + 1}. " + _ihc[i].ContainerNumber + " ";
                            }

                            //var crl = _crlRepo.GetFirst(c => c.ContainerNumber == _ihc[i].ContainerNumber && c.VIRNumber == _ihc[i].VIRNumber);
                            var crl = _appicationDbContext.CRLs.FromSql($"SELECT * From CRL Where ContainerNumber = {_ihc[i].ContainerNumber} and VIRNumber = {  _ihc[i].VIRNumber }  ").FirstOrDefault();

                            if (crl != null)
                            {
                                _crlRepo.Delete(crl);

                                //  var cycont = _cyContainerRepo.GetFirst(c => c.ContainerNo == _ihc[i].ContainerNumber && c.VIRNo == _ihc[i].VIRNumber);


                            }

                            var cycont = _appicationDbContext.CYContainers.FromSql($"SELECT * From CYContainer Where ContainerNo = {_ihc[i].ContainerNumber} and VIRNo = {_ihc[i].VIRNumber }  ").FirstOrDefault();

                            if (cycont != null)
                            {
                                cycont.IsGrounded = false;
                                _cyContainerRepo.Update(cycont);
                            }
                        }

                        if (contList.Length > 0)
                        {
                            try
                            {
                                var subject = "Section 79B - Containers";
                                var body = "The following containers are marked under section 79B " + contList;
                                _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("IHC", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }


                #endregion

                #region IPAO
                //Process IPAO
                try
                {
                    var _ipao = EDIFileReader.IPAOReadFile();

                    _ipaoRepo.AddRange(_ipao);
                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("IPAO", e.Message);

                }
                #endregion


                #region GDIO

                //Process GDIO

                try
                {
                    var _gdio = EDIFileReader.GDIOReadFile();

                    _gdioRepo.AddRange(_gdio);

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("GDIO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }

                #endregion

                #region SIM

                //Process SIM

                try
                {
                    var _sim = EDIFileReader.SIMReadFile();

                    //foreach (var item in _sim)
                    //{
                    //    // var container = _cyContainerRepo.GetFirst(c => c.ContainerNo == item.ContainerNumber);
                    //    var container = _appicationDbContext.CYContainers.FromSql($"SELECT * From CYContainer Where ContainerNo = {item.ContainerNumber}  ").FirstOrDefault();


                    //    if (item.Status == "HO")
                    //    {

                    //        try
                    //        {
                    //            var subject = "Hold Alert! - " + container.ContainerNo;
                    //            var body = container.ContainerNo + " has been put on hold. Please avoid any activities until hold is released";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }

                    //        // container.IsHold = true;
                    //    }
                    //    else
                    //    {
                    //        try
                    //        {
                    //            var subject = "Hold Released! - " + container.ContainerNo;
                    //            var body = container.ContainerNo + " has been been released from Hold. This container can now be gated out";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }

                    //        //  container.IsHold = false;
                    //    }

                    //    // _cyContainerRepo.Update(container);
                    //}

                    _simRepo.AddRange(_sim);

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("SIM", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }

                #endregion

                #region ECM

                //Process ECM

                try
                {
                    var _ecm = EDIFileReader.ECMReadFile();

                    _ecmRepo.AddRange(_ecm);

                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("ECM", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }

                #endregion

                #region SIMO

                //Process SIM
                try
                {
                    var _simo = EDIFileReader.SIMOReadFile();

                    //foreach (var item in _simo)
                    //{
                    //    //var containers = _containerRepo.GetIndexes(item.VIRNumber, Convert.ToInt32(item.IndexNumber), item.BLNumber);
                    //    var indexno = Convert.ToInt32(item.IndexNumber);
                    //    var containers = _appicationDbContext.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VIRNumber} and IndexNo = { indexno }    and BLNo = {item.BLNumber }   ").ToList();

                    //    if (containers.Count > 0)
                    //    {

                    //        if (item.Status == "HO")
                    //        {
                    //            try
                    //            {
                    //                var subject = "Hold Alert! - " + item.BLNumber;
                    //                var body = "BL Number: " + item.BLNumber + " has been put on hold. Please avoid any activities until hold is released";
                    //                _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //            }
                    //            catch (Exception e)
                    //            {

                    //            }

                    //            //foreach (var index in containers)
                    //            //{
                    //            //    index.IsHold = true;
                    //            //}

                    //        }
                    //        else
                    //        {
                    //            try
                    //            {
                    //                var subject = "Hold Released! - " + item.BLNumber;
                    //                var body = "BL Number: " + item.BLNumber + " has been been released from Hold. This container can now be gated out";
                    //                _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //            }
                    //            catch (Exception e)
                    //            {

                    //            }

                    //            //foreach (var index in containers)
                    //            //{
                    //            //    index.IsHold = false;
                    //            //}
                    //        }

                    //        // _containerIndexRepo.UpdateRange(containers);
                    //    }

                    //}

                    _simoRepo.AddRange(_simo);
                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("SIMO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }


                #endregion

                #region ECMO

                //Process ECMO
                try
                {
                    var _ecmo = EDIFileReader.ECMOReadFile();

                    _ecmoRepo.AddRange(_ecmo);

                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("ECMO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }

                #endregion

                #region CCMO

                //Process CCMO
                try

                {

                    var datetime = DateTime.Now;


                    var scmos = new List<SCMO>();
                    var pGOOs = new List<PGOO>();

                    var _ccmo = EDIFileReader.CCMOReadFile();

                    if (_ccmo.Count() > 0)
                    {
                        foreach (var item in _ccmo)
                        {

                            //  var igmo = _igmoRepo.GetAll().Where(x => x.VIRNumber == item.VIRNo && x.BLNumber == item.BLNo && x.IndexNumber == item.IndexNo).LastOrDefault();
                            var igmo = _appicationDbContext.IGMOs.FromSql($"SELECT * From IGMO Where VIRNumber = {item.VIRNo} and BLNumber = { item.BLNo }  and  IndexNumber = {item.IndexNo} ").LastOrDefault();


                            if (igmo != null)
                            {
                                var scmo = new SCMO
                                {
                                    VIRNo = item.VIRNo,
                                    BLNo = item.BLNo,
                                    Category = item.Category,
                                    IndexNo = item.IndexNo ?? 0,
                                    ContainerNo = item.ContainerNo,
                                    Weight = igmo.BLGrossWeight,
                                    NoOfPackages = item.NoOfPackages,
                                    OpType = "A",
                                    Performed = DateTime.Now,
                                    TpNo = item.TPNo,
                                    MessageFrom = "AIT",
                                    MessageTo = "WeBOC",
                                    FileName = $"SCMO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"

                                };
                                scmos.Add(scmo);

                                var pgo = new PGOO
                                {
                                    VIRNo = item.VIRNo,
                                    ContainerNo = item.ContainerNo,
                                    VehicleNo = item.VehicleNo,
                                    BondedCarrierId = item.BondedCarrierId,
                                    BondedCarrierName = item.BondedCarrierName,
                                    OpType = "A",
                                    MessageFrom = "AIT",
                                    MessageTo = "WeBOC",
                                    Performed = DateTime.Now,
                                    FileName = $"PGOO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"



                                };

                                pGOOs.Add(pgo);
                            }


                        }

                    }
                    _ccmoRepo.AddRange(_ccmo);
                    _scmoRepo.AddRange(scmos);
                    _pgooRepo.AddRange(pGOOs);

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("CCMO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }

                #endregion

                #region CRL

                //Process CRL

                try
                {
                    var datetime = DateTime.Now;
                    //var pGOOs = new List<PGOO>();

                    var _crl = EDIFileReader.CRLReadFile();


                    //if (_crl.Count() > 0)
                    //{
                    //    foreach (var item in _crl)
                    //    {
                    //        var gdcr = _appicationDbContext.GDCRs.FromSql($"SELECT * From GDCR Where VirNumber = {item.VIRNumber} and NewContainerNumber = { item.ContainerNumber }  ").LastOrDefault();
                    //        if (gdcr != null)
                    //        {
                    //            var pgo = new PGOO
                    //            {
                    //                VIRNo = item.VIRNumber,
                    //                ContainerNo = item.ContainerNumber,
                    //                VehicleNo = "",
                    //                BondedCarrierId = "",
                    //                BondedCarrierName = "",
                    //                OpType = "A",
                    //                MessageFrom = "AIT",
                    //                MessageTo = "WeBOC",
                    //                Performed = DateTime.Now,
                    //                FileName = $"PGOO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"
                    //            };
                    //            pGOOs.Add(pgo);
                    //        }
                    //    }
                    //}

                    _crlRepo.AddRange(_crl);
                    //_pgooRepo.AddRange(pGOOs);

                    //foreach (var item in _crl)
                    //{
                    //    if (item.Status == "RE")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Release under Escort (Inter Port)";
                    //            var body = "Container Released With The Status RE";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "RA")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Intra Port (used by KICT/PICT to transfer to KPT)";
                    //            var body = "Container Released With The Status RA";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "BR")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Intra Port (used by KICT/PICT to transfer to KPT)";
                    //            var body = "Container Released With The Status BR";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "IB")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Into Bond (Public/Private Warehousing)";
                    //            var body = "Container Released With The Status IB";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "UM")
                    //    {
                    //        try
                    //        {
                    //            var subject = "US Military/NATO/ISAF (Afghan Transit)";
                    //            var body = "Container Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }
                    //    if (item.Status == "AN")
                    //    {
                    //        try
                    //        {
                    //            var subject = "US Military/NATO/ISAF (Afghan Transit)";
                    //            var body = "Container Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }


                    //    if (item.Status == "AC")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Afghan Commercial (Afghan Transit)";
                    //            var body = "Container Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "OC")
                    //    {
                    //        try
                    //        {
                    //            var subject = "One Custom";
                    //            var body = "Container Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "DD")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Delivery on Document for One Custom";
                    //            var body = "Container Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //}

                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("CRL", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }


                #endregion

                #region CRLO

                //Process CRLO

                try
                {
                    var _crlo = EDIFileReader.CRLOReadFile();

                    _crloRepo.AddRange(_crlo);

                    //foreach (var item in _crlo)
                    //{
                    //    if (item.Status == "RE")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Release under Escort (Inter Port)";
                    //            var body = "Shipment Released With The Status RE";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "RA")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Intra Port (used by KICT/PICT to transfer to KPT)";
                    //            var body = "Shipment Released With The Status RA";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "BR")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Intra Port (used by KICT/PICT to transfer to KPT)";
                    //            var body = "Shipment Released With The Status BR";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "IB")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Into Bond (Public/Private Warehousing)";
                    //            var body = "Shipment Released With The Status IB";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "UM")
                    //    {
                    //        try
                    //        {
                    //            var subject = "US Military/NATO/ISAF (Afghan Transit)";
                    //            var body = "Shipment Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }
                    //    if (item.Status == "AN")
                    //    {
                    //        try
                    //        {
                    //            var subject = "US Military/NATO/ISAF (Afghan Transit)";
                    //            var body = "Shipment Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }


                    //    if (item.Status == "AC")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Afghan Commercial (Afghan Transit)";
                    //            var body = "Shipment Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "OC")
                    //    {
                    //        try
                    //        {
                    //            var subject = "One Custom";
                    //            var body = "Shipment Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //    if (item.Status == "DD")
                    //    {
                    //        try
                    //        {
                    //            var subject = "Delivery on Document for One Custom";
                    //            var body = "Shipment Released With The Status UM";
                    //            _emailSender.SendBulkCompanyEmailAsync(users, subject, body);
                    //        }
                    //        catch (Exception e)
                    //        {

                    //        }
                    //    }

                    //}


                }
                catch (Exception e)
                {
                    SaveFailedFileMessage("CRLO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }

                #endregion

                #region SVMO

                //Process SVMO

                try
                {
                    var _svmo = EDIFileReader.SVMOReadFile();

                    _svmoRepo.AddRange(_svmo);

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("SVMO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }

                #endregion
                #region SVM

                //Process SVM

                try
                {
                    var _svm = EDIFileReader.SVMReadFile();

                    _svmRepo.AddRange(_svm);

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("SVM", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

                }

                #endregion


                ArchiveFiles(Configuration.GetConnectionString("DefaultConnection"));

            }
            catch (Exception e)
            {

                throw;
            }

        }

        public void DeleteFromTable(string connString, string tableName, string field, string value)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand($"delete from {tableName} where {field} = '{value}'", connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
            }
        }


        public void ArchiveFiles(string connString)
        {


            foreach (var item in EDIFileReader.CurrentFiles)
            {

                SaveEdiMessage(item.Name);

            }

        }


        public void SendUnprocessedFiles(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);

            FileInfo[] Files = d.GetFiles(); //Getting JSON files

            foreach (FileInfo file in Files)
            {
                if ((
                    file.Name.Contains("OGIE")
                    || file.Name.Contains("OSRC")
                    || file.Name.Contains("OGDC")
                    || file.Name.Contains("OGTE")
                    || file.Name.Contains("GIIO")
                    || file.Name.Contains("GDCR")
                    || file.Name.Contains("SRC")
                    || file.Name.Contains("SRCO")
                    || file.Name.Contains("GTO")
                    || file.Name.Contains("GTOO")
                    || file.Name.Contains("TTSO")
                    || file.Name.Contains("SCMO")
                    || file.Name.Contains("PGOO")
                    || file.Name.Contains("PGO")
                    || file.Name.Contains("GTTO")
                    || file.Name.Contains("ACK")))
                {


                    var sendTo = file.Name.Split("_")[0];

                    UploadEdiFiles(sendTo, path, file.Name);


                    continue;
                }
            }
        }

        public void SaveEdiMessage(string filename)
        {
            if (_ediMessageRepo.GetFirst(e => e.FileName == filename) == null)
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

        public void SaveFailedFileMessage(string filename, string message)
        {
            using (var db = new ApplicationDbContext())
            {
                var newMessage = new FailedFile
                {
                    Message = message,
                    FileName = filename
                };

                db.FailedFiles.Add(newMessage);
                db.SaveChanges();
            }
        }

        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void GenerateEDIMessages(string rootPath)
        {




            #region OGIE

            var ogies = _ogieRepo.GetList(s => s.IsProcessed == false);

            if (ogies.Count() > 0)
            {
                _ogieRepo.UpdateRange(ogies);
            }



            #endregion

            #region OSRC


            var osrcs = _osrcRepo.GetList(s => s.IsProcessed == false);
            if (osrcs.Count() > 0)
            {
                _osrcRepo.UpdateRange(osrcs);
            }

            #endregion

            #region OGDC

            var ogdcs = _ogdcRepo.GetList(s => s.IsProcessed == false);

            if (ogdcs.Count() > 0)
            {
                _ogdcRepo.UpdateRange(ogdcs);
            }



            #endregion

            #region OGTE

            var ogtes = _ogteRepo.GetList(s => s.IsProcessed == false);

            if (ogtes.Count() > 0)
            {

                _ogteRepo.UpdateRange(ogtes);
            }


            #endregion

            #region GIIO

            var giios = _giioRepo.GetList(s => s.IsProcessed == false);

            if (giios.Count() > 0)
            {


                _giioRepo.UpdateRange(giios);
            }



            #endregion

            #region GDCR

            var gdcrs = _gdcrrepository.GetList(s => s.IsSubmit == true && s.IsProcessed == false);

            if (gdcrs.ToList().Count > 0)
            {


                _gdcrrepository.UpdateRange(gdcrs);
            }
            #endregion

            #region PGO

            var pgos = _pgoRepository.GetList(s => s.IsProcessed == false);

            if (pgos.ToList().Count > 0)
            {


                _pgoRepository.UpdateRange(pgos);
            }

            #endregion

            #region TTSO

            var ttsos = _ttsoRepo.GetList(s => s.IsProcessed == false);

            if (ttsos.Count() > 0)
            {


                _ttsoRepo.UpdateRange(ttsos);
            }


            #endregion

            #region SCMO

            var scmos = _scmoRepo.GetList(s => s.IsProcessed == false);

            if (scmos.Count() > 0)
            {

                _scmoRepo.UpdateRange(scmos);

            }

            #endregion

            #region PGOO

            var pgoos = _pgooRepo.GetList(s => s.IsProcessed == false);

            if (pgoos.Count() > 0)
            {

                _pgooRepo.UpdateRange(pgoos);
            }



            #endregion

            #region GTTO

            var gttos = _gttoRepo.GetList(s => s.IsProcessed == false);

            if (gttos.Count() > 0)
            {


                _gttoRepo.UpdateRange(gttos);
            }


            #endregion

            #region GTOO

            var gtoos = _gtooRepo.GetList(s => s.IsProcessed == false);

            if (gtoos.Count() > 0)
            {


                _gtooRepo.UpdateRange(gtoos);

            }


            #endregion

            #region GTO

            var gtos = _gtoRepo.GetList(s => s.IsProcessed == false);

            if (gtos.Count() > 0)
            {

                _gtoRepo.UpdateRange(gtos);
            }


            #endregion

            #region SRC

            var srcs = _srcRepo.GetList(s => s.IsProcessed == false);

            if (srcs.Count() > 0)
            {


                _srcRepo.UpdateRange(srcs);
            }


            #endregion

            #region SRCO

            var srcos = _srcoRepo.GetList(s => s.IsProcessed == false);

            if (srcos.Count() > 0)
            {
                try
                {

                    _appicationDbContext.UpdateRange(srcos);
                    _appicationDbContext.SaveChanges();

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("SRCO", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
                }

            }
            #endregion



            //ada auto grounding work add here  
            #region vechile indexes cfs

            var containers = _containerRepo.GetAutoUngroundedCFSContainers().ToList();

            try
            {


                var datetime = DateTime.Now;


                var indexList = new List<ContainerIndex>();
                var vechilesrcos = new List<SRCO>();

                foreach (var container in containers)
                {


                    //   var simo = _simoRepo.GetAll().Where(x => x.BLNumber == container.BLNumber && x.IndexNumber == container.IndexNo.ToString() && x.VIRNumber == container.VIRNumber).LastOrDefault();

                    var indexno = container.IndexNo.ToString();
                    var simo = _appicationDbContext.SIMOs.FromSql($"SELECT * From SIMO Where IndexNumber = { indexno }  and  VIRNumber = {container.VIRNumber}  ").LastOrDefault();

                    if (simo != null && simo.Status == "HO")
                    {
                        continue;
                    }
                    else
                    {

                        //var indexes = _containerIndexRepo.GetList(s => s.VirNo == container.VIRNumber && s.BLNo == container.BLNumber && s.IndexNo == container.IndexNo).LastOrDefault();
                        var indexes = _appicationDbContext.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {container.VIRNumber} and BLNo = { container.IgmoBLNumber }  and  IndexNo = {container.IndexNo}  ").ToList();

                        var checkData = indexes.Where(x => x.IsDestuffed == false).FirstOrDefault();

                        if (checkData != null)
                        {
                            continue;

                        }


                        indexes.ForEach(x => x.IsGrounded = true);
                        indexList.AddRange(indexes);


                        var srco = new SRCO
                        {
                            Weight = container.Weight,
                            VIRNumber = container.VIRNumber,
                            BLNumber = container.BLNumber,
                            IndexNumber = container.IndexNo,
                            Category = "I",
                            ActivityType = container.ActivityType,
                            HandlingCode = container.HandlingCode,
                            Location = container.Location,
                            Performed = datetime,
                            FileName = $"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}",
                            CreateDT = DateTime.Now,
                            MessageFrom = "AIT",
                            MessageTo = "WEBOC"
                        };

                        vechilesrcos.Add(srco);


                    }




                }


                var resdata = _groundedRepo.GetFileNameSRCO($"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                if (resdata != true)
                {
                    if (vechilesrcos.Count() > 0)
                    {
                        vechilesrcos.ForEach(x => x.FileName = $"SRCO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                        _srcoRepo.AddRange(vechilesrcos);
                        _containerIndexRepo.UpdateRange(indexList);

                        //_appicationDbContext.SaveChanges();
                    }

                }



            }
            catch (Exception e)
            {
                SaveFailedFileMessage("Auto Vechile", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
            }

            #endregion

            #region auto grounding cy

            var cyautogroundingcontainers = _containerRepo.GetAutoUngroundedCYContainers().ToList();

            try
            {


                var datetime = DateTime.Now;


                var indexListcy = new List<CYContainer>();
                var vechilesrcoscy = new List<SRC>();
                var groundedContainerscy = new List<GroundedContainer>();


                foreach (var container in cyautogroundingcontainers)
                {


                    //   var simo = _simoRepo.GetAll().Where(x => x.BLNumber == container.BLNumber && x.IndexNumber == container.IndexNo.ToString() && x.VIRNumber == container.VIRNumber).LastOrDefault();

                    var sim = _appicationDbContext.SIMs.FromSql($"SELECT * From SIM Where ContainerNumber = { container.ContainerNo }  and  VIRNumber = {container.VIRNumber}  ").LastOrDefault();

                    if (sim != null && sim.Status == "HO")
                    {
                        continue;
                    }
                    else
                    {

                        //var indexes = _containerIndexRepo.GetList(s => s.VirNo == container.VIRNumber && s.BLNo == container.BLNumber && s.IndexNo == container.IndexNo).LastOrDefault();
                        var cyindexes = _appicationDbContext.CYContainers.FromSql($"SELECT * From CYContainer Where VIRNo = {container.VIRNumber} and ContainerNo = { container.ContainerNo }   and IsDeleted = 0").ToList();


                        cyindexes.ForEach(x => x.IsGrounded = true);
                        indexListcy.AddRange(cyindexes);

                        var srco = new SRC
                        {
                            Weight = container.Weight,
                            VIRNumber = container.VIRNumber,
                            ContainerNumber = container.ContainerNo,
                            Status = container.Status,
                            Category = "I",
                            ActivityType = container.ActivityType,
                            HandlingCode = container.HandlingCode,
                            Location = container.Location,
                            Performed = datetime,
                        };

                        vechilesrcoscy.Add(srco);

                        //var groundedC = new GroundedContainer
                        //{
                        //    GroundedDate = datetime,
                        //    ActivityType = container.ActivityType,
                        //    Category = "I",
                        //    CyContainerId = container.ContainerId,
                        //    HandlingCode = container.HandlingCode,
                        //    Location = container.Location,
                        //    Status = container.Status,
                        //    Weight = container.Weight
                        //};

                        //groundedContainerscy.Add(groundedC);


                    }




                }


                var resdata = _groundedRepo.GetFileNameSRC($"SRC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                if (resdata != true)
                {
                    if (vechilesrcoscy.Count() > 0)
                    {
                        vechilesrcoscy.ForEach(x => x.FileName = $"SRC_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");


                        _srcRepo.AddRange(vechilesrcoscy);

                        //_groundedRepo.AddRange(groundedContainerscy);

                        _cyContainerRepo.UpdateRange(indexListcy);


                        //_appicationDbContext.SaveChanges();
                    }

                }



            }
            catch (Exception e)
            {
                SaveFailedFileMessage("Auto Vechile CY", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);
            }

            #endregion

        }





        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void UpdateIndexDetail()
        {

            try
            {
                #region set cargo type

                var cargotype = _containerIndexRepo.GetList(s => s.CargoType == null);

                if (cargotype.Count() > 0)
                {
                    var indexdetail = new List<ContainerIndex>();
                    foreach (var item in cargotype)
                    {
                        var indexes = _appicationDbContext.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VirNo}  and  IndexNo = {item.IndexNo}  ").ToList();

                        if (indexes.Count() > 0)
                        {
                            var contaubers = _appicationDbContext.ContainerIndices.FromSql($"SELECT * From ContainerIndex Where VirNo = {item.VirNo}  and  ContainerNo = {item.ContainerNo} ").ToList();

                            if (contaubers.Count() > 0)
                            {
                                if (contaubers.Count() == 1)
                                {
                                    item.CargoType = "FCL";

                                    indexdetail.Add(item);
                                }

                                if (contaubers.Count() > 1)
                                {
                                    item.CargoType = "LCL";
                                    indexdetail.Add(item);

                                }


                            }

                        }

                    }

                    _containerIndexRepo.UpdateRange(indexdetail);

                }


                var cargotypecy = _cyContainerRepo.GetList(s => s.CargoType == null);

                if (cargotypecy.Count() > 0)
                {
                    cargotypecy.ToList().ForEach(x => x.CargoType = "FCL");
                    _cyContainerRepo.UpdateRange(cargotypecy);

                }

                #endregion

            }
            catch (Exception e)
            {
                SaveFailedFileMessage("Update Index Info", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

            }


            #region update port and terminal


            try
            {

                var cargotype = _containerIndexRepo.GetList(s => s.PortAndTerminalId == null);


                if (cargotype.Count() > 0)
                {

                    if (cargotype.Where(x => x.VirNo.Substring(0, 4) == "KPPI").ToList().Count() > 0)
                    {

                        var portandterminal = _portAndTerminalRepository.GetAll().Where(x => x.PortName == "QICT").LastOrDefault();
                        if (portandterminal != null)
                        {
                            cargotype.Where(x => x.VirNo.Substring(0, 4) == "KPPI").ToList().ForEach(x => x.PortAndTerminalId = portandterminal.PortAndTerminalId);
                        }

                    }

                    if (cargotype.Where(x => x.VirNo.Substring(0, 4) == "KAPW").ToList().Count() > 0)
                    {
                        var portandterminal = _portAndTerminalRepository.GetAll().Where(x => x.PortName == "KICT").LastOrDefault();
                        if (portandterminal != null)
                        {
                            cargotype.Where(x => x.VirNo.Substring(0, 4) == "KAPW").ToList().ForEach(x => x.PortAndTerminalId = portandterminal.PortAndTerminalId);
                        }
                    }

                    if (cargotype.Where(x => x.VirNo.Substring(0, 4) == "KAPS").ToList().Count() > 0)
                    {

                        var portandterminal = _portAndTerminalRepository.GetAll().Where(x => x.PortName == "SAPT").LastOrDefault();
                        if (portandterminal != null)
                        {
                            cargotype.Where(x => x.VirNo.Substring(0, 4) == "KAPS").ToList().ForEach(x => x.PortAndTerminalId = portandterminal.PortAndTerminalId);
                        }

                    }

                    if (cargotype.Where(x => x.VirNo.Substring(0, 4) == "KAPE").ToList().Count() > 0)
                    {

                        var portandterminal = _portAndTerminalRepository.GetAll().Where(x => x.PortName == "KGTL").LastOrDefault();
                        if (portandterminal != null)
                        {
                            cargotype.Where(x => x.VirNo.Substring(0, 4) == "KAPE").ToList().ForEach(x => x.PortAndTerminalId = portandterminal.PortAndTerminalId);
                        }
                    }

                    cargotype.ToList().RemoveAll(x => x.PortAndTerminalId == null);

                    _containerIndexRepo.UpdateRange(cargotype);

                }


            }
            catch (Exception e)
            {
                SaveFailedFileMessage("Update Index Info FOr Port And terminal", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

            }
            #endregion

            #region update port and terminal cy


            try
            {

                var cargotypecy = _cyContainerRepo.GetList(s => s.PortAndTerminalId == null);


                if (cargotypecy.Count() > 0)
                {

                    if (cargotypecy.Where(x => x.VIRNo.Substring(0, 4) == "KPPI").ToList().Count() > 0)
                    {

                        var portandterminal = _portAndTerminalRepository.GetAll().Where(x => x.PortName == "QICT").LastOrDefault();
                        if (portandterminal != null)
                        {
                            cargotypecy.Where(x => x.VIRNo.Substring(0, 4) == "KPPI").ToList().ForEach(x => x.PortAndTerminalId = portandterminal.PortAndTerminalId);
                        }

                    }

                    if (cargotypecy.Where(x => x.VIRNo.Substring(0, 4) == "KAPW").ToList().Count() > 0)
                    {
                        var portandterminal = _portAndTerminalRepository.GetAll().Where(x => x.PortName == "KICT").LastOrDefault();
                        if (portandterminal != null)
                        {
                            cargotypecy.Where(x => x.VIRNo.Substring(0, 4) == "KAPW").ToList().ForEach(x => x.PortAndTerminalId = portandterminal.PortAndTerminalId);
                        }
                    }

                    if (cargotypecy.Where(x => x.VIRNo.Substring(0, 4) == "KAPS").ToList().Count() > 0)
                    {

                        var portandterminal = _portAndTerminalRepository.GetAll().Where(x => x.PortName == "SAPT").LastOrDefault();
                        if (portandterminal != null)
                        {
                            cargotypecy.Where(x => x.VIRNo.Substring(0, 4) == "KAPS").ToList().ForEach(x => x.PortAndTerminalId = portandterminal.PortAndTerminalId);
                        }

                    }

                    if (cargotypecy.Where(x => x.VIRNo.Substring(0, 4) == "KAPE").ToList().Count() > 0)
                    {

                        var portandterminal = _portAndTerminalRepository.GetAll().Where(x => x.PortName == "KGTL").LastOrDefault();
                        if (portandterminal != null)
                        {
                            cargotypecy.Where(x => x.VIRNo.Substring(0, 4) == "KAPE").ToList().ForEach(x => x.PortAndTerminalId = portandterminal.PortAndTerminalId);
                        }
                    }

                    cargotypecy.ToList().RemoveAll(x => x.PortAndTerminalId == null);

                    _cyContainerRepo.UpdateRange(cargotypecy);

                }


            }
            catch (Exception e)
            {
                SaveFailedFileMessage("Update Index Info FOr Port And terminal cy", e.InnerException != null ? e.InnerException.InnerException.Message : e.Message);

            }
            #endregion


        }

        [DisableConcurrentExecution(timeoutInSeconds: 10 * 60)]
        public void ReProcessGIIO(string rootPath)
        {


            var datetime = DateTime.Now;

            var newediPath = Path.Combine(rootPath, "EDIFiles");

            EDIFileReader.PATH = newediPath;

            try
            {

                #region AutoProcessinfGIIO 
                try
                {

                    var resdata = _containerRepo.GetFileNameGIIO($"GIIO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}");

                    var filename = $"GIIO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}";
                    if (resdata == false)
                    {
                        var _auto = EDIFileReader.GIIOAutoReProcessing(filename);

                        _giioRepo.AddRange(_auto);

                    }

                }
                catch (Exception e)
                {

                    SaveFailedFileMessage("GIIO", e.Message);

                }
                #endregion


            }
            catch (Exception e)
            {
                throw;
            }

        }


    }
}
