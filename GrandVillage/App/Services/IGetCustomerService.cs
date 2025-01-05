using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;

namespace GrandVillage.App.Services
{
    public interface IGetCustomerService
    {
       Result<CustomerDto> Execute(string documentNumber);
    }
}
