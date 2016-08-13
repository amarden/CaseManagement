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
    public class EthnicitiesController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/Ethnicities
        public IQueryable<Ethnicity> GetEthnicities()
        {
            return db.Ethnicities;
        }

        // GET: api/Ethnicities/5
        [ResponseType(typeof(Ethnicity))]
        public async Task<IHttpActionResult> GetEthnicity(int id)
        {
            Ethnicity ethnicity = await db.Ethnicities.FindAsync(id);
            if (ethnicity == null)
            {
                return NotFound();
            }

            return Ok(ethnicity);
        }

        // PUT: api/Ethnicities/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEthnicity(int id, Ethnicity ethnicity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ethnicity.ethnicityId)
            {
                return BadRequest();
            }

            db.Entry(ethnicity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EthnicityExists(id))
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

        // POST: api/Ethnicities
        [ResponseType(typeof(Ethnicity))]
        public async Task<IHttpActionResult> PostEthnicity(Ethnicity ethnicity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ethnicities.Add(ethnicity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = ethnicity.ethnicityId }, ethnicity);
        }

        // DELETE: api/Ethnicities/5
        [ResponseType(typeof(Ethnicity))]
        public async Task<IHttpActionResult> DeleteEthnicity(int id)
        {
            Ethnicity ethnicity = await db.Ethnicities.FindAsync(id);
            if (ethnicity == null)
            {
                return NotFound();
            }

            db.Ethnicities.Remove(ethnicity);
            await db.SaveChangesAsync();

            return Ok(ethnicity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EthnicityExists(int id)
        {
            return db.Ethnicities.Count(e => e.ethnicityId == id) > 0;
        }
    }
}