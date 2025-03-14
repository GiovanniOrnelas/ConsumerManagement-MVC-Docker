using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;
using GrandVillage.App.Repositories.SQLServer;

namespace GrandVillage.App.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ISqlServerCustomerRepository _sqlServerCustomerRepository;
        public CustomerService(ISqlServerCustomerRepository sqlServerCustomerRepository)
        {
            _sqlServerCustomerRepository = sqlServerCustomerRepository;
        }

        public Result<object> CreateCustomer(CustomerDto customerDto)
        {
            try
            {
                //Validating if the customer exist in the BD
                var fetchResponse = _sqlServerCustomerRepository.GetAsync(null, customerDto.DocumentNumber);
                if (fetchResponse.Success) return new Result<object> { Data = fetchResponse.Data, Message = "Consumidor já existe!", Success = false };

                var createResponse = _sqlServerCustomerRepository.CreateAsync(customerDto);
                return new Result<object> { id = createResponse.Data.ToString(), Message = "Consumidor cadastrado com sucesso!", Success = true };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool DeleteCustomer(string customerId)
        {
            try
            {
                if (!_sqlServerCustomerRepository.GetAsync(customerId).Success) return false;

                return _sqlServerCustomerRepository.DeleteAsync(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<CustomerDto> GetCustomerById(string customerId)
        {
            try
            {
                return _sqlServerCustomerRepository.GetAsync(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public bool UpdateCustomer(CustomerDto customerDto)
        {
            try
            {
                //Validating if the customer not exist in the BD
                if (!_sqlServerCustomerRepository.GetAsync(customerDto.CustomerId).Success) return false;

                return _sqlServerCustomerRepository.UpdateAsync(customerDto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
