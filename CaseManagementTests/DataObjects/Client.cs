namespace CaseManagementTest.Models.SQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    [Serializable]
    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            ClientFamilies = new HashSet<ClientFamily>();
            ClientNeeds = new HashSet<ClientNeed>();
            ClientProviders = new HashSet<ClientProvider>();
            ClientVisits = new HashSet<ClientVisit>();
        }

        public int clientId { get; set; }

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

        public bool english { get; set; }

        public bool veteran { get; set; }

        public int ethnicityId { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(12)]
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

        [XmlIgnore]
        public virtual Ethnicity Ethnicity { get; set; }

        public virtual Gender Gender { get; set; }

        [XmlIgnore]
        public virtual HousingStatu HousingStatu { get; set; }

        [XmlIgnore]
        public virtual Language Language { get; set; }

        [XmlIgnore]
        public virtual LearnAbout LearnAbout { get; set; }

        [XmlIgnore]
        public virtual Race Race { get; set; }

        [XmlIgnore]
        public virtual Translation Translation { get; set; }

        [XmlIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientFamily> ClientFamilies { get; set; }

        [XmlIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientNeed> ClientNeeds { get; set; }

        [XmlIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientProvider> ClientProviders { get; set; }

        [XmlIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientVisit> ClientVisits { get; set; }
    }
}
