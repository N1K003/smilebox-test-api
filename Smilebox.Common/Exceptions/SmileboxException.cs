using System;
using System.Collections.Generic;

namespace Smilebox.Common.Exceptions
{
    public class SmileboxException : Exception
    {
        public SmileboxException(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}