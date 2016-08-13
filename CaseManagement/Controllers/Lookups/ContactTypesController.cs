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
    public class ContactTypesController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/ContactTypes
        public IQueryable<ContactType> GetContactTypes()
        {
            return db.ContactTypes;
        }

        // GET: api/ContactTypes/5
        [ResponseType(typeof(ContactType))]
        public async Task<IHttpActionResult> GetContactType(int id)
        {
            ContactType contactType = await db.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            return Ok(contactType);
        }

        // PUT: api/ContactTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContactType(int id, ContactType contactType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactType.contactTypeId)
            {
                return BadRequest();
            }

            db.Entry(contactType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeExists(id))
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

        // POST: api/ContactTypes
        [ResponseType(typeof(ContactType))]
        public async Task<IHttpActionResult> PostContactType(ContactType contactType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactTypes.Add(contactType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = contactType.contactTypeId }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [ResponseType(typeof(ContactType))]
        public async Task<IHttpActionResult> DeleteContactType(int id)
        {
            ContactType contactType = await db.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            db.ContactTypes.Remove(contactType);
            await db.SaveChangesAsync();

            return Ok(contactType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactTypeExists(int id)
        {
            return db.ContactTypes.Count(e => e.contactTypeId == id) > 0;
        }
    }
}