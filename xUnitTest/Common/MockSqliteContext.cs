using Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace xUnitTest.Common
{
    internal class MockSqliteContext : IDisposable
    {
        private readonly SqliteConnection connection;
        public MockSqliteContext()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
        }
        public void Dispose() => connection.Dispose();
        public Context CreateContext()
        {
            var result = new Context(new DbContextOptionsBuilder<Context>()
                .UseSqlite(connection)
                .Options);
            result.Database.EnsureCreated();
            return result;
        }
    }
}
