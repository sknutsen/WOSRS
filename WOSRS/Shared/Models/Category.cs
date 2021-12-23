using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOSRS.Shared.Models.BaseClasses;
using WOSRS.Shared.Models.Interfaces;

namespace WOSRS.Shared.Models
{
    [Table("categories")]
    public class Category : TimeStamped, IEntityClass
    {
        [Key]
        [Column("category_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        [Column("category_name", TypeName = "text")]
        public string CategoryName { get; set; }

        [Required]
        [ForeignKey("application_users")]
        [Column("user_id")]
        public string UserId { get; set; }

        public ICollection<ItemCategory> ItemCategories { get; set; }
    }
}
