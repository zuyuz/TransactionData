using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionData.Data;
using TransactionData.Data.Interfaces.Interfaces;
using TransactionData.Data.Repositories;
using TransactionData.Service.Dxos;

namespace TransactionData.IoC
{
    public static class ServiceContainer
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = AppDomain.CurrentDomain.Load("TransactionData.Service");
            services.AddMediatR(assembly);
            services.AddDbContext<TransactionDataContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<CsvTransactionDxo>(); 
            services.AddTransient<XmlTransactionDxo>();
            services.AddTransient<TransactionDxo>();

            services.AddTransient<ITransactionRepository, TransactionRepository>();
        }
    }
}
