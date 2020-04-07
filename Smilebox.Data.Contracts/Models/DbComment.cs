using System;
using Smilebox.Data.Contracts.Abstractions;

namespace Smilebox.Data.Contracts.Models
{
    public class DbComment : BaseIdEntity
    {
        public int PostId { get; set; }
        public DbPost Post { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CommentDate { get; set; }
    }
}