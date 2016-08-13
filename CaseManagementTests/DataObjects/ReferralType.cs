namespace CaseManagementTest.Models.SQL
{
	using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReferralType")]
    public partial class ReferralType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReferralType()
        {
            ClientVisitReferrals = new HashSet<ClientVisitReferral>();
        }

        public int referralTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string referralTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[JsonIgnore]
        public virtual ICollection<ClientVisitReferral> ClientVisitReferrals { get; set; }
    }
}
