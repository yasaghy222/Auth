using ErrorOr;

namespace Auth.Shared.CustomErrors;

public static class GlobalErrors
{
    public const string InvalidTokenLogMsg = "request with invalid token {token}";

    public static Error Validation(string action, string code, string description) => Error.Validation($"{action}.{code}.Validation", description);
    public static Error InvalidToken() => Error.Validation(description: "Your token are not valid.");

    public static Error NotFound(string entity, string action, string id) => Error.NotFound($"{entity}.${action}.NotFound", $"the {entity} with this id:'{id}' was not found in the database");
    public static Error NotFound(string entity, string action) => Error.NotFound($"{entity}.${action}.NotFound", $"the {entity} with imputed condition was not found in the database");
}
