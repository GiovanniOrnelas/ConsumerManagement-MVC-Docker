using GrandVillage.App.Repositories.SQLServer;
using GrandVillage.App.Services;
using GrandVillage.Controllers;

namespace GrandVillage.App.Infrastructure.DependencyResolver
{
    public class DependencyResolverConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            #region [ Logging ]
            serviceCollection.AddLogging(configure => configure.AddConsole());
            #endregion

            #region [ Controllers ]
            serviceCollection.AddTransient<CustomerController, CustomerController>();
            #endregion

            #region [ Repositories ]
            serviceCollection.AddTransient<ISqlServerCustomerRepository, SqlServerCustomerRepository>();
            #endregion

            #region [ Services ]
            serviceCollection.AddTransient<ICustomerService, CustomerService>();
            #endregion

            serviceCollection.AddControllers();
        }
    }
}
