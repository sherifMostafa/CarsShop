using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.Domains;

namespace Vega.Persistence
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options)
        {

        }
        public DbSet<Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Model Fluint Api
            modelBuilder.Entity<Model>().ToTable("Models", "dbo");
            modelBuilder.Entity<Model>().Property(p => p.Name).HasMaxLength(255).IsRequired();
            //modelBuilder.Entity<Model>()
            // .HasOne(r => r.Make).WithMany().HasForeignKey(f => f.MakeId);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Model>().HasData(new Model[] {
                new Model{Id = 1, Name="Make1-ModelA" , MakeId=1},
                new Model{Id = 2, Name="Make1-ModelB" , MakeId=1},
                new Model{Id = 3, Name="Make1-ModelC" , MakeId=1},
                
                new Model{Id = 4, Name="Make2-ModelA" , MakeId=2},
                new Model{Id = 5, Name="Make2-ModelB" , MakeId=2},
                new Model{Id = 6, Name="Make2-ModelC" , MakeId=2}, 

                new Model{Id = 7, Name="Make3-ModelA" , MakeId=3},
                new Model{Id = 8, Name="Make3-ModelB" , MakeId=3},
                new Model{Id = 9, Name="Make3-ModelC" , MakeId=3},
            });

            //Make Fluint Api
            modelBuilder.Entity<Make>().ToTable("Makes", "dbo");
            modelBuilder.Entity<Make>().HasMany(p => p.Models).WithOne(p => p.Make).HasForeignKey(f => f.MakeId);
            modelBuilder.Entity<Make>().Property(p => p.Name).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Make>().HasData(new Make[] {
               new Make{Id=1, Name = "Make1"},
               new Make{Id=2, Name = "Make2"},
               new Make{Id=3, Name = "Make3"},
            });



            //Feature Fluint Api
            modelBuilder.Entity<Feature>().ToTable("Features" , "dbo");
            modelBuilder.Entity<Feature>().Property(p => p.Name).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Feature>().HasMany(p => p.Vehicles).WithOne(p=> p.Feature).HasForeignKey(f => f.FeatureId);

            //Vehicle 
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles", "dbo");
            modelBuilder.Entity<Vehicle>().Property(p => p.Name).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Vehicle>().HasOne(r => r.Model).WithMany().HasForeignKey(f => f.ModelId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Vehicle>().Property(p => p.LastUpdate).HasColumnType("date");
            modelBuilder.Entity<Vehicle>().Property(p => p.ContactName).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Vehicle>().Property(p => p.ContactPhone).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Vehicle>().Property(p => p.ContactEmail).HasMaxLength(255);
            modelBuilder.Entity<Vehicle>().HasMany(P => P.Features).WithOne(p=> p.Vehicle).HasForeignKey(f => f.VehicleId);
            modelBuilder.Entity<Vehicle>().HasMany(P => P.Photos).WithOne(p => p.Vehicle).HasForeignKey(f => f.VehicleId);

            //VehiclesFeatures
            modelBuilder.Entity<VehicleFeature>().ToTable("VehiclesFeatures", "dbo");
            modelBuilder.Entity<VehicleFeature>().HasKey(p => new { p.FeatureId , p.VehicleId});

            //Photo
            modelBuilder.Entity<Photo>().ToTable("Photos", "dbo");
            modelBuilder.Entity<Photo>().Property(p => p.FileName).HasMaxLength(255).IsRequired();

        }
    }
}
