using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LocativeApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<RentPayment> RentPayments { get; set; }
        public DbSet<LeaseContract> LeaseContracts { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<PropertyFeature> PropertyFeatures { get; set; }
        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Property>()
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Tenant>()
                .HasOne(t => t.Owner)
                .WithMany()
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Candidate>()
                .HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RentPayment>()
                .HasOne(r => r.Owner)
                .WithMany()
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<LeaseContract>()
                .HasOne(l => l.Property)
                .WithMany(p => p.LeaseContracts)
                .HasForeignKey(l => l.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LeaseContract>()
                .HasOne(l => l.Tenant)
                .WithMany(t => t.LeaseContracts)
                .HasForeignKey(l => l.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PropertyFeature>()
                .HasKey(pf => new { pf.PropertyId, pf.FeatureId });

            builder.Entity<PropertyFeature>()
                .HasOne(pf => pf.Property)
                .WithMany(p => p.PropertyFeatures)
                .HasForeignKey(pf => pf.PropertyId);

            builder.Entity<PropertyFeature>()
                .HasOne(pf => pf.Feature)
                .WithMany()
                .HasForeignKey(pf => pf.FeatureId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Insertion données de base initiales types de biens
            builder.Entity<PropertyType>().HasData(
                new PropertyType { Id = 1, Name = "Studio" },
                new PropertyType { Id = 2, Name = "T1" },
                new PropertyType { Id = 3, Name = "T2" },
                new PropertyType { Id = 4, Name = "T3" },
                new PropertyType { Id = 5, Name = "Garage" },
                new PropertyType { Id = 6, Name = "Cave" }
            );
            // Insertion données de base initiales équipements
            builder.Entity<Feature>().HasData(
                new Feature { Id = 1, Name = "Balcon" },
                new Feature { Id = 2, Name = "Piscine" },
                new Feature { Id = 3, Name = "Parking" },
                new Feature { Id = 4, Name = "Box fermé" },
                new Feature { Id = 5, Name = "Ascenseur" }
            );
        }
    }
}
