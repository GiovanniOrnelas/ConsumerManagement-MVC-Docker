using GrandVillage.App.Domain.Dto;

namespace GrandVillage.App.Services
{
    public interface IUpdateCustomerService
    {
        bool Execute(CustomerDto customer);
    }
}
