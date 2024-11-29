using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class ACKRepository : RepoBase<ACK>, IACKRepository
    {
        public ACKRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<ExportFilesViewModel> getExportfilesBygdnumer(string GDNumber)
        {
            var datalist = new List<ExportFilesViewModel>();

            var opias = (from opia in Db.OPIAs where opia.GDNumber == GDNumber
                         select new ExportFilesViewModel {
                             GDNumber = opia.GDNumber,
                             FileName = opia.FileName,
                             Performerd = opia.Performed
                         }).ToList();

            var ogies = (from ogie in Db.OGIEs
                         where ogie.GDNumber == GDNumber
                         select new ExportFilesViewModel
                         {
                             GDNumber = ogie.GDNumber,
                             FileName = ogie.FileName,
                             Performerd = ogie.Performed
                         }).ToList();


            var OGDEs = (from ogde in Db.OGDEs
                         where ogde.GDNumber == GDNumber
                         select new ExportFilesViewModel
                         {
                             GDNumber = ogde.GDNumber,
                             FileName = ogde.FileName,
                             Performerd = ogde.Performed
                         }).ToList();

            var oehcs = (from oehc in Db.OEHCs
                         where oehc.GDNumber == GDNumber
                         select new ExportFilesViewModel
                         {
                             GDNumber = oehc.GDNumber,
                             FileName = oehc.FileName,
                             Performerd = oehc.Performed
                         }).ToList();
            
            var oscrs = (from oscr in Db.OSRCs
                         where oscr.GDNumber == GDNumber
                         select new ExportFilesViewModel
                         {
                             GDNumber = oscr.GDNumber,
                             FileName = oscr.FileName,
                             Performerd = oscr.Performed
                         }).ToList(); 
            
            var oecms = (from oecm in Db.OECMs
                         where oecm.GDNumber == GDNumber
                         select new ExportFilesViewModel
                         {
                             GDNumber = oecm.GDNumber,
                             FileName = oecm.FileName,
                             Performerd = oecm.Performed
                         }).ToList();

            var ocrls = (from ocrl in Db.OCRLs
                         where ocrl.GDNumber == GDNumber
                         select new ExportFilesViewModel
                         {
                             GDNumber = ocrl.GDNumber,
                             FileName = ocrl.FileName,
                             Performerd = ocrl.Performed
                         }).ToList();

            var ogdcs = (from ogdc in Db.OGDCs
                         where ogdc.GDNumber == GDNumber
                         select new ExportFilesViewModel
                         {
                             GDNumber = ogdc.GDNumber,
                             FileName = ogdc.FileName,
                             Performerd = null
                         }).ToList();

            var osvms = (from osvm in Db.OSVMs
                         where osvm.GDNumber == GDNumber
                         select new ExportFilesViewModel
                         {
                             GDNumber = osvm.GDNumber,
                             FileName = osvm.FileName,
                             Performerd = osvm.Performed
                         }).ToList();

            var ogtes = (from ogte in Db.OGTEs
                         where ogte.GDNumber == GDNumber
                         select new ExportFilesViewModel
                         {
                             GDNumber = ogte.GDNumber,
                             FileName = ogte.FileName,
                             Performerd = ogte.GateOutTime
                         }).ToList();

            datalist.AddRange(opias);
            datalist.AddRange(ogies);
            datalist.AddRange(OGDEs);
            datalist.AddRange(oehcs);
            datalist.AddRange(oscrs);
            datalist.AddRange(oecms);
            datalist.AddRange(ocrls);
            datalist.AddRange(ogdcs);
            datalist.AddRange(osvms);
            datalist.AddRange(ogtes);
            
            return datalist;
        }



        public List<ImportFielsViewModel> getImportfilesByvirNumberCFS(string virNumber)
        {

            var dataListImport = new List<ImportFielsViewModel>();

            var viros = (from viro in Db.VIROs
                         where viro.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = viro.VIRNumber,
                             FileName = viro.FileName,
                             ContainerNumber = "",
                             BlNumber = "",
                             Performerd = viro.Performed
                         }).ToList();

            var igmos = (from igmo in Db.IGMOs
                         where igmo.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = igmo.VIRNumber,
                             FileName = igmo.FileName,
                             ContainerNumber = igmo.ContainerNumber,
                             BlNumber = igmo.BLNumber,
                             Performerd = igmo.Performed
                         }).ToList();


            var ipaos = (from ipao in Db.IPAOs
                         where ipao.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = ipao.VIRNumber,
                             FileName = ipao.FileName,
                             ContainerNumber = ipao.ContainerNumber,
                             BlNumber = "",
                             Performerd = ipao.Performed
                         }).ToList();
             
            var gdios = (from gdio in Db.GDIOs
                         where gdio.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = gdio.VIRNumber,
                             FileName = gdio.FileName,
                             ContainerNumber = "",
                             BlNumber = gdio.BLNumber,
                             Performerd = gdio.Performed
                         }).ToList();

            var giios = (from giio in Db.GIIOs
                         where giio.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = giio.VIRNumber,
                             FileName = giio.FileName,
                             ContainerNumber = giio.ContainerNumber,
                             BlNumber = "",
                             Performerd = giio.Performed
                         }).ToList();

             
            var ihcos = (from ihco in Db.IHCOs
                         where ihco.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = ihco.VIRNumber,
                             FileName = ihco.FileName,
                             ContainerNumber = "",
                             BlNumber = ihco.BLNumber,
                             Performerd = ihco.Performed
                         }).ToList();
            
             

            var srcos = (from srco in Db.SRCOs
                         where srco.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = srco.VIRNumber,
                             FileName = srco.FileName,
                             ContainerNumber = "",
                             BlNumber = srco.BLNumber,
                             Performerd = srco.Performed
                         }).ToList();
             
            var ecmos = (from ecmo in Db.ECMOs
                         where ecmo.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = ecmo.VIRNumber,
                             FileName = ecmo.FileName,
                             ContainerNumber = "",
                             BlNumber = ecmo.BLNumber,
                             Performerd = ecmo.Performed
                         }).ToList();
             
            var crlos = (from crlo in Db.CRLOs
                         where crlo.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = crlo.VIRNumber,
                             FileName = crlo.FileName,
                             ContainerNumber = "",
                             BlNumber = crlo.BLNumber,
                             Performerd = crlo.Performed
                         }).ToList();

             
            var gtoos = (from gtoo in Db.GTOOs
                         where gtoo.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = gtoo.VIRNumber,
                             FileName = gtoo.FileName,
                             BlNumber =  gtoo.BLNumber,
                             ContainerNumber =gtoo.ContainerNumber,
                             Performerd = gtoo.CreateDT
                         }).ToList();

             
            var simos = (from simo in Db.SIMOs
                         where simo.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = simo.VIRNumber,
                             FileName = simo.FileName,
                             BlNumber = simo.BLNumber,
                             ContainerNumber = "",
                             Performerd = simo.CreateDT
                         }).ToList();



            dataListImport.AddRange(viros);
            dataListImport.AddRange(igmos);
            dataListImport.AddRange(ipaos);
            dataListImport.AddRange(giios);
            dataListImport.AddRange(gdios);
            dataListImport.AddRange(ihcos);
            dataListImport.AddRange(srcos);
            dataListImport.AddRange(ecmos);
            dataListImport.AddRange(crlos);
            dataListImport.AddRange(gtoos);
            dataListImport.AddRange(simos);

            return dataListImport;
        }

        public List<ImportFielsViewModel> getImportfilesByvirNumberCY(string virNumber)
        {

            var dataListImport = new List<ImportFielsViewModel>();

            var viros = (from viro in Db.VIROs
                         where viro.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = viro.VIRNumber,
                             FileName = viro.FileName,
                             ContainerNumber = "",
                             BlNumber = "",
                             Performerd = viro.Performed
                         }).ToList();

            var igmos = (from igmo in Db.IGMOs
                         where igmo.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = igmo.VIRNumber,
                             FileName = igmo.FileName,
                             ContainerNumber = igmo.ContainerNumber,
                             BlNumber = igmo.BLNumber,
                             Performerd = igmo.Performed
                         }).ToList();


            var ipaos = (from ipao in Db.IPAOs
                         where ipao.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = ipao.VIRNumber,
                             FileName = ipao.FileName,
                             ContainerNumber = ipao.ContainerNumber,
                             BlNumber = "",
                             Performerd = ipao.Performed
                         }).ToList();


            var gdis = (from gdi in Db.GDIs
                        where gdi.VIRNumber == virNumber
                        select new ImportFielsViewModel
                        {
                            VirNumber = gdi.VIRNumber,
                            FileName = gdi.FileName,
                            BlNumber = gdi.BLNumber,
                            ContainerNumber = gdi.ContainerNumber,
                            Performerd = gdi.Performed
                        }).ToList();
             
            var giios = (from giio in Db.GIIOs
                         where giio.VIRNumber == virNumber
                         select new ImportFielsViewModel
                         {
                             VirNumber = giio.VIRNumber,
                             FileName = giio.FileName,
                             ContainerNumber = giio.ContainerNumber,
                             BlNumber = "",
                             Performerd = giio.Performed
                         }).ToList();

            var ihcs = (from ihc in Db.IHCs
                        where ihc.VIRNumber == virNumber
                        select new ImportFielsViewModel
                        {
                            VirNumber = ihc.VIRNumber,
                            FileName = ihc.FileName,
                            ContainerNumber = ihc.ContainerNumber,
                            BlNumber = "",
                            Performerd = ihc.Performed
                        }).ToList();


            var srcs = (from src in Db.SRCs
                        where src.VIRNumber == virNumber
                        select new ImportFielsViewModel
                        {
                            VirNumber = src.VIRNumber,
                            FileName = src.FileName,
                            ContainerNumber = src.ContainerNumber,
                            BlNumber = "",
                            Performerd = src.Performed
                        }).ToList();

            var ecms = (from ecm in Db.ECMs
                        where ecm.VIRNumber == virNumber
                        select new ImportFielsViewModel
                        {
                            VirNumber = ecm.VIRNumber,
                            FileName = ecm.FileName,
                            ContainerNumber = ecm.ContainerNumber,
                            BlNumber = "",
                            Performerd = ecm.Performed
                        }).ToList();

            var crls = (from crl in Db.CRLs
                        where crl.VIRNumber == virNumber
                        select new ImportFielsViewModel
                        {
                            VirNumber = crl.VIRNumber,
                            FileName = crl.FileName,
                            ContainerNumber = crl.ContainerNumber,
                            BlNumber = "",
                            Performerd = crl.Performed
                        }).ToList();

            var gtos = (from gto in Db.GTOs
                        where gto.VIRNumber == virNumber
                        select new ImportFielsViewModel
                        {
                            VirNumber = gto.VIRNumber,
                            FileName = gto.FileName,
                            ContainerNumber = gto.ContainerNumber,
                            BlNumber = "",
                            Performerd = gto.CreateDT
                        }).ToList();

            var sims = (from sim in Db.SIMs
                        where sim.VIRNumber == virNumber
                        select new ImportFielsViewModel
                        {
                            VirNumber = sim.VIRNumber,
                            FileName = sim.FileName,
                            ContainerNumber =  sim.ContainerNumber,
                            BlNumber = "",
                            Performerd = sim.CreateDT
                        }).ToList();
             

            dataListImport.AddRange(viros);
            dataListImport.AddRange(igmos);
            dataListImport.AddRange(ipaos);
            dataListImport.AddRange(giios);
            dataListImport.AddRange(gdis);             
            dataListImport.AddRange(ihcs);
            dataListImport.AddRange(srcs);
            dataListImport.AddRange(ecms);
            dataListImport.AddRange(crls);
            dataListImport.AddRange(gtos);
            dataListImport.AddRange(sims);
             
            return dataListImport;
        }



        public List<ImportFielsViewModel> getfilesInfo(string VIRNumber, string ContainerNo, string gdnumber, string BLno)
        {

            var dataListImport = new List<ImportFielsViewModel>();

            if(VIRNumber != null)
            {
                var viros = (from viro in Db.VIROs
                             where
                              viro.VIRNumber == VIRNumber
                             select new ImportFielsViewModel
                             {
                                 //VirNumber = viro.VIRNumber,
                                 FileName = viro.FileName,
                                 //ContainerNumber = "",
                                 //BlNumber = "",
                                 //GDNumber = "",
                                 //Type = "VIRO",
                                 //Performerd = viro.Performed
                             }).ToList();

                dataListImport.AddRange(viros);
            }

            if (VIRNumber != null || ContainerNo != null || BLno != null)
            {
                var igmos = (from igmo in Db.IGMOs
                             where
                             (igmo.VIRNumber == VIRNumber  || VIRNumber == null )
                             && (igmo.ContainerNumber == ContainerNo || ContainerNo == null)
                             && (igmo.BLNumber == BLno || BLno == null)
                             select new ImportFielsViewModel
                             {
                                 //VirNumber = igmo.VIRNumber,
                                 FileName = igmo.FileName,
                                 //ContainerNumber = igmo.ContainerNumber,
                                 //BlNumber = igmo.BLNumber,
                                 //GDNumber = "",
                                 //Type = igmo.Status,
                                 //Performerd = igmo.Performed

                             }).ToList();

                dataListImport.AddRange(igmos);
            }

             if(VIRNumber != null || ContainerNo != null)
            {
                var ipaos = (from ipao in Db.IPAOs
                             where
                            ( ipao.VIRNumber == VIRNumber  || VIRNumber  == null )
                         & (ipao.ContainerNumber == ContainerNo || ContainerNo == null)
                             select new ImportFielsViewModel
                             {
                                 //VirNumber = ipao.VIRNumber,
                                 FileName = ipao.FileName,
                                 //ContainerNumber = ipao.ContainerNumber,
                                 //BlNumber = "",
                                 //GDNumber = "",
                                 //Type = "IPAO",
                                 //Performerd = ipao.Performed
                             }).ToList();
                dataListImport.AddRange(ipaos);

            }


            if (VIRNumber != null || gdnumber != null || BLno != null)
            {
                var gdios = (from gdio in Db.GDIOs
                             where
                              (gdio.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                             && (gdio.BLNumber == BLno || BLno == null || BLno == "")
                             && (gdio.CRN == gdnumber || gdnumber == null || gdnumber == "")
                             select new ImportFielsViewModel
                             {
                                 //VirNumber = gdio.VIRNumber,
                                 FileName = gdio.FileName,
                                 //ContainerNumber = "",
                                 //BlNumber = gdio.BLNumber,
                                 //GDNumber = gdio.CRN,
                                 //Type = "GDIO",
                                 //Performerd = gdio.Performed

                             }).ToList();

                dataListImport.AddRange(gdios);

            }



            if (VIRNumber != null   || BLno != null)
            {
                var ihcos = (from ihco in Db.IHCOs
                             where
                              (ihco.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                             && (ihco.BLNumber == BLno || BLno == null || BLno == "")
                             select new ImportFielsViewModel
                             {
                                 //VirNumber = ihco.VIRNumber,
                                 FileName = ihco.FileName,
                                 //ContainerNumber = "",
                                 //BlNumber = ihco.BLNumber,
                                 //GDNumber = "",
                                 //Type = "IHCO",
                                 //Performerd = ihco.Performed

                             }).ToList();

                dataListImport.AddRange(ihcos);

            }


            if (VIRNumber != null || BLno != null)
            {
                var ecmos = (from ecmo in Db.ECMOs
                             where
                               (ecmo.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                             && (ecmo.BLNumber == BLno || BLno == null || BLno == "")
                             select new ImportFielsViewModel
                             {
                                 //VirNumber = ecmo.VIRNumber,
                                 FileName = ecmo.FileName,
                                 //ContainerNumber = "",
                                 //BlNumber = ecmo.BLNumber,
                                 //GDNumber = "",
                                 //Type = "ECMO",
                                 //Performerd = ecmo.Performed

                             }).ToList();

                dataListImport.AddRange(ecmos);

            }



            if (VIRNumber != null || BLno != null || gdnumber != null)
            {
                var crlos = (from crlo in Db.CRLOs
                             where
                              (crlo.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                             && (crlo.BLNumber == BLno || BLno == null || BLno == "")
                             && (crlo.GDNumber == gdnumber || gdnumber == null || gdnumber == "")
                             select new ImportFielsViewModel
                             {

                                 //VirNumber = crlo.VIRNumber,
                                 FileName = crlo.FileName,
                                 //ContainerNumber = "",
                                 //BlNumber = crlo.BLNumber,
                                 //GDNumber = crlo.GDNumber,
                                 //Type = "CRLO",
                                 //Performerd = crlo.Performed

                             }).ToList();

                dataListImport.AddRange(crlos);

            }

            if (VIRNumber != null || ContainerNo != null  )
            {
                var svmos = (from svmo in Db.SVMOs
                             where
                              (svmo.VIRNo == VIRNumber || VIRNumber == null || VIRNumber == "")
                             && (svmo.ContainerNo == ContainerNo || ContainerNo == null || ContainerNo == "")
                             select new ImportFielsViewModel
                             {

                                 //VirNumber = svmo.VIRNo,
                                 FileName = svmo.FileName,
                                 //ContainerNumber = svmo.ContainerNo,
                                 //BlNumber = "",
                                 //GDNumber = "",
                                 //Type = "SVMO",
                                 //Performerd = svmo.Performed

                             }).ToList();

                dataListImport.AddRange(svmos);

            }

            if (VIRNumber != null || BLno != null)
            {
                var simos = (from simo in Db.SIMOs
                             where
                             (simo.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                             && (simo.BLNumber == BLno || BLno == null || BLno == "")
                             select new ImportFielsViewModel
                             {

                                 //VirNumber = simo.VIRNumber,
                                 FileName = simo.FileName,
                                 //ContainerNumber = "",
                                 //BlNumber = simo.BLNumber,
                                 //GDNumber = "",
                                 //Type = "SIMO",
                                 //Performerd = simo.Performed

                             }).ToList();

                dataListImport.AddRange(simos);

            }



            if (VIRNumber != null || ContainerNo != null || BLno != null)
            {
                var gdis = (from gdi in Db.GDIs
                            where

                            (gdi.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                             && (gdi.BLNumber == BLno || BLno == null || BLno == "")
                             && (gdi.ContainerNumber == ContainerNo || ContainerNo == null || ContainerNo == "")

                            select new ImportFielsViewModel
                            {
                                //VirNumber = gdi.VIRNumber,
                                FileName = gdi.FileName,
                                //ContainerNumber = gdi.ContainerNumber,
                                //BlNumber = gdi.BLNumber,
                                //GDNumber = "",
                                //Type = "GDI",
                                //Performerd = gdi.Performed

                            }).ToList();

                dataListImport.AddRange(gdis);

            }


            if (VIRNumber != null || ContainerNo != null)
            {
                var ihcs = (from ihc in Db.IHCs
                            where
                             (ihc.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                              && (ihc.ContainerNumber == ContainerNo || ContainerNo == null || ContainerNo == "")

                            select new ImportFielsViewModel
                            {
                                //VirNumber = ihc.VIRNumber,
                                FileName = ihc.FileName,
                                //ContainerNumber = ihc.ContainerNumber,
                                //BlNumber = "",
                                //GDNumber = "",
                                //Type = "IHC",
                                //Performerd = ihc.Performed

                            }).ToList();

                dataListImport.AddRange(ihcs);

            }



            if (VIRNumber != null || ContainerNo != null)
            {
                var ecms = (from ecm in Db.ECMs
                            where
                             (ecm.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                              && (ecm.ContainerNumber == ContainerNo || ContainerNo == null || ContainerNo == "")
                            select new ImportFielsViewModel
                            {
                                //VirNumber = ecm.VIRNumber,
                                FileName = ecm.FileName,
                                //ContainerNumber = ecm.ContainerNumber,
                                //BlNumber = "",
                                //GDNumber = "",
                                //Type = "ECM",
                                //Performerd = ecm.Performed

                            }).ToList();

                dataListImport.AddRange(ecms);

            }

            if (VIRNumber != null || ContainerNo != null || gdnumber != null)
            {
                var crls = (from crl in Db.CRLs
                            where
                            (crl.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                             && (crl.GDNumber == gdnumber || gdnumber == null || gdnumber == "")
                             && (crl.ContainerNumber == ContainerNo || ContainerNo == null || ContainerNo == "")
                            select new ImportFielsViewModel
                            {
                                //VirNumber = crl.VIRNumber,
                                FileName = crl.FileName,
                                //ContainerNumber = crl.ContainerNumber,
                                //BlNumber = "",
                                //GDNumber = crl.GDNumber,
                                //Type = "CRL",
                                //Performerd = crl.Performed

                            }).ToList();

                dataListImport.AddRange(crls);

            }


            if (VIRNumber != null || ContainerNo != null )
            {
                var svms = (from svm in Db.SVMs
                            where
                            (svm.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                              && (svm.ContainerNumber == ContainerNo || ContainerNo == null || ContainerNo == "")
                            select new ImportFielsViewModel
                            {
                                //VirNumber = svm.VIRNumber,
                                FileName = svm.FileName,
                                //ContainerNumber = svm.ContainerNumber,
                                //BlNumber = "",
                                //GDNumber = "",
                                //Type = "SVM",
                                //Performerd = svm.Performed

                            }).ToList();

                dataListImport.AddRange(svms);

            }

            if (VIRNumber != null || ContainerNo != null)
            {
                var svms = (from svm in Db.SVMs
                            where
                            (svm.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                              && (svm.ContainerNumber == ContainerNo || ContainerNo == null || ContainerNo == "")
                            select new ImportFielsViewModel
                            {
                                //VirNumber = svm.VIRNumber,
                                FileName = svm.FileName,
                                //ContainerNumber = svm.ContainerNumber,
                                //BlNumber = "",
                                //GDNumber = "",
                                //Type = "SVM",
                                //Performerd = svm.Performed

                            }).ToList();

                dataListImport.AddRange(svms);

            }

            if (VIRNumber != null || ContainerNo != null)
            {
                var sims = (from sim in Db.SIMs
                            where
                            (sim.VIRNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                              && (sim.ContainerNumber == ContainerNo || ContainerNo == null || ContainerNo == "")
                            select new ImportFielsViewModel
                            {

                                //VirNumber = sim.VIRNumber,
                                FileName = sim.FileName,
                                //ContainerNumber = sim.ContainerNumber,
                                //BlNumber = "",
                                //GDNumber = "",
                                //Type = "SIM",
                                //Performerd = sim.Performed
                            }).ToList();

                dataListImport.AddRange(sims);

            }

            if (VIRNumber != null || ContainerNo != null || gdnumber != null || BLno != null)
            {
                var ccmos = (from ccmo in Db.CCMOs
                             where
                              (ccmo.VIRNo == VIRNumber || VIRNumber == null || VIRNumber == "")
                              && (ccmo.TPNo == gdnumber || gdnumber == null || gdnumber == "")
                              && (ccmo.ContainerNo == ContainerNo || ContainerNo == null || ContainerNo == "")
                              && (ccmo.BLNo == BLno || BLno == null || BLno == "")
                             select new ImportFielsViewModel
                             {

                                 //VirNumber = ccmo.VIRNo,
                                 FileName = ccmo.FileName,
                                 //ContainerNumber = ccmo.ContainerNo,
                                 //BlNumber = ccmo.BLNo,
                                 //GDNumber = ccmo.TPNo,
                                 //Type = "CCMO",
                                 //Performerd = ccmo.Performed

                             }).ToList();

                dataListImport.AddRange(ccmos);

            }


            if (VIRNumber != null || ContainerNo != null  || BLno != null)
            {
                var gdcrs = (from gdcr in Db.GDCRs
                             where
                              (gdcr.VirNumber == VIRNumber || VIRNumber == null || VIRNumber == "")
                              && (gdcr.NewContainerNumber == ContainerNo || ContainerNo == null || ContainerNo == "")
                             && (gdcr.BLNumber == BLno || BLno == null || BLno == "")
                             select new ImportFielsViewModel
                             {

                                 //VirNumber = gdcr.VirNumber,
                                 FileName = gdcr.FileName,
                                 //ContainerNumber = gdcr.NewContainerNumber,
                                 //BlNumber = gdcr.BLNumber,
                                 //GDNumber = "",
                                 //Type = "GDCR",
                                 //Performerd = gdcr.Performed

                             }).ToList();

                dataListImport.AddRange(gdcrs);

            }

          



            var containerMerged = (from container in dataListImport
                                   group container by container.FileName into g
                                   select new ImportFielsViewModel
                                   {
                                       FileName = g.FirstOrDefault().FileName,                                        
                                   }).ToList();

            return containerMerged;
        }

    }
}
