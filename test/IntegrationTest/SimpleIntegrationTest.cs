using System;
using System.IO;
using DataAccessMySqlProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace IntegrationTest
{
    /*public class SimpleIntegrationTest : IDisposable
    {
        private readonly DomainModelMySqlContext _context;

        public static ILoggerFactory LoggerFactory;
        public static IConfigurationRoot Configuration;

        public SimpleIntegrationTest()
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

            Console.Write("Hello");

            var dbContextBuilder = new DbContextOptionsBuilder<DomainModelMySqlContext>();
            dbContextBuilder.UseMySQL(sqlConnectionString,
                    b => b.MigrationsAssembly("Web"));

            _context = new DomainModelMySqlContext(dbContextBuilder.Options, LoggerFactory);

            _context.Database.Migrate();

        }

        [Fact]
        public void QueryMonstersFromSqlTest()
        {
            _context.Database.BeginTransaction();

            _context.DataEventRecords.Add(new DataEventRecord
            {
                DataEventRecordId = 1,
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
    }*/
}