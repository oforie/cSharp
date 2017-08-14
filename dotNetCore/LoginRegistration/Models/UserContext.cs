using Microsoft.EntityFrameworkCore;
 
namespace LoginRegistration.Models
{
    public class UserContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
    }
