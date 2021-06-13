﻿using Emerald.Application.Services;
using Emerald.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Emerald.Application.Controllers
{
    [Route("image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        /// <summary>
        /// Retrive images by imageid
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/{imageId}")]
        public async Task<FileStreamResult> Get(string imageId)
        {
            return new FileStreamResult(
                await imageService.Download(imageId),
                "image/jpeg");
        }

        /// <summary>
        /// Upload a image for testing purposes
        /// </summary>
        /// <returns></returns>
        [HttpPost("test")]
        public async Task<IActionResult> Test(
            IFormFile file,
            [FromServices] ISafeSearchService safeSearchService)
        {
            string imageId = await imageService.Upload(file.OpenReadStream());

            return Ok(new
            {
                ImageId = imageId,
                Safe = await safeSearchService.Detect(imageId)
            });
        }
    }
}