using Microsoft.EntityFrameworkCore;
using Sales.Contexts;

namespace Sales.Tests.Support;

public static class DatabaseTestHelper
{
    public static Context CreateTestContext()
    {
        var connectionString = DotNetEnv.Env.GetString("DbConnectionString");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("DbConnectionString is not set");
        }

        var options = new DbContextOptionsBuilder<Context>()
            .UseNpgsql(connectionString)
            .Options;
        var context = new Context(options);
        
        context.Database.Migrate();
        
        return context;
    }

    public static void ResetDatabase(Context context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}