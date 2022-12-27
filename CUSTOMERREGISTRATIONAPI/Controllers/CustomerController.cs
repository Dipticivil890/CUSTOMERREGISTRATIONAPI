using CUSTOMERREGISTRATIONAPI.Model;
using CUSTOMERREGISTRATIONAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSTOMERREGISTRATIONAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _IService;

        public CustomerController(ICustomerService IService)
        {
            _IService = IService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetallCustomer()
        {

                return await _IService.GetallCustomerAsync();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerbyId(int id)
        {
            return await _IService.GetCustomerbyIDAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> SaveCustomerDetails(Customer cu)
        {
            try
            {
                 await _IService.InsertCustomerAsync(cu);
            }
            catch(Exception ex)
            {
                var result = _IService.InsertErrorLogsAsync(ex.Message, DateTime.Now, ex.Source, "SaveCustomerDetails", "StckTrace=" + ex.StackTrace);
            }
            return CreatedAtAction("GetallCustomer", cu);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Update(int id,Customer cu)
        {
            try
            {
                await _IService.UpdateCustomerAsync(cu);
            }
            catch (Exception ex)
            {
                var result = _IService.InsertErrorLogsAsync(ex.Message, DateTime.Now, ex.Source, "SaveCustomerDetails", "StckTrace=" + ex.StackTrace);
            }
              return CreatedAtAction("GetallCustomer", new { id = cu.CustomerId }, cu);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(int id)
        {
            var cust = await _IService.GetCustomerbyIDAsync(id);
            if (cust == null)
            {
                return NotFound();
            }
            await _IService.DeleteCustopmerAsync(id);
            return cust;
        }
    }
}
