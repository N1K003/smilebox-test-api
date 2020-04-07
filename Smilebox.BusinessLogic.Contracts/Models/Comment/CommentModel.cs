using System;

namespace Smilebox.BusinessLogic.Contracts.Models.Comment
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CommentDate { get; set; }
    }
}