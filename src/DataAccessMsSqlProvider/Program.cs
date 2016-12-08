using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccessMsSqlProvider
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    public class TemporaryDbContextFactory : IDbContextFactory<DomainModelMsSqlContext>
    {
        DomainModelMsSqlContext IDbContextFactory<DomainModelMsSqlContext>.Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<DomainModelMsSqlContext>();
            builder.UseSqlServer("Server=.\\sqlexpress;Database=dotnetdata;Trusted_Connection=True;MultipleActiveResultSets=true", b => b.MigrationsAssembly("DataAccessMsSqlProvider"));
            return new DomainModelMsSqlContext(builder.Options);
        }
    }
}