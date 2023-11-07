using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolShare.Models;

namespace schoolshare.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SchoolShare.Models.Item> Item { get; set; } = default!;
        public DbSet<SchoolShare.Models.ItemCategory> ItemCategory { get; set; } = default!;
        public DbSet<SchoolShare.Models.Notification> Notification { get; set; } = default!;
        public DbSet<SchoolShare.Models.Rating> Rating { get; set; } = default!;
        public DbSet<SchoolShare.Models.Transaction> Transaction { get; set; } = default!;
    }
}