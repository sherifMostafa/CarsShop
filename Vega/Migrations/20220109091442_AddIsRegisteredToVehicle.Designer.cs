﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vega.Persistence;

namespace Vega.Migrations
{
    [DbContext(typeof(VegaDbContext))]
    [Migration("20220109091442_AddIsRegisteredToVehicle")]
    partial class AddIsRegisteredToVehicle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vega.Domains.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Features", "dbo");
                });

            modelBuilder.Entity("Vega.Domains.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Makes", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Make1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Make2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Make3"
                        });
                });

            modelBuilder.Entity("Vega.Domains.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MakeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.ToTable("Models", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MakeId = 1,
                            Name = "Make1-ModelA"
                        },
                        new
                        {
                            Id = 2,
                            MakeId = 1,
                            Name = "Make1-ModelB"
                        },
                        new
                        {
                            Id = 3,
                            MakeId = 1,
                            Name = "Make1-ModelC"
                        },
                        new
                        {
                            Id = 4,
                            MakeId = 2,
                            Name = "Make2-ModelA"
                        },
                        new
                        {
                            Id = 5,
                            MakeId = 2,
                            Name = "Make2-ModelB"
                        },
                        new
                        {
                            Id = 6,
                            MakeId = 2,
                            Name = "Make2-ModelC"
                        },
                        new
                        {
                            Id = 7,
                            MakeId = 3,
                            Name = "Make3-ModelA"
                        },
                        new
                        {
                            Id = 8,
                            MakeId = 3,
                            Name = "Make3-ModelB"
                        },
                        new
                        {
                            Id = 9,
                            MakeId = 3,
                            Name = "Make3-ModelC"
                        });
                });

            modelBuilder.Entity("Vega.Domains.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactEmail")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsRegistered")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("date");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Vehicles", "dbo");
                });

            modelBuilder.Entity("Vega.Domains.VehicleFeature", b =>
                {
                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("FeatureId", "VehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehiclesFeatures", "dbo");
                });

            modelBuilder.Entity("Vega.Domains.Model", b =>
                {
                    b.HasOne("Vega.Domains.Make", null)
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vega.Domains.Vehicle", b =>
                {
                    b.HasOne("Vega.Domains.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Vega.Domains.VehicleFeature", b =>
                {
                    b.HasOne("Vega.Domains.Feature", null)
                        .WithMany("VehicleFeatures")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vega.Domains.Vehicle", null)
                        .WithMany("VehicleFeatures")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vega.Domains.Feature", b =>
                {
                    b.Navigation("VehicleFeatures");
                });

            modelBuilder.Entity("Vega.Domains.Make", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("Vega.Domains.Vehicle", b =>
                {
                    b.Navigation("VehicleFeatures");
                });
#pragma warning restore 612, 618
        }
    }
}
