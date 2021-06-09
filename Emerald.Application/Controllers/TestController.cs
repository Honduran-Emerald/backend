using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Emerald.Application.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IQuestPrototypeRepository questPrototypeRepository;
        private IQuestRepository questRepository;
        private UserManager<User> userManager;

        public TestController(IQuestPrototypeRepository questPrototypeRepository, IQuestRepository questRepository, UserManager<User> userManager)
        {
            this.questPrototypeRepository = questPrototypeRepository;
            this.questRepository = questRepository;
            this.userManager = userManager;
        }
    }
}
