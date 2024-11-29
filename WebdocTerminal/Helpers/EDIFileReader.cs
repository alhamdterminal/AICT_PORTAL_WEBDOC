using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Data;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Helpers
{


    public class EDIFileReader
    {
        public static string PATH { get; set; }

        public static string ConnString { get; set; }

        public static List<string> AllFileNames { get; set; }

        public static FileInfo[] CurrentFiles { get; set; }

        public static T[] ConcatArrays<T>(params T[][] list)
        {
            var result = new T[list.Sum(a => a.Length)];

            return result;
        }

        public static InterportIntraportViewModel InterPortIntraPort()
        {
            var viro = VIROReadFile();

            var igmo = IGMOReadFile();

            var ipao = IPAOReadFile();

            return new InterportIntraportViewModel
            {
                VIROFiles = viro,
                IGMOFiles = igmo,
                IPAOFiles = ipao
            };
        }

        public static CYFLViewModel CYFCLCYLCL()
        {
            var gdi = GDIReadFile();

            var ihc = IHCReadFile();

            var ecm = ECMReadFile();

            var crl = ReleaseMessageReadFile();

            return new CYFLViewModel
            {
                GDIFiles = gdi,
                IHCFiles = ihc,
                ECMFiles = ecm,
                CRLFIles = crl
            };
        }

        public static CFSLCLViewModel CFSLCL()
        {
            var gdio = GDIOReadFile();
            var ecmo = ECMOReadFile();
            var crlo = CRLOReadFile();
            var gtoo = GTOOReadFile();

            return new CFSLCLViewModel
            {
                GDIOFiles = gdio,
                ECMOFiles = ecmo,
                CRLOFiles = crlo,
                GTOOFiles = gtoo
            };

        }


        public static List<VIRO> VIROReadFile()
        {
            var _viroFiles = new List<VIRO>();

            return _viroFiles;
        }

        public static List<IPAO> IPAOReadFile()
        {

            var _ipaoFiles = new List<IPAO>();

            return _ipaoFiles;
        }

        public static List<GIIO> GIIOReadFile()
        {

            var _giioFiles = new List<GIIO>();

            return _giioFiles;
        }

        public static List<IGMO> IGMOReadFile()
        {
            var _igmoFiles = new List<IGMO>();

            return _igmoFiles;

        }

        public static List<GateInViewModel> GIIOFOrIncommingReadFile()
        {
            var _igmoFiles = new List<GateInViewModel>();

            return _igmoFiles;

        }

        public static List<DestuffingViewModelForTTSOUpload> TTSOFOrIncommingReadFile()
        {
            var _igmoFiles = new List<DestuffingViewModelForTTSOUpload>();

            return _igmoFiles;

        }

        public static List<ContainerIndexDetail> CYIGMODetailFOrIncommingReadFile()
        {
            var _igmoFiles = new List<ContainerIndexDetail>();

            return _igmoFiles;

        }


        public static List<ContainerIndexDetail> CFSIGMODetailFOrIncommingReadFile()
        {
            var _igmoFiles = new List<ContainerIndexDetail>();

            return _igmoFiles;

        }

        public static List<ContainerIndexDetail> CFSIGMODetailIndexesFOrIncommingReadFile()
        {
            var _igmoFiles = new List<ContainerIndexDetail>();

            return _igmoFiles;

        }

        public static List<CFSGroundingViewModel> SRCOfordataReadFile()
        {
            var _igmoFiles = new List<CFSGroundingViewModel>();

            return _igmoFiles;

        }


        public static List<CYGroundingViewModel> SRCfordataReadFile()
        {
            var _igmoFiles = new List<CYGroundingViewModel>();

            return _igmoFiles;

        }

        public static List<ContainerIndex> AuctionMarkCFSfordataReadFile()
        {
            var _igmoFiles = new List<ContainerIndex>();

            return _igmoFiles;

        }

        public static List<CYContainer> AuctionMarkCYfordataReadFile()
        {
            var _igmoFiles = new List<CYContainer>();

            return _igmoFiles;

        }


        public static List<OtherChargeAssigning> CFSOtherchargesReadFile()
        {
            var _igmoFiles = new List<OtherChargeAssigning>();

            return _igmoFiles;

        }
        public static List<OtherChargeAssigning> CYOtherchargesReadFile()
        {
            var _igmoFiles = new List<OtherChargeAssigning>();

            return _igmoFiles;

        }


        public static List<BRT> CFSBRTReadFile()
        {
            var _igmoFiles = new List<BRT>();

            return _igmoFiles;

        }
        public static List<BrtItem> CFSBRTDEtailReadFile()
        {
            var _igmoFiles = new List<BrtItem>();

            return _igmoFiles;

        }
        public static List<BRT> CYBRTReadFile()
        {
            var _igmoFiles = new List<BRT>();

            return _igmoFiles;

        }
        public static List<SIM> SIMReadFile()
        {
            var _simFiles = new List<SIM>();

            return _simFiles;

        }

        public static List<SIMO> SIMOReadFile()
        {
            var _simoFiles = new List<SIMO>();

            return _simoFiles;

        }



        public static List<OSIM> OSIMReadFile()
        {
            var _osimFiles = new List<OSIM>();

            return _osimFiles;

        }



        public static List<GDIO> GDIOReadFile()
        {
            var _gdioFiles = new List<GDIO>();

            return _gdioFiles;
        }

        public static List<CCMO> CCMOReadFile()
        {
            var _ccmoFiles = new List<CCMO>();

            return _ccmoFiles;
        }

        public static List<SCMO> SCMOReadFile()
        {
            var _scmoFiles = new List<SCMO>();

            return _scmoFiles;
        }

        public static List<SVMO> SVMOReadFile()
        {
            var _svmoFiles = new List<SVMO>();

            return _svmoFiles;
        }


        public static List<SVM> SVMReadFile()
        {
            var _svmFiles = new List<SVM>();

            return _svmFiles;
        }

        public static List<TTSO> TTSOReadFile()
        {
            var _ttsoFiles = new List<TTSO>();

            return _ttsoFiles;
        }

        public static List<ECMO> ECMOReadFile()
        {
            var _ecmoFiles = new List<ECMO>();

            return _ecmoFiles;
        }
        public static List<CRL> CRLReadFile()
        {

            var _crlFiles = new List<CRL>();

            return _crlFiles;

        }

        public static List<CRLO> CRLOReadFile()
        {

            var _crloFiles = new List<CRLO>();

            return _crloFiles;

        }

        public static List<GTOO> GTOOReadFile()
        {

            var _gtooFiles = new List<GTOO>();

            return _gtooFiles;

        }

        public static List<GDI> GDIReadFile()
        {

            var _gdiFiles = new List<GDI>();

            return _gdiFiles;
        }

        public static List<IHC> IHCReadFile()
        {
            var _ihcFiles = new List<IHC>();

            return _ihcFiles;
        }

        public static List<IHCO> IHCOReadFile()
        {
            var _ihcoFiles = new List<IHCO>();

            return _ihcoFiles;
        }

        public static List<ECM> ECMReadFile()
        {
            var _ecmFiles = new List<ECM>();

            return _ecmFiles;
        }

        public static List<OPIA> OPIAReadFile()
        {
            var _opiaFiles = new List<OPIA>();

            return _opiaFiles.OrderBy(s => s.RecordSequence).ToList();
        }


        public static List<OGDE> OGDEReadFile()
        {
            var _ogdeiles = new List<OGDE>();

            return _ogdeiles.OrderBy(s => s.RecordSequence).ToList();
        }

        public static List<OEHC> OEHCReadFile()
        {
            var _oehcFiles = new List<OEHC>();

            return _oehcFiles;
        }

        public static List<OCRL> OCRLReadFile()
        {
            var _ocrlFiles = new List<OCRL>();

            return _ocrlFiles;
        }

        public static List<OSVM> OSVMReadFile()
        {
            var _osvmFiles = new List<OSVM>();

            return _osvmFiles;
        }

        public static List<OVAM> OVAMReadFile()
        {
            var _ovamFiles = new List<OVAM>();

            return _ovamFiles;
        }

        public static List<OECM> OECMReadFile()
        {
            var _oecmFiles = new List<OECM>();

            return _oecmFiles;
        }

        public static List<CRL> ReleaseMessageReadFile()
        {
            var _crlFiles = new List<CRL>();

            return _crlFiles;
        }

        public static List<ACK> ACKReadFile()
        {
            var _ackFiles = new List<ACK>();

            return _ackFiles;
        }




        public static List<GIIO> GIIOAutoReProcessing(string filename)
        {

            var _giioFiles = new List<GIIO>();

            return _giioFiles;
        }

    }
}
