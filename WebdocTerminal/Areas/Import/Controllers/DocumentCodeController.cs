using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Interfaces;

namespace WebdocTerminal.Areas.Import.Controllers
{
    public class DocumentCodeController : ParentController
    {


        public IDocumentCodeRepository  _documentCodeRepository;
        public DocumentCodeController(IDocumentCodeRepository documentCodeRepository)
        {
             _documentCodeRepository =  documentCodeRepository ;
        }

        public IActionResult DocumentCodeView()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDocumentCode()
        {
            var documentCodes = _documentCodeRepository.GetAll();
            return Json(documentCodes);
        }

        [HttpPost]
        public IActionResult AddDocumentCode(DocumentCode documentCode)
        {
            _documentCodeRepository.Add(documentCode);
            return new OkObjectResult(new { DocumentCodeId = documentCode.DocumentCodeId});
        }

        [HttpPost]
        public IActionResult UpdateDocumentCode(DocumentCode documentCode)
        {
            _documentCodeRepository.Update(documentCode);
            return new OkObjectResult(new { DocumentCodeId = documentCode.DocumentCodeId });
        }

        public void DeleteDocumentCode(long key)
        {
            var data = _documentCodeRepository.Find(key);

            _documentCodeRepository.Delete(data);
        }

    }
}
