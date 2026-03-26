using System;
using System.Collections.Generic;

namespace Logorhythms.Backend.Models;

public partial class EquipmentApproval
{
    public int ApprovalId { get; set; }

    public string TaNumber { get; set; } = null!;

    public string ApplicantName { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string Status { get; set; } = null!;

    public int? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }
}
