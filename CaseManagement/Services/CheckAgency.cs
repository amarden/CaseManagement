using AutoMapper;
using AutoMapper.QueryableExtensions;
using CaseManagement.ClientObjects;
using CaseManagement.DataObjects;
using System.Collections.Generic;
using System.Linq;
using System;
using CaseManagement.Models.SQL;

namespace CaseManagement.Services
{
    public class CheckAgency
    {
        List<Agency> agencies = new List<Agency>();
        private int clientId;
        public CheckAgency(int id)
        {
            this.agencies = queryAgencies();
            this.clientId = id; 
        }

        private List<Agency> queryAgencies()
        {
            using (var db = new SQLDatabase())
            {
                return db.Agencies.ToList();
            }
        }

        public List<ClientVisitReferral> ProcessReferrals(List<ViewClientVisitReferral> agencies)
        {
            Mapper.CreateMap<ViewClientVisitReferral, ClientVisitReferral>();
            foreach (var agency in agencies)
            {
                try
                {
                    agency.agencyId = grabAgencyId(agency.agencyName);
                    if (agency.isProvider && agency.associatedProviderId == null)
                    {
                        agency.associatedProviderId = CreateProvider(agency);
                    }
                }
                catch
                {
                    throw;
                }
     
            }
            return agencies.AsQueryable().ProjectTo<ClientVisitReferral>().ToList();
        }

        private int CreateProvider(ViewClientVisitReferral agency)
        {
            ClientProvider cp = new ClientProvider();
            cp.agencyId = agency.agencyId;
            cp.dateCreated = DateTime.Now;
            cp.needId = (int)agency.needId;
            cp.contactPerson = agency.contactName;
            cp.clientId = this.clientId;
            using (var db = new SQLDatabase())
            {
                db.ClientProviders.Add(cp);
                db.SaveChanges();
            }
            return cp.clientProviderId;
        }

        public List<ClientProvider> ProcessProviders(List<ViewClientProvider> agencies)
        {
            Mapper.CreateMap<ViewClientProvider, ClientProvider>();
            foreach (var agency in agencies)
            {
                agency.agencyId = grabAgencyId(agency.agencyName);
            }
            return agencies.AsQueryable().ProjectTo<ClientProvider>().ToList();
        }

        public int grabAgencyId (string agencyName)
        {
            try
            {
                if (agencyName == "")
                {
                    throw new Exception("Must fill in something for Agency");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            int id;
            var dbItem = this.agencies.Where(x => x.name == agencyName).SingleOrDefault();
            if (dbItem != null)
            {
                id = dbItem.agencyId;
            }
            else
            {
                Agency a = new Agency();
                a.name = agencyName;
                using (var db = new SQLDatabase())
                {
                    db.Agencies.Add(a);
                    db.SaveChanges();
                    id = a.agencyId;
                }
            }
            return id;
        }
    }
}
