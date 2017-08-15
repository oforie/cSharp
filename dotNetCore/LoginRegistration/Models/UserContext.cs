using Microsoft.EntityFrameworkCore;
 
namespace LoginRegistration.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users {get; set;}
        // base() calls the parent class' constructor passing the "options" parameter along
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    }
}
