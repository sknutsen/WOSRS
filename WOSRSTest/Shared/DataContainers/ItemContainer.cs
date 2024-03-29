﻿using System.Collections.Generic;
using WOSRSTest.Shared.DataContainers.BaseClasses;
using WOSRSTest.Shared.DataContainers.Interfaces;
using WOSRSTest.Shared.Models;
using WOSRSTest.Shared.Models.Interfaces;

namespace WOSRSTest.Shared.DataContainers;

public class ItemContainer : ContainerBase, IContainer
{
    public int ItemId { get; set; }
    public string ItemName { get; set; }

    public ICollection<ItemCategory> ItemCategories { get; set; }

    public ItemContainer() { }

    public ItemContainer(IEntityClass entity)
    {
        Fill(entity);
    }

    public IEntityClass ToEntityClass()
    {
        return new Item
        {
            ItemId = ItemId,
            ItemName = ItemName,
            ItemCategories = ItemCategories
        };
    }

    public void Fill(IEntityClass entity)
    {
        Item item = (Item)entity;

        ItemId = item.ItemId;
        ItemName = item.ItemName;
        ItemCategories = item.ItemCategories;
    }
}
