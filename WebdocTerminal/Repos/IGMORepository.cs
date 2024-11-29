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
    public class IGMORepository : RepoBase<IGMO> , IIGMORepository
    {
        public IGMORepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<IGMO> GetIndexInfo(string Virno)
        {
            //return Db.Database.SqlQuery<TSet>(sql, param);

            var asd = Db.IGMOs.FromSql($"SELECT * From IGMO where  IsDeleted = 0  ").Where(x=> x.VIRNumber == Virno).ToList();


            //var containerMerged = (from container in asd
            //                       group container by container.IndexNumber into g
            //                       select new IndexDropdownViewModel
            //                       {                                
            //                           IndexNo = g.FirstOrDefault().IndexNumber ?? 0, 
            //                       }).ToList();

            return asd;
        }


        public IGMO GetIndexDetail(string Virno, int indexno)
        {
            var asd = Db.IGMOs.FromSql($"SELECT * From IGMO  where IsDeleted = 0  ").Where(x => x.VIRNumber == Virno && x.IndexNumber == indexno).FirstOrDefault();

 
            return asd;
        }

        public IGMO GetIGMODetailBYBLnumber(string blnumber)
        {
            var asd = Db.IGMOs.FromSql($"SELECT * From IGMO where  BLNumber = {blnumber}   and IsDeleted = 0  ").FirstOrDefault();


            return asd;
        }

        public IGMO GetManualIgmo(string virno)
        {
            var asd = Db.IGMOs.FromSql($"SELECT * From IGMO where  VIRNumber = {virno}   and IsDeleted = 0  ").LastOrDefault();


            return asd;
        }


        public List<IGMO> GetManualIgmoList()
        {
            var asd = Db.IGMOs.FromSql($"SELECT * From IGMO where   MessageFrom = 'MANUAL'   and IsDeleted = 0  ").ToList();


            return asd;
        }

        public IGMO GetIGMOInfo(string VIRNumber , string ContainerNumber , long IndexNumber , string BLNumber)
        {
            var igmores = Db.IGMOs.FromSql($"SELECT * From IGMO Where VIRNumber = {VIRNumber} and ContainerNumber = { ContainerNumber }  and  IndexNumber = { IndexNumber} and BLNumber = {BLNumber } and   MessageFrom = 'MANUAL' ").LastOrDefault();

            if(igmores != null)
            {
                return igmores;

            }

            return null;
        }

        public ISOCode GetISOCodeBySize(string size)
        {
            var iSOCode = Db.ISOCodes.FromSql($"SELECT * From ISOCode Where ContainerSize = {size}  and   IsDeleted = 0 ").FirstOrDefault();

            if (iSOCode != null)
            {
                return iSOCode;
            }

            return null;
        }

        public IGMO GetMANUALIGMOByIGMandContainer(string VIRNumber, string ContainerNumber, long IndexNumber, string BLNumber)
        {
            var igmores = Db.IGMOs.FromSql($"SELECT * From IGMO Where VIRNumber = {VIRNumber} and ContainerNumber = { ContainerNumber }  and  IndexNumber = { IndexNumber} and BLNumber = {BLNumber } and   MessageFrom = 'MANUAL' ").LastOrDefault();

            if (igmores != null)
            {
                return igmores;

            }

            return null;
        }

        public IGMO GetMANUALIGMOByVirAndContainerno(string VIRNumber, string ContainerNumber )
        {
            var igmores = Db.IGMOs.FromSql($"SELECT * From IGMO Where VIRNumber = {VIRNumber} and ContainerNumber = { ContainerNumber }  and   MessageFrom = 'MANUAL' ").LastOrDefault();

            if (igmores != null)
            {
                return igmores;

            }

            return null;
        }



    }
}
