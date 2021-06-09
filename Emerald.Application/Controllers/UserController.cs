﻿using Emerald.Application.Models.Response;
using Emerald.Application.Services;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [ApiController]
    [Route("user")]
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
