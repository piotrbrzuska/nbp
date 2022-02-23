using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nbp.core;

namespace nbp.cli
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {

                // Start!
                MainAsync(args).Wait();
                return 0;
            }
            catch(Exception ex)
            {
                return 1;
            }
        }
        
        static async Task MainAsync(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            var services = new ServiceCollection();
            var connectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));;
            IoC.ServiceRegistrer(services);
            services.AddScoped<CurrenciesCli>();
            services.AddScoped<ExchangeRateTableCli>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            
            switch (args[0].ToLower())
            {
                case "currency":
                    var currenciesCli = (serviceProvider.GetService<CurrenciesCli>());
                    await currenciesCli?.DispatchAction(args.Skip(1).ToArray())!;
                    
                    break;
                case "exchange-rate":
                    var  exchangeRateCli = (serviceProvider.GetService<ExchangeRateTableCli>());
                    await exchangeRateCli?.DispatchAction(args.Skip(1).ToArray())!;
                    
                    break;
            }
        }
    }
}
