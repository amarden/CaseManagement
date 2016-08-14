using CaseManagement.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.ClientObjects
{
    public class ViewReferral
    {
        public int clientVisitId { get; set; }

        public DateTime clientVisitDate { get; set; }

        public string referralType { get; set; }

        public string agency { get; set; }

        public string contactName { get; set; }
        public string contactPhone { get; set; }

        public int? needId { get; set; }
    }
}
