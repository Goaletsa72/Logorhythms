using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Logorhythms.Backend.Models;

namespace Logorhythms.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly BocraHackathonContext _context;

        public AnalyticsController(BocraHackathonContext context)
        {
            _context = context;
        }

        [HttpGet("applications-per-day")]
        public async Task<IActionResult> ApplicationsPerDay()
        {
            var data = await _context.LicenseApplications
                .GroupBy(a => a.ApplicationDate)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("licenses-by-type")]
        public async Task<IActionResult> LicensesByType()
        {
            var data = await _context.Licenses
                .GroupBy(l => l.LicenseTypeId)
                .Select(g => new { LicenseTypeId = g.Key, Count = g.Count() })
                .ToListAsync();

            var types = await _context.LicenseTypes.ToDictionaryAsync(t => t.TypeId, t => t.TypeName);
            var result = data.Select(d => new { TypeName = types.GetValueOrDefault(d.LicenseTypeId), d.Count });
            return Ok(result);
        }

        [HttpGet("complaints-by-status")]
        public async Task<IActionResult> ComplaintsByStatus()
        {
            var data = await _context.Complaints
                .GroupBy(c => c.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();
            return Ok(data);
        }
    }
}