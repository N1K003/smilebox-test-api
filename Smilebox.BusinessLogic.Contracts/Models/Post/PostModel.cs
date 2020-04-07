using System;
using System.Collections.Generic;
using Smilebox.BusinessLogic.Contracts.Models.Comment;

namespace Smilebox.BusinessLogic.Contracts.Models.Post
{
    public class PostModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }

        public DateTimeOffset PostDate { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
    }
}