using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOSRS.Shared.Models.BaseClasses;
using WOSRS.Shared.Models.Interfaces;

namespace WOSRS.Shared.Models
{
    [Table("item_categories")]
    public class ItemCategory : TimeStamped, IEntityClass
    {
        [Key]
        [Column("item_category_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemCategoryId { get; set; }

        [Required]
        [ForeignKey("items")]
        [Column("item_id")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Required]
        [ForeignKey("categories")]
        [Column("category_id")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
