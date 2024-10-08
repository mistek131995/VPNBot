﻿using Infrastructure.Database;
using Infrastructure.Database.Entity;
using Microsoft.EntityFrameworkCore;
using File = Infrastructure.Database.Entity.File;

namespace Infrastructure.Common
{
    public abstract class ContextFactory : DbContext
    {
        public ContextFactory() { }

        public ContextFactory(DbContextOptions<Context> options) : base(options)
        {
        }

        internal DbSet<User> Users { get; set; }
        internal DbSet<UserSetting> UserSettings { get; set; }
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
        internal DbSet<MessageFile> MessageFiles { get; set; }
        internal DbSet<ResetPassword> ResetPasswords { get; set; }
        internal DbSet<UserConnection> UserConnections { get; set; }
        internal DbSet<PromoCode> PromoCodes { get; set; }
        internal DbSet<ConnectionStatistic> ConnectionStatistics { get; set; }
        internal DbSet<ChangeEmailRequest> ChangeEmailRequests { get; set; }
        internal DbSet<ChangePasswordRequest> ChangePasswordRequests { get; set; }
    }
}
