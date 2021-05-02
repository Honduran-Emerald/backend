using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [Route("component")]
    [ApiController]
    public class ComponentController : ControllerBase
    {

        [HttpPost("addimage")]
        public async Task<IActionResult> AddImageComponent(
            IFormFile image)
        {
            return Ok(image.FileName);
        }
    }
}
