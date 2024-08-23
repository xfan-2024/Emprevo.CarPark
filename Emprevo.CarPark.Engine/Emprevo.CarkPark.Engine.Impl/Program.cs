using Emprevo.CarPark.Impl;
using Emprevo.CarPark.Impl.Services;
using Emprevo.CarPark.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Emprevo.CarkPark.Engine.Impl
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }

        /// <summary>
        /// Create a host builder to build the service provider
        /// </summary>
        /// <returns></returns>
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddScoped<IRateCalculatorService, RateCalculatorService>();
                    services.Configure<TimeOptions>(context.Configuration.GetSection(TimeOptions.SectionName));
                    services.Configure<PriceOptions>(context.Configuration.GetSection(PriceOptions.SectionName));
                    services.AddTransient<MainForm>();
                });
        }
    }
}