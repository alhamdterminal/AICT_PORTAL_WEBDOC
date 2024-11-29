using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebdocTerminal.Areas.Import.Models;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

using Microsoft.AspNetCore.SignalR;
using WebdocTerminal.Hubs;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class WeighmentController : ParentController
    {
        private IWeighmentRepository _weighmentRepo;
        private IContainerIndexRepository _indexRepo;
        private ICYContainerRepository _cyContainerRepo;
        private IIndexWeighmentRepository _indexWeighmentRepository;
        private IConfiguration Configuration;
        private readonly IOptions<AppConfig> config;
        //private readonly IHubContext<NotificationHub> _notificationHub;


        public WeighmentController(IWeighmentRepository weighmentRepo, IContainerIndexRepository indexRepo,
             ICYContainerRepository cyContainerRepo,
             IIndexWeighmentRepository indexWeighmentRepository,
              IConfiguration _configuration,
                              IOptions<AppConfig> config
                              //IHubContext<NotificationHub> notificationHub
            )
        {
            _weighmentRepo = weighmentRepo;
            _indexRepo = indexRepo;
            _cyContainerRepo = cyContainerRepo;
            _indexWeighmentRepository = indexWeighmentRepository;
            Configuration = _configuration;
            this.config = config;
            //_notificationHub = notificationHub;

        }

        public IActionResult CFSWeightment()
        {
            return View();
        }

        public IActionResult CYWeightment()
        {
            return View();
        }

        public IActionResult SaveCFSWeighmentContainers(List<CFSWeighmentViewModel> containers)
        {
            var datetime = DateTime.Now;
            var weightments = new List<Weighment>();

            foreach (var container in containers)
            {
                if (container.ContainerIndexId  != null )
                {
                     
                    var weightment = new Weighment
                    {
                        WeighmentDate = datetime,
                        ContainerIndexId = container.ContainerIndexId,
                        FoundWeight = container.FoundWeight,
                        GrossWeight = container.BLGrossWeight,
                        HandlingCode = container.HandlingCode
                    };
                    weightments.Add(weightment);

                    var containerIndex = _indexRepo.Find(container.ContainerIndexId);
                    containerIndex.IsWeighed = true;
                    _indexRepo.Update(containerIndex);
                }
                else
                {
                    var weightment = new Weighment
                    {
                        WeighmentDate = datetime,
                        CyContainerId = container.CYContainerId,
                        FoundWeight = container.FoundWeight,
                        GrossWeight = container.BLGrossWeight,
                        HandlingCode = container.HandlingCode
                    };

                    weightments.Add(weightment);

                    var cycontainr = _cyContainerRepo.Find(container.CYContainerId);
                    cycontainr.IsWeighed = true;
                    _cyContainerRepo.Update(cycontainr);

                }
                

           
            }

            _weighmentRepo.AddRange(weightments);

            return Ok();
        }

        public IActionResult SaveCYWeighmentContainers(List<CFSWeighmentViewModel> containers)
        {
            var weightments = new List<Weighment>();

            foreach (var container in containers)
            {
                var weightment = new Weighment
                {
                    CyContainerId = container.CYContainerId,
                    FoundWeight = container.FoundWeight,
                    GrossWeight = container.BLGrossWeight,
                    HandlingCode = container.HandlingCode
                };

                weightments.Add(weightment);

                var cycontainr = _cyContainerRepo.Find(container.CYContainerId);
                cycontainr.IsWeighed = true;
                _cyContainerRepo.Update(cycontainr);
            }

            _weighmentRepo.AddRange(weightments);

            return Ok();
        }

        #region Index Weighment


        public IActionResult IndexWeighmentView()
        {
            return View();
        }

         

        public double GetIndexWeigth()
        {
            try
            {
                double res = 0;

                var ip = config.Value.WeightmentIP;
                var port = Convert.ToInt32(config.Value.WeightmentPort);

                using (var client = new TcpClient(ip, port))
                {

                    byte[] buffer = new byte[8];
                     
                    var stream = client.GetStream();
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (buffer.Length == 8 && bytesRead == 8)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        res = Convert.ToDouble(Regex.Replace(data, "\u0002+", ""));
                         
                    }
                    else
                    {
                        res = 0;
                    }

                    client.Close();
                }

                return res;
                //await _notificationHub.Clients.All.SendAsync("ReceiveMsg", res);

            }
            catch (Exception e)
            { 
                return 0;
            }

}


        public JsonResult GetIndexInfo(string igm , int indexno)
        {
            var res = _indexRepo.GetIndexInfoForWeighment(igm , indexno);
            if(res != null)
            {
                return Json(res);
            }
            return Json(null);

        }

        public JsonResult GetIndexWeighmentInfo(string igm, int indexno)
        {
            var res = _indexWeighmentRepository.GetIndexWeighmentByIgmIndex(igm, indexno);
 
             return Json(res);
             
        }


        public IActionResult AddIndexWeighment(string igm, int indexno , double weight   , double palletweight  , string remarks)
        {
            try
            {

                var res = _indexRepo.GetSingleIndexInfo(igm, indexno);
                if (res != null)
                {
                    var are = new IndexWeighment()
                    {
                        CreatedAt = DateTime.Now,
                        Indexno = indexno,
                        VirNo = igm,
                        IndexWeight = weight,
                        PalletWeight = palletweight,
                        Weight = weight - palletweight,
                        Remarks = remarks
                    };

                    _indexWeighmentRepository.Add(are);

                    return new OkObjectResult(new { error = false, Message = "Save" });
                     
                }
                return new OkObjectResult(new { error = true, Message = "no igm index found" });
                 
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { error = true, Message = e.InnerException.InnerException.Message});
            }

             

        }

        public IActionResult UpdateIndexWeight(long indexWeighmentId, string remarks)
        {
            var res = _indexWeighmentRepository.GetAll().Where(x=> x.IndexWeighmentId == indexWeighmentId).LastOrDefault();

            if(res != null)
            {
                if(res.IsMarkComplete == true)
                {
                    return new OkObjectResult(new { error = true, Message = "can't update due to mark complete" });
                }
                else
                {
                    res.Remarks = remarks;
                    _indexWeighmentRepository.Update(res);

                    return new OkObjectResult(new { error = false , Message = "update" });
                }
                
            }

            return new OkObjectResult(new { error = true, Message = "no record found" });

        }

        public IActionResult IndexWeightMarkComplete(string virno , long index)
        {
            long number = 1;
            var res = _indexWeighmentRepository.GetAll().Where(x => x.VirNo == virno && x.Indexno == index ).ToList();

            if (res.Count() > 0)
            {
                var newres = res.Where(x => x.IsMarkComplete == false).ToList();
                if (newres.Count() > 0)
                {
                    var lastrecord = res.Where(x => x.IsMarkComplete == true).LastOrDefault();

                    if (lastrecord != null)
                    {
                        var newnumber = lastrecord.Sequence + 1;
                        number = newnumber;
                    }


                    newres.ForEach(x => x.IsMarkComplete = true);
                    newres.ForEach(x => x.Sequence = number);
                    _indexWeighmentRepository.UpdateRange(newres);

                    return new OkObjectResult(new { error = false, Message = "Marked complete" });
                }
                else
                {
                    return new OkObjectResult(new { error = true, Message = "no data found to mark complete" });
                }
                
                 

            }

            return new OkObjectResult(new { error = true, Message = "no record found" });

        }

        public IActionResult DeleteIndexWeight(long id )
        {
            var res = _indexWeighmentRepository.GetAll().Where(x => x.IndexWeighmentId == id).LastOrDefault();

            if (res != null)
            {
                if (res.IsMarkComplete == true)
                {
                    return new OkObjectResult(new { error = true, Message = "can't update due to mark complete" });
                }
                else
                { 
                    _indexWeighmentRepository.Delete(res);

                    return new OkObjectResult(new { error = false, Message = "update" });
                }

            }

            return new OkObjectResult(new { error = true, Message = "no record found" });

        }

        #endregion


    }
}