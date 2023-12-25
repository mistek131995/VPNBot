using Infrastructure.Database.Entity;
using Microsoft.EntityFrameworkCore;
using File = Infrastructure.Database.Entity.File;

namespace Infrastructure.Database
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {
        internal DbSet<User> Users { get; set; }
        internal DbSet<Access> Accesses { get; set; }
        internal DbSet<VpnServer> VpnServers { get; set; }
        internal DbSet<Setting> Settings { get; set; }
        internal DbSet<Payment> Payments { get; set; }
        internal DbSet<AccessPosition> AccessPositions { get; set; }
        internal DbSet<Log> Logs { get; set; }
        internal DbSet<File> Files { get; set; }
        internal DbSet<Country> Countries { get; set; }
    }
}
