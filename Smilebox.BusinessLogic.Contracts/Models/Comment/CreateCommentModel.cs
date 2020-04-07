using System;

namespace Smilebox.BusinessLogic.Contracts.Models.Comment
{
    public class CreateCommentModel
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CommentDate { get; set; }
    }
}