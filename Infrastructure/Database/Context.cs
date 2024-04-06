using Infrastructure.Database.Entity;
using Microsoft.EntityFrameworkCore;
using File = Infrastructure.Database.Entity.File;

namespace Infrastructure.Database
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {

        internal DbSet<User> Users { get; set; }
        internal DbSet<VpnServer> VpnServers { get; set; }
        internal DbSet<Setting> Settings { get; set; }
        internal DbSet<Payment> Payments { get; set; }
        internal DbSet<AccessPosition> AccessPositions { get; set; }
        internal DbSet<Log> Logs { get; set; }
        internal DbSet<File> Files { get; set; }
        internal DbSet<Location> Locations { get; set; }
        internal DbSet<Activation> Activations { get; set; }
        internal DbSet<TicketCategory> TicketCategories { get; set; }
        internal DbSet<TicketMessage> TicketMessages { get; set; }
        internal DbSet<Ticket> Tickets { get; set; }
        internal DbSet<ResetPassword> ResetPasswords { get; set; }
        internal DbSet<UserConnection> UserConnections { get; set; }
    }
}
