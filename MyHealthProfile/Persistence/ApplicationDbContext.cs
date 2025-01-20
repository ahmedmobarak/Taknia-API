using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyHealthProfile.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MyHealthProfile.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Patient>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<UserAllergy> UserAllergies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.DateOfBirth)
                      .HasColumnType("datetime");

                entity.Property(e => e.EmailOtpExpiration)
                      .HasColumnType("datetime");

                entity.Property(e => e.PasswordOtpExpiration)
                      .HasColumnType("datetime");
            });
            // Configure relationships
            builder.Entity<UserAllergy>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAllergies)
                .HasForeignKey(ua => ua.UserId);

            builder.Entity<UserAllergy>()
                .HasOne(ua => ua.Allergy)
                .WithMany(a => a.UserAllergies)
                .HasForeignKey(ua => ua.AllergyId);

            builder.Entity<Allergy>().HasData(
       new Allergy { Id = 1, NameAr = "الحليب", NameEn = "Milk" },
       new Allergy { Id = 2, NameAr = "البيض", NameEn = "Egg" },
       new Allergy { Id = 3, NameAr = "السمك", NameEn = "Fish" },
       new Allergy { Id = 4, NameAr = "الفول السوداني", NameEn = "Peanuts" },
       new Allergy { Id = 5, NameAr = "القمح", NameEn = "Wheat" }
   );
        }
    }
}
