using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FuelStation.Models.Etities
{
    public partial class EfCoreDbContext : DbContext
    {
        public static EfCoreDbContext Instance { get; private set; }

        static EfCoreDbContext() => Instance = new EfCoreDbContext();

        private EfCoreDbContext()
        {
        }

        private EfCoreDbContext(DbContextOptions<EfCoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Fuel> Fuels { get; set; } = null!;
        public virtual DbSet<FuelSale> FuelSales { get; set; } = null!;
        public virtual DbSet<FuelSupply> FuelSupplies { get; set; } = null!;
        public virtual DbSet<Measure> Measures { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Vendor> Vendors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=7684;database=fuel_station", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"))
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.Idrole, "employee_role_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(45)
                    .HasColumnName("firstname");

                entity.Property(e => e.Idrole).HasColumnName("idrole");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(45)
                    .HasColumnName("lastname");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(45)
                    .HasColumnName("patronymic");

                entity.HasOne(d => d.IdroleNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Idrole)
                    .HasConstraintName("employee_role");
            });

            modelBuilder.Entity<Fuel>(entity =>
            {
                entity.ToTable("fuel");

                entity.HasIndex(e => e.Idmeasure, "fuel_measure_idx");

                entity.HasIndex(e => e.Idvendor, "fuel_vendor_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CostPerUnit)
                    .HasPrecision(5, 2)
                    .HasColumnName("cost_per_unit");

                entity.Property(e => e.Idmeasure).HasColumnName("idmeasure");

                entity.Property(e => e.Idvendor).HasColumnName("idvendor");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.IdmeasureNavigation)
                    .WithMany(p => p.Fuels)
                    .HasForeignKey(d => d.Idmeasure)
                    .HasConstraintName("fuel_measure");

                entity.HasOne(d => d.IdvendorNavigation)
                    .WithMany(p => p.Fuels)
                    .HasForeignKey(d => d.Idvendor)
                    .HasConstraintName("fuel_vendor");
            });

            modelBuilder.Entity<FuelSale>(entity =>
            {
                entity.ToTable("fuel_sales");

                entity.HasIndex(e => e.Idemployee, "fuel_sales_employee_idx");

                entity.HasIndex(e => e.Idfuel, "fuel_sales_fuel_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime");

                entity.Property(e => e.Idemployee).HasColumnName("idemployee");

                entity.Property(e => e.Idfuel).HasColumnName("idfuel");

                entity.Property(e => e.Quantity)
                    .HasPrecision(5, 2)
                    .HasColumnName("quantity");

                entity.Property(e => e.Totalcost)
                    .HasPrecision(5, 2)
                    .HasColumnName("totalcost");

                entity.HasOne(d => d.IdemployeeNavigation)
                    .WithMany(p => p.FuelSales)
                    .HasForeignKey(d => d.Idemployee)
                    .HasConstraintName("fuel_sales_employee");

                entity.HasOne(d => d.IdfuelNavigation)
                    .WithMany(p => p.FuelSales)
                    .HasForeignKey(d => d.Idfuel)
                    .HasConstraintName("fuel_sales_fuel");
            });

            modelBuilder.Entity<FuelSupply>(entity =>
            {
                entity.ToTable("fuel_supplies");

                entity.HasIndex(e => e.IdBringingEmployee, "fuel_supplies_bringing_employee_idx");

                entity.HasIndex(e => e.Idfuel, "fuel_supplies_fuel_idx");

                entity.HasIndex(e => e.IdReceivingEmployee, "fuel_supplies_receiving_employee_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime");

                entity.Property(e => e.IdBringingEmployee).HasColumnName("id_bringing_employee");

                entity.Property(e => e.IdReceivingEmployee).HasColumnName("id_receiving_employee");

                entity.Property(e => e.Idfuel).HasColumnName("idfuel");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdBringingEmployeeNavigation)
                    .WithMany(p => p.FuelSupplyIdBringingEmployeeNavigations)
                    .HasForeignKey(d => d.IdBringingEmployee)
                    .HasConstraintName("fuel_supplies_bringing_employee");

                entity.HasOne(d => d.IdReceivingEmployeeNavigation)
                    .WithMany(p => p.FuelSupplyIdReceivingEmployeeNavigations)
                    .HasForeignKey(d => d.IdReceivingEmployee)
                    .HasConstraintName("fuel_supplies_receiving_employee");

                entity.HasOne(d => d.IdfuelNavigation)
                    .WithMany(p => p.FuelSupplies)
                    .HasForeignKey(d => d.Idfuel)
                    .HasConstraintName("fuel_supplies_fuel");
            });

            modelBuilder.Entity<Measure>(entity =>
            {
                entity.ToTable("measure");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Measure1)
                    .HasMaxLength(45)
                    .HasColumnName("measure");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendor");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
