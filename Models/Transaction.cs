using SchoolShare.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolShare.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        // FK to User for the lender
        [ForeignKey("LenderID")]
        public int LenderID { get; set; }

        public virtual User Lender { get; set; }

        // FK to User for the borrower
        public int BorrowerID { get; set; }
        [ForeignKey("BorrowerID")]
        public virtual User Borrower { get; set; }

        // FK to Item being borrowed
        [ForeignKey("ItemID")]
        public int ItemID { get; set; }

        public virtual Item Item { get; set; }

        [Required]
        public DateTime StartDate { get; set; } // When the item was borrowed

        [Required]
        public DateTime ExpectedEndDate { get; set; } // When the item is expected to be returned

        public DateTime? ActualEndDate { get; set; } // When the item was actually returned

        // Assuming rating is a float between 0 and 5, can adjust as necessary
        public float? RatingGiven { get; set; }

        [StringLength(500)]
        public string Comment { get; set; } // Additional comments about the transaction
    }
}
