namespace Auth.Shared.CustomException
{
    public class NotFoundException(string title, string message)
    : BaseException(StatusCodes.Status404NotFound, title, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4", message)
    {
    }
}