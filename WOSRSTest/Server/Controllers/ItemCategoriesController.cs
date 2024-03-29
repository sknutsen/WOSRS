﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenIddict.Validation.AspNetCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOSRSTest.Server.Data;
using WOSRSTest.Server.Logic;
using WOSRSTest.Server.Models;
using WOSRSTest.Shared.Constants;
using WOSRSTest.Shared.DataContainers;
using WOSRSTest.Shared.Models;

namespace WOSRSTest.Server.Controllers;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class ItemCategoriesController : Controller
{
    private DataDbContext context;
    private readonly ILogger<ItemCategoriesController> logger;
    private readonly UserManager<ApplicationUser> userManager;

    public ItemCategoriesController(ILogger<ItemCategoriesController> logger, UserManager<ApplicationUser> userManager, DataDbContext context)
    {
        this.logger = logger;
        this.userManager = userManager;
        this.context = context;
    }

    [HttpPost("new")]
    public async Task<ActionResult<ItemCategoryContainer>> New([FromBody] ItemCategoryContainer container)
    {
        logger.LogInformation(LogTexts.CreatingItemCategoryLink);

        var userId = User.GetUserId();

        var item = await context.Items.FindAsync(container.ItemId);
        var category = await context.Categories.FindAsync(container.CategoryId);

        if (item.UserId != userId || category.UserId != userId)
        {
            logger.LogWarning(LogTexts.NewItemCategoryLinkFailed);

            return BadRequest();
        }

        ItemCategory itemCategory = (ItemCategory)container.ToEntityClass();

        itemCategory.CreatedAt = System.DateTime.Now;
        itemCategory.UpdatedAt = System.DateTime.Now;

        var result = context.ItemCategories.Add(itemCategory);
        await context.SaveChangesAsync();

        logger.LogInformation(LogTexts.NewItemCategoryLinkSuccess, result.Entity.ItemCategoryId);

        return NoContent();
    }

    [HttpPost("delete")]
    public async Task<ActionResult<ItemCategoryContainer>> Delete([FromBody] ItemCategoryContainer container)
    {
        logger.LogInformation(LogTexts.DeletingItemCategoryLink);

        var userId = User.GetUserId();

        var result = await context.ItemCategories.FindAsync(container.ItemCategoryId);

        var item = await context.FindAsync<Item>(container.ItemId);
        var category = await context.FindAsync<Category>(container.ItemId);

        if (item.UserId != userId || category.UserId != userId)
        {
            logger.LogWarning(LogTexts.DeleteItemCategoryLinkFailed);

            return BadRequest();
        }

        context.ItemCategories.Remove(result);
        await context.SaveChangesAsync();

        logger.LogInformation(LogTexts.DeleteItemCategoryLinkSuccess, result.ItemCategoryId);

        return NoContent();
    }
}
