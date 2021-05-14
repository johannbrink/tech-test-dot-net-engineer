using System;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.LeadManagement;

namespace TechChallenge.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Lead> Leads { get; set; }
        public virtual DbSet<Suburb> Suburbs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.ParentId)
                    .HasName("idx_categories_parent_category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId)
                    .HasColumnName("parent_category_id")
                    .HasColumnType("int(11) unsigned");
            });

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.ToTable("jobs");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11) unsigned");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("idx_jobs_category");
                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11) unsigned");
                entity.HasOne(d => d.Category);

                entity.HasIndex(e => e.SuburbId)
                    .HasName("idx_jobs_suburb");
                entity.Property(e => e.SuburbId)
                    .HasColumnName("suburb_id")
                    .HasColumnType("int(11) unsigned");
                entity.HasOne(d => d.Suburb);

                entity.OwnsOne(d => d.Contact)
                    .Property(p=>p.FullName)
                    .HasColumnName("contact_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.OwnsOne(d => d.Contact)
                    .Property(p=>p.EmailAddress)
                    .HasColumnName("contact_email")
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.OwnsOne(d => d.Contact)
                    .Property(p=>p.PhoneNumber)
                    .HasColumnName("contact_phone")
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.OwnsOne(d => d.Contact)
                    .Ignore(p => p.FirstName)
                    .Ignore(p => p.LastName);
                
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.OwnsOne(d => d.Price)
                    .Property(p=>p.Value)
                    .HasColumnName("price")
                    .HasColumnType("double(5,2) unsigned");

                entity.Property(p => p.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'New'")
                    .HasConversion(
                        v => v.ToString(),
                        v => (LeadStatus) Enum.Parse(typeof(LeadStatus), v));

            entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'")
                    .ValueGeneratedOnAddOrUpdate();

            });

            modelBuilder.Entity<Suburb>(entity =>
            {
                entity.ToTable("suburbs");

                entity.HasIndex(e => e.PostCode)
                    .HasName("idx_suburbs_postcode");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasColumnName("postcode")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

        }
    }
}
