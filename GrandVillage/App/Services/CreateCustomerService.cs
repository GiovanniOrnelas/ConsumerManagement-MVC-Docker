using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;
using GrandVillage.App.Repositories.SQLServer;

namespace GrandVillage.App.Services
{
    public class CreateCustomerService : ICreateCustomerService
    {
        private readonly ISqlServerCustomerRepository _sqlServerCustomerRepository;
        public CreateCustomerService(ISqlServerCustomerRepository sqlServerRepository)
        {
            _sqlServerCustomerRepository = sqlServerRepository;
        }

        public Result<object> Execute(CustomerDto customerDto)
        {
            try
            {
                //Validating if the customer exist in the BD
                var fetchResponse = _sqlServerCustomerRepository.Get(null, customerDto.DocumentNumber);
                if (fetchResponse.Success) return new Result<object> { Data = fetchResponse.Data, Message = "Consumidor já existe!", Success = false };

                var createResponse = _sqlServerCustomerRepository.Create(customerDto);
                return new Result<object> { CustomerId = createResponse.Data.ToString(), Message = "Consumidor cadastrado com sucesso!", Success = true };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
