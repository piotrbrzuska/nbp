using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nbp.core;
using nbp.core.commands;
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
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            
            switch (args[0].ToLower())
            {
                case "currency":
                    var currenciesCli = (serviceProvider.GetService<CurrenciesCli>());
                    await currenciesCli?.DispatchAction(args.Skip(1).ToArray())!;
                    
                    break;
            }
        }
    }

    public class CurrenciesCli
    {
        private readonly IMediator  _mediator;

        public CurrenciesCli(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAction(string[] args)
        {
            switch (args[0].ToLower())
            {
                case "import":
                    var importCommand = new CurrencyImportCommand();
                    var response = await _mediator.Send(importCommand, new CancellationToken());
                    break;
            }
        }
    }
    
}
