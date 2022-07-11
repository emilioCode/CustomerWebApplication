using System;
using System.Collections.Generic;
using CustomerWebApplication.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerWebApplication.Models
{
    public partial class DBCustomerContext : DbContext
    {
        public DBCustomerContext()
        {
        }

        public DBCustomerContext(DbContextOptions<DBCustomerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(ConnectionStrings.SQLServer);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESSES");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Address0)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS0");

                entity.Property(e => e.Type0)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TYPE0");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Address)
                    .HasForeignKey<Address>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMERS_ADDRESSES");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMERS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GENDER")
                    .HasDefaultValueSql("('M')")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
