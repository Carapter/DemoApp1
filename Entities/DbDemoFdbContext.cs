using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Entities;

public partial class DbDemoFdbContext : DbContext
{
    public DbDemoFdbContext()
    {
    }

    public DbDemoFdbContext(DbContextOptions<DbDemoFdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerType> PartnerTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductMaterialType> ProductMaterialTypes { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseFirebird("DataSource=localhost;Port=3050;Database=/db/demo.fdb;Username=sysdba;Password=7827940");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.MaterialTypeId).HasName("RDB$PRIMARY2");

            entity.ToTable("MATERIAL_TYPE");

            entity.HasIndex(e => e.MaterialTypeId, "RDB$PRIMARY2").IsUnique();

            entity.Property(e => e.MaterialTypeId).HasColumnName("MATERIAL_TYPE_ID");
            entity.Property(e => e.MaterialTypeName)
                .HasMaxLength(100)
                .HasColumnName("MATERIAL_TYPE_NAME");
            entity.Property(e => e.PercentageDefectiveMaterial)
                .HasColumnType("DECIMAL(5,4)")
                .HasColumnName("PERCENTAGE_DEFECTIVE_MATERIAL");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.PartnerId).HasName("RDB$PRIMARY6");

            entity.ToTable("PARTNER");

            entity.HasIndex(e => e.PartnerType, "RDB$FOREIGN7");

            entity.HasIndex(e => e.PartnerId, "RDB$PRIMARY6").IsUnique();

            entity.Property(e => e.PartnerId).HasColumnName("PARTNER_ID");
            entity.Property(e => e.Director)
                .HasMaxLength(100)
                .HasColumnName("DIRECTOR");
            entity.Property(e => e.DirectorMail)
                .HasMaxLength(100)
                .HasColumnName("DIRECTOR_MAIL");
            entity.Property(e => e.DirectorPhone)
                .HasMaxLength(25)
                .HasColumnName("DIRECTOR_PHONE");
            entity.Property(e => e.PartnerInn)
                .HasMaxLength(100)
                .HasColumnName("PARTNER_INN");
            entity.Property(e => e.PartnerLegalAddress)
                .HasMaxLength(100)
                .HasColumnName("PARTNER_LEGAL_ADDRESS");
            entity.Property(e => e.PartnerName)
                .HasMaxLength(100)
                .HasColumnName("PARTNER_NAME");
            entity.Property(e => e.PartnerRating).HasColumnName("PARTNER_RATING");
            entity.Property(e => e.PartnerType).HasColumnName("PARTNER_TYPE");

            entity.HasOne(d => d.PartnerTypeNavigation).WithMany(p => p.Partners)
                .HasForeignKey(d => d.PartnerType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("INTEG_8");
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.HasKey(e => e.PartnerTypeId).HasName("RDB$PRIMARY5");

            entity.ToTable("PARTNER_TYPE");

            entity.HasIndex(e => e.PartnerTypeId, "RDB$PRIMARY5").IsUnique();

            entity.Property(e => e.PartnerTypeId).HasColumnName("PARTNER_TYPE_ID");
            entity.Property(e => e.PartnerTypeName)
                .HasMaxLength(100)
                .HasColumnName("PARTNER_TYPE_NAME");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductArticle).HasName("RDB$PRIMARY3");

            entity.ToTable("PRODUCT");

            entity.HasIndex(e => e.ProductType, "RDB$FOREIGN4");

            entity.HasIndex(e => e.ProductArticle, "RDB$PRIMARY3").IsUnique();

            entity.Property(e => e.ProductArticle)
                .HasMaxLength(7)
                .HasColumnName("PRODUCT_ARTICLE");
            entity.Property(e => e.MinimumCostForPartner)
                .HasColumnType("DECIMAL(15,2)")
                .HasColumnName("MINIMUM_COST_FOR_PARTNER");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("PRODUCT_NAME");
            entity.Property(e => e.ProductType).HasColumnName("PRODUCT_TYPE");

            entity.HasOne(d => d.ProductTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("INTEG_5");
        });

        modelBuilder.Entity<ProductMaterialType>(entity =>
        {
            entity.HasKey(e => e.ProductMaterialTypeId).HasName("RDB$PRIMARY11");

            entity.ToTable("PRODUCT_MATERIAL_TYPE");

            entity.HasIndex(e => e.Product, "RDB$FOREIGN12");

            entity.HasIndex(e => e.MaterialType, "RDB$FOREIGN13");

            entity.HasIndex(e => e.ProductMaterialTypeId, "RDB$PRIMARY11").IsUnique();

            entity.Property(e => e.ProductMaterialTypeId).HasColumnName("PRODUCT_MATERIAL_TYPE_ID");
            entity.Property(e => e.MaterialType).HasColumnName("MATERIAL_TYPE");
            entity.Property(e => e.Product)
                .HasMaxLength(7)
                .HasColumnName("PRODUCT");

            entity.HasOne(d => d.MaterialTypeNavigation).WithMany(p => p.ProductMaterialTypes)
                .HasForeignKey(d => d.MaterialType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("INTEG_14");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.ProductMaterialTypes)
                .HasForeignKey(d => d.Product)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("INTEG_13");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("RDB$PRIMARY1");

            entity.ToTable("PRODUCT_TYPE");

            entity.HasIndex(e => e.ProductTypeId, "RDB$PRIMARY1").IsUnique();

            entity.Property(e => e.ProductTypeId).HasColumnName("PRODUCT_TYPE_ID");
            entity.Property(e => e.ProductTypeCoefficient)
                .HasColumnType("DECIMAL(10,2)")
                .HasColumnName("PRODUCT_TYPE_COEFFICIENT");
            entity.Property(e => e.ProductTypeName)
                .HasMaxLength(100)
                .HasColumnName("PRODUCT_TYPE_NAME");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("RDB$PRIMARY8");

            entity.ToTable("SALE");

            entity.HasIndex(e => e.ProductArticle, "RDB$FOREIGN10");

            entity.HasIndex(e => e.Partner, "RDB$FOREIGN9");

            entity.HasIndex(e => e.SaleId, "RDB$PRIMARY8").IsUnique();

            entity.Property(e => e.SaleId).HasColumnName("SALE_ID");
            entity.Property(e => e.Partner).HasColumnName("PARTNER");
            entity.Property(e => e.ProductArticle)
                .HasMaxLength(7)
                .HasColumnName("PRODUCT_ARTICLE");
            entity.Property(e => e.ProductCount).HasColumnName("PRODUCT_COUNT");
            entity.Property(e => e.SaleDate)
                .HasColumnType("DATE")
                .HasColumnName("SALE_DATE");

            entity.HasOne(d => d.PartnerNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.Partner)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("INTEG_10");

            entity.HasOne(d => d.ProductArticleNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductArticle)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("INTEG_11");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
