using Emerald.Domain.Models.ComponentAggregate;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IComponentRepository componentRepository;

        public TestController(IComponentRepository componentRepository)
        {
            this.componentRepository = componentRepository;
        }

        [HttpPost("addtextcomponent")]
        public async Task<IActionResult> AddTextComponent(string text)
        {
            await componentRepository.Add(new TextComponent(text));
            return Ok();
        }

        [HttpGet("allcomponents")]
        public async Task<IActionResult> AllComponents()
        {
            return Ok(await componentRepository.GetAll());
        }
    }
}
