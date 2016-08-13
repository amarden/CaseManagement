﻿using System;
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
using AutoMapper;
using CaseManagement.Models.View;
using AutoMapper.QueryableExtensions;
using RefactorThis.GraphDiff;
using CaseManagement.Models.SQL;
using CaseManagement.DataObjects;

namespace CaseManagement.Controllers
{
    [Authorize]
    [RoutePrefix("api/Clients")]
    public class ClientsController : ApiController
    {
        private SQLDatabase db = new SQLDatabase();

        public ClientsController()
        {
        }

        public ClientsController(SQLDatabase context)
        {
            this.db = context;
        }

        // GET: api/Clients
        public IQueryable<ViewListClient> GetClients()
        {
            Mapper.CreateMap<Client, ViewListClient>()
                .ForMember(dest => dest.gender, opt => opt.MapFrom(src=> src.Gender.genderName));

            var result = db.Clients.ProjectTo<ViewListClient>();

            return db.Clients.ProjectTo<ViewListClient>();
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> GetClient(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClient(int id, Client client)
        {
            client.dateEdited = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.clientId)
            {
                return BadRequest();
            }

            db.UpdateGraph(client, map => map
                .OwnedCollection(p => p.ClientNeeds)
                .OwnedCollection(p=> p.ClientFamilies)
                .OwnedCollection(p=> p.ClientProviders));

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> PostClient(Client client)
        {
            client.dateCreated = DateTime.Now;
            client.dateEdited = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(client.clientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = client.clientId }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> DeleteClient(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            await db.SaveChangesAsync();

            return Ok(client);
        }

        [Route("FollowUp")]
        [HttpGet]
        public IQueryable<FollowUpClient> GetFollowUps()
        {
            return db.ClientVisits.Where(x => x.dateFollowUp != null)
                .Select(x => new FollowUpClient
                {
                    firstName = x.Client.firstName,
                    dateFollowUp = (DateTime)x.dateFollowUp,
                    clientId = x.clientId,
                    clientVisitId = x.clientVisitId
                });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.clientId == id) > 0;
        }
    }
}