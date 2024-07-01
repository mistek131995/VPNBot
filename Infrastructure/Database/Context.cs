using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class Context(DbContextOptions<Context> options) : ContextFactory(options)
    {
        
    }
}
