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
    public class DistrictController : ControllerBase
    {
        private readonly ICustomerService _IService;

        public DistrictController(ICustomerService IService)
        {
            _IService = IService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<District>>> GetAllDist(int  id)
        {
            return await _IService.GetDistbyIdAsync(id);
        }

    }
}
