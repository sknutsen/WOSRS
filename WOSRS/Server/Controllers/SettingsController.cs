using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenIddict.Validation.AspNetCore;
using System.Linq;
using System.Threading.Tasks;
using WOSRS.Server.Data;
using WOSRS.Server.Logic;
using WOSRS.Server.Models;
using WOSRS.Shared.DataContainers;
using WOSRS.Shared.Models;

namespace WOSRS.Server.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        private DataDbContext context;
        private readonly ILogger<SettingsController> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public SettingsController(ILogger<SettingsController> logger, UserManager<ApplicationUser> userManager, DataDbContext context)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet("get")]
        public async Task<ActionResult<SettingsContainer>> Get()
        {
            string userId = User.GetUserId();

            SettingsContainer result = context.Settings.Where(e => e.UserId == userId).Select(e => new SettingsContainer { SettingsId = e.SettingsId, OrderType = e.OrderType, PointSystem = e.PointSystem }).FirstOrDefault();

            if (result == null)
            {
                Settings newSettings = new Settings();
                newSettings.UserId = userId;
                newSettings.OrderType = 0;
                newSettings.PointSystem = false;

                var settings = await context.AddAsync(newSettings);
                await context.SaveChangesAsync();

                result = new SettingsContainer();
                result.Fill(settings.Entity);
            }

            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<ActionResult<SettingsContainer>> Update([FromBody] SettingsContainer container)
        {
            string userId = User.GetUserId();

            Settings settings = await context.Settings.FindAsync(container.SettingsId);

            if (settings.UserId != userId)
            {
                return BadRequest();
            }

            settings.OrderType = container.OrderType;
            settings.PointSystem = container.PointSystem;

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
