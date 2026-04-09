using Microsoft.AspNetCore.Http;

namespace scheduling_management.Application.Common;

public static class ResultExtensions
{
    private sealed record ErrorPayload(IReadOnlyList<ResultError> Errors);

    public static IResult ToActionResult<T>(this Result<T> result)
    {
        if (result.Success)
        {
            return Results.Ok(new ApiResponse<T>(true, result.Value!));
        }

        var errors = result.Errors;
        var payload = new ApiResponse<ErrorPayload>(false, new ErrorPayload(errors));

        // Prefer the "most specific" status if mixed.
        if (errors.Any(e => e.Type == ResultErrorType.Unauthorized))
            return Results.Json(payload, statusCode: StatusCodes.Status401Unauthorized);

        if (errors.Any(e => e.Type == ResultErrorType.Forbidden))
            return Results.Json(payload, statusCode: StatusCodes.Status403Forbidden);

        if (errors.Any(e => e.Type == ResultErrorType.NotFound))
            return Results.Json(payload, statusCode: StatusCodes.Status404NotFound);

        if (errors.Any(e => e.Type == ResultErrorType.Conflict))
            return Results.Json(payload, statusCode: StatusCodes.Status409Conflict);

        // Validation / default
        return Results.Json(payload, statusCode: StatusCodes.Status400BadRequest);
    }
}

