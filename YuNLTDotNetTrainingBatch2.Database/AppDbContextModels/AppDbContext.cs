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

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblSale> TblSales { get; set; }

    public virtual DbSet<TblSaleDetail> TblSaleDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-NFU692OK\\LOCALDB;Initial Catalog=POS;User ID=sa;Password=sasa@123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Tbl_Prod__B40CC6ED05262C43");

            entity.ToTable("Tbl_Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Createat)
                .HasColumnType("datetime")
                .HasColumnName("CREATEAT");
            entity.Property(e => e.PName)
                .HasMaxLength(50)
                .HasColumnName("P_NAME");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("PRICE");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
