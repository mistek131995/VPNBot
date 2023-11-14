using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        internal DbSet<User> Users { get; set; }
        internal DbSet<Access> Accesses { get; set; }
    }
}
