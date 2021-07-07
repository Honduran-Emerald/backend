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
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            await userRepository.Update(user);

            return Ok(new UserUpdateImageResponse(user.ImageId));
        }

        /// <summary>
        /// Get profile information about current authorized user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserSingleResponse>> Me()
        {
            return Ok(new UserSingleResponse(
                await userModelFactory.Create(
                    await userService.CurrentUser())));
        }

        /// <summary>
        /// Get profile information about another user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("get")]
        public async Task<ActionResult<UserSingleResponse>> Get(
            [FromQuery] ObjectId userId)
        {
            return Ok(new UserSingleResponse(
                await userModelFactory.Create(await userRepository.Get(userId))));
        }

        /// <summary>
        /// Get all users followed by the authorized user
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("followers")]
        public async Task<ActionResult<UserMultipleResponse>> Followers(
            [FromQuery] int offset,
            [FromServices] IConfiguration configuration,
            [FromServices] IUserService userService,
            [FromServices] IUserRepository userRepository,
            [FromServices] UserModelFactory userModelFactory)
        {
            var response = new UserMultipleResponse(new List<Models.UserModel>());
            var user = await userService.CurrentUser();

            foreach (ObjectId follower in user.Followers
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize")))
            {
                response.Users.Add(
                    await userModelFactory.Create(await userRepository.Get(follower)));
            }

            return Ok(response);
        }

        /// <summary>
        /// Get all users following the authorized user
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("following")]
        public async Task<ActionResult<UserMultipleResponse>> Following(
            [FromQuery] int offset,
            [FromServices] IConfiguration configuration,
            [FromServices] IUserService userService,
            [FromServices] IUserRepository userRepository,
            [FromServices] UserModelFactory userModelFactory)
        {
            var response = new UserMultipleResponse(new List<Models.UserModel>());
            var user = await userService.CurrentUser();

            foreach (ObjectId following in user.Following
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize")))
            {
                response.Users.Add(
                    await userModelFactory.Create(await userRepository.Get(following)));
            }

            return Ok(response);
        }

        /// <summary>
        /// Follow or unfollow a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("togglefollow")]
        public async Task<IActionResult> ToggleFollow(
            [FromBody] ObjectId userId,
            [FromServices] IUserService userService,
            [FromServices] IUserRepository userRepository)
        {
            var user = await userService.CurrentUser();
            var following = await userRepository.Get(userId);

            if (user.Following.Contains(userId))
            {
                user.Unfollow(following);
            }
            else
            {
                user.Follow(following);
            }

            await userRepository.Update(user);
            await userRepository.Update(following);

            return Ok();
        }

        /// <summary>
        /// All followers from the authorized user, that the authorized follower is following back
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("friends")]
        public async Task<ActionResult<UserMultipleResponse>> Friends(
            [FromQuery] int offset,
            [FromServices] IConfiguration configuration,
            [FromServices] IUserService userService,
            [FromServices] IUserRepository userRepository,
            [FromServices] UserModelFactory userModelFactory)
        {
            var response = new UserMultipleResponse(new List<Models.UserModel>());
            var user = await userService.CurrentUser();

            foreach (ObjectId friend in user.Following
                .Where(u => user.Followers.Contains(u))
                .Skip(offset)
                .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize")))
            {
                response.Users.Add(
                    await userModelFactory.Create(await userRepository.Get(friend)));
            }

            return Ok(response);
        }

        /// <summary>
        /// Query for users optionally by search string
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public async Task<ActionResult<UserMultipleResponse>> Query(
            [FromQuery] int offset,
            [FromQuery] string? search,
            [FromServices] IConfiguration configuration,
            [FromServices] IUserService userService,
            [FromServices] IUserRepository userRepository,
            [FromServices] UserModelFactory userModelFactory)
        {
            var usersQuery = userRepository.GetQueryable();

            if (search != null)
            {
                usersQuery.Where(u => u.UserName.Contains(search));
            }

            return Ok(new UserMultipleResponse(
                await userModelFactory.Create(usersQuery
                    .Skip(offset)
                    .Take(configuration.GetValue<int>("Emerald:MediumResponsePackSize"))
                    .ToList())));
        }
    }
}
