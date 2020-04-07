using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Smilebox.BusinessLogic.Contracts.Models.Common;
using Smilebox.BusinessLogic.Contracts.Models.Post;
using Smilebox.BusinessLogic.Contracts.Services;
using Smilebox.TestApi.Infrastructure;
using Smilebox.TestApi.Models.Request.Common;
using Smilebox.TestApi.Models.Request.Post;
using Smilebox.TestApi.Models.Response;
using Smilebox.TestApi.Models.Response.Post;
using Swashbuckle.AspNetCore.Annotations;

namespace Smilebox.TestApi.Controllers
{
    [ApiController]
    [Produces(Constants.DefaultMimeType)]
    public class PostController : ControllerBase
    {
        private readonly IHtmlSanitizer _htmlSanitizer;
        private readonly IPostService _postService;

        public PostController(IPostService postService, IHtmlSanitizer htmlSanitizer)
        {
            _postService = postService;
            _htmlSanitizer = htmlSanitizer;
        }

        [HttpGet]
        [Route("api/posts")]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(IEnumerable<PostResponse>))]
        public async Task<IActionResult> GetPostsAsync([FromQuery] FilterRequest model)
        {
            var result = await _postService.GetPostsAsync(new FilterModel
            {
                Limit = model.Limit,
                Offset = model.Offset,
                SortDirection = model.SortDirection ?? SortDirection.Asc
            }, HttpContext.RequestAborted);

            return Ok(result.Select(x => x.ToResponse()));
        }

        [HttpGet]
        [Route("api/posts/{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(PostResponse))]
        public async Task<IActionResult> GetPostAsync([FromRoute] ByIdRequest idModel)
        {
            var result = await _postService.GetPostAsync(idModel.Id, HttpContext.RequestAborted);

            return Ok(result.ToResponse());
        }

        [HttpPost]
        [Route("api/posts")]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(PostResponse))]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequest model)
        {
            var text = _htmlSanitizer.Sanitize(model.Text).Replace("\n", string.Empty);
            var result = await _postService.CreatePostAsync(new CreatePostModel
            {
                Title = model.Title,
                Text = text,
                PostDate = model.PostDate
            }, HttpContext.RequestAborted);

            return CreatedAtAction(nameof(GetPostAsync), new ByIdRequest {Id = result.Id}, result);
        }

        [HttpDelete]
        [Route("api/posts/{id}")]
        public async Task<IActionResult> DeletePostAsync([FromRoute] ByIdRequest idModel)
        {
            await _postService.DeletePostAsync(idModel.Id, HttpContext.RequestAborted);

            return Ok();
        }
    }
}