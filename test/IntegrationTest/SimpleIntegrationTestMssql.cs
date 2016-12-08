using System;
using System.IO;
using DataAccessMsSqlServerProvider;
using DomainModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace IntegrationTest
{
    public class SimpleIntegrationTestMssql : IDisposable
    {
        private readonly DomainModelMsSqlServerContext _context;

        public static ILoggerFactory LoggerFactory;
        public static IConfigurationRoot Configuration;

        public SimpleIntegrationTestMssql()
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
            var sqlConnectionString = Configuration.GetConnectionString("DataAccessMsSqlServerProvider");

            var dbContextBuilder = new DbContextOptionsBuilder<DomainModelMsSqlServerContext>();
            dbContextBuilder.UseSqlServer(sqlConnectionString, b => b.MigrationsAssembly("DataAccessMsSqlServerProvider"));

            _context = new DomainModelMsSqlServerContext(dbContextBuilder.Options);

            _context.Database.Migrate();

        }

        [Fact]
        public void QueryMonstersFromSqlTest()
        {
            _context.DataEventRecords.Add(new DataEventRecord
            {
                Description = "New description",
                Timestamp = new DateTime()
            });

            _context.SaveChanges();

            Assert.True(true);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}