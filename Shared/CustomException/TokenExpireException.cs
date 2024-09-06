using Auth.Shared.CustomErrors;

namespace Auth.Shared.CustomException
{
    public class TokenExpireException()
    : BaseException(StatusCodes.Status401Unauthorized, "Token Expire", "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4", GlobalErrors.TokenExpireMsg)
    {
    }
}