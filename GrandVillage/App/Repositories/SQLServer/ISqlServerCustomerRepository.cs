using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;

namespace GrandVillage.App.Repositories.SQLServer
{
    public interface ISqlServerCustomerRepository
    {
        public Result<object> Create(CustomerDto customerDto);
        public Result<CustomerDto> Get(string? customerId = null, string? documentNumber = null);
        public bool Update(CustomerDto customerDto);

        public bool Delete(string customerId);
    }
}
