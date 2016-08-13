namespace CaseManagementTest.Models.SQL
{
	using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientNeed")]
    public partial class ClientNeed
    {
        public int clientNeedId { get; set; }

        public int clientId { get; set; }

        public int needId { get; set; }

        public bool met { get; set; }

        public DateTime? dateCreated { get; set; }

		[JsonIgnore]
        public virtual Client Client { get; set; }

        public virtual Need Need { get; set; }
    }
}
