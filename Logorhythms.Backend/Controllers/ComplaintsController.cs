using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Logorhythms.Backend.Models;

namespace Logorhythms.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintsController : ControllerBase
    {
        private readonly BocraHackathonContext _context;

        public ComplaintsController(BocraHackathonContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComplaint([FromBody] ComplaintDto complaintDto)
        {
            // Create a new Complaint entity
            var complaint = new Complaint
            {
                UserId = complaintDto.UserId,               // You may need to get the logged-in user ID
                ComplaintType = complaintDto.ComplaintType,
                Description = complaintDto.Description,
                ServiceProvider = complaintDto.ServiceProvider,
                Status = "Pending",                         // Default status
                SubmissionDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Complaint submitted successfully", complaintId = complaint.ComplaintId });
        }
    }

    // DTO for receiving complaint data
    public class ComplaintDto
    {
        public int? UserId { get; set; }                    // Optional, if not logged in
        public string ComplaintType { get; set; }
        public string Description { get; set; }
        public string ServiceProvider { get; set; }
    }
}