namespace CaseManagement.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileType")]
    public partial class FileType
    {
        public int fileTypeId { get; set; }

        [Column("fileType")]
        [Required]
        [StringLength(50)]
        public string fileType1 { get; set; }
    }
}
