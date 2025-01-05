
using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;
using GrandVillage.App.Repositories.SQLServer;
using Microsoft.Data.SqlClient;

namespace GrandVillage.App.Services
{
    public class DeleteCustomerService : IDeleteCustomerService
    {
        private readonly ISqlServerCustomerRepository _sqlServerCustomerRepository;

        public DeleteCustomerService(ISqlServerCustomerRepository sqlServerCustomerRepository)
        {
            _sqlServerCustomerRepository = sqlServerCustomerRepository;
        }
        public bool Execute(string customerId)
        {
            try
            {
                if (!_sqlServerCustomerRepository.Get(customerId).Success) return false;

                return _sqlServerCustomerRepository.Delete(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
