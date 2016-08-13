namespace CaseManagement.DataObjects
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientVisit")]
    public partial class ClientVisit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientVisit()
        {
            ClientVisitReferrals = new HashSet<ClientVisitReferral>();
        }

        public int clientId { get; set; }

        public int clientVisitId { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateVisit { get; set; }

        public int contactTypeId { get; set; }

        public int responderId { get; set; }

        public int timeSpentMintues { get; set; }

        public bool housingChange { get; set; }

        public bool mentalHealthChange { get; set; }

        public DateTime? dateFollowUp { get; set; }

        public int visitResultId { get; set; }

        [Column(TypeName = "text")]
        public string notes { get; set; }

        public bool followUp { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateEdited { get; set; }

        [JsonIgnore]
        public virtual Client Client { get; set; }

        public virtual ContactType ContactType { get; set; }

        public virtual Responder Responder { get; set; }

        public virtual VisitResult VisitResult { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientVisitReferral> ClientVisitReferrals { get; set; }
    }
}
