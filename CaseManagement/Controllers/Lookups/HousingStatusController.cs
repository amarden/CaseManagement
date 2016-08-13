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
    public class HousingStatusController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/HousingStatus
        public IQueryable<HousingStatu> GetHousingStatus()
        {
            return db.HousingStatus;
        }

        // GET: api/HousingStatus/5
        [ResponseType(typeof(HousingStatu))]
        public async Task<IHttpActionResult> GetHousingStatu(int id)
        {
            HousingStatu housingStatu = await db.HousingStatus.FindAsync(id);
            if (housingStatu == null)
            {
                return NotFound();
            }

            return Ok(housingStatu);
        }

        // PUT: api/HousingStatus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHousingStatu(int id, HousingStatu housingStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != housingStatu.housingStatusId)
            {
                return BadRequest();
            }

            db.Entry(housingStatu).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HousingStatuExists(id))
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

        // POST: api/HousingStatus
        [ResponseType(typeof(HousingStatu))]
        public async Task<IHttpActionResult> PostHousingStatu(HousingStatu housingStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HousingStatus.Add(housingStatu);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = housingStatu.housingStatusId }, housingStatu);
        }

        // DELETE: api/HousingStatus/5
        [ResponseType(typeof(HousingStatu))]
        public async Task<IHttpActionResult> DeleteHousingStatu(int id)
        {
            HousingStatu housingStatu = await db.HousingStatus.FindAsync(id);
            if (housingStatu == null)
            {
                return NotFound();
            }

            db.HousingStatus.Remove(housingStatu);
            await db.SaveChangesAsync();

            return Ok(housingStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HousingStatuExists(int id)
        {
            return db.HousingStatus.Count(e => e.housingStatusId == id) > 0;
        }
    }
}