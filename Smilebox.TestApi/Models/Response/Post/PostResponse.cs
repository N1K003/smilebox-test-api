using System;
using System.Collections.Generic;
using Smilebox.TestApi.Models.Request.Comment;

namespace Smilebox.TestApi.Models.Response.Post
{
    public class PostResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }

        public DateTimeOffset PostDate { get; set; }
        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}