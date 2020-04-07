using System.Collections.Generic;

namespace Smilebox.TestApi.Models.Response.Common
{
    public class ExceptionResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}