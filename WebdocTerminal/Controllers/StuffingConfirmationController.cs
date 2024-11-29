using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Controllers
{
    [MyAuthorize]
    public class StuffingConfirmationController : Controller
    {
        private ISCMORepository _scmoRepo;
        private IHostingEnvironment env;
        private IContainerIndexRepository _containerIndexRepository;
        private IDeliveryOrderRepository _deliveryOrderRepository;
        private ICCMORepository _ccmoRepo;
        private IOrderDetailRepository _orderDetailRepository;
        private readonly IOptions<AppConfig> _config;

        private WebocProcessor _webocProcessor;
        public StuffingConfirmationController(ISCMORepository scmoRepo , 
                                              WebocProcessor webocProcessor, IHostingEnvironment _env,
                                              IOptions<AppConfig> config ,
                                              ICCMORepository ccmoRepo ,
                                              IContainerIndexRepository containerIndexRepository,
                                              IDeliveryOrderRepository deliveryOrderRepository,
                                              IOrderDetailRepository orderDetailRepository)
        {
            _scmoRepo = scmoRepo;
            _webocProcessor = webocProcessor;
            env = _env;
            _config = config;
            _ccmoRepo = ccmoRepo;
            _containerIndexRepository = containerIndexRepository;
            _deliveryOrderRepository = deliveryOrderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public IActionResult StuffingConfirmationMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStuffingConfirmation(List<StuffingConfirmationViewModel> data)
        {
            try
            {

             
                var datetime = DateTime.Now;


                bool hasMatch = _scmoRepo.GetAll().Any(x => data.Any(y => y.ContainerNo == x.ContainerNo && y.VIRNo == x.VIRNo && y.BLNo == x.BLNo));

                if (!hasMatch)
                {
                    List<SCMO> scmos = new List<SCMO>();
                    foreach (var item in data)
                    {

                        var containerindex = _containerIndexRepository.GetFirst(x => x.Voyage.VIRNo == item.VIRNo && x.BLNo == item.BLNo && x.ContainerNo == item.ContainerNumberIGMO && x.IndexNo == item.IndexNo, v => v.Voyage);

 
                            if (containerindex != null)
                            {
                              var delivery = _deliveryOrderRepository.GetFirst(x => x.ContainerIndexId == containerindex.ContainerIndexId);

                              if(delivery == null){
                                return new OkObjectResult(new { error = true, message = "Please Create Do Of This Container " + containerindex.ContainerNo +"& Index Of"  + containerindex.IndexNo});
                                }

                            if (delivery != null)
                            {
                                var gatePass = _orderDetailRepository.GetFirst(x => x.DeliveryOrderId == delivery.DeliveryOrderId);
                                if (gatePass == null)
                                {
                                    return new OkObjectResult(new { error = true, message = "Please Generate Gate Pass Of Do " + delivery.DONumber });
                                }
                            }



                        }

                        if (containerindex == null) {
                            return new OkObjectResult(new { error = true, message = "Not Found This Container No " + containerindex.ContainerNo + "& Index Of" + containerindex.IndexNo });

                        }


                        if (containerindex.IsHold == true)
                        {
                            return new OkObjectResult(new { error = true, message = "This Container is currently on hold" });
                        }

                        var indexes = _ccmoRepo.GetAll().Where(s => s.ContainerNo == item.ContainerNo && s.VIRNo == item.VIRNo).ToList().Count();
                        var svmoindexes = data.Where(s => s.ContainerNo == item.ContainerNo && s.VIRNo == item.VIRNo).ToList().Count();
                         
                        if (svmoindexes != indexes)
                        {
                            return Json(new { error = true, message = "Please select  all indexes Of this container" });

                        }


                        var scmo = new SCMO
                        {
                            VIRNo = item.VIRNo,
                            BLNo = item.BLNo,
                            Category = item.Category,
                            IndexNo = item.IndexNo ?? 0,
                            ContainerNo = item.ContainerNo,
                            Weight = item.Weight,
                            NoOfPackages = item.NoOfPackages,
                            OpType = "A",
                            Performed = DateTime.Now,
                            TpNo = item.TPNo,
                            MessageFrom = "BWP",
                            MessageTo = "WEBOC",
                            FileName = $"SCMO_{DateFormatter.ConvertToyyyyMMddFormat(datetime)}_{DateFormatter.ConvertToHHmmFormat(datetime)}{".txt"}"



                        };
                        scmos.Add(scmo);
                    }


                    _scmoRepo.AddRange(scmos);

                    return new OkObjectResult(new { error = false, message = "Saved" });

                }
                else
                {
                    return new OkObjectResult(new { error = true, message = "The list contains a container which is already stuffed. Please remove the container and try again." });
                }




            }
            catch (Exception e)
            {
                foreach (var item in data)
                {

                    var result = _scmoRepo.GetFirst(g => g.ContainerNo == item.ContainerNo && g.VIRNo == item.VIRNo);
                    if (result != null)
                        _scmoRepo.Delete(result);
                }

                return new OkObjectResult(new { error = true, message = "Error! Please Stuffed again." });


            }

            return Ok();
        }
    }
}