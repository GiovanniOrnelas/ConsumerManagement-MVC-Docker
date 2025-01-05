using GrandVillage.App.Domain.Dto;
using GrandVillage.App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GrandVillage.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICreateCustomerService _createCustomerService;
        private readonly IConfiguration _configuration;
        private readonly IGetCustomerService _getCustomerService;
        private readonly IUpdateCustomerService _updateCustomerService;
        private readonly IDeleteCustomerService _deleteCustomerService;

        public CustomerController(ILogger<CustomerController> logger, IConfiguration configuration, ICreateCustomerService createCustomerService, IGetCustomerService getCustomerService, IUpdateCustomerService updateCustomerService,IDeleteCustomerService deleteCustomerService)
        {
            _logger = logger;
            _configuration = configuration;
            _createCustomerService = createCustomerService;
            _getCustomerService = getCustomerService;
            _updateCustomerService = updateCustomerService;
            _deleteCustomerService = deleteCustomerService;
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerDto customerDto, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            try
            {
                if (authorizationHeader == _configuration["API_AUTHORIZATION"])
                {
                    if (customerDto != null)
                    {
                        customerDto.DocumentNumber = Regex.Replace(customerDto.DocumentNumber, @"\D", "");
                        var response = _createCustomerService.Execute(customerDto);
                        if (!response.Success)
                            return BadRequest(new { customerInfo = response.Data, message = response.Message, success = response.Success });
                        else
                            return CreatedAtAction("CreateCustomer", new { id = response.CustomerId }, new { customerId = response.CustomerId, message = response.Message, success = response.Success });
                    }
                    else
                        return BadRequest(new { error = "Fields DocumentNumber or Name are empty" });
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetCustomer([FromQuery] string customerId, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            _logger.LogInformation("teste de log");
            try
            {
                if (authorizationHeader == _configuration["API_AUTHORIZATION"])
                {
                    if (customerId != null)
                    {
                        var response = _getCustomerService.Execute(customerId);
                        if (!response.Success) return NotFound(new { message = "Customer Not Exist.", success = response.Success });
                        else return Ok(new { customerInfo = response.Data, success = response.Success });
                    }
                    else
                        return BadRequest(new { error = "Parameter customerId is required." });
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] CustomerDto customer, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            try
            {
                if (authorizationHeader == _configuration["API_AUTHORIZATION"])
                {
                    if (customer != null)
                    {
                        var response = _updateCustomerService.Execute(customer);
                        if (!response) return NotFound(new { message = "Customer not exist.", success = response });
                        else return Ok(new { message = "Customer updated successfully", success = response });
                    }
                    else
                        return BadRequest(new { error = "Invalid body." });
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomer([FromRoute] string customerId, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            try
            {
                if (authorizationHeader == _configuration["API_AUTHORIZATION"])
                {
                    if (customerId != null)
                    {
                        var response = _deleteCustomerService.Execute(customerId);
                        if (!response) return NotFound(new { message = "Customer not exist.", success = response });
                        else return Ok(new { message = "Customer deleted successfully", success = response });
                    }
                    else
                        return BadRequest(new { error = "CustomerId is required in the route" });
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
