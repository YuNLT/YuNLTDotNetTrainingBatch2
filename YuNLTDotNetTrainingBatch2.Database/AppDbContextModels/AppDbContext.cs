using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace YuNLTDotNetTrainingBatch2.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblPosition> TblPositions { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblSale> TblSales { get; set; }

    public virtual DbSet<TblSaleDetail> TblSaleDetails { get; set; }

    public virtual DbSet<TblStaffRegistration> TblStaffRegistrations { get; set; }

    public virtual DbSet<TblStaffUser> TblStaffUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-NFU692OK\\LOCALDB;Initial Catalog=POS;User ID=sa;Password=sasa@123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblPosition>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_Position");

            entity.HasIndex(e => e.PositionCode, "UQ_PositionCode").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeleteFlag).HasDefaultValue(false);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.PositionCode).HasMaxLength(50);
            entity.Property(e => e.PositionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PositionID");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Tbl_Prod__B40CC6ED05262C43");

            entity.ToTable("Tbl_Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Createat)
                .HasColumnType("datetime")
                .HasColumnName("CREATEAT");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblSale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Tbl_Sale__1EE3C41FD47ABBFD");

            entity.ToTable("Tbl_Sale");

            entity.Property(e => e.SaleId).HasColumnName("SaleID");
            entity.Property(e => e.SaleDate)
                .HasColumnType("datetime")
                .HasColumnName("Sale_Date");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("Total_Amount");
            entity.Property(e => e.VoucherNo)
                .HasMaxLength(50)
                .HasColumnName("Voucher_No");
        });

        modelBuilder.Entity<TblSaleDetail>(entity =>
        {
            entity.HasKey(e => e.SaleDetailId).HasName("PK__Tbl_Sale__70DB141EDDEAE031");

            entity.ToTable("Tbl_SaleDetail");

            entity.Property(e => e.SaleDetailId).HasColumnName("SaleDetailID");
            entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SaleId).HasColumnName("SaleID");
        });

        modelBuilder.Entity<TblStaffRegistration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Tbl_StaffRegistration");

            entity.HasIndex(e => e.StaffCode, "UQ_StaffCode").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserCode).HasMaxLength(50);
            entity.Property(e => e.DeleteFlag).HasDefaultValue(false);
            entity.Property(e => e.Education).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FatherName).HasMaxLength(50);
            entity.Property(e => e.MaritalStatus).HasMaxLength(50);
            entity.Property(e => e.ModeifiedUserCode).HasMaxLength(50);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.MotherName).HasMaxLength(50);
            entity.Property(e => e.Nrc)
                .HasMaxLength(50)
                .HasColumnName("NRC");
            entity.Property(e => e.PhoneNo).HasMaxLength(50);
            entity.Property(e => e.PositionCode).HasMaxLength(50);
            entity.Property(e => e.SpouseName).HasMaxLength(50);
            entity.Property(e => e.StaffCode).HasMaxLength(50);
            entity.Property(e => e.StaffId)
                .ValueGeneratedOnAdd()
                .HasColumnName("StaffID");
            entity.Property(e => e.StaffName).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<TblStaffUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TblStaffUser");

            entity.Property(e => e.CreattedAt).HasColumnType("datetime");
            entity.Property(e => e.DeleteFlag).HasDefaultValue(false);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNo).HasMaxLength(50);
            entity.Property(e => e.PositionCode).HasMaxLength(50);
            entity.Property(e => e.StaffCode).HasMaxLength(50);
            entity.Property(e => e.StaffUserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("StaffUserID");
        });
        modelBuilder.HasSequence("PositionCodeSeq");
        modelBuilder.HasSequence("StaffCodeSeq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
