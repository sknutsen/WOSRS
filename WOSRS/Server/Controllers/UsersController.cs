using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WOSRS.Server.Data;
using WOSRS.Server.Logic;
using WOSRS.Server.Models;

namespace WOSRS.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private DataDbContext context;
        private readonly ILogger<UsersController> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(ILogger<UsersController> logger, UserManager<ApplicationUser> userManager, DataDbContext context)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var userId = User.GetUserId();

            logger.LogWarning($"sending userid: {userId}\n\n\n");

            return Ok(userId);
        }
    }
}
