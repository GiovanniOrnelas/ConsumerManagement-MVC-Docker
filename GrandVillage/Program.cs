using GrandVillage.App.Infrastructure.DependencyResolver;
using GrandVillage.App.Services;

namespace Customer.Create
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // Inicia a configuração de injeção
                        DependencyResolverConfigurator.Configure(services);
                    });

                    webBuilder.Configure(app =>
                    {
                        // Configura o pipeline da aplicação
                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                            // Mapeia os controladores
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
