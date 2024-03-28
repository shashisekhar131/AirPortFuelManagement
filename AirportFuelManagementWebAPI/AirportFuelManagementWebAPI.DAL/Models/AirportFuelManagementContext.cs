using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirportFuelManagementWebAPI.DAL.Models;

public partial class AirportFuelManagementContext : DbContext
{
    public AirportFuelManagementContext()
    {
    }

    public AirportFuelManagementContext(DbContextOptions<AirportFuelManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircraft { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<FuelTransaction> FuelTransactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.AircraftId).HasName("PK__Aircraft__F75CBC0BA7A126F0");

            entity.Property(e => e.AircraftId).HasColumnName("AircraftID");
            entity.Property(e => e.AirLine).HasMaxLength(100);
            entity.Property(e => e.AircraftNumber).HasMaxLength(50);
            entity.Property(e => e.DestinationId).HasColumnName("DestinationID");
            entity.Property(e => e.SourceId).HasColumnName("SourceID");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__Airport__E3DBE08AC6ACB37E");

            entity.ToTable("Airport");

            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.AirportName).HasMaxLength(100);
            entity.Property(e => e.FuelAvailable).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FuelCapacity).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<FuelTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__FuelTran__55433A4BA3E8D9D2");

            entity.ToTable("FuelTransaction");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.AircraftId).HasColumnName("AircraftID");
            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TransactionIdparent).HasColumnName("TransactionIDParent");
            entity.Property(e => e.TransactionTime).HasColumnType("datetime");

            entity.HasOne(d => d.Aircraft).WithMany(p => p.FuelTransactions)
                .HasForeignKey(d => d.AircraftId)
                .HasConstraintName("FK__FuelTrans__Aircr__3C69FB99");

            entity.HasOne(d => d.Airport).WithMany(p => p.FuelTransactions)
                .HasForeignKey(d => d.AirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FuelTrans__Airpo__3B75D760");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACE7D81BBD");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
