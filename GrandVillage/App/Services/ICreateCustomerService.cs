using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;

namespace GrandVillage.App.Services
{
    public interface ICreateCustomerService
    {
        public Result<object> Execute(CustomerDto customerDto);
    }
}
