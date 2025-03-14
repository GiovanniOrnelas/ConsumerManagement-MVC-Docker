using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;

namespace GrandVillage.App.Services
{
    public interface ICustomerService
    {
        public Result<object> CreateCustomer(CustomerDto customerDto);
        public Result<CustomerDto> GetCustomerById(string customerId);
        public bool DeleteCustomer(string customerId);
        bool UpdateCustomer(CustomerDto customerDto);
    }
}
