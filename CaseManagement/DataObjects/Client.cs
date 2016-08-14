namespace CaseManagement.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            ClientDocuments = new HashSet<ClientDocument>();
            ClientFamilies = new HashSet<ClientFamily>();
            ClientNeeds = new HashSet<ClientNeed>();
            ClientVisits = new HashSet<ClientVisit>();
        }

        public int clientId { get; set; }

        public int waysideId { get; set; }

        [Required]
        [StringLength(255)]
        public string firstName { get; set; }

        [StringLength(50)]
        public string nickName { get; set; }

        [Required]
        [StringLength(255)]
        public string lastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dob { get; set; }

        public string address { get; set; }

        [Column(TypeName = "date")]
        public DateTime intakeDate { get; set; }

        public int genderId { get; set; }

        public int languageId { get; set; }

        public bool veteran { get; set; }

        public int ethnicityId { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateEdited { get; set; }

        public int learnAboutId { get; set; }

        public int raceId { get; set; }

        public int housingStatusId { get; set; }

        public bool houseLossRisk { get; set; }

        public int translationId { get; set; }

        public bool canContact { get; set; }

        public int? houseHoldCount { get; set; }

        public virtual Ethnicity Ethnicity { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual HousingStatu HousingStatu { get; set; }

        public virtual Language Language { get; set; }

        public virtual LearnAbout LearnAbout { get; set; }

        public virtual Race Race { get; set; }

        public virtual Translation Translation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientDocument> ClientDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientFamily> ClientFamilies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientNeed> ClientNeeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientVisit> ClientVisits { get; set; }
    }
}
