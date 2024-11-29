using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class OPIARepository : RepoBase<OPIA>, IOPIARepository
    {
        public OPIARepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public void AddAllEDIMessages(OPIA model)
        {
            Table.Add(model);

            var ogde = new OGDE
            {
                GDNumber = model.GDNumber,
                MessageFrom = "Manual",
                MessageTo = model.MessageTo,
                OperationType = "A"
            };

            Db.OGDEs.Add(ogde);

            var oehc = new OEHC
            {
                GDNumber = model.GDNumber,
                MessageFrom = "Manual",
                MessageTo = model.MessageTo

            };

            Db.OEHCs.Add(oehc);

            var oecm = new OECM
            {
                GDNumber = model.GDNumber,
                MessageFrom = "Manual",
                MessageTo = model.MessageTo


            };

            Db.OECMs.Add(oecm);

            var ocrl = new OCRL
            {
                GDNumber = model.GDNumber,
                MessageFrom = "Manual",
                MessageTo = model.MessageTo,
                Status = "AL"


            };

            Db.OCRLs.Add(ocrl);


            //var osvms = new OSVM
            //{
            //    GDNumber = model.GDNumber,
            //    MessageFrom = "Manual",
            //    MessageTo = model.MessageTo,
            //    OPType = "A"
            //};

            //Db.OSVMs.Add(osvms);

            Db.SaveChanges();

        }

        public long UpcomingGDsCount()
        {
            var data = (from opia in Db.OPIAs
                        where !Db.OGIEs.Any(s => s.GDNumber.Trim().ToUpper() == opia.GDNumber.Trim().ToUpper())
                        select opia).ToList();

            if (data == null)
            {
                return 0;
            }

            return data.Count;
        }

        public List<OPIA> GetUnAssociatedGds()
        {

            var data = Db.OPIAs.FromSql($" select opias.* from opias left join exportcontaineritem on opias.gdnumber  =  exportcontaineritem.GDNumber  and  exportcontaineritem.IsDeleted = 0 where  exportcontaineritem.GDNumber is  null and OPIAs.IsDeleted = 0 ").ToList();

            return data;

        }


        public OPIA GetOPIAByGD(string gdnumber)
        {

            var data = Db.OPIAs.FromSql($" select * from opias   where   GDNumber  = {gdnumber} and MessageFrom != 'Manual'  and  IsDeleted = 0 ").LastOrDefault();

            return data;

        }


        public ExportContainer GetExportContainerById(long exportcontainerid)
        {

            var data = Db.ExportContainers.FromSql($" select * from ExportContainers   where   ExportContainerId  = {exportcontainerid}  and  IsDeleted = 0 ").LastOrDefault();

            return data;

        }


        public List<OPIA> GetOpiaslist()
        {
            var opiaslist = (from OPIA in Db.OPIAs
                             where
                             OPIA.MessageFrom == "Manual"
                             &&
                               !Db.ExportContainerItems.Any(s => s.GDNumber == OPIA.GDNumber && s.IsGateOut == true)
                               &&
                               !Db.OCRLs.Any(s => s.GDNumber == OPIA.GDNumber && s.Status == "OD")
                             select OPIA).ToList();
            return opiaslist;


        }

        public List<OGDE> Getogdelist()
        {
            var ogdeslist = (from ogde in Db.OGDEs
                             where
                             ogde.MessageFrom == "Manual"
                             &&
                               !Db.ExportContainerItems.Any(s => s.GDNumber == ogde.GDNumber && s.IsGateOut == true)
                               &&
                                !Db.OCRLs.Any(s => s.GDNumber == ogde.GDNumber && s.Status == "OD")
                             select ogde).ToList();
            return ogdeslist;

        }

        public List<OEHC> Getoehcslist()
        {
            var oehcslist = (from oehc in Db.OEHCs
                             where
                             oehc.MessageFrom == "Manual"
                             &&
                               !Db.ExportContainerItems.Any(s => s.GDNumber == oehc.GDNumber && s.IsGateOut == true)
                               &&
                                !Db.OCRLs.Any(s => s.GDNumber == oehc.GDNumber && s.Status == "OD")
                             select oehc).ToList();
            return oehcslist;

        }
        public List<OECM> Getoecmslist()
        {
            var oecmslist = (from oecms in Db.OECMs
                             where
                             oecms.MessageFrom == "Manual"
                             &&
                               !Db.ExportContainerItems.Any(s => s.GDNumber == oecms.GDNumber && s.IsGateOut == true)
                               &&
                                !Db.OCRLs.Any(s => s.GDNumber == oecms.GDNumber && s.Status == "OD")
                             select oecms).ToList();
            return oecmslist;

        }

        public List<OCRL> Getocrlslist()
        {
            var ocrlslist = (from ocrls in Db.OCRLs
                             where
                             ocrls.MessageFrom == "Manual"
                             &&
                               !Db.ExportContainerItems.Any(s => s.GDNumber == ocrls.GDNumber && s.IsGateOut == true)
                               &&
                                !Db.OCRLs.Any(s => s.GDNumber == ocrls.GDNumber && s.Status == "OD")
                             select ocrls).ToList();
            return ocrlslist;

        }


        public List<OSVM> Getosvmslist()
        {
            var osvmslist = (from osvms in Db.OSVMs
                             where
                             osvms.MessageFrom == "Manual"
                             &&
                               !Db.ExportContainerItems.Any(s => s.GDNumber == osvms.GDNumber && s.IsGateOut == true)
                               &&
                                !Db.OCRLs.Any(s => s.GDNumber == osvms.GDNumber && s.Status == "OD")
                             select osvms).ToList();
            return osvmslist;

        }

        public List<OPIA> GetNotGateInOPIAsList()
        {
            var data = (from opia in Db.OPIAs
                        where !Db.GateInExports.Any(s => s.GDNumber.Trim().ToUpper() == opia.GDNumber.Trim().ToUpper())
                        select opia).ToList();
            return data;

        }

        public List<OPIA> GetOpiaByGdnumber(string gdnumber)
        {
            var res = Db.OPIAs.FromSql($" select * from OPIAs   where   GDNumber  = {gdnumber}  and  IsDeleted = 0 ").ToList();

            return res;
        }

        public OPIA GeLastGdnumberInfo(string gdnumber)
        {
            var res = Db.OPIAs.FromSql($" select * from OPIAs   where   GDNumber  = {gdnumber}  and  IsDeleted = 0 ").LastOrDefault();

            return res;
        }

        public List<OSVM> GetPenddingosvms()
        {
            var osvmslist = (from osvms in Db.OSVMs
                             where
                             osvms.MessageFrom == "Manual"
                             &&
                               !Db.ExportContainerItems.Any(s => s.GDNumber == osvms.GDNumber && s.ContainerNumber == osvms.ContainerNo && s.IsGateOut == true)
                             select osvms).ToList();
            return osvmslist;

        }
    }
}
