using SchoolShare.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolShare.Models
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingID { get; set; }

        // FK to User for the rater
        public int RaterID { get; set; }

        [ForeignKey("RaterID")]
        public virtual User Rater { get; set; }

        // FK to User for the ratee
        public int RateeID { get; set; }

        [ForeignKey("RateeID")]
        public virtual User Ratee { get; set; }

        // FK to Transaction
        public int TransactionID { get; set; }

        [ForeignKey("TransactionID")]
        public virtual Transaction Transaction { get; set; }

        [Range(1, 5)] // Ratings usually start from 1, but adjust if 0 is a valid score
        public float Score { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }
    }
}
