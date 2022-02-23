using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using nbp.api.client;
using nbp.core.commands;
using nbp.core.currenciesCommandHandlers;
using nbp.core.repositories;

namespace nbp.core
{
    public class IoC
    {
        public static void ServiceRegistrer(ServiceCollection services)
        {
            const string NBP_API_URL = "http://api.nbp.pl";
            services.AddAutoMapper(typeof(DatabaseContext));
            services.AddMediatR(typeof(CurrencyImportCommand));
            services.AddScoped( x => new ExchangeCurrencyListClient(NBP_API_URL));
            services.AddScoped( x => new ExchangeRatesCurrencyClient(NBP_API_URL));
            services.AddScoped( x => new ExchangeRatesTablesClient(NBP_API_URL));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
            services.AddScoped<IExchangeRatesTablesRepository, ExchangeRatesTablesRepository>();
            services.AddScoped<IExchangeRatesTablesWithRatesRepository, ExchangeRatesTablesWithRatesRepository>();
            services.AddScoped<ApiToDatabaseCurrenciesBridge>();
            services.AddScoped<ApiToDatabaseExchangeRatesTablesBridge>();
            services.AddScoped<IRequestHandler<CurrencyImportCommand, int>, CurrencyImportCommandHandler>();
            services.AddScoped<IRequestHandler<ExchangeRatesTablesImportCommand, int>, ExchangeRatesTablesImportCommandHandler>();
            

        }
    }
}