namespace Smilebox.Common.Exceptions
{
    public class NotFoundException : SmileboxException
    {
        public NotFoundException(string message = default) : base(new[] {message}) { }
    }
}