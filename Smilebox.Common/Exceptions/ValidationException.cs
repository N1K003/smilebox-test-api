using System.Collections.Generic;

namespace Smilebox.Common.Exceptions
{
    public class ValidationException : SmileboxException
    {
        public ValidationException(string message) : base(new[] {message}) { }
        public ValidationException(IEnumerable<string> messages) : base(messages) { }
    }
}