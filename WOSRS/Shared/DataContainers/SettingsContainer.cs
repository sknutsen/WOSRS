using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOSRS.Shared.DataContainers.BaseClasses;
using WOSRS.Shared.DataContainers.Interfaces;
using WOSRS.Shared.Models;
using WOSRS.Shared.Models.Interfaces;

namespace WOSRS.Shared.DataContainers;

public class SettingsContainer : ContainerBase, IContainer
{
    public int SettingsId { get; set; }
    public bool PointSystem { get; set; }
    public int? OrderType { get; set; }

    public IEntityClass ToEntityClass()
    {
        return new Settings
        {
            SettingsId = SettingsId,
            OrderType = OrderType,
            PointSystem = PointSystem
        };
    }

    public void Fill(IEntityClass entity)
    {
        Settings settings = (Settings)entity;

        SettingsId = settings.SettingsId;
        PointSystem = settings.PointSystem;
        OrderType = settings.OrderType;
    }
}
