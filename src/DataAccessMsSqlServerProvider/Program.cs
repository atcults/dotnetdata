using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccessMsSqlServerProvider
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    public class TemporaryDbContextFactory : IDbContextFactory<DomainModelMsSqlServerContext>
    {
        DomainModelMsSqlServerContext IDbContextFactory<DomainModelMsSqlServerContext>.Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<DomainModelMsSqlServerContext>();
            builder.UseSqlServer("Server=.\\sqlexpress;Database=dotnetdata;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("DataAccessMsSqlServerProvider"));
            return new DomainModelMsSqlServerContext(builder.Options);
        }
    }
}