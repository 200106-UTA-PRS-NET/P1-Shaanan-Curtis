using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBox.Storing.Entities
{
    public partial class PIZZABOXContext : DbContext
    {
        public PIZZABOXContext()
        {
        }

        public PIZZABOXContext(DbContextOptions<PIZZABOXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Ordertype> Ordertype { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=PIZZABOX;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("INV_PK");

                entity.ToTable("INVENTORY");

                entity.HasIndex(e => e.StoreId)
                    .HasName("UQ__INVENTOR__3B82F0E01D6A424F")
                    .IsUnique();

                entity.Property(e => e.StoreId)
                    .HasColumnName("StoreID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Store)
                    .WithOne(p => p.Inventory)
                    .HasForeignKey<Inventory>(d => d.StoreId)
                    .HasConstraintName("INV_STORE_FK");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("ORDERS_PK");

                entity.ToTable("ORDERS");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ORDERS_STORE_FK");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ORDERS_USER_FK");
            });

            modelBuilder.Entity<Ordertype>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("OT_PK");

                entity.ToTable("ORDERTYPE");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.Custom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Dt)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Preset)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Tm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.Ordertype)
                    .HasForeignKey<Ordertype>(d => d.OrderId)
                    .HasConstraintName("OT_FK");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("STORE");

                entity.HasIndex(e => e.StoreId)
                    .HasName("UQ__STORE__3B82F0E064C44037")
                    .IsUnique();

                entity.HasIndex(e => new { e.City, e.State })
                    .HasName("Location")
                    .IsUnique();

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("USER_PK");

                entity.ToTable("USER");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__USER__536C85E46EE71351")
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
