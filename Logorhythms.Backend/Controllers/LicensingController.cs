using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Logorhythms.Backend.Models;
using System.Security.Claims;
using System.Text.Json;

namespace Logorhythms.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicensingController : ControllerBase
    {
        private readonly BocraHackathonContext _context;
        private readonly ILogger<LicensingController> _logger;

        public LicensingController(BocraHackathonContext context, ILogger<LicensingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("applications")]
        public async Task<IActionResult> CreateApplication()
        {
            // Get the service category from the form
            var serviceCategory = Request.Form["service_category"].ToString().ToLowerInvariant();

            // Map frontend value to a license type name
            var nameMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // New categories (exact matches)
                { "network services", "Network Services" },
                { "internet service provider", "Internet Service Provider" },
                { "broadcasting", "Broadcasting" },
                { "broadcast", "Broadcasting" },               // added to handle 'broadcast'
                { "postal services", "Postal Services" },
                { "spectrum usage", "Spectrum Usage" },
                { "spectrum", "Spectrum Usage" },              // added for 'spectrum'
                // Old values for backward compatibility
                { "network", "Network Services" },             // map 'network' to Network Services
                { "internet", "Internet Service Provider" },   // map 'internet' to ISP
                { "cellular", "Cellular Services and Applications" },
                { "mobile", "Cellular Services and Applications" }
            };

            if (!nameMapping.TryGetValue(serviceCategory, out string licenseTypeName))
            {
                // Optionally try a partial match if exact fails (e.g., contains "broadcast")
                var partialMatch = nameMapping.Keys.FirstOrDefault(k => serviceCategory.Contains(k, StringComparison.OrdinalIgnoreCase));
                if (partialMatch != null)
                {
                    licenseTypeName = nameMapping[partialMatch];
                }
                else
                {
                    return BadRequest(new { error = $"Unknown service category '{serviceCategory}'." });
                }
            }

            // Find the license type by its name
            var licenseType = await _context.LicenseTypes.FirstOrDefaultAsync(lt => lt.TypeName == licenseTypeName);
            if (licenseType == null)
            {
                return BadRequest(new { error = $"License type '{licenseTypeName}' not found in database." });
            }

            // Determine user ID (logged-in or guest)
            int userId;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int id))
            {
                userId = id;
            }
            else
            {
                var guest = await _context.Users.FirstOrDefaultAsync(u => u.Email == "guest@bocra.org.bw");
                if (guest == null)
                {
                    guest = new User
                    {
                        Email = "guest@bocra.org.bw",
                        PasswordHash = "placeholder",
                        Role = "applicant",
                        FullName = "Guest User",
                        Phone = "",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.Users.Add(guest);
                    await _context.SaveChangesAsync();
                }
                userId = guest.UserId;
            }

            // Collect all form data into a JSON string
            var formData = new Dictionary<string, string>();
            foreach (var key in Request.Form.Keys)
            {
                formData[key] = Request.Form[key];
            }
            string submittedDocuments = JsonSerializer.Serialize(formData);

            // Parse the requested start date as DateOnly
            var requestedStartDate = Request.Form.ContainsKey("requested_start_date") && DateOnly.TryParse(Request.Form["requested_start_date"], out var startDate)
                ? startDate
                : DateOnly.FromDateTime(DateTime.UtcNow);

            // Create the license application
            var application = new LicenseApplication
            {
                LicenseTypeId = licenseType.TypeId,
                UserId = userId,
                ApplicationDate = requestedStartDate,
                Status = "Pending",
                SubmittedDocuments = submittedDocuments,
                Notes = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.LicenseApplications.Add(application);
            await _context.SaveChangesAsync();

            // Return a nice success message
            return Ok(new
            {
                success = true,
                message = "Your license application has been submitted successfully!",
                applicationId = application.ApplicationId,
                licenseType = licenseType.TypeName
            });
        }
    }
}