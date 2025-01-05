using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;
using GrandVillage.App.Repositories.SQLServer;

namespace GrandVillage.App.Services
{
    public class GetCustomerService : IGetCustomerService
    {
        private readonly ISqlServerCustomerRepository _sqlServerCustomerRepository;
        public GetCustomerService(ISqlServerCustomerRepository sqlServerGetCustomerRepository)
        {
            _sqlServerCustomerRepository = sqlServerGetCustomerRepository;
        }
        public Result<CustomerDto> Execute(string customerId)
        {
            try
            {
                return _sqlServerCustomerRepository.Get(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
