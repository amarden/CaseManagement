using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CaseManagement.DataObjects;
using CaseManagement.Models.SQL;

namespace CaseManagement.Controllers.Lookups
{
    public class VisitResultsController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/VisitResults
        public IQueryable<VisitResult> GetVisitResults()
        {
            return db.VisitResults;
        }

        // GET: api/VisitResults/5
        [ResponseType(typeof(VisitResult))]
        public async Task<IHttpActionResult> GetVisitResult(int id)
        {
            VisitResult visitResult = await db.VisitResults.FindAsync(id);
            if (visitResult == null)
            {
                return NotFound();
            }

            return Ok(visitResult);
        }

        // PUT: api/VisitResults/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVisitResult(int id, VisitResult visitResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != visitResult.visitResultId)
            {
                return BadRequest();
            }

            db.Entry(visitResult).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitResultExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/VisitResults
        [ResponseType(typeof(VisitResult))]
        public async Task<IHttpActionResult> PostVisitResult(VisitResult visitResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VisitResults.Add(visitResult);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = visitResult.visitResultId }, visitResult);
        }

        // DELETE: api/VisitResults/5
        [ResponseType(typeof(VisitResult))]
        public async Task<IHttpActionResult> DeleteVisitResult(int id)
        {
            VisitResult visitResult = await db.VisitResults.FindAsync(id);
            if (visitResult == null)
            {
                return NotFound();
            }

            db.VisitResults.Remove(visitResult);
            await db.SaveChangesAsync();

            return Ok(visitResult);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VisitResultExists(int id)
        {
            return db.VisitResults.Count(e => e.visitResultId == id) > 0;
        }
    }
}