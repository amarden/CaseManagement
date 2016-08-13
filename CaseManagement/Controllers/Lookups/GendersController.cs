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
    public class GendersController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/Genders
        public IQueryable<Gender> GetGenders()
        {
            return db.Genders;
        }

        // GET: api/Genders/5
        [ResponseType(typeof(Gender))]
        public async Task<IHttpActionResult> GetGender(int id)
        {
            Gender gender = await db.Genders.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }

            return Ok(gender);
        }

        // PUT: api/Genders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGender(int id, Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gender.genderId)
            {
                return BadRequest();
            }

            db.Entry(gender).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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

        // POST: api/Genders
        [ResponseType(typeof(Gender))]
        public async Task<IHttpActionResult> PostGender(Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genders.Add(gender);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GenderExists(gender.genderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = gender.genderId }, gender);
        }

        // DELETE: api/Genders/5
        [ResponseType(typeof(Gender))]
        public async Task<IHttpActionResult> DeleteGender(int id)
        {
            Gender gender = await db.Genders.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }

            db.Genders.Remove(gender);
            await db.SaveChangesAsync();

            return Ok(gender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenderExists(int id)
        {
            return db.Genders.Count(e => e.genderId == id) > 0;
        }
    }
}