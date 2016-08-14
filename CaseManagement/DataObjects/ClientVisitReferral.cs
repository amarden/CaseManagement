namespace CaseManagement.DataObjects
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientVisitReferral")]
    public partial class ClientVisitReferral
    {
        public int clientVisitReferralId { get; set; }

        public int clientVisitId { get; set; }

        public int referralTypeId { get; set; }

        public int agencyId { get; set; }

        [StringLength(255)]
        public string contactName { get; set; }

        [StringLength(50)]
        public string contactPhone { get; set; }

        public int? needId { get; set; }

        public virtual Agency Agency { get; set; }

        [JsonIgnore]
        public virtual ClientVisit ClientVisit { get; set; }

        public virtual ReferralType ReferralType { get; set; }
    }
}
