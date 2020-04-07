using System;

namespace Smilebox.BusinessLogic.Contracts.Models.Post
{
    public class CreatePostModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset PostDate { get; set; }
    }
}