using Microsoft.EntityFrameworkCore;
 
namespace TheWall.Models
{
    public class WallContext : DbContext
    {
        public DbSet<User> User {get; set;}
        public DbSet<Message> Message {get; set;}
        public DbSet<Comment> Comment {get; set;}
        // base() calls the parent class' constructor passing the "options" parameter along
        public WallContext(DbContextOptions<WallContext> options) : base(options) { }
    }
}
