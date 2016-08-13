using System;

namespace CaseManagement.Controllers
{
    public class FollowUpClient
    {
        public string firstName { get; set; }
        public DateTime dateFollowUp { get; set; }
        public int clientId { get; set; }
        public int clientVisitId { get; set; } 
    }
}