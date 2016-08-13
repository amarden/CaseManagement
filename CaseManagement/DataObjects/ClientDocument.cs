namespace CaseManagement.DataObjects
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientDocument")]
    public partial class ClientDocument
    {
        public int clientDocumentId { get; set; }

        public int clientId { get; set; }

        [Required]
        public string fileName { get; set; }

        [Required]
        public string filePath { get; set; }

        public int fileTypeId { get; set; }

        public DateTime uploadDate { get; set; }

        [JsonIgnore]
        public virtual Client Client { get; set; }
    }
}
