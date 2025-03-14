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
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;


        public CustomerController(ILogger<CustomerController> logger, IConfiguration configuration, ICustomerService customerService)
        {
            _logger = logger;
            _configuration = configuration;
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerDto customerDto, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            try
            {
                if (authorizationHeader == _configuration["API_AUTHORIZATION"])
                {
                    if (customerDto != null)
                    {
                        customerDto.DocumentNumber = Regex.Replace(customerDto.DocumentNumber, @"\D", "");
                        var response = _customerService.CreateCustomer(customerDto);
                        if (!response.Success)
                            return BadRequest(new { data = response.Data, message = response.Message, success = response.Success });
                        else
                            return CreatedAtAction("Create", null, new { id = response.id, message = response.Message, success = response.Success });
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
        public IActionResult Get([FromQuery] string customerId, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            try
            {
                if (authorizationHeader == _configuration["API_AUTHORIZATION"])
                {
                    if (customerId != null)
                    {
                        var response = _customerService.GetCustomerById(customerId);
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
        public IActionResult Update([FromBody] CustomerDto customer, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            try
            {
                if (authorizationHeader == _configuration["API_AUTHORIZATION"])
                {
                    if (customer != null)
                    {
                        var response = _customerService.UpdateCustomer(customer);
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
        public IActionResult Delete([FromRoute] string customerId, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            try
            {
                if (authorizationHeader == _configuration["API_AUTHORIZATION"])
                {
                    if (customerId != null)
                    {
                        var response = _customerService.DeleteCustomer(customerId);
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
