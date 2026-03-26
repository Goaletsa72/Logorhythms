using System;
using System.Collections.Generic;

namespace Logorhythms.Backend.Models;

public partial class LicenseType
{
    public int TypeId { get; set; }

    public string Category { get; set; } = null!;

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public int? TypicalDurationYears { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<LicenseApplication> LicenseApplications { get; set; } = new List<LicenseApplication>();
}
