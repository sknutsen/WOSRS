using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOSRSTest.Shared.Models.BaseClasses;

public abstract class TimeStamped
{
    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }
}
