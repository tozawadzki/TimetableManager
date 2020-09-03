using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimetableManager.Helpers.Interfaces;
using TimetableManager.Helpers.Models;

namespace TimetableManager.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AutomationController : Controller
    {
        private readonly IAutomationService _automationService;

        public AutomationController(IAutomationService automationService)
        {
            _automationService = automationService;
        }

        [HttpPost("hours/{color}")]
        public async Task<IActionResult> GetSpentHours(string color)
        {
            int hours = await _automationService.GetSpentHours(color);
            return Ok(hours);
        }

        [HttpPost("hours")]
        public async Task<IActionResult> GetSpentHours(ReportInterval interval)
        {
            var hours = await _automationService.GetSpentHours(interval);
            return Ok(hours);
        }
    }
}