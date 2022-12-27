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
    public class StateController : ControllerBase
    {
        private readonly ICustomerService _IService;

        public StateController(ICustomerService IService)
        {
            _IService = IService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetAllState()
        {
            return await _IService.GettallStateAsync();
        }

    }
}
