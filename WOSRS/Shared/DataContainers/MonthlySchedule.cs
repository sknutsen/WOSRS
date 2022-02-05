using System.Collections.Generic;

namespace WOSRS.Shared.DataContainers;

public class MonthlySchedule
{
    public int Month { get; set; }
    public int Year { get; set; }
    public IEnumerable<ScheduledItemContainer> ScheduledItems { get; set; }
}
