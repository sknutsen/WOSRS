using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOSRSTest.Shared.DataContainers.BaseClasses;
using WOSRSTest.Shared.DataContainers.Interfaces;
using WOSRSTest.Shared.Models;
using WOSRSTest.Shared.Models.Interfaces;

namespace WOSRSTest.Shared.DataContainers;

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
