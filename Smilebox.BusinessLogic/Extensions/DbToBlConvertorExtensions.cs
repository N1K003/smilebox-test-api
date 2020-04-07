using System.Linq;
using Smilebox.BusinessLogic.Contracts.Models.Comment;
using Smilebox.BusinessLogic.Contracts.Models.Post;
using Smilebox.Data.Contracts.Models;

namespace Smilebox.BusinessLogic.Extensions
{
    internal static class DbToBlConvertorExtensions
    {
        public static PostModel ToBlModel(this DbPost model)
        {
            return new PostModel
            {
                Id = model.Id,
                Title = model.Title,
                Text = model.Text,
                PostDate = model.PostDate,
                Comments = model.Comments.Select(ToBlModel).ToList()
            };
        }

        public static CommentModel ToBlModel(this DbComment model)
        {
            return new CommentModel
            {
                Id = model.Id,
                PostId = model.PostId,
                Text = model.Text,
                CommentDate = model.CommentDate
            };
        }
    }
}