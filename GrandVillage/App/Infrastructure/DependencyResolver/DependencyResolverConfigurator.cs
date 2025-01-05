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
            serviceCollection.AddTransient<ICreateCustomerService, CreateCustomerService>();
            serviceCollection.AddTransient<IGetCustomerService, GetCustomerService>();
            serviceCollection.AddTransient<IUpdateCustomerService, UpdateCustomerService>();
            serviceCollection.AddTransient<IDeleteCustomerService, DeleteCustomerService>();
            #endregion

            serviceCollection.AddControllers();
        }
    }
}
