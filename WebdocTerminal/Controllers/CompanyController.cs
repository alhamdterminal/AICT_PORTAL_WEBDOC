using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebdocTerminal.Helpers;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;


namespace WebdocTerminal.Controllers
{
    public class CompanyController : Controller
    {
        private ICompanyRepository _repo;
        private IHostingEnvironment env; 

        public CompanyController(ICompanyRepository repo, IHostingEnvironment _env   )
        {
            _repo = repo;
            env = _env; 
        }

        public JsonResult GetCompanies()
        {
            var data = _repo.GetAll().Where(x=> x.Status == true);
            if(data != null)
            {
                return Json(data);
            }
            return Json("");
        }
         
        public IActionResult ComapanyView()
        {
            return View();
        }
   
         public IActionResult updateCompany(Company model)
        {
            var company = _repo.GetFirst(x => x.CompanyName.Trim().ToUpper() == model.CompanyName.Trim().ToUpper() && x.CompanyId != model.CompanyId);

            if (company != null)
            {
                return new JsonResult(new { error = true, message = "This Company Name Already Exist" });
            }

            company.CompanyName = model.CompanyName;
            company.Address = model.Address;
            company.CompanyEmail = model.CompanyEmail;

            _repo.Update(company);

            return new JsonResult(new { error = false, message = "Saved" });
        }



        public IActionResult Delete(long key)
        {

            var user = _repo.GetUserByCompanyId(key);

            if(user != null)
            { 
                return new JsonResult(new { error = true, message = "This Company Has Already Assign To User" });
            }
            var company = _repo.GetAll().Where(x=> x.CompanyId == key).FirstOrDefault();

            company.Status = false;
             
            _repo.Update(company);
            return new JsonResult(new { error = false , message = "Delete" });
        }

        public JsonResult GetCompanyByID(long key)
        {
            var company = _repo.GetAll().Where(x => x.CompanyId == key).FirstOrDefault();
            if(company != null)
            {
                return Json(company);
            }
            return Json("");
        }



        public async Task<IActionResult> UpdateDocument(IFormFile f, string CompanyName, string CompanyEmail, string Address , long CompanyId)

        {
            var company = _repo.GetFirst(x => x.CompanyId  == CompanyId);

            if(f != null)
            {
                string storePath = f.Name;
                string docFolder = Path.Combine("CompanyImages", storePath);


                try
                {
                    if (f == null || f.Length == 0)
                    {
                        return Content("File not selected");
                    }

                    string path = FileUploadHandler.GetFilePathForUpload(docFolder, f.FileName);

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await f.CopyToAsync(stream);
                    }
                }
                catch (Exception e)
                {
                    return Json(new { result = "Upload Failed", error = e.Message });
                }

                string filePath = FileUploadHandler.FileReturnPath(docFolder, f.FileName);

                company.CompanyName = CompanyName;
                company.CompanyEmail = CompanyEmail;
                company.Address = Address;
                company.FilePath = filePath;
                _repo.Update(company);
                 
                return new OkObjectResult(new { error = true, message = "Saved" });

            } 

            company.CompanyName = CompanyName;
            company.CompanyEmail = CompanyEmail;
            company.Address = Address;
            company.FilePath = company.FilePath;





            _repo.Update(company);


            return new OkObjectResult(new { error = true, message = "Saved" });
        }


        public async Task<IActionResult> AddDocument(IFormFile f, string CompanyName, string CompanyEmail, string Address)

        {
             
         
            string storePath = f.Name;
            string docFolder = Path.Combine( "CompanyImages", storePath);
          
       
            try
            {
                if (f == null || f.Length == 0)
                {
                    return Content("File not selected");
                }

                string path = FileUploadHandler.GetFilePathForUpload(    docFolder, f.FileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await f.CopyToAsync(stream);
                }
            }
            catch (Exception e)
            {
                return Json(new { result = "Upload Failed", error = e.Message });
            }

            string filePath = FileUploadHandler.FileReturnPath(docFolder, f.FileName);

            
                Company doc = new Company
                {
                    CompanyName = CompanyName,
                    CompanyEmail = CompanyEmail,
                    Address = Address,
                    FilePath = filePath ,
                    Status = true

                };
                _repo.Add(doc);
            

            return new OkObjectResult(new { error = true, message = "Saved" });
        }
 
        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

    
    }
}