using Microsoft.EntityFrameworkCore;
using System;

namespace Podium.Models
{
    public class MortgageContext : DbContext
    {
        public MortgageContext(DbContextOptions<MortgageContext> options)
            : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Lender> Lenders { get; set; }
        public DbSet<MortgageRequirement> MortgageRequirements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
         
            builder.Entity<Applicant>().HasKey(p => p.Id);

            // seed applicant data
            builder.Entity<Applicant>().HasData(
            new Applicant
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1992, 09, 15)
            });

            // seed lender data 
            builder.Entity<Lender>().HasData(
            new Lender
            {
                Id = 1,
                Name = "Bank A",
                LenderType = LenderType.Variable,
                LoanToValue = 60
            },
            new Lender
            {
                Id = 2,
                Name = "Bank B",
                LenderType = LenderType.Fixed,
                LoanToValue = 60
            },
            new Lender
            {
                Id = 3,
                Name = "Bank C",
                LenderType = LenderType.Variable,
                LoanToValue = 90
            }
            );

            builder.Entity<MortgageRequirement>()
               .HasOne(p => p.Applicant)
               .WithMany(p => p.MortgageRequirements)
               .HasForeignKey(p => p.ApplicantId);
        }
    }
}
