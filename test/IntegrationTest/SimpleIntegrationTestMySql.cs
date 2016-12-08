using System;
using System.IO;
using DataAccessMySqlProvider;
using DomainModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySQL.Data.Entity.Extensions;
using Xunit;

namespace IntegrationTest
{
    public class SimpleIntegrationTestMySql : IDisposable
    {
        private readonly DomainModelMySqlContext _context;

        public static ILoggerFactory LoggerFactory;
        public static IConfigurationRoot Configuration;

        public SimpleIntegrationTestMySql()
        {
            // work with with a builder using multiple calls
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("config.json");
            Configuration = configBuilder.Build();

            LoggerFactory = new LoggerFactory()
                .AddConsole(Configuration.GetSection("Logging"))
                .AddDebug();

            //Use a MySQL database
            var sqlConnectionString = Configuration.GetConnectionString("DataAccessMySqlProvider");

            var dbContextBuilder = new DbContextOptionsBuilder<DomainModelMySqlContext>();
            dbContextBuilder.UseMySQL(sqlConnectionString, b => b.MigrationsAssembly("DataAccessMySqlProvider"));

            _context = new DomainModelMySqlContext(dbContextBuilder.Options);

            _context.Database.Migrate();

        }

        [Fact]
        public void QueryMonstersFromSqlTest()
        {
            _context.Database.BeginTransaction();

            _context.DataEventRecords.Add(new DataEventRecord
            {
                Description = "New description",
                Timestamp = new DateTime()
            });

            _context.SaveChanges();

            _context.Database.RollbackTransaction();

            Assert.True(true);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}