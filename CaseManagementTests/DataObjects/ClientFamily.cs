namespace CaseManagementTest.Models.SQL
{
	using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientFamily")]
    public partial class ClientFamily
    {
        public int clientFamilyId { get; set; }

        public int clientId { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [StringLength(10)]
        public string age { get; set; }

        public bool liveWith { get; set; }

        public DateTime dateCreated { get; set; }

		[JsonIgnore]
        public virtual Client Client { get; set; }
    }
}
