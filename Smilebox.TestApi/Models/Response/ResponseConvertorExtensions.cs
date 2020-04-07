using System;
using System.Linq;
using Smilebox.BusinessLogic.Contracts.Models.Comment;
using Smilebox.BusinessLogic.Contracts.Models.Post;
using Smilebox.Common.Exceptions;
using Smilebox.TestApi.Models.Request.Comment;
using Smilebox.TestApi.Models.Response.Common;
using Smilebox.TestApi.Models.Response.Post;

namespace Smilebox.TestApi.Models.Response
{
    public static class ResponseConvertorExtensions
    {
        public static ExceptionResponse ToResponse(this Exception model)
        {
            if (model is SmileboxException smileboxException)
            {
                return new ExceptionResponse {Errors = smileboxException.Errors};
            }

            return new ExceptionResponse {Errors = new[] {model.Message}};
        }

        public static PostResponse ToResponse(this PostModel model)
        {
            return new PostResponse
            {
                Id = model.Id,
                Text = model.Text,
                Title = model.Title,
                PostDate = model.PostDate,
                Comments = model.Comments.Select(ToResponse).OrderByDescending(x => x.CommentDate)
            };
        }

        public static CommentResponse ToResponse(this CommentModel model)
        {
            return new CommentResponse
            {
                Id = model.Id,
                Text = model.Text,
                CommentDate = model.CommentDate
            };
        }
    }
}