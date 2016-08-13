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
    public class NeedsController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/Needs
        public IQueryable<Need> GetNeeds()
        {
            return db.Needs;
        }

        // GET: api/Needs/5
        [ResponseType(typeof(Need))]
        public async Task<IHttpActionResult> GetNeed(int id)
        {
            Need need = await db.Needs.FindAsync(id);
            if (need == null)
            {
                return NotFound();
            }

            return Ok(need);
        }

        // PUT: api/Needs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNeed(int id, Need need)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != need.needId)
            {
                return BadRequest();
            }

            db.Entry(need).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NeedExists(id))
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

        // POST: api/Needs
        [ResponseType(typeof(Need))]
        public async Task<IHttpActionResult> PostNeed(Need need)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Needs.Add(need);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = need.needId }, need);
        }

        // DELETE: api/Needs/5
        [ResponseType(typeof(Need))]
        public async Task<IHttpActionResult> DeleteNeed(int id)
        {
            Need need = await db.Needs.FindAsync(id);
            if (need == null)
            {
                return NotFound();
            }

            db.Needs.Remove(need);
            await db.SaveChangesAsync();

            return Ok(need);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NeedExists(int id)
        {
            return db.Needs.Count(e => e.needId == id) > 0;
        }
    }
}