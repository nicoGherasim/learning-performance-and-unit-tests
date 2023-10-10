namespace ProductsApp.Application.Exceptions
{
    public class InputValidationException : Exception
    {
        public InputValidationException(string? message) : base(message)
        { }
    }
}
