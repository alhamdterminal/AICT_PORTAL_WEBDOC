using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class BRTController : ParentController
    {
        private IBRTRepository _repo;
        private IBrtItemRepository _brtitemrepo;
        private IBRTLocationRepository _brtLocationRepository;
        private IGroundedDaysRepository _groundedDaysRepository;

        public BRTController(IBRTRepository repo , 
            IBrtItemRepository brtitemrepo,
            IBRTLocationRepository brtLocationRepository,
            IGroundedDaysRepository groundedDaysRepository)
        {
            _repo = repo;
            _brtitemrepo = brtitemrepo;
            _brtLocationRepository = brtLocationRepository;
            _groundedDaysRepository = groundedDaysRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BrtViewCY()
        {

            ViewData["BRTLocations"] = _brtLocationRepository.GetAll()
                    .Select(v => new SelectListItem { Text = v.BRTLocationName, Value = v.BRTLocationId.ToString() });


            return View();
        }


        public IActionResult UpdateBRTItem(long brtitemId , BrtItem item)
        {

            var brtitem = _brtitemrepo.GetAll().Where(x => x.BrtItemId == brtitemId).LastOrDefault();

            brtitem.NumberOfItems = item.NumberOfItems;
            brtitem.PackageLocation = item.PackageLocation;

            _brtitemrepo.Update(brtitem);


            return new OkObjectResult(new { message = "Location Saved" });

        }





        public IActionResult UpdateIndexLocation(long ContainerIndexId, string Location, List<BrtItem> items)
        {
            var brt = _repo.GetAll().Where(c => c.ContainerIndexId == ContainerIndexId).LastOrDefault();

            if (brt != null)
            {
                brt.Location = Location;


                var result = _brtitemrepo.GetList(x => x.BRTId == brt.BRTId);
                if (result != null)
                {
                    _brtitemrepo.DeleteRange(result);
                }


                foreach (var detail in items)
                {
                    detail.BRTId = brt.BRTId; 
                    detail.BrtItemId = 0;
                    _brtitemrepo.Add(detail);

                } 

                _repo.Update(brt);

                return new OkObjectResult(new { message = "Location Updated" });
            }
            else
            {
                List<BrtItem> BrtItems = new List<BrtItem>();

                brt = new BRT
                {
                    ContainerIndexId = ContainerIndexId,
                    Location = Location
                };

                _repo.Add(brt);

                foreach (var item in items)
                {
                    var brtitm = new BrtItem
                    {
                        BRTId = brt.BRTId,
                        NumberOfItems = item.NumberOfItems,
                        PackageLocation = item.PackageLocation,

                    };

                    BrtItems.Add(brtitm);

                }
                _brtitemrepo.AddRange(BrtItems);
            }

          

            return new OkObjectResult(new { message = "Location Saved" });
        }


        public IActionResult UpdateCyContainerLocation(long CyContainerId, long locationid  ,  string location)
        {
             
            var   brt = new BRT
            {
                CyContainerId = CyContainerId,
                BRTLocationId = locationid
            };
            if (location.Contains("EXAMINED"))
            {

                var groundedDays = _groundedDaysRepository.GetAll().Where(x => x.CYContainerId == CyContainerId  && x.SealIssueDate == null).LastOrDefault();

                if (groundedDays != null)
                {

                    return new OkObjectResult(new { error = true, Message = "For Re examination first assign Seal on Previous examination" });


                }
                else
                {
                    var groundeddays = new GroundedDays
                    {
                        CYContainerId = CyContainerId,
                        ExaminationDate = DateTime.Now

                    };

                    _groundedDaysRepository.Add(groundeddays);
                }


                
 
            }
             
            _repo.Add(brt);

            return new OkObjectResult(new { error = false, Message = "Location Updated" });

        }


        public IActionResult DeleteBRTItem(long key)
        {

            var res = _brtitemrepo.Find(key);

            res.IsCancelled = true;
            _brtitemrepo.Update(res);

            return new OkObjectResult(new { message = "Deleted" });
        }


        public IActionResult UpdateCFSBrt(long containerId , string location)
        {

            var res = _repo.GetAll().Where(x=> x.ContainerIndexId == containerId).LastOrDefault();

            if(res != null)
            {
                res.Location = location;
                _repo.Update(res);
            }
            else
            {
                var brt = new BRT
                {
                    ContainerIndexId = containerId,
                    Location = location


                };
                 
                _repo.Add(brt);
            }

            

            return new OkObjectResult(new { message = "Update Location" });
        }

        public IActionResult UpdateCYBrt(long containerId, string location)
        {

            var res = _repo.GetAll().Where(x => x.CyContainerId == containerId).LastOrDefault();

            if (res != null)
            {

                res.Location = location;
                _repo.Update(res);
            }
            else
            {
                var brt = new BRT
                {
                    CyContainerId = containerId,
                    Location = location
                     
                };

                _repo.Add(brt);
            }



            return new OkObjectResult(new { message = "Update Location" });
        }



    }
}