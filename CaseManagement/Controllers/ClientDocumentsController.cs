using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CaseManagement.DataObjects;
using CaseManagement.Models.SQL;
using System.IO;

namespace CaseManagement.Controllers
{
    [Authorize]
    public class ClientDocumentsController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/ClientDocuments/5
        public List<ClientDocument> GetClientDocument(int id)
        {
            return db.ClientDocuments.Where(x => x.clientId == id).ToList();
        }

        // DELETE: api/ClientDocuments/5
        [ResponseType(typeof(ClientDocument))]
        public void DeleteClientDocument(int id)
        {
            var doc = db.ClientDocuments.Where(x => x.clientDocumentId == id).Single();
            var file = new FileInfo(doc.filePath);
            file.Delete();
            db.ClientDocuments.Remove(doc);
            db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}