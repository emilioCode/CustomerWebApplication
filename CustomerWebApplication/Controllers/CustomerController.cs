using CustomerWebApplication.Models;
using CustomerWebApplication.Models.Data;
using CustomerWebApplication.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DBCustomerContext _context;

        public CustomerController(DBCustomerContext context)
        {
            this._context = context;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<object>> GetAsync()
        {
            genericJsonResponse response = new();
            List<Customer> customers = new();
            try
            {
                customers = await _context.Customers.ToListAsync();
                response.success = true;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            finally
            {
                response.data = customers;
            }
            return response;
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<ActionResult<object>> PostAsync([FromBody] genericJsonRequest request)
        {
            genericJsonResponse response = new();
            try
            {
                Customer customer;
                Customer customerParsed = JSON.Parse<Customer>(request.stringify);
                response.success = true;
                switch (request.operation)
                {
                    case CONSTANT.CREATE:
                        await _context.Customers.AddAsync(customerParsed);
                        response.message = "created: done";
                        break;

                    case CONSTANT.EDIT:
                        customer = await _context.Customers.FindAsync(customerParsed.Id);
                        customer.Name = customerParsed.Name;
                        customer.Gender = customerParsed.Gender;
                        customer.Phone = customerParsed.Phone;
                        _context.Entry(customer).State = EntityState.Modified;
                        response.message = "edited: done";
                        break;

                    case CONSTANT.DELETE:
                        customer = await _context.Customers.FindAsync(customerParsed.Id);
                        _context.Entry(customer).State = EntityState.Deleted;
                        response.message = "deleted: done";
                        break;

                    default:
                        response.success = false;
                        break;
                }
                if (response.success) await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }


    }
}
