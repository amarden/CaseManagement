using CaseManagement.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.ClientObjects
{
    public class ViewClientVisit
    {

        public int clientId { get; set; }

        public int clientVisitId { get; set; }

        public DateTime dateVisit { get; set; }

        public int contactTypeId { get; set; }

        public int responderId { get; set; }

        public int timeSpentMintues { get; set; }

        public bool housingChange { get; set; }

        public bool mentalHealthChange { get; set; }

        public DateTime? dateFollowUp { get; set; }

        public int visitResultId { get; set; }

        public string notes { get; set; }

        public bool followUp { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateEdited { get; set; }
        public List<ViewClientVisitReferral> viewReferrals { get; set; }
        public virtual List<ClientVisitReferral> ClientVisitReferrals { get; set; }

    }
}
