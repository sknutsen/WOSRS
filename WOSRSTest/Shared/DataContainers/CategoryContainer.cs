using System.Collections.Generic;
using WOSRSTest.Shared.DataContainers.BaseClasses;
using WOSRSTest.Shared.DataContainers.Interfaces;
using WOSRSTest.Shared.Models;
using WOSRSTest.Shared.Models.Interfaces;

namespace WOSRSTest.Shared.DataContainers;

public class CategoryContainer : ContainerBase, IContainer
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }

    public ICollection<ItemCategory> ItemCategories { get; set; }

    public CategoryContainer() { }

    public CategoryContainer(IEntityClass entity)
    {
        Fill(entity);
    }

    public IEntityClass ToEntityClass()
    {
        return new Category
        {
            CategoryId = CategoryId,
            CategoryName = CategoryName,
            ItemCategories = ItemCategories
        };
    }

    public void Fill(IEntityClass entity)
    {
        Category category = (Category)entity;

        CategoryId = category.CategoryId;
        CategoryName = category.CategoryName;
        ItemCategories = category.ItemCategories;
    }
}
