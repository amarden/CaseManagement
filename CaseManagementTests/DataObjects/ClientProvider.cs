namespace CaseManagementTest.Models.SQL
{
	using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientProvider")]
    public class ClientProvider
    {
        public int clientProviderId { get; set; }

        public int clientId { get; set; }

        public int agencyId { get; set; }

        public int needId { get; set; }

        [StringLength(255)]
        public string contactPerson { get; set; }

        [StringLength(20)]
        public string phoneNumber { get; set; }

        public DateTime dateCreated { get; set; }

        public virtual Agency Agency { get; set; }

		[JsonIgnore]
        public virtual Client Client { get; set; }

        public virtual Need Need { get; set; }
    }
}
