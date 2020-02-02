using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Socialite.Domain.AggregateModels.PostAggregate;
using Socialite.WebAPI.Application.Commands.Posts;
using Socialite.WebAPI.Application.Enums;
using Socialite.WebAPI.Queries.Posts;

namespace Socialite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPostQueries _postQueries;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public PostsController(IMediator mediator, IPostQueries postQueries, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _postQueries = postQueries;
            // _claimsPrincipal = httpContextAccessor.HttpContext.User;
        }

        // GET: api/person/:personGuid/posts
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postQueries.FindAllAsync();

            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            try
            {
                var post = await _postQueries.FindAsync(id);

                return Ok(post);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(command);

                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(command);
        }

        [HttpPut("{id}/publish")]
        public async Task<IActionResult> PublishPost(Guid id)
        {
            var command = new PublishPostCommand(id);

            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok();
            }

            return BadRequest(command);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var command = new DeletePostCommand(id);

            var result = await _mediator.Send(command);

            return result switch
            {
                DeleteCommandResult.Success => Ok(),
                DeleteCommandResult.NotFound => NotFound(),
                _ => BadRequest(),
            };
        }
    }
}
