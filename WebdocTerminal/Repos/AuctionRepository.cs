using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class AuctionRepository : RepoBase<Auction> , IAuctionRepository
    {
        public AuctionRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<MarkedAuctionDetailViewModel> GetAllAuctionUnMark()
        {

            var resdata = new List<MarkedAuctionDetailViewModel>();
            var res = (from contaienrindex in Db.ContainerIndices
                       where 
                       contaienrindex.IsAuction == true
                       && contaienrindex.IsDelivered == false
                       && !Db.Invoices.Any(x => x.ContainerIndexId == Db.ContainerIndices.Where(s => s.VirNo == contaienrindex.VirNo && s.IndexNo == contaienrindex.IndexNo).FirstOrDefault().ContainerIndexId)
                       select new MarkedAuctionDetailViewModel
                       {
                           Key = contaienrindex.VirNo + "-"+ contaienrindex.IndexNo,
                           VirNumber = contaienrindex.VirNo,
                           IndexNumber = contaienrindex.IndexNo,
                           Type = "CFS",
                           IsChecked = false

                       }).ToList();


            var containerMerged = (from container in res
                                   group container by container.Key into g

                                   select new MarkedAuctionDetailViewModel
                                   {
                                       Key = g.FirstOrDefault().Key,                                       
                                       VirNumber = g.FirstOrDefault().VirNumber,
                                       IndexNumber = g.FirstOrDefault().IndexNumber,
                                       Type = g.FirstOrDefault().Type,
                                       IsChecked = g.FirstOrDefault().IsChecked
                                   }).ToList();

            resdata.AddRange(containerMerged);


            var cyres = (from contaienrindex in Db.CYContainers
                       where
                       contaienrindex.IsAuction == true
                       && contaienrindex.IsDelivered == false
                       && !Db.Invoices.Any(x => x.CYContainerId == Db.CYContainers.Where(s => s.VIRNo == contaienrindex.VIRNo && s.IndexNo == contaienrindex.IndexNo).FirstOrDefault().CYContainerId)
                       select new MarkedAuctionDetailViewModel
                       {
                           Key = contaienrindex.VIRNo + "-" + contaienrindex.IndexNo,
                           VirNumber = contaienrindex.VIRNo,
                           IndexNumber = contaienrindex.IndexNo,
                           Type = "CY",
                           IsChecked = false
                       }).ToList();


            var cycontainerMerged = (from container in cyres
                                     group container by container.Key into g

                                   select new MarkedAuctionDetailViewModel
                                   {
                                       Key = g.FirstOrDefault().Key,
                                       VirNumber = g.FirstOrDefault().VirNumber,
                                       IndexNumber = g.FirstOrDefault().IndexNumber,
                                       Type = g.FirstOrDefault().Type,
                                       IsChecked = g.FirstOrDefault().IsChecked
                                   }).ToList();

            resdata.AddRange(cycontainerMerged);

            return resdata;

        }
    }
}
