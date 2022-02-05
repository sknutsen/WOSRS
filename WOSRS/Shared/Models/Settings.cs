using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOSRS.Shared.Models.Interfaces;

namespace WOSRS.Shared.Models;

[Table("settings")]
public class Settings : IEntityClass
{
    [Key]
    [Column("settings_id")]
    public int SettingsId { get; set; }

    [Column("point_system")]
    public bool PointSystem { get; set; }

    [Column("order_type")]
    public int? OrderType { get; set; }

    [Required]
    [ForeignKey("application_users")]
    [Column("user_id")]
    public string UserId { get; set; }
}
