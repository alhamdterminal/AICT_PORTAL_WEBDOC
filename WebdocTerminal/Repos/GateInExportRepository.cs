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
    public class GateInExportRepository  : RepoBase<GateInExport> , IGateInExportRepository
    {
         public GateInExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public string getstatusbygdnumber(string gdnumber, int noofpackages, double weight)
        {

            var data = Db.GateInExports.FromSql($" select * from GateInExports where GDNumber = {gdnumber}    and IsDeleted = 0").ToList();

            if (data.Count() > 0)
            {
                var totoaldeliverredweitgh = data.Sum(x => x.Weight) + weight;
                var totoaldeliverrednoofpackages = data.Sum(x => x.NumberofPackages) + noofpackages;



                var opiasdata = Db.OPIAs.FromSql($" select * from OPIAs where GDNumber = {gdnumber}  and OperationType = 'A'   and IsDeleted = 0").LastOrDefault();

                if (opiasdata != null)
                {
                    if (totoaldeliverrednoofpackages > opiasdata.NoOfPackages || totoaldeliverredweitgh > opiasdata.GrossWeight)
                    {
                        return "Packages excess or weight more then mainfest in gd " + gdnumber;

                    }

                    //if (totoaldeliverrednoofpackages < opiasdata.NoOfPackages || totoaldeliverredweitgh < opiasdata.GrossWeight)
                    if (totoaldeliverrednoofpackages < opiasdata.NoOfPackages)
                    {
                        return "P";
                    }
                    else
                    {
                        return "F";
                    }

                }

                else
                {
                    return "opia not avaiable for gd " + gdnumber;
                }
            }
            else
            {
                var totoaldeliverredwitgh = weight;
                var totoaldeliverrednoofpckages = noofpackages;


                var opiasdata = Db.OPIAs.FromSql($" select * from OPIAs where GDNumber = {gdnumber}  and OperationType = 'A'   and IsDeleted = 0").LastOrDefault();

                if (opiasdata != null)
                {
                    if (totoaldeliverrednoofpckages > opiasdata.NoOfPackages || totoaldeliverredwitgh > opiasdata.GrossWeight)
                    {
                        return "Packages or weight excess more then mainfest in gd " + gdnumber;

                    }

                    //  if (totoaldeliverrednoofpckages < opiasdata.NoOfPackages || totoaldeliverredwitgh < opiasdata.GrossWeight)
                    if (totoaldeliverrednoofpckages < opiasdata.NoOfPackages)
                    {
                        return "P";
                    }
                    else
                    {
                        return "F";
                    }

                }

                else
                {
                    return "opia not avaiable for gd " + gdnumber;
                }

            }
        }

        public List<GateInExport> GetGateInGdInfo(string gdnumber)
        {
            var res = Db.GateInExports.FromSql($" select * from GateInExports where GDNumber = {gdnumber} and IsDeleted = 0").ToList();

            return res;
        }
    }
}
