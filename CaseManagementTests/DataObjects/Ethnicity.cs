namespace CaseManagementTest.Models.SQL
{
	using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    [Table("Ethnicity")]
    public partial class Ethnicity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ethnicity()
        {
            Clients = new HashSet<Client>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ethnicityId { get; set; }

        [Required]
        [StringLength(50)]
        public string ethnicityName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; }
    }
}
