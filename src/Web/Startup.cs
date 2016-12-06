using System.IO;
using DataAccessMsSqlServerProvider;
using DomainModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace DotNetData
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            DataProviderHelper.AddMsSqlProvider(services, Configuration);

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseIISIntegration()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public static class DataProviderHelper
    {
//        public static void AddMySqlProvider(IServiceCollection services, IConfigurationRoot configuration)
//        {
//            //Use a MySQL database
//            var sqlConnectionString = configuration.GetConnectionString("DataAccessMySqlProvider");
//
//            services.AddDbContext<DomainModelMySqlContext>(options =>
//                options.UseMySQL(
//                    sqlConnectionString,
//                    b => b.MigrationsAssembly("Web")
//                )
//            );
//
//            services.AddScoped<IDataAccessProvider, DataAccessMySqlProvider.DataAccessMySqlProvider>();
//        }

//         Use a SQLite database
//         var sqlConnectionString = Configuration.GetConnectionString("DataAccessSqliteProvider");
//
//        services.AddDbContext<DomainModelSqliteContext>(options =>
//            options.UseSqlite(
//                sqlConnectionString,
//                b => b.MigrationsAssembly("AspNet5MultipleProject")
//            )
//        );
//
//        services.AddScoped<IDataAccessProvider, DataAccessSqliteProvider.DataAccessSqliteProvider>();

        public static void AddMsSqlProvider(IServiceCollection services, IConfigurationRoot configuration)
        {
             //Use a MS SQL Server database
             var sqlConnectionString = configuration.GetConnectionString("DataAccessMsSqlServerProvider");

            services.AddDbContext<DomainModelMsSqlServerContext>(options =>
                options.UseSqlServer(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("Web")
                )
            );

            services.AddScoped<IDataAccessProvider, DataAccessMsSqlServerProvider.DataAccessMsSqlServerProvider>();

        }

//        Use a PostgreSQL database
//        var sqlConnectionString = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");
//
//        services.AddDbContext<DomainModelPostgreSqlContext>(options =>
//            options.UseNpgsql(
//                sqlConnectionString,
//                b => b.MigrationsAssembly("AspNet5MultipleProject")
//            )
//        );
//
//        services.AddScoped<IDataAccessProvider, DataAccessPostgreSqlProvider.DataAccessPostgreSqlProvider>();
    }
}
