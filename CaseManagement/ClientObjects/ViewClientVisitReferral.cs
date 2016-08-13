using CaseManagement.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.ClientObjects
{
    public class ViewClientVisitReferral
    {
        public int clientVisitReferralId { get; set; }

        public int clientVisitId { get; set; }

        public int referralTypeId { get; set; }

        public int agencyId { get; set; }

        public string contactName { get; set; }

        public bool isProvider { get; set; }

        public int? needId { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual ClientVisit ClientVisit { get; set; }

        public virtual ReferralType ReferralType { get; set; }
        public string agencyName { get; set; }
        public int? associatedProviderId { get; set; }


    }
}
