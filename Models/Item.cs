using SchoolShare.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolShare.Models
{
    public enum ItemStatus
    {
        Available,
        Borrowed,
        Unavailable
    }

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // FK to ItemCategory (we will define this model later)
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual ItemCategory Category { get; set; }

        // FK to User for owner of the item
        public int? OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public virtual User? Owner { get; set; }

        // FK to User for current borrower, can be nullable
        public int? CurrentBorrowerID { get; set; }
        [ForeignKey("CurrentBorrowerID")]
        public virtual User? CurrentBorrower { get; set; }

        public int BorrowPeriod { get; set; } // in days

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SecurityDeposit { get; set; }

        // Assuming image will be stored as a URL. 
        public string Image { get; set; }

        [Required]
        public ItemStatus Status { get; set; } = ItemStatus.Available;
    }
}
