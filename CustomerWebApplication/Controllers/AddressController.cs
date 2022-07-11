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
    public class AddressController : ControllerBase
    {
        private readonly DBCustomerContext _context;

        public AddressController(DBCustomerContext context)
        {
            this._context = context;
        }
        // GET: api/<AddressController>
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAsync(int id)
        {
            genericJsonResponse response = new();
            List<Address> addresses = new();
            try
            {
                addresses = await _context.Addresses.Where(x=>x.).ToListAsync();
                response.success = true;
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            finally
            {
                response.data = addresses;
            }
            return response;
        }

        // POST api/<AddressController>
        [HttpPost]
        public async Task<ActionResult<object>> PostAsync([FromBody] genericJsonRequest request)
        {
            genericJsonResponse response = new();
            try
            {
                Address address;
                Address addressParsed = JSON.Parse<Address>(request.stringify);
                response.success = true;
                switch (request.operation)
                {
                    case CONSTANT.CREATE:
                        await _context.Addresses.AddAsync(addressParsed);
                        response.message = "created: done";
                        break;

                    case CONSTANT.EDIT:
                        address = await _context.Addresses.FindAsync(addressParsed.Id);
                        address.Address0 = addressParsed.Address0;
                        address.Type0 = addressParsed.Type0;
                        _context.Entry(address).State = EntityState.Modified;
                        response.message = "edited: done";
                        break;

                    case CONSTANT.DELETE:
                        address = await _context.Addresses.FindAsync(addressParsed.Id);
                        _context.Entry(address).State = EntityState.Deleted;
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
