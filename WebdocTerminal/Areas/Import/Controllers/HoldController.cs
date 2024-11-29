using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class HoldController : ParentController
    {

        public IVesselRepository _vesselRepo;
        public IVoyageRepository _voyageRepo;
        public IHoldRepository _holdRepo;
        public IContainerIndexRepository _containerIndexRepo;
        public ICYContainerRepository _cyContainerRepo;
        public ISIMORepository _simoreport;
        public ISIMRepository _simreport;
        private UserManager<IdentityUser> _userManager;
        private IEDOHoldRepository _eDOHoldRepository;
        private IHoldPrivilegeRepository  _holdPrivilegeRepository;
        private IContainerRepository _containerRepository;




        public HoldController(IVesselRepository vesselRepo, 
                              IHoldRepository holdRepo,
                              UserManager<IdentityUser> userManager, 
                              IContainerIndexRepository containerIndexRepo,
                              ICYContainerRepository cyContainerRepo, 
                              IVoyageRepository voyageRepo, 
                              ISIMORepository simoreport, 
                              ISIMRepository simreport,
                              IEDOHoldRepository eDOHoldRepository,
                              IHoldPrivilegeRepository holdPrivilegeRepository,
                              IContainerRepository containerRepository)
        {
            _vesselRepo = vesselRepo;
            _voyageRepo = voyageRepo;
            _holdRepo = holdRepo;
            _containerIndexRepo = containerIndexRepo;
            _cyContainerRepo = cyContainerRepo;
            _simoreport = simoreport;
            _simreport = simreport;
            _userManager = userManager;
            _eDOHoldRepository = eDOHoldRepository;
            _holdPrivilegeRepository = holdPrivilegeRepository;
            _containerRepository = containerRepository;
        }

        public IActionResult HoldSpecialInstruction()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateHoldBlWise(Hold values, int indexNumber, string igm)
        {

            try
            {
                //DateTime now = DateTime.Now;
                //var startDate = new DateTime(now.Year, now.Month, 1);
                //var endDate = startDate.AddMonths(1).AddDays(-1);

                var identityUser = await _userManager.GetUserAsync(User);
                var role = await _userManager.GetRolesAsync(identityUser);

                string concat = String.Join(",", role.ToArray());

                //var simo = _simoreport.GetAll().Where(x => x.BLNumber == blNumber && x.IndexNumber == indexNumber.ToString() && x.VIRNumber == igm).LastOrDefault();

                //if (simo != null && simo.Status == "HO")
                //{
                //    return new OkObjectResult(new { message = "Hold can't  apply due To simo hold" });
                //}
                //else
                //{
                var datetime = DateTime.Now;

                values.HoldDate = datetime;

                //var resdata = _containerIndexRepo.GetContainerIndexInfo(blNumber, indexNumber, igm);

                //if (resdata != null)
                //{

                //List<Hold> holdList = new List<Hold>();
                //List<ContainerIndex> containerIndexes = new List<ContainerIndex>();
                // foreach (var item in resdata)
                //{

                //var containerIndex = _containerIndexRepo.Find(item.ContainerIndexId);

                //if (containerIndex != null)
                //{

                //containerIndex.IsHold = true;

                var hld = new Hold
                {
                    IsHold = true,
                    VirNo = igm,
                    IndexNo = indexNumber,
                    Type = "CFS",
                    SpecialInstructions = values.SpecialInstructions,
                    HoldType = values.HoldType,
                    //ContainerIndexId = containerIndex.ContainerIndexId,
                    //AuctionLotNo = values.AuctionLotNo,
                    HoldDate = values.HoldDate,
                    //RO = values.RO,
                    Nature = "Normal",
                    Role = concat
                };
                //containerIndexes.Add(containerIndex);
                //holdList.Add(hld); 

                //} 
                //} 
                //_containerIndexRepo.UpdateRange(containerIndexes);
                _holdRepo.Add(hld);

                //}

                return Json(new { error = false, message = "this index all containers are hold" });

                //}
            }
            catch (Exception e)
            {

                return Json(new { error = true, message = e.InnerException.InnerException.Message });
            }



        }



        [HttpPost]
        public async Task<IActionResult> CreateHold(Hold values, string igm, int indexNo, string ContainerNumber)
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var role = await _userManager.GetRolesAsync(identityUser);
            string userRoles = String.Join(",", role.ToArray());

            //var simo = _simreport.GetAll().Where(x => x.VIRNumber == igm && x.ContainerNumber == ContainerNumber).LastOrDefault();

            //if (simo != null && simo.Status == "HO")
            //{
            //    return new OkObjectResult(new { message = "Hold can't  apply due To sim hold" });
            //}
            //else
            //{
                var datetime = DateTime.Now;

                //List<Hold> holdList = new List<Hold>();
                //List<CYContainer> cycontainers = new List<CYContainer>();

                values.HoldDate = datetime;



                //var _allContainers = _cyContainerRepo.GetList(c => c.VIRNo == igm && c.IndexNo == indexNo);

                //foreach (var container in _allContainers)
                //{
                    //container.IsHold = true;

                    var hld = new Hold
                    {
                        ////CYContainerId = container.CYContainerId,
                        //SpecialInstructions = values.SpecialInstructions,
                        //AuctionLotNo = values.AuctionLotNo,
                        //HoldDate = values.HoldDate,
                        //RO = values.RO,
                        //Role = userRoles

                        IsHold = true,
                        VirNo = igm,
                        IndexNo = indexNo,
                        Type = "CY",
                        SpecialInstructions = values.SpecialInstructions,
                        HoldType = values.HoldType,
                        //ContainerIndexId = containerIndex.ContainerIndexId,
                        //AuctionLotNo = values.AuctionLotNo,
                        HoldDate = values.HoldDate,
                        //RO = values.RO,
                        Nature = "Normal",
                        Role = userRoles

                    };

                    //cycontainers.Add(container);
                    //holdList.Add(hld);



                //}
                //_cyContainerRepo.UpdateRange(cycontainers);
                _holdRepo.Add(hld);
                //}



                return new OkObjectResult(new { message = "Container Hold" });

            //}
          
        }

        [HttpPost]
        public async Task<IActionResult> RemoveHoldCFS(long ContainerIndexId, string blNumber, int indexNo, string igm)
        { 

            //var identityUser = await _userManager.GetUserAsync(User);
            //var role = await _userManager.GetRolesAsync(identityUser);

           
            //var simo = _simoreport.GetAll().Where(x => x.BLNumber == blNumber && x.IndexNumber == indexNo.ToString() && x.VIRNumber == igm ).LastOrDefault();

            //if (simo != null && simo.Status == "HO")
            //{
            //    return new OkObjectResult(new { message = "Hold can't  Removed due To simo hold" });
            //}
            //else
            //{

            //    var resdata = _containerIndexRepo.GetAll().Where(x => x.BLNo == blNumber && x.IndexNo == indexNo && x.VirNo == igm).LastOrDefault();
                 
            //    if (resdata != null)
            //    {

            //        var holdResult = _holdRepo.GetAll().Where(x => x.ContainerIndexId == resdata.ContainerIndexId && x.IsHold == false).ToList();
            //        var reas = holdResult.LastOrDefault();
            //        bool containsAny = role.Intersect(reas.Role.Split(',')).Any();
            //        if (containsAny == true)
            //        { 

            //        if (holdResult.Count() == 1)
            //        {
            //            resdata.IsHold = false;
            //            _containerIndexRepo.Update(resdata);
            //        }

            //        reas.IsHold = true;
            //        _holdRepo.Update(reas);

            //        }
            //        else
            //        {
            //            return new OkObjectResult(new { error = true, message = reas.SpecialInstructions });

            //        } 
            //    }


                return new OkObjectResult(new { message = "Hold Removed" });

            //}


        }

        [HttpPost]
        public IActionResult RemoveHold(long ContainerIndexId)
        {
            //var hold = _holdRepo.GetFirst(c => c.ContainerIndexId == ContainerIndexId);

            //if (hold != null)
            //{
            //    _holdRepo.Delete(hold);

            //    var containerindex = _containerIndexRepo.Find(ContainerIndexId);

            //    containerindex.IsHold = false;

            //    _containerIndexRepo.Update(containerindex);
            //}

            return new OkObjectResult(new { message = "Hold Removed" });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveHoldCYContainer(string RO, string igm, int indexNo, string ContainerNumber)
        {
            //var identityUser = await _userManager.GetUserAsync(User);
            //var role = await _userManager.GetRolesAsync(identityUser);

            //var simo = _simreport.GetAll().Where(x => x.VIRNumber == igm && x.ContainerNumber == ContainerNumber ).LastOrDefault();

            //if (simo != null && simo.Status == "HO")
            //{
            //    return new OkObjectResult(new { message = "Hold can't  Removed due To simo hold" });
            //}
            //else
            //{
            //    var _allContainers = _cyContainerRepo.GetList(c => c.VIRNo == igm && c.IndexNo == indexNo);

            //    foreach (var container in _allContainers)
            //    {
            //        var holdResult = _holdRepo.GetAll().Where(x => x.CYContainerId == container.CYContainerId && x.IsHold == false).ToList();
            //        var xdxx = holdResult.LastOrDefault();

            //        bool containsAny = role.Intersect(xdxx.Role.Split(',')).Any();
            //        if (containsAny == true)
            //        {
            //            if (holdResult.Count() == 1)
            //            {
            //                container.IsHold = false;
            //                _cyContainerRepo.Update(container);
            //            }

            //            xdxx.IsHold = true;
            //            _holdRepo.Update(xdxx);
            //        }
            //        else
            //        {
            //            return new OkObjectResult(new { error = true, message = xdxx.SpecialInstructions });
            //        }
            //    }
                    return new OkObjectResult(new { message = "Hold Removed" });
            //}
        }

        [HttpPost]
        public IActionResult ReleasedHoldCFS(Hold values, string blNumber, int indexNo, string igm)
        {


            List<Hold> holdList = new List<Hold>();
            List<ContainerIndex> containerIndexes = new List<ContainerIndex>();
            var simo = _simoreport.GetAll().Where(x => x.BLNumber == blNumber && x.IndexNumber == indexNo.ToString() && x.VIRNumber == igm ).LastOrDefault();

            if (simo != null && simo.Status == "HO")
            {
                return new OkObjectResult(new { message = "Hold can't  Released due To simo hold" });
            }

            else
            {  
                var _cIndex = _containerIndexRepo.GetAll().Where (ci=> ci.BLNo== blNumber && ci.IndexNo == indexNo && ci.VirNo == igm).LastOrDefault();
                 
                if (_cIndex.IsAuction == true)
                {
                    _cIndex.RO = values.RO;
                    _cIndex.IsAuction = false;
                    _containerIndexRepo.Update(_cIndex);
                    return new OkObjectResult(new { message = "Hold Released" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "Auction hold not apply" });

                } 

            }


        }

        public IActionResult ReleasedHoldCY(Hold values, string igm, int indexNo, string ContainerNumber)
        {


            var simo = _simreport.GetAll().Where(x => x.VIRNumber == igm && x.ContainerNumber == ContainerNumber ).LastOrDefault();

            if (simo != null && simo.Status == "HO")
            {
                return new OkObjectResult(new { message = "Hold can't  Released due To simo hold" });
            }
            else
            {
                List<Hold> holdList = new List<Hold>();
                List<CYContainer> containers = new List<CYContainer>();

                var _allContainers = _cyContainerRepo.GetAll().Where(x => x.VIRNo == igm && x.IndexNo == indexNo).ToList();

                foreach (var container in _allContainers)
                {
 
                    if (container.IsAuction == true)
                    {
                        container.RO = values.RO;
                        container.IsAuction = false;
                        _cyContainerRepo.Update(container);
                        return new OkObjectResult(new { message = "Hold Released" });

                    }
                    else
                    {
                        return new OkObjectResult(new { error = true, message = "Auction hold not apply" });

                    } 
                } 
            }
                return new OkObjectResult(new { message = "Hold Released" });
             
        }


        [HttpPost]
        public IActionResult ReleasedEDOHold(string remarks , string igm , long indexno)
        {
             
            var _cIndex = _eDOHoldRepository.GetAll().Where(ci => ci.VirNo == igm  && ci.IndexNo == indexno).LastOrDefault();

            if (_cIndex != null)
            {
                return new OkObjectResult(new { error = true, message = "already Released" });
            }
            else
            {
                var edohold = new EDOHold
                {
                    VirNo = igm,
                    IndexNo = indexno,
                    Remarks = remarks,
                     IsEDOHold = false
                };

                _eDOHoldRepository.Add(edohold);

                return new OkObjectResult(new { error = false, message = "Released EDO Hold" });
            }
              
        }


        [HttpPost]
        public IActionResult RemoveDOHold(string remarks, string igm, long indexno)
        {

            var _cIndex = _eDOHoldRepository.GetAll().Where(ci => ci.VirNo == igm && ci.IndexNo == indexno).LastOrDefault();

            if (_cIndex != null)
            {
                return new OkObjectResult(new { error = true, message = "already Released" });
            }
            else
            {
                var edohold = new EDOHold
                {
                    VirNo = igm,
                    IndexNo = indexno,
                    Remarks = remarks,
                    IsEDOHold = false
                };

                _eDOHoldRepository.Add(edohold);

                return new OkObjectResult(new { error = false, message = "Released EDO Hold" });
            }

        }

        public IActionResult GetHoldData(string igm , long indexno)
        {
            var res = _holdRepo.GetAll().Where(x => x.VirNo == igm && x.IndexNo == indexno).ToList();

            return Json(res);
        }



        [HttpPost]
        public async Task<IActionResult> CreateHoldIndex(Hold values, string igm, long indexno , string type)
        {

            try
            {

                //var deliveryres = _containerRepository.CheckDeliveryStatus(values.VirNo, values.IndexNo);
                //if (deliveryres == true)
                //{
                //    return Json(new { error = true, message = "can't mark hold due to cargo is delivered " });
                //}

                var identityUser = await _userManager.GetUserAsync(User);
                var role = await _userManager.GetRolesAsync(identityUser);

                string concat = String.Join(",", role.ToArray());
                 
                var datetime = DateTime.Now;

                values.HoldDate = datetime;
                 
                var hld = new Hold
                {
                    IsHold = true,
                    VirNo = igm,
                    IndexNo = indexno,
                    Type = type,
                    SpecialInstructions = values.SpecialInstructions,
                    HoldType = values.HoldType,
                    HoldDate = values.HoldDate,
                    Nature = "Normal",
                    Role = concat
                };


                _holdRepo.Add(hld);
                 
                return Json(new { error = false, message = "this index all containers are hold" });
                 
            }
            catch (Exception e)
            {

                return Json(new { error = true, message = e.InnerException.InnerException.Message });
            }



        }


        [HttpPost]
        public async Task<IActionResult> ReleasedHold(Hold values)
        {
            var identityUser = await _userManager.GetUserAsync(User);

            var resdata = _holdPrivilegeRepository.GetAll().Where(x => x.UserName == identityUser.UserName  && x.HoldType == values.HoldType && x.IsActive == true).ToList();

            if(resdata.Count() > 0)
            { 
                if(values.Type == "CFS")
                {

                    var _cIndex = _containerIndexRepo.GetContainerIndexByIgmAndIndex(values.VirNo  , values.IndexNo).ToList();
                    var holdres = _holdRepo.GetAll().Where(x => x.HoldId == values.HoldId).LastOrDefault();

                    if (_cIndex.Count() > 0 && holdres != null)
                    {


                        _cIndex.ForEach(x => x.RO = values.RO);

                        holdres.IsHold = false;
                        holdres.RemoveInstruction = values.RemoveInstruction;
                        holdres.RO = values.RO;

                        _containerIndexRepo.UpdateRange(_cIndex);
                        _holdRepo.Update(holdres);

                        return Json(new { error = false, message = "hold remove" });
                    }
                    else
                    {
                        return Json(new { error = true, message = "no data found" });

                    }
                }
                else
                {
                    var _cIndex = _cyContainerRepo.GetUndeliveredIndexbyigmindex(values.VirNo , values.IndexNo).ToList();
                    var holdres = _holdRepo.GetAll().Where(x => x.HoldId == values.HoldId).LastOrDefault();

                    if (_cIndex.Count() > 0 && holdres != null)
                    {


                        _cIndex.ForEach(x => x.RO = values.RO);

                        holdres.IsHold = false;
                        holdres.RemoveInstruction = values.RemoveInstruction;
                        holdres.RO = values.RO;

                        _cyContainerRepo.UpdateRange(_cIndex);
                        _holdRepo.Update(holdres);

                        return Json(new { error = false, message = "hold remove" });
                    }
                    else
                    {
                        return Json(new { error = true, message = "no data found" });

                    }
                }

                
            }

            else
            {
                return Json(new { error = true, message = "user not found in hold privileges" });

            }






        }

    }
}