using CaseManagement.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseManagement.ClientObjects
{
    public class ViewClient
    {
        public int clientId { get; set; }
        public int waysideId { get; set; }
        public string firstName { get; set; }
        public string nickName { get; set; }
        public string lastName { get; set; }
        public DateTime? dob { get; set; }
        public string address { get; set; }
        public DateTime intakeDate { get; set; }
        public int genderId { get; set; }
        public int languageId { get; set; }
        public bool veteran { get; set; }
        public int ethnicityId { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateEdited { get; set; }
        public int learnAboutId { get; set; }
        public int raceId { get; set; }
        public int housingStatusId { get; set; }
        public bool houseLossRisk { get; set; }
        public int translationId { get; set; }
        public bool canContact { get; set; }
        public int? houseHoldCount { get; set; }
        public virtual ICollection<ClientDocument> ClientDocuments { get; set; }
        public virtual ICollection<ClientFamily> ClientFamilies { get; set; }
        public virtual ICollection<ClientNeed> ClientNeeds { get; set; }
        public virtual ICollection<ClientVisit> ClientVisits { get; set; }
        public List<ViewReferral> ClientVisitReferrals { get; set; }
    }
}