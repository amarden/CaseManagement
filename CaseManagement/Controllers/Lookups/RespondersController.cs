using CaseManagement.DataObjects;
using CaseManagement.Models.SQL;
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

namespace CaseManagement.Controllers.Lookups
{
    public class RespondersController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/Responders
        public IQueryable<Responder> GetResponders()
        {
            return db.Responders;
        }

        // GET: api/Responders/5
        [ResponseType(typeof(Responder))]
        public async Task<IHttpActionResult> GetResponder(int id)
        {
            Responder responder = await db.Responders.FindAsync(id);
            if (responder == null)
            {
                return NotFound();
            }

            return Ok(responder);
        }

        // PUT: api/Responders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutResponder(int id, Responder responder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != responder.responderId)
            {
                return BadRequest();
            }

            db.Entry(responder).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponderExists(id))
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

        // POST: api/Responders
        [ResponseType(typeof(Responder))]
        public async Task<IHttpActionResult> PostResponder(Responder responder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Responders.Add(responder);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = responder.responderId }, responder);
        }

        // DELETE: api/Responders/5
        [ResponseType(typeof(Responder))]
        public async Task<IHttpActionResult> DeleteResponder(int id)
        {
            Responder responder = await db.Responders.FindAsync(id);
            if (responder == null)
            {
                return NotFound();
            }

            db.Responders.Remove(responder);
            await db.SaveChangesAsync();

            return Ok(responder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResponderExists(int id)
        {
            return db.Responders.Count(e => e.responderId == id) > 0;
        }
    }
}