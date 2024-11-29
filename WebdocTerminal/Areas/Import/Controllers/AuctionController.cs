using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class AuctionController : ParentController
    {

        public IAuctionRepository _auctionRepo;
        public IGatePassAuctionRepository _gatePassAuctionRepo;
        public IVesselRepository _vesselRepo;
        public IContainerIndexRepository  _containerIndexRepo;
        public ICYContainerRepository _cycontainerRepo;
        public IBidderRepository _bidderRepo;
        public IDictionaryRepository _dictionaryRepo;
        public IAuctionAmountRepository _auctionAmountRepo;
        public IHoldRepository _holdRepository;
        public IInvoiceRepository _invoiceRepository;


        public AuctionController(IAuctionRepository auctionRepo , 
                                IVesselRepository vesselRepo , 
                                IDictionaryRepository dictionaryRepo, 
                                IGatePassAuctionRepository gatePassAuctionRepo,
                                IContainerIndexRepository containerIndexRepo,
                                ICYContainerRepository cycontainerRepo,
                                IBidderRepository bidderRepo,
                                IAuctionAmountRepository auctionAmountRepo,
                                IHoldRepository holdRepository,
                                IInvoiceRepository invoiceRepository)
        {
            _auctionRepo = auctionRepo;
            _gatePassAuctionRepo = gatePassAuctionRepo;
            _vesselRepo = vesselRepo;
            _containerIndexRepo = containerIndexRepo;
            _cycontainerRepo = cycontainerRepo;
            _bidderRepo = bidderRepo;
            _dictionaryRepo = dictionaryRepo;
            _auctionAmountRepo = auctionAmountRepo;
            _holdRepository = holdRepository;
            _invoiceRepository = invoiceRepository;
        }
        public IActionResult AuctionInformation()
        {
            ViewData["Bidders"] = _bidderRepo.GetAll()
                .Select(x => new SelectListItem { Text = x.BidderName, Value = x.BidderId.ToString() }); 

            return View();
        }

        public IActionResult SubmitedAuctions()
        {
            return View();
        }


        public  JsonResult GetAuctionDetail(string type , long containerId)
        {
             if(type == "CFS")
            {
                var data = _auctionRepo.GetFirst(x => x.ContainerIndexId == containerId , v=> v.Bidder);

                if(data != null)
                {
                    return Json(data);
                }
            }
            if (type == "CY")
            {
                var data = _auctionRepo.GetFirst(x => x.CYContainerId == containerId, v => v.Bidder);
                
                if (data != null)
                {
                    return Json(data);
                }
            }

            return Json("");
        } 

        [HttpPost]
        public IActionResult CreateAuction(Auction values , long containerId , string type)
        {
            if(type == "CFS")
            {
                var Index =  _auctionRepo.GetFirst(x => x.ContainerIndexId == containerId);
                if (Index != null)
                {
                    return new OkObjectResult(new { message = "This Container Already Assign" });
                }

                values.ContainerIndexId = containerId;
                _auctionRepo.Add(values);

                return new JsonResult(new { error = false, message = "Saved" });


            }
            else
            {
                var cycontainer = _auctionRepo.GetFirst(y => y.CYContainerId == containerId);
                if (cycontainer != null)
                {
                    return new OkObjectResult(new { message = "This Index Already Assign" });
                }

                values.CYContainerId = containerId;
                _auctionRepo.Add(values);

                return new JsonResult(new { error = false, message = "Saved" });
            }

         }
        
        [HttpPost]
        public IActionResult UpdateAuction(Auction values , long containerId , string type)
        {
            if(type == "CFS")
            {
                var gatePassAution = _gatePassAuctionRepo.GetAll().Where(x => x.ContainerIndexId == containerId).LastOrDefault();

                if(gatePassAution != null)
                {
                    return new JsonResult(new { error = true, message = "GatePass Created ! " });

                }
                else
                {
                   
                        var auction = _auctionRepo.GetFirst(x => x.ContainerIndexId == containerId);
                        if (auction != null)
                        {
                            auction.AuctionDate = values.AuctionDate;
                            auction.AuctionLotNumber = values.AuctionLotNumber;
                             auction.BankVoucherNo25 = values.BankVoucherNo25;
                            auction.BankVoucherNo75 = values.BankVoucherNo75;
                            auction.BidderId = values.BidderId;
                            auction.CustomDoDate = values.CustomDoDate;
                            auction.CustomDoNo = values.CustomDoNo;
                            auction.FinalBidAmount = values.FinalBidAmount;
                            _auctionRepo.Update(auction);

                        return new JsonResult(new { error = false, message = "Updated" });

                    }
                    else
                    {
                        return new JsonResult(new { error = true, message = "Not Found" });

                    }




                }

   


            }
            else
            {
                var gatePassAution = _gatePassAuctionRepo.GetAll().Where(x => x.CYContainerId == containerId).LastOrDefault();

                if (gatePassAution != null)
                {
                    return new JsonResult(new { error = true, message = "GatePass Created ! " });

                }
                else
                {

                    var auction = _auctionRepo.GetFirst(x => x.CYContainerId == containerId);
                    if (auction != null)
                    {
                        auction.AuctionDate = values.AuctionDate;
                        auction.AuctionLotNumber = values.AuctionLotNumber;
                         auction.BankVoucherNo25 = values.BankVoucherNo25;
                        auction.BankVoucherNo75 = values.BankVoucherNo75;
                        auction.BidderId = values.BidderId;
                        auction.CustomDoDate = values.CustomDoDate;
                        auction.CustomDoNo = values.CustomDoNo;
                        auction.FinalBidAmount = values.FinalBidAmount;
                        _auctionRepo.Update(auction);

                        return new JsonResult(new { error = false, message = "Updated" });

                    }
                    else
                    {
                        return new JsonResult(new { error = true, message = "Not Found" });

                    }
                     

                }
                  
            }

        }


        public IActionResult AuctionDetailView()
        {
            return View();
        }

        public IActionResult AuctionDetailCYView()
        {
            return View();
        }




        [HttpPost]
        public IActionResult AlotAuctionNumberCFS(List<ContainerIndex> model, string type)
        {

            var cargotype = "";
            var month = DateTime.Now.ToString("MMM-y").ToUpper();
            var dictnory = _dictionaryRepo.GetAll().Where(x => x.Key == "CFS Auction Lot No").LastOrDefault();

            foreach (var item in model)
            {
                var res = _containerIndexRepo.GetContainerIndexByIgmAndIndex(item.VirNo, item.IndexNo ?? 0);


                if (type == "LCL Vehicle")
                {
                    cargotype = "CV";
                }
                else
                {
                    cargotype = "CF";
                }

                if (res.Count() > 0)
                {
                    var no = Convert.ToInt16(dictnory.Value) + 1;
                    if (dictnory.MonthYear == month)
                    {

                        res.ForEach(x => x.AuctionLotNo = cargotype + "-" + no + "-" + month);
                        res.ForEach(x => x.IsAuction = item.IsAuction);

                        var hold = new Hold
                        {
                            IsHold = true,
                            VirNo = item.VirNo,
                            IndexNo = item.IndexNo ?? 0,
                            Type = "CFS",
                            SpecialInstructions = "Auction Hold apply by Form",
                            HoldType = "Auction",
                            AuctionLotNo = cargotype + "-" + no + "-" + month,
                            HoldDate = DateTime.Now,
                            Nature = "Auction",
                            Role = ""
                        };

                        _containerIndexRepo.UpdateRange(res);
                        _holdRepository.Add(hold);


                        dictnory.Value = no.ToString();
                        dictnory.MonthYear = month;
                        _dictionaryRepo.Update(dictnory);




                    }
                    else
                    {


                        var numLot = 1;
                        res.ForEach(x => x.AuctionLotNo = cargotype + "-" + numLot + "-" + month);
                        res.ForEach(x => x.IsAuction = item.IsAuction);

                        dictnory.Value = no.ToString();
                        dictnory.MonthYear = month;

                        dictnory.Value = numLot.ToString();
                        dictnory.MonthYear = month;


                        var hold = new Hold
                        {
                            IsHold = true,
                            VirNo = item.VirNo,
                            IndexNo = item.IndexNo ?? 0,
                            Type = "CFS",
                            SpecialInstructions = "Auction Hold apply by Form",
                            HoldType = "Auction",
                            AuctionLotNo = cargotype + "-" + numLot + "-" + month,
                            HoldDate = DateTime.Now,
                            Nature = "Auction",
                            Role = ""
                        };

                        _holdRepository.Add(hold);
                        _containerIndexRepo.UpdateRange(res);
                        _dictionaryRepo.Update(dictnory);


                    }
                }

            }

            return Json(new { error = false, message = "Save" });

        }


        [HttpPost]
        public IActionResult AlotAuctionNumberCY(List<CYContainer> model)
        {
            foreach (var item in model)
            {
                var res = _cycontainerRepo.GetUndeliveredIndexbyigmindex(item.VIRNo, item.IndexNo ?? 0).ToList();
                var month = DateTime.Now.ToString("MMM-y").ToUpper();
                var dictnory = _dictionaryRepo.GetAll().Where(x => x.Key == "CY Auction Lot No").LastOrDefault();

                if (res.Count() > 0)
                {
                    //foreach (var itm in res)
                    //{
                    if (dictnory.MonthYear == month)
                    {

                        var no = Convert.ToInt16(dictnory.Value) + 1;

                        res.ForEach(x => x.AuctionLotNo = "CY" + "-" + no + "-" + month);
                        res.ForEach(x => x.IsAuction = item.IsAuction);

                        var hold = new Hold
                        {
                            IsHold = true,
                            VirNo = item.VIRNo,
                            IndexNo = item.IndexNo ?? 0,
                            Type = "CY",
                            SpecialInstructions = "Auction Hold apply by Form",
                            HoldType = "Auction",
                            AuctionLotNo = "CY" + "-" + no + "-" + month,
                            HoldDate = DateTime.Now,
                            Nature = "Auction",
                            Role = ""
                        };



                        dictnory.Value = no.ToString();

                        _cycontainerRepo.UpdateRange(res);
                        _holdRepository.Add(hold);
                        _dictionaryRepo.Update(dictnory);
                    }
                    else
                    {
                        var number = 1;


                        res.ForEach(x => x.AuctionLotNo = "CY" + "-" + number + "-" + month);
                        res.ForEach(x => x.IsAuction = item.IsAuction);
                         
                        var hold = new Hold
                        {
                            IsHold = true,
                            VirNo = item.VIRNo,
                            IndexNo = item.IndexNo ?? 0,
                            Type = "CY",
                            SpecialInstructions = "Auction Hold apply by Form",
                            HoldType = "Auction",
                            AuctionLotNo = "CY" + "-" + number + "-" + month,
                            HoldDate = DateTime.Now,
                            Nature = "Auction",
                            Role = ""
                        };

                        _holdRepository.Add(hold);

                        dictnory.Value = number.ToString();
                        dictnory.MonthYear = month;

                        _cycontainerRepo.UpdateRange(res);
                        _dictionaryRepo.Update(dictnory);

                    }
                    //}
                }
            }

            return Json(new { error = false, message = "Save" });

        }


        public IActionResult AuctionUnMark()
        {
            return View();
        }

        public IActionResult UnMarkAuctionIndex(List<MarkedAuctionDetailViewModel> model)
        {
            var cfsrres = model.Where(x => x.Type == "CFS");
            var cyrres = model.Where(x => x.Type == "CY");


            foreach (var item in cfsrres)
            {
                var res = _containerIndexRepo.GetContainerIndexByIgmAndIndex(item.VirNumber, item.IndexNumber ?? 0);

                var indeces = res.Select(v => v.ContainerIndexId).ToList();

                var resauction = _auctionRepo.GetAll().Where(x => indeces.Contains(x.ContainerIndexId ?? 0)).ToList();
                 
                if (res.Count() > 0)
                {
                    var invoceres = _invoiceRepository.GetInvoicesByIGMIndex(item.VirNumber, item.IndexNumber.ToString() , "CFS").ToList();

                    if(invoceres.Count() > 0)
                    {
                        return Json(new { error = true, message = "please un select Invoice Created againts " +  item.VirNumber + " and " +item.IndexNumber });
                    }
                     
                    res.ForEach(x => x.AuctionLotNo = null);
                    res.ForEach(x => x.IsAuction = false);

                    if(resauction.Count() > 0)
                    {
                        _auctionRepo.DeleteRange(resauction);
                    }
                       
                    _containerIndexRepo.UpdateRange(res);
                     

                }

            }

            
            foreach (var item in cyrres)
            {
                var res = _cycontainerRepo.GetUndeliveredIndexbyigmindex(item.VirNumber, item.IndexNumber ?? 0).ToList();
                var indeces = res.Select(v => v.CYContainerId).ToList();

                var resauction = _auctionRepo.GetAll().Where(x => indeces.Contains(x.CYContainerId ?? 0)).ToList();

                if (res.Count() > 0)
                {
                    var invoceres = _invoiceRepository.GetInvoicesByIGMIndex(item.VirNumber, item.IndexNumber.ToString(), "CY").ToList();

                    if (invoceres.Count() > 0)
                    {
                        return Json(new { error = true, message = "please un select Invoice Created againts " + item.VirNumber + " and " + item.IndexNumber });
                    }

                    res.ForEach(x => x.AuctionLotNo = null);
                    res.ForEach(x => x.IsAuction = false);

                    if (resauction.Count() > 0)
                    {
                        _auctionRepo.DeleteRange(resauction);
                    }

                    _cycontainerRepo.UpdateRange(res);

 
                }

            }

            return Json(new { error = false, message = "Updated" });

        }

        public IActionResult AuctionAmountView()
        {
             
            return View();
        }


        public JsonResult GetAuctionAmount()
        {
            var res = _auctionAmountRepo.GetLast() ;
            if(res != null)
            {
                return Json(res);
            }

            return Json("");
        }


        public IActionResult UpdateAuctionAmount(AuctionAmount model)
        {
            try
            {
                var res = _auctionAmountRepo.GetLast();
                if (res != null)
                {

                    res.Rate20 = model.Rate20;
                    res.Rate40 = model.Rate40;
                    res.Rate45 = model.Rate45;
                    res.CBM = model.CBM;
                    res.Weight = model.Weight;
                    res.HanndlingWeight = model.HanndlingWeight;

                    _auctionAmountRepo.Update(res);
                 }
                else
                {
                    var data = new AuctionAmount()
                    {
                        Rate20 = model.Rate20,
                        Rate40 = model.Rate40,
                        Rate45 = model.Rate45,
                        CBM = model.CBM,
                        Weight = model.Weight,
                        HanndlingWeight = model.Weight
                    };
                    _auctionAmountRepo.Add(data);
                }

                return new OkObjectResult(new { error = false, message = "Successfully Save!" });

            }
            catch (Exception e)
            {

                return new OkObjectResult(new { error = true, message = e.InnerException.InnerException.Message });
            }



        }

    }
}