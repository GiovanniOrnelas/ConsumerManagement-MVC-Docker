using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;

namespace GrandVillage.App.Repositories.SQLServer
{
    public interface ISqlServerCustomerRepository
    {
        public Result<object> CreateAsync(CustomerDto customerDto);
        public Result<CustomerDto> GetAsync(string? customerId = null, string? documentNumber = null);
        public bool UpdateAsync(CustomerDto customerDto);

        public bool DeleteAsync(string customerId);
    }
}
