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
    public class ReferralTypesController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        // GET: api/ReferralTypes
        public IQueryable<ReferralType> GetReferralTypes()
        {
            return db.ReferralTypes;
        }

        // GET: api/ReferralTypes/5
        [ResponseType(typeof(ReferralType))]
        public async Task<IHttpActionResult> GetReferralType(int id)
        {
            ReferralType referralType = await db.ReferralTypes.FindAsync(id);
            if (referralType == null)
            {
                return NotFound();
            }

            return Ok(referralType);
        }

        // PUT: api/ReferralTypes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutReferralType(int id, ReferralType referralType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != referralType.referralTypeId)
            {
                return BadRequest();
            }

            db.Entry(referralType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferralTypeExists(id))
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

        // POST: api/ReferralTypes
        [ResponseType(typeof(ReferralType))]
        public async Task<IHttpActionResult> PostReferralType(ReferralType referralType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ReferralTypes.Add(referralType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = referralType.referralTypeId }, referralType);
        }

        // DELETE: api/ReferralTypes/5
        [ResponseType(typeof(ReferralType))]
        public async Task<IHttpActionResult> DeleteReferralType(int id)
        {
            ReferralType referralType = await db.ReferralTypes.FindAsync(id);
            if (referralType == null)
            {
                return NotFound();
            }

            db.ReferralTypes.Remove(referralType);
            await db.SaveChangesAsync();

            return Ok(referralType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReferralTypeExists(int id)
        {
            return db.ReferralTypes.Count(e => e.referralTypeId == id) > 0;
        }
    }
}