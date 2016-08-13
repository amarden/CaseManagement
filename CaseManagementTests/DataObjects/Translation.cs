namespace CaseManagementTest.Models.SQL
{
	using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Translation")]
    public partial class Translation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Translation()
        {
            Clients = new HashSet<Client>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int translationId { get; set; }

        [Required]
        [StringLength(10)]
        public string translationValue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		[JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; }
    }
}
