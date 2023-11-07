using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolShare.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        // Passwords should be stored hashed and salted.
        // Never store plain-text passwords.
        // Here, you'd typically store the result of a hashing function.
        [Required]
        public string Password { get; set; }

        // Assuming rating is a float between 0 and 5, can adjust as necessary
        public float? Rating { get; set; }

        public int? NumberOfTransactions { get; set; }

        // Assuming profile picture will be stored as a URL to the image.
        // You can adjust this to byte[] if you want to store the image in the database.
        // However, storing images in a database isn't usually recommended for scalability.
        // Better to store in a file storage solution and just keep the path or URL here.
        public string ProfilePicture { get; set; }
    }
}
