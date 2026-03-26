using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logorhythms.Backend.Models
{
    public partial class Complaint
    {
        [Column("complaint_id")]
        public int ComplaintId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("complaint_type")]
        public string? ComplaintType { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("service_provider")]
        public string? ServiceProvider { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("submission_date")]
        public DateTime? SubmissionDate { get; set; }

        [Column("resolution_date")]
        public DateTime? ResolutionDate { get; set; }

        [Column("resolution_notes")]
        public string? ResolutionNotes { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // New fields with explicit column names
        [Column("full_name")]
        public string? FullName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("subject")]
        public string? Subject { get; set; }

        [Column("service_type")]
        public string? ServiceType { get; set; }

        [Column("incident_date")]
        public DateTime? IncidentDate { get; set; }

        [Column("account_ref")]
        public string? AccountRef { get; set; }

        [Column("preferred_contact")]
        public string? PreferredContact { get; set; }

        public virtual User? User { get; set; }
    }
}