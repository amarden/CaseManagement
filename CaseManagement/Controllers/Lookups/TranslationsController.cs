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
    public class TranslationsController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/Translations
        public IQueryable<Translation> GetTranslations()
        {
            return db.Translations;
        }

        // GET: api/Translations/5
        [ResponseType(typeof(Translation))]
        public async Task<IHttpActionResult> GetTranslation(int id)
        {
            Translation translation = await db.Translations.FindAsync(id);
            if (translation == null)
            {
                return NotFound();
            }

            return Ok(translation);
        }

        // PUT: api/Translations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTranslation(int id, Translation translation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != translation.translationId)
            {
                return BadRequest();
            }

            db.Entry(translation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranslationExists(id))
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

        // POST: api/Translations
        [ResponseType(typeof(Translation))]
        public async Task<IHttpActionResult> PostTranslation(Translation translation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Translations.Add(translation);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TranslationExists(translation.translationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = translation.translationId }, translation);
        }

        // DELETE: api/Translations/5
        [ResponseType(typeof(Translation))]
        public async Task<IHttpActionResult> DeleteTranslation(int id)
        {
            Translation translation = await db.Translations.FindAsync(id);
            if (translation == null)
            {
                return NotFound();
            }

            db.Translations.Remove(translation);
            await db.SaveChangesAsync();

            return Ok(translation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TranslationExists(int id)
        {
            return db.Translations.Count(e => e.translationId == id) > 0;
        }
    }
}