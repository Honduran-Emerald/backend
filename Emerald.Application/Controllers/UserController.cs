using Emerald.Application.Models.Response;
using Emerald.Application.Services;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.Services;
using Google.Cloud.Vision.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private UserModelFactory userModelFactory;
        private UserManager<User> userManager;
        private IUserRepository userRepository;
        private IUserService userService;

        public UserController(UserModelFactory userModelFactory, UserManager<User> userManager, IUserRepository userRepository, IUserService userService)
        {
            this.userModelFactory = userModelFactory;
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.userService = userService;
        }

        /// <summary>
        /// Set FCM cloud messaging token
        /// </summary>
        /// <param name="messagingToken"></param>
        /// <returns></returns>
        [HttpPost("updatemessagingtoken")]
        public async Task<IActionResult> UpdateMessagingToken(
            [FromBody] string messagingToken,
            [FromServices] IUserRepository userRepository,
            [FromServices] IUserService userService)
        {
            User? user = await userRepository.GetQueryable()
                .Where(u => u.MessagingToken == messagingToken)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                user.MessagingToken = null;
                await userRepository.Update(user);
            }

            user = await userService.CurrentUser();
            user.MessagingToken = messagingToken;
            await userRepository.Update(user);

            return Ok();
        }

        /// <summary>
        /// Change profile picture of authorized user
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost("updateimage")]
        public async Task<IActionResult> UpdateProfileImage(
            [FromBody] string binaryImage,
            [FromServices] IImageService imageService,
            [FromServices] ISafeSearchService safeSearchService,
            [FromServices] IUserService userService)
        {
            byte[] binary = Convert.FromBase64String(binaryImage);

            if (await safeSearchService.Detect(
                    await Image.FromStreamAsync(new MemoryStream(binary))))
            {
                return BadRequest(new
                {
                    Message = "Bad Image"
                });
            }

            User user = await userService.CurrentUser();
            user.ImageId = await imageService.Upload(new MemoryStream(binary));

            return Ok(new UserUpdateImageResponse(user.ImageId));
        }

        /// <summary>
        /// Get profile information about current authorized user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserMeResponse>> Me()
        {
            return Ok(new UserMeResponse(
                await userModelFactory.Create(
                    await userService.CurrentUser())));
        }

        /// <summary>
        /// Get profile information about another user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("get")]
        public async Task<ActionResult<UserMeResponse>> Get(
            [FromQuery] ObjectId userId)
        {
            return Ok(new UserMeResponse(
                await userModelFactory.Create(await userRepository.Get(userId))));
        }
    }
}
