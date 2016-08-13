using CaseManagement.DataObjects;
using CaseManagement.Models.SQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mime;

namespace CaseManagement.Controllers
{
    public class UploadController : Controller
    {
        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Uploads";

        public void Create(HttpPostedFileBase uploadFile, int fileType, int clientId)
        {
            string clientFolder = workingFolder + @"\" + clientId;

            if (!Directory.Exists(workingFolder))
            {
                Directory.CreateDirectory(workingFolder);
            }

            if (!Directory.Exists(clientFolder))
            {
                Directory.CreateDirectory(clientFolder);
            }
            ClientDocument document = new ClientDocument();
            document.clientId = clientId;
            document.fileTypeId = fileType;
            document.uploadDate = DateTime.Now;
            document.fileName = uploadFile.FileName;
            document.filePath = clientFolder + "\\" + (uploadFile.FileName);

            uploadFile.SaveAs(clientFolder + "\\" +(uploadFile.FileName));

            using (var db = new SQLDatabase())
            {
                db.ClientDocuments.Add(document);
                db.SaveChanges();
            }
        }

        [HttpGet]
        public FileResult Download(int docId)
        {
            ClientDocument file;
            using (var db = new SQLDatabase())
            {
                file = db.ClientDocuments.Find(docId);
            }
            string type = "";
            var fileExtension = file.fileName.Split('.').Last();
            switch(fileExtension)
            {
                case "docx":
                    type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case "pdf":
                    type = MediaTypeNames.Application.Pdf;
                    break;
            }
            return File(file.filePath, type);
        }
    }
}