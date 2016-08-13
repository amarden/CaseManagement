namespace CaseManagementTest.Models.SQL
{
	using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Need")]
    public partial class Need
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Need()
        {
            ClientNeeds = new HashSet<ClientNeed>();
            ClientProviders = new HashSet<ClientProvider>();
        }

        public int needId { get; set; }

        [Required]
        [StringLength(50)]
        public string needName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[JsonIgnore]
        public virtual ICollection<ClientNeed> ClientNeeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[JsonIgnore]
        public virtual ICollection<ClientProvider> ClientProviders { get; set; }
    }
}
