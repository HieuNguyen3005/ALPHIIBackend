using ALPHII.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ALPHII.Data
{
    public class ALPHIIAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public ALPHIIAuthDbContext(DbContextOptions<ALPHIIAuthDbContext> options) : base(options) {
            /*Docker*/
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Image> Images { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // 1 - n: Plan - User
            builder.Entity<ApplicationUser>()
                .HasOne(p => p.Plan)
                .WithMany(c => c.Users)
                .HasForeignKey(p => p.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 - n: User - Project
            builder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);



            // 1 - n: Tool - Project
            builder.Entity<Project>()
                .HasOne(p => p.Tool)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ToolId)
                .OnDelete(DeleteBehavior.Restrict);



            // Configuring one-to-one relationship using Fluent API 
            // 1 - 1: Project - VMProject
            builder.Entity<Project>()
                .HasOne(p => p.VMProject)
                .WithOne(c => c.Project)
                .HasForeignKey<VMProject>(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
            var adminRoleId = "39860ce3-950e-4f55-a439-1a7a3da5bef7";
            var userRoleId = "3f338c25-c15f-47c1-8e6e-3c982a5fa562";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
