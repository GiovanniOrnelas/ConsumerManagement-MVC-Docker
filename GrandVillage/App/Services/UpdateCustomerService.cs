using GrandVillage.App.Domain.Dto;
using GrandVillage.App.Repositories.SQLServer;

namespace GrandVillage.App.Services
{
    public class UpdateCustomerService : IUpdateCustomerService
    {
        private readonly ISqlServerCustomerRepository _sqlServerCustomerRepository;
        public UpdateCustomerService(ISqlServerCustomerRepository sqlServerCustomerRepository)
        {
            _sqlServerCustomerRepository = sqlServerCustomerRepository;
        }

        public bool Execute(CustomerDto customerDto)
        {
            try
            {
                //Validating if the customer not exist in the BD
                if (!_sqlServerCustomerRepository.Get(customerDto.CustomerId).Success) return false;

                return _sqlServerCustomerRepository.Update(customerDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
