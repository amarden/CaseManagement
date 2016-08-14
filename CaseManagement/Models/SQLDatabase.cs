namespace CaseManagement.Models.SQL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Configurations;
    using DataObjects;
    using System.Threading.Tasks;

    public partial class SQLDatabase : IdentityDbContext<AppUser>
    {
        public SQLDatabase()
            : base("SQLDatabase", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientFamily> ClientFamilies { get; set; }
        public virtual DbSet<ClientNeed> ClientNeeds { get; set; }
        public virtual DbSet<ClientDocument> ClientDocuments { get; set; }
        public virtual DbSet<ClientVisit> ClientVisits { get; set; }
        public virtual DbSet<ClientVisitReferral> ClientVisitReferrals { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Ethnicity> Ethnicities { get; set; }
        public virtual DbSet<FileType> FileTypes{ get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<HousingStatu> HousingStatus { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LearnAbout> LearnAbouts { get; set; }
        public virtual DbSet<Need> Needs { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<ReferralType> ReferralTypes { get; set; }
        public virtual DbSet<Responder> Responders { get; set; }
        public virtual DbSet<Translation> Translations { get; set; }
        public virtual DbSet<VisitResult> VisitResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>()
                .HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Configurations.Add(new AppUserConfiguration());

            modelBuilder.Entity<Agency>()
                .HasMany(e => e.ClientVisitReferrals)
                .WithRequired(e => e.Agency)
                .HasForeignKey(e => e.agencyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
               .HasMany(e => e.ClientDocuments)
               .WithRequired(e => e.Client)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientFamily>()
                .Property(e => e.age)
                .IsFixedLength();

            modelBuilder.Entity<ClientVisit>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<ContactType>()
                .HasMany(e => e.ClientVisits)
                .WithRequired(e => e.ContactType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ethnicity>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Ethnicity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Gender>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Gender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HousingStatu>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.HousingStatu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LearnAbout>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.LearnAbout)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Need>()
                .HasMany(e => e.ClientNeeds)
                .WithRequired(e => e.Need)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Race>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Race)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReferralType>()
                .HasMany(e => e.ClientVisitReferrals)
                .WithRequired(e => e.ReferralType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Responder>()
                .HasMany(e => e.ClientVisits)
                .WithRequired(e => e.Responder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Translation>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Translation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VisitResult>()
                .HasMany(e => e.ClientVisits)
                .WithRequired(e => e.VisitResult)
                .WillCascadeOnDelete(false);
        }

        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public static SQLDatabase Create()
        {
            return new SQLDatabase();
        }
    }
}
