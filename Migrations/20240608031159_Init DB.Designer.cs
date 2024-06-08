﻿// <auto-generated />
using System;
using ALPHII.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ALPHII.Migrations
{
    [DbContext(typeof(ALPHIIBackendDbContext))]
    [Migration("20240608031159_Init DB")]
    partial class InitDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ALPHII.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulty");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Plan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CostPerMonth")
                        .HasColumnType("int");

                    b.Property<int>("CostPerYear")
                        .HasColumnType("int");

                    b.Property<string>("NamePlan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fa07fbcb-0c19-4d93-997e-bf00bbb71a02"),
                            Code = "XYZ",
                            Name = "ABC",
                            RegionImageUrl = "gpx.jpg"
                        },
                        new
                        {
                            Id = new Guid("abeecaf5-cdcf-4e7b-8547-90e2dda1fb36"),
                            Code = "XYZ1",
                            Name = "ABC1",
                            RegionImageUrl = "gpx1.jpg"
                        },
                        new
                        {
                            Id = new Guid("adcbf644-0a80-4677-afd5-4d4e3437fbfd"),
                            Code = "XYZ2",
                            Name = "ABC2",
                            RegionImageUrl = "gpx2.jpg"
                        });
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ToolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ToolId");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Tool", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BasePlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ToolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BasePlanId");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Credit")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.VMTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ImageInputId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ImageMaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NegativePrompt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PositivePrompt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TextDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.ToTable("VMTask");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Task", b =>
                {
                    b.HasOne("ALPHII.Models.Domain.Tool", "Tool")
                        .WithMany("Tasks")
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ALPHII.Models.Domain.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tool");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Tool", b =>
                {
                    b.HasOne("ALPHII.Models.Domain.Plan", "BasePlan")
                        .WithMany()
                        .HasForeignKey("BasePlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BasePlan");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.User", b =>
                {
                    b.HasOne("ALPHII.Models.Domain.Plan", "Plan")
                        .WithMany("Users")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.VMTask", b =>
                {
                    b.HasOne("ALPHII.Models.Domain.Task", "Task")
                        .WithOne("VMTask")
                        .HasForeignKey("ALPHII.Models.Domain.VMTask", "TaskId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Walk", b =>
                {
                    b.HasOne("ALPHII.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ALPHII.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Plan", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Task", b =>
                {
                    b.Navigation("VMTask")
                        .IsRequired();
                });

            modelBuilder.Entity("ALPHII.Models.Domain.Tool", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ALPHII.Models.Domain.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
