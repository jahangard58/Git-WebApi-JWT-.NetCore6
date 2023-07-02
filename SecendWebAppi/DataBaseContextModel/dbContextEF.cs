using Microsoft.EntityFrameworkCore;
using SecendWebAppi.Models;

namespace SecendWebAppi.DataBaseContextModel
{
    public class dbContextEF : DbContext
    {
        public dbContextEF(DbContextOptions<dbContextEF> options) : base(options)
        {

        }

        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Truck> Trucks { get; set; } = null!;
    }
}
