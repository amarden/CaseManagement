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
using CaseManagement.Models.SQL;
using CaseManagement.DataObjects;

namespace CaseManagement.Controllers.Lookups
{
    public class LearnAboutsController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/LearnAbouts
        public IQueryable<LearnAbout> GetLearnAbouts()
        {
            return db.LearnAbouts;
        }

        // GET: api/LearnAbouts/5
        [ResponseType(typeof(LearnAbout))]
        public async Task<IHttpActionResult> GetLearnAbout(int id)
        {
            LearnAbout learnAbout = await db.LearnAbouts.FindAsync(id);
            if (learnAbout == null)
            {
                return NotFound();
            }

            return Ok(learnAbout);
        }

        // PUT: api/LearnAbouts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLearnAbout(int id, LearnAbout learnAbout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != learnAbout.learnAboutId)
            {
                return BadRequest();
            }

            db.Entry(learnAbout).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearnAboutExists(id))
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

        // POST: api/LearnAbouts
        [ResponseType(typeof(LearnAbout))]
        public async Task<IHttpActionResult> PostLearnAbout(LearnAbout learnAbout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LearnAbouts.Add(learnAbout);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LearnAboutExists(learnAbout.learnAboutId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = learnAbout.learnAboutId }, learnAbout);
        }

        // DELETE: api/LearnAbouts/5
        [ResponseType(typeof(LearnAbout))]
        public async Task<IHttpActionResult> DeleteLearnAbout(int id)
        {
            LearnAbout learnAbout = await db.LearnAbouts.FindAsync(id);
            if (learnAbout == null)
            {
                return NotFound();
            }

            db.LearnAbouts.Remove(learnAbout);
            await db.SaveChangesAsync();

            return Ok(learnAbout);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LearnAboutExists(int id)
        {
            return db.LearnAbouts.Count(e => e.learnAboutId == id) > 0;
        }
    }
}