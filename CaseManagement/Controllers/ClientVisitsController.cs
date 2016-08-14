using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using RefactorThis.GraphDiff;
using System;
using CaseManagement.ClientObjects;
using CaseManagement.Services;
using CaseManagement.DataObjects;
using System.Collections.Generic;
using CaseManagement.Models.SQL;

namespace CaseManagement.Controllers
{
    [Authorize]
    public class ClientVisitsController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();
        MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<ViewClientVisit, ClientVisit>();
            cfg.CreateMap<ViewClientVisitReferral, ClientVisitReferral>();
        });


        // GET: api/ClientVisits
        public IQueryable<ClientVisit> GetClientVisits()
        {
            return db.ClientVisits;
        }

        // GET: api/ClientVisits/5
        [ResponseType(typeof(ClientVisit))]
        public async Task<IHttpActionResult> GetClientVisit(int id)
        {
            ClientVisit clientVisit = await db.ClientVisits.FindAsync(id);
            if (clientVisit == null)
            {
                return NotFound();
            }

            return Ok(clientVisit);
        }

        // PUT: api/ClientVisits/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClientVisit(int id, ViewClientVisit clientVisit)
        {
            CheckAgency ca = new CheckAgency(clientVisit.clientId);
            try
            {
                clientVisit.viewReferrals = ca.ProcessReferrals(clientVisit.viewReferrals);
            }
            catch
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cv = new ClientVisit();
            var mapper = config.CreateMapper();
            mapper.Map(clientVisit, cv);
            mapper.Map(clientVisit.viewReferrals, cv.ClientVisitReferrals);
            cv.dateEdited = DateTime.Now;
            db.UpdateGraph(cv, map => map
                .OwnedCollection(x=>x.ClientVisitReferrals));

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientVisitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ClientVisits
        [ResponseType(typeof(ClientVisit))]
        public async Task<IHttpActionResult> PostClientVisit(ViewClientVisit clientVisit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CheckAgency ca = new CheckAgency(clientVisit.clientId);
            clientVisit.viewReferrals = ca.ProcessReferrals(clientVisit.viewReferrals);
            var cv = new ClientVisit();
            var mapper = config.CreateMapper();
            mapper.Map(clientVisit, cv);
            cv.dateCreated = DateTime.Now;
            cv.dateEdited = DateTime.Now;
            db.ClientVisits.Add(cv);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientVisitExists(clientVisit.clientVisitId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = clientVisit.clientVisitId }, clientVisit);
        }

        // DELETE: api/ClientVisits/5
        [ResponseType(typeof(ClientVisit))]
        public async Task<IHttpActionResult> DeleteClientVisit(int id)
        {
            ClientVisit clientVisit = await db.ClientVisits.FindAsync(id);
            if (clientVisit == null)
            {
                return NotFound();
            }

            db.ClientVisits.Remove(clientVisit);
            await db.SaveChangesAsync();

            return Ok(clientVisit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientVisitExists(int id)
        {
            return db.ClientVisits.Count(e => e.clientVisitId == id) > 0;
        }
    }
}