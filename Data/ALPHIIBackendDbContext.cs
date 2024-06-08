using ALPHII.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Task = ALPHII.Models.Domain.Task;

namespace ALPHII.Data
{
    public class ALPHIIBackendDbContext : DbContext
    {
        // Using when create a new connection string and then inject the connection through the Program.cs
        public ALPHIIBackendDbContext(DbContextOptions<ALPHIIBackendDbContext> dbContextOptions) : base(dbContextOptions)
        {
            /*Docker*/
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }    
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }


        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }  

        public DbSet<Image> Images { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<User> Users { get; set; }

        



        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            base.OnModelCreating(modelBuider);

            // Seed data for Difficulties
            // Easy, Medium, Hard
            //var difficulties = new List<Difficulty>()
            //{ new Difficulty()
            //{
            //    Id = Guid.Parse("0c02e3cb-3963-4eee-b1cb-052df693a9dd"),
            //    Name = "Easy"
            //},
            //new Difficulty()
            //{
            //    Id = Guid.Parse("e41f3e81-db84-4d23-93d3-22a7a3e24863"),
            //    Name = "Medium"
            //},
            //new Difficulty()
            //{
            //    Id = Guid.Parse("23ddaa4d-4da2-45e6-ac9a-0596fd1f67d8"),
            //    Name = "Hard"
            //}
            //};

            //modelBuider.Entity<Difficulty>().HasData(difficulties);

            //Seed data for Regions
           var regions = new List<Region>()
           {
                new Region()
                {
                    Id = Guid.Parse("fa07fbcb-0c19-4d93-997e-bf00bbb71a02"),
                    Code = "XYZ",
                    Name = "ABC",
                    RegionImageUrl = "gpx.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("abeecaf5-cdcf-4e7b-8547-90e2dda1fb36"),
                    Code = "XYZ1",
                    Name = "ABC1",
                    RegionImageUrl = "gpx1.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("adcbf644-0a80-4677-afd5-4d4e3437fbfd"),
                    Code = "XYZ2",
                    Name = "ABC2",
                    RegionImageUrl = "gpx2.jpg"
                }
           };

            modelBuider.Entity<Region>().HasData(regions);

            // 1 - n: Plan - User
            modelBuider.Entity<User>()
                .HasOne(p => p.Plan)
                .WithMany(c => c.Users)
                .HasForeignKey(p => p.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 - n: User - Task
            modelBuider.Entity<Task>()
                .HasOne(p => p.User)
                .WithMany(c => c.Tasks)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);



            // 1 - n: Tool - Task
            modelBuider.Entity<Task>()
                .HasOne(p => p.Tool)
                .WithMany(c => c.Tasks)
                .HasForeignKey(p => p.ToolId)
                .OnDelete(DeleteBehavior.Restrict);



            // Configuring one-to-one relationship using Fluent API 
            // 1 - 1: Task - VMTask
            modelBuider.Entity<Task>()
                .HasOne(p => p.VMTask)
                .WithOne(c => c.Task)
                .HasForeignKey<VMTask>(c => c.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
