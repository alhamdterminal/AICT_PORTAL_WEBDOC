using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Export.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class OGIERepository : RepoBase<OGIE>, IOGIERepository
    {
        public OGIERepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public long CargoReceivedCount()
        {
            var data = (from ogie in Db.OGIEs
                        select ogie).ToList();
            if (data == null)
            {
                return 0;
            }
            return data.Count;
        }

        public List<GateInExportGDsViewModel> GetGateInGDs()
        {
            var res = (from opia in Db.OPIAs
                       where
                        !Db.GateInExports.Any(s => s.GDNumber == opia.GDNumber && s.GateInStatus == "F")
                       select new GateInExportGDsViewModel
                       {
                           GDNumber = opia.GDNumber,
                           ClearingAgentName = opia.ClearingAgentName,
                           VehicleNo = !string.IsNullOrEmpty(opia.VehicleNo) ? opia.VehicleNo : "",
                           NoOfPackages = Convert.ToInt32(opia.NoOfPackages),
                           PackageType = opia.PackageType,
                           GrossWeight = opia.GrossWeight,
                           DeliveredNoOfPackages = Db.GateInExports.Where(s => s.GDNumber == opia.GDNumber).Sum(s => s.NumberofPackages) ?? 0,
                           DeliveredGrossWeight = Db.GateInExports.Where(s => s.GDNumber == opia.GDNumber).Sum(s => s.Weight),
                           ContainerNo = !string.IsNullOrEmpty(opia.ContainerNo) ? opia.ContainerNo : "",
                           Type = !string.IsNullOrEmpty(opia.ContainerNo) ? "TP" : "Normal",
                           MessageFrom = opia.MessageFrom != "Manual" ? !string.IsNullOrEmpty(opia.ContainerNo) ? "TP" : "WEBOC" : "Manual",
                           IsGateIn = false
                       }).ToList();

            if (res.Count() > 0)
            {
                foreach (var item in res)
                {
                    item.RemainingNoOfPackages = item.NoOfPackages - item.DeliveredNoOfPackages;
                    item.RemainingGrossWeight = item.GrossWeight - item.DeliveredGrossWeight;
                }
            }

            return res;
        }

        public List<OPIA> GetUnSetteledGDNumbers()
        {
            var res = (from opia in Db.OPIAs
                       where
                        !Db.EnteringCargos.Any(s => s.GDNumber == opia.GDNumber)
                       select new OPIA
                       {
                           GDNumber = opia.GDNumber,
                       }).ToList();
            return res;
        }


        public OPIA GetGDDetailGdnumber(string res)
        {

            var data = Db.OPIAs.FromSql($" select * from opias where GDNumber = {res}  and IsDeleted = 0").LastOrDefault();

            var dataogde = Db.OGDEs.FromSql($" select * from ogdes where GDNumber = {res}  and IsDeleted = 0").LastOrDefault();

            if (dataogde != null)
            {
                data.MessageTo = dataogde.MessageTo;
            }

            return data;
        }


        public GDInfoViewModel GetGDDetailingo(string res)
        {

            var resdata = new GDInfoViewModel();
            var opias = Db.OPIAs.FromSql($" select * from opias where GDNumber = {res}  and IsDeleted = 0").LastOrDefault();

            if (opias != null)
            {
                resdata.NoOfPackages = opias.NoOfPackages;
            }

            var ogie = Db.OGIEs.FromSql($" select top 1 * from ogies where GDNumber = {res}  and IsDeleted = 0").LastOrDefault();

            if (ogie != null)
            {
                resdata.Performed = ogie.Performed;
            }


            var item = Db.ExportContainerItems.FromSql($" select * from ExportContainerItem where GDNumber = {res}  and IsDeleted = 0").LastOrDefault();

            if (item != null)
            {
                resdata.SubmitedDate = item.SubmitedDate;
                resdata.GateOutDate = item.GateOutDate;
            }

            return resdata;
        }


        public string GetAgentNameByCNIC(string res)
        {
            var data = Db.LoadingPrograms.FromSql($" select * from loadingprogram where ClearingAgentCNIC = {res}  and IsDeleted = 0").LastOrDefault();

            if (data != null)
            {
                return data.CLearingAgentReprsentative;
            }
            return "";


        }

        public string GetDriverNameByCNIC(string res)
        {
            var data = Db.LoadingPrograms.FromSql($" select * from loadingprogram where DriverCNIC = {res}  and IsDeleted = 0").LastOrDefault();

            if (data != null)
            {
                return data.DriverName;
            }
            return "";
        }
    }
}
