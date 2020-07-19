using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionData.Data;
using TransactionData.Data.Interfaces.Interfaces;
using TransactionData.Data.Repositories;
using TransactionData.Service.Dxos;
using TransactionData.Service.Interfaces.Dxos;
using TransactionData.Service.Interfaces.Services;
using TransactionData.Service.Services;

namespace TransactionData.IoC
{
    public static class ServiceContainer
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = AppDomain.CurrentDomain.Load("TransactionData.Service");
            services.AddMediatR(assembly);
            services.AddDbContext<TransactionDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("TransactionData.Data.MSSql")));

            services.AddTransient<ICsvTransactionDxo, CsvTransactionDxo>(); 
            services.AddTransient<IXmlTransactionDxo, XmlTransactionDxo>();
            services.AddTransient<ITransactionDxo, TransactionDxo>();


            services.AddTransient<ICsvTransactionService, CsvTransactionService>();
            services.AddTransient<IXmlTransactionService, XmlTransactionService>();

            services.AddTransient<ITransactionRepository, TransactionRepository>();
        }
    }
}
