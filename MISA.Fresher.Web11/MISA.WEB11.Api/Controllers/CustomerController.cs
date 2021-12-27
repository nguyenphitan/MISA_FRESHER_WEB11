using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB11.Core.Entities;
using MISA.WEB11.Core.Exceptions;
using MISA.WEB11.Core.Interface.Infrastructure;
using MISA.WEB11.Core.Interface.Service;

namespace MISA.WEB11.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService _customerService;
        ICustomerRepository _customerRepository;
        public CustomerController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customers = _customerRepository.GetCustomer();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vôi long liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }
        }

        [HttpPost]
        public IActionResult Insert(Customer customer)
        {
            try
            {
                var res = _customerService.InsertService(customer);
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch(MISAValidateException ex)
            {
                return StatusCode(500, ex.Data);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vôi long liên hệ MISA để được trợ giúp",
                    data = ""
                };
                return StatusCode(500, result);
            }
        }
    }
}
