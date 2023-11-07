using SchoolShare.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolShare.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationID { get; set; }

        // FK to User for the recipient
        public int RecipientID { get; set; }
        [ForeignKey("RecipientID")]
        public virtual User Recipient { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        [Required]
        public bool IsRead { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
