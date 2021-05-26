using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BDF.VehicleTracker.PL
{
    public partial class VehicleEntities : DbContext
    {
        public VehicleEntities()
        {
        }

        public VehicleEntities(DbContextOptions<VehicleEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<tblColor> tblColors { get; set; }
        public virtual DbSet<tblMake> tblMakes { get; set; }
        public virtual DbSet<tblModel> tblModels { get; set; }
        public virtual DbSet<tblVehicle> tblVehicles { get; set; }
        public virtual DbSet<tblLeaderboard> tblLeaderboards { get; set; }
        
        public virtual DbSet<tblStravaEvent> tblStravaEvents { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=BDF.VehicleTracker.DB;Integrated Security=true");
                optionsBuilder.UseSqlServer("Data Source =fvtcit.database.windows.net;Initial Catalog=fvtcit;User ID=bdfootedb;Password=SecretPa$$!;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblColor>(entity =>
            {
                entity.ToTable("tblColor");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblMake>(entity =>
            {
                entity.ToTable("tblMake");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblModel>(entity =>
            {
                entity.ToTable("tblModel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblVehicle>(entity =>
            {
                entity.ToTable("tblVehicle");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.VIN)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("VIN");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.tblVehicles)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("tblVehicle_ColorId");

                entity.HasOne(d => d.Make)
                    .WithMany(p => p.tblVehicles)
                    .HasForeignKey(d => d.MakeId)
                    .HasConstraintName("tblVehicle_MakeId");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.tblVehicles)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("tblVehicle_ModelId");
            });


            modelBuilder.Entity<tblLeaderboard>(entity =>
            {
                entity.ToTable("tblLeaderboard");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblStravaEvent>(entity =>
            {
                entity.ToTable("tblStravaEvent");

                entity.Property(e => e.Id).ValueGeneratedNever();

            });


            modelBuilder.Entity<spGetVehiclesResult>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
