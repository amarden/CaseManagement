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
    public class AgenciesController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/Agencies
        public IQueryable<Agency> GetAgencies()
        {
            return db.Agencies;
        }

        // GET: api/Agencies/5
        [ResponseType(typeof(Agency))]
        public async Task<IHttpActionResult> GetAgency(int id)
        {
            Agency agency = await db.Agencies.FindAsync(id);
            if (agency == null)
            {
                return NotFound();
            }

            return Ok(agency);
        }

        // PUT: api/Agencies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAgency(int id, Agency agency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agency.agencyId)
            {
                return BadRequest();
            }

            db.Entry(agency).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyExists(id))
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

        // POST: api/Agencies
        [ResponseType(typeof(Agency))]
        public async Task<IHttpActionResult> PostAgency(Agency agency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Agencies.Add(agency);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AgencyExists(agency.agencyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = agency.agencyId }, agency);
        }

        // DELETE: api/Agencies/5
        [ResponseType(typeof(Agency))]
        public async Task<IHttpActionResult> DeleteAgency(int id)
        {
            Agency agency = await db.Agencies.FindAsync(id);
            if (agency == null)
            {
                return NotFound();
            }

            db.Agencies.Remove(agency);
            await db.SaveChangesAsync();

            return Ok(agency);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgencyExists(int id)
        {
            return db.Agencies.Count(e => e.agencyId == id) > 0;
        }
    }
}