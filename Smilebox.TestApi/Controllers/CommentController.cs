using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Smilebox.BusinessLogic.Contracts.Models.Comment;
using Smilebox.BusinessLogic.Contracts.Services;
using Smilebox.TestApi.Infrastructure;
using Smilebox.TestApi.Models.Request.Comment;
using Smilebox.TestApi.Models.Request.Common;

namespace Smilebox.TestApi.Controllers
{
    [ApiController]
    [Produces(Constants.DefaultMimeType)]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public CommentController(ICommentService commentService, IHtmlSanitizer htmlSanitizer)
        {
            _commentService = commentService;
            _htmlSanitizer = htmlSanitizer;
        }

        [HttpPost]
        [Route("api/posts/{id}/comment")]
        public async Task<IActionResult> CreateCommentAsync([FromRoute] ByIdRequest idModel, [FromBody] CreateCommentRequest bodyModel)
        {
            var text = _htmlSanitizer.Sanitize(bodyModel.Text).Replace("\n", string.Empty);
            await _commentService.CreateCommentAsync(new CreateCommentModel
            {
                PostId = idModel.Id,
                Text = text,
                CommentDate = bodyModel.CommentDate
            }, HttpContext.RequestAborted);

            return Ok();
        }
    }
}