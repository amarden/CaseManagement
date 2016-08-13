namespace CaseManagement.DataObjects
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VisitResult")]
    public partial class VisitResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VisitResult()
        {
            ClientVisits = new HashSet<ClientVisit>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int visitResultId { get; set; }

        [Required]
        [StringLength(50)]
        public string visitResultName { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientVisit> ClientVisits { get; set; }
    }
}
