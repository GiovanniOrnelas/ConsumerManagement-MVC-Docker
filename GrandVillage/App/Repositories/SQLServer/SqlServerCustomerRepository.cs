using GrandVillage.App.Common.Models;
using GrandVillage.App.Domain.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace GrandVillage.App.Repositories.SQLServer
{
    public class SqlServerCustomerRepository : ISqlServerCustomerRepository
    {
        private readonly ILogger<SqlServerCustomerRepository> _logger;
        private readonly IConfiguration _configuration;

        public SqlServerCustomerRepository(ILogger<SqlServerCustomerRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_configuration["CONNECTION_STRING"]);
            connection.Open();
            return connection;
        }

        public Result<object> CreateAsync(CustomerDto customerDto)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new SqlCommand($@" INSERT INTO {_configuration["DBO_NAME"]} (DocumentNumber, Name) VALUES (@DocumentNumber, @Name); SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                    {
                        command.Parameters.AddWithValue("@DocumentNumber", customerDto.DocumentNumber);
                        command.Parameters.AddWithValue("@Name", customerDto.Name);
                        int generatedId = (int)command.ExecuteScalar();

                        return new Result<object> { Data = generatedId, Success = true };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar cliente no banco de dados");
                throw new Exception(ex.Message);    
            }
        }

        public Result<CustomerDto> GetAsync(string? customerId = null, string? documentNumber = null)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    CustomerDto customer = null;
                    using (var command = new SqlCommand($"SELECT * FROM {_configuration["DBO_NAME"]} WHERE Id = '{customerId}' OR DocumentNumber = '{documentNumber}'", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer = new CustomerDto
                                {
                                    CustomerId = reader["Id"].ToString(),
                                    DocumentNumber = reader["DocumentNumber"].ToString(),
                                    Name = reader["Name"].ToString()
                                };
                            }
                        }
                    }

                    if (customer != null) return new Result<CustomerDto> { Success = true, Data = customer };

                    else return new Result<CustomerDto> { Success = false };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateAsync(CustomerDto customerDto)
        {
            bool result = false;
            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new SqlCommand($"UPDATE {_configuration["DBO_NAME"]} SET Name = '{customerDto.Name}' WHERE Id = {customerDto.CustomerId}", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    result = true;
                }
                    return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteAsync(string customerId)
        {
            bool result = false;
            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new SqlCommand($"DELETE FROM {_configuration["DBO_NAME"]} WHERE Id = {customerId}", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    result = true;                   
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}