using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Logorhythms.Backend.Models;

public partial class BocraHackathonContext : DbContext
{
    public BocraHackathonContext()
    {
    }

    public BocraHackathonContext(DbContextOptions<BocraHackathonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<EquipmentApproval> EquipmentApprovals { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<LicenseApplication> LicenseApplications { get; set; }

    public virtual DbSet<LicenseType> LicenseTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId).HasName("PK__complain__A771F61C23A1AF96");

            entity.ToTable("complaints");

            entity.HasIndex(e => e.Status, "idx_complaint_status");

            entity.HasIndex(e => e.ServiceProvider, "idx_service_provider");

            entity.Property(e => e.ComplaintId).HasColumnName("complaint_id");
            entity.Property(e => e.ComplaintType)
                .HasMaxLength(50)
                .HasColumnName("complaint_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ResolutionDate).HasColumnName("resolution_date");
            entity.Property(e => e.ResolutionNotes).HasColumnName("resolution_notes");
            entity.Property(e => e.ServiceProvider)
                .HasMaxLength(150)
                .HasColumnName("service_provider");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValue("Pending")
                .HasColumnName("status");
            entity.Property(e => e.SubmissionDate).HasColumnName("submission_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_complaints_user");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("PK__document__8AD0292492930976");

            entity.ToTable("documents");

            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UploadDate).HasColumnName("upload_date");
        });

        modelBuilder.Entity<EquipmentApproval>(entity =>
        {
            entity.HasKey(e => e.ApprovalId).HasName("PK__equipmen__C94AE61A87CF6C04");

            entity.ToTable("equipment_approvals");

            entity.HasIndex(e => e.TaNumber, "UQ__equipmen__3960AC697DFCE784").IsUnique();

            entity.HasIndex(e => new { e.Manufacturer, e.Model }, "idx_manufacturer_model");

            entity.HasIndex(e => e.TaNumber, "idx_ta_number");

            entity.Property(e => e.ApprovalId).HasColumnName("approval_id");
            entity.Property(e => e.ApplicantName)
                .HasMaxLength(200)
                .HasColumnName("applicant_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(150)
                .HasColumnName("manufacturer");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Approved")
                .HasColumnName("status");
            entity.Property(e => e.TaNumber)
                .HasMaxLength(50)
                .HasColumnName("ta_number");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.EquipmentApprovals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_ea_user");
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.LicenseId).HasName("PK__licenses__BBBB75789A28CF49");

            entity.ToTable("licenses");

            entity.HasIndex(e => e.LicenseNumber, "UQ__licenses__D482A003889FDB34").IsUnique();

            entity.Property(e => e.LicenseId).HasColumnName("license_id");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(50)
                .HasColumnName("license_number");
            entity.Property(e => e.LicenseTypeId).HasColumnName("license_type_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_licenses_user");
        });

        modelBuilder.Entity<LicenseApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__license___3BCBDCF21FB71155");

            entity.ToTable("license_applications");

            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.ApplicationDate).HasColumnName("application_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.LicenseTypeId).HasColumnName("license_type_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValue("Pending")
                .HasColumnName("status");
            entity.Property(e => e.SubmittedDocuments).HasColumnName("submitted_documents");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.LicenseType).WithMany(p => p.LicenseApplications)
                .HasForeignKey(d => d.LicenseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_la_type");

            entity.HasOne(d => d.User).WithMany(p => p.LicenseApplications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_la_user");
        });

        modelBuilder.Entity<LicenseType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__license___2C00059886A6D68C");

            entity.ToTable("license_types");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Category)
                .HasMaxLength(20)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TypeName)
                .HasMaxLength(150)
                .HasColumnName("type_name");
            entity.Property(e => e.TypicalDurationYears)
                .HasDefaultValue(15)
                .HasColumnName("typical_duration_years");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F5742236D");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164CB83AB07").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(200)
                .HasColumnName("company_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .HasColumnName("full_name");
            entity.Property(e => e.IsCompany)
                .HasDefaultValue(false)
                .HasColumnName("is_company");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("applicant")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
