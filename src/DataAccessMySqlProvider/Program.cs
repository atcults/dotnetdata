using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySQL.Data.Entity.Extensions;

namespace DataAccessMySqlProvider
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    public class TemporaryDbContextFactory : IDbContextFactory<DomainModelMySqlContext>
    {
        DomainModelMySqlContext IDbContextFactory<DomainModelMySqlContext>.Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<DomainModelMySqlContext>();
            builder.UseMySQL("server=localhost;userid=root;password=;database=dotnetdata;", b => b.MigrationsAssembly("DataAccessMySqlProvider"));
            return new DomainModelMySqlContext(builder.Options);
        }
    }
}