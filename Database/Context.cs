using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            //Database.MigrateAsync();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<VpnServer> VpnServers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AccessPosition> AccessPositions { get; set; }

    }
}
