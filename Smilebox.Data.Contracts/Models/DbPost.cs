using System;
using System.Collections.Generic;
using Smilebox.Data.Contracts.Abstractions;

namespace Smilebox.Data.Contracts.Models
{
    public class DbPost : BaseIdEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset PostDate { get; set; }
        public IEnumerable<DbComment> Comments { get; set; } = new List<DbComment>();
    }
}