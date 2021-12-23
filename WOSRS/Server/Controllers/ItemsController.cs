using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOSRS.Server.Data;
using WOSRS.Server.Logic;
using WOSRS.Server.Models;
using WOSRS.Shared.Constants;
using WOSRS.Shared.DataContainers;
using WOSRS.Shared.Models;

namespace WOSRS.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        private DataDbContext context;
        private readonly ILogger<ItemsController> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public ItemsController(ILogger<ItemsController> logger, UserManager<ApplicationUser> userManager, DataDbContext context)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<ItemContainer>>> List()
        {
            logger.LogInformation(LogTexts.ListingItems);

            var userId = User.GetUserId();

            var result = context.Items.Where(e => e.UserId == userId).Select<Item, ItemContainer>(e => new ItemContainer { ItemId = e.ItemId, ItemName = e.ItemName, ItemCategories = e.ItemCategories }).ToList();

            return Ok(result);
        }

        [HttpPost("get")]
        public async Task<ActionResult<ItemContainer>> Get([FromBody] ItemContainer container)
        {
            logger.LogInformation(LogTexts.GettingItem);

            var userId = User.GetUserId();

            var result = await context.Items.FindAsync(container.ItemId);

            if (result.UserId != userId)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<ActionResult<ItemContainer>> Update([FromBody] ItemContainer container)
        {
            logger.LogInformation(LogTexts.UpdatingItem);

            var userId = User.GetUserId();

            var item = await context.Items.FindAsync(container.ItemId);

            if (item.UserId != userId)
            {
                return BadRequest();
            }

            item.ItemName = container.ItemName;
            item.UpdatedAt = System.DateTime.Now;

            context.Update(item);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("new")]
        public async Task<ActionResult<ItemContainer>> New([FromBody] ItemContainer container)
        {
            logger.LogInformation(LogTexts.CreatingItem);

            var userId = User.GetUserId();

            Item item = (Item) container.ToEntityClass();

            item.UserId = userId;
            item.CreatedAt = System.DateTime.Now;
            item.UpdatedAt = System.DateTime.Now;

            await context.AddAsync(item);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ItemContainer>> Delete([FromBody] ItemContainer container)
        {
            logger.LogInformation(LogTexts.DeletingItem);

            var userId = User.GetUserId();

            var item = await context.Items.FindAsync(container.ItemId);

            if (item.UserId != userId)
            {
                return BadRequest();
            }

            context.Remove(item);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
