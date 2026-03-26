using System;
using System.Collections.Generic;

namespace Logorhythms.Backend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool? IsCompany { get; set; }

    public string? CompanyName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

    public virtual ICollection<EquipmentApproval> EquipmentApprovals { get; set; } = new List<EquipmentApproval>();

    public virtual ICollection<LicenseApplication> LicenseApplications { get; set; } = new List<LicenseApplication>();

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
}
