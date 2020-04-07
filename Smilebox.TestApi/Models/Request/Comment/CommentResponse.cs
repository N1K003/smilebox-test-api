using System;

namespace Smilebox.TestApi.Models.Request.Comment
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CommentDate { get; set; }
    }
}