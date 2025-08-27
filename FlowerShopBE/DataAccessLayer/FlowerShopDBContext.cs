using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial class FlowerShopDBContext : DbContext
    {
        public FlowerShopDBContext()
        {
        }
        public FlowerShopDBContext(DbContextOptions<FlowerShopDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Bouquet> Bouquets { get; set; }
        public virtual DbSet<BouquetType> BouquetTypes { get; set; }
        public virtual DbSet<SubscriptionPackage> SubscriptionPackages { get; set; }
        public virtual DbSet<SubscriptionBouquetType> SubscriptionBouquetTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bouquet>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Bouquet__C1F887FF3D8E2E2E");
                entity.ToTable("Bouquet");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.BouquetTypeId).HasColumnName("BouquetTypeID");
                entity.Property(e => e.Description).IsUnicode(false);
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");
                entity.HasOne(d => d.BouquetType).WithMany(p => p.Bouquets)
                    .HasForeignKey(d => d.BouquetTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bouquet__Bouque__3A81B327");
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.isAvailable).HasColumnType("bit").HasDefaultValueSql("((1))");
            });
            modelBuilder.Entity<BouquetType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__BouquetT__5E5A8E27D3C2F1E3");
                entity.ToTable("BouquetType");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Description).IsUnicode(false);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.isActive).HasColumnType("bit");
            });
            modelBuilder.Entity<SubscriptionPackage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Subscript__3214EC07D8E2B8C3");
                entity.ToTable("SubscriptionPackage");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Description).IsUnicode(false);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.TotalOrderAmount).HasDefaultValueSql("((1))");
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.isActive).HasColumnType("bit");
            });
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Subscript__3214EC07E6F3A5C3");
                entity.ToTable("Subscription");
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.SubscriptionPackageId).HasColumnName("SubscriptionPackageID");
                entity.Property(e => e.StartDate).HasColumnType("datetime");
                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.isActive).HasColumnType("bit");
                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
                entity.HasOne(d => d.SubscriptionPackage).WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.SubscriptionPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Subscript__Subsc__440B1D61");
                entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Subscript__UserI__44FF419A");
                entity.HasOne(d => d.Payment).WithOne(p => p.Subscription)
                    .HasForeignKey<Subscription>(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Subscript__Payme__45F365D3");
            });
            modelBuilder.Entity<SubscriptionBouquetType>(entity =>
            {
                entity.Property(e => e.SubscriptionPackageId).HasColumnName("ID");
                entity.Property(e => e.BouquetTypeId).HasColumnName("BouquetTypeID");
                entity.HasKey(e => new { e.SubscriptionPackageId, e.BouquetTypeId });
                entity.ToTable("SubscriptionBouquetType");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__User__3214EC07E3CDAF8B");
                entity.ToTable("User");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Address).IsUnicode(false);
                entity.Property(e => e.RoleId).HasColumnName("RoleID");
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.isActive).HasColumnType("bit");
                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__RoleID__398D8EEE");
            });
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07D1E2D7B3");
                entity.ToTable("Payment");
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.PaymentStatus).HasColumnType("int");
                entity.Property(e => e.OrderCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasOne(d => d.User).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payment__UserID__3B75D760");
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07D5A2B2C3");
                entity.ToTable("Role");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Order__3214EC07D2B2E5C3");
                entity.ToTable("Order");
                entity.Property(e => e.ShipperId).HasColumnName("ShipperId");
                entity.Property(e => e.BouquetId).HasColumnName("BouquetID");
                entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");
                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
                entity.Property(e => e.OrderStatus).HasColumnType("int");
                entity.HasOne(d => d.User).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__UserID__3E52440B");
                entity.HasOne(d => d.Bouquet).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BouquetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__BouquetI__3F115E1A");
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
                entity.HasOne(d => d.Subscription).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__Subscript__40F9A68C");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
