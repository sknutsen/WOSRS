using System.Collections.Generic;

namespace WOSRSTest.Shared.DataContainers;

public class MonthlySchedule
{
    public int Month { get; set; }
    public int Year { get; set; }
    public IEnumerable<ScheduledItemContainer> ScheduledItems { get; set; }
}
