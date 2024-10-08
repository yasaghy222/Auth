﻿using ErrorOr;

namespace Auth.Shared.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails<T>(this ErrorOr<T> result)
    {
        if (!result.IsError)
        {
            throw new InvalidOperationException("Can't convert success result to problem");
        }

        return Results.Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: "Bad Request",
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            extensions: new Dictionary<string, object?>
            {
                { "errors", result.Errors }
            });
    }

    public static IResult ToResult(this Error error)
    {
        return error.Type switch
        {
            ErrorType.Failure => Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: error.Code,
                detail: error.Description,
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                extensions: new Dictionary<string, object?>
                {
                    { "errors", error.Metadata }
                }),
            ErrorType.Unexpected => Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: error.Code,
                detail: error.Description,
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                extensions: new Dictionary<string, object?>
                {
                    { "errors", error.Metadata }
                }),
            ErrorType.Validation => Results.BadRequest(error),
            ErrorType.Conflict => Results.Conflict(error),
            ErrorType.NotFound => Results.NotFound(error.Metadata),
            ErrorType.Unauthorized => Results.Unauthorized(),
            ErrorType.Forbidden => Results.Forbid(),
            _ => Results.Empty,
        };
    }

    public static IResult ToResult(this List<Error> errors)
    {
        return Results.BadRequest(errors);
    }
}