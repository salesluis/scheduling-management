using Microsoft.AspNetCore.Http;
using scheduling_management.Application.Common;

namespace scheduling_management.Api.Responses;

public static class ApiResults
{
    private sealed record ErrorPayload(IReadOnlyList<ResultError> Errors);

    public static IResult Ok<T>(T data) => Results.Ok(new ApiResponse<T>(true, data));

    public static IResult Ok() => Results.Ok(new ApiResponse<object?>(true, null));

    public static IResult NotFound(string message, string code = "NOT_FOUND")
        => Results.Json(
            new ApiResponse<ErrorPayload>(false, new ErrorPayload(new[] { new ResultError(code, message, ResultErrorType.NotFound) })),
            statusCode: StatusCodes.Status404NotFound);

    public static IResult BadRequest(string message, string code = "VALIDATION")
        => Results.Json(
            new ApiResponse<ErrorPayload>(false, new ErrorPayload(new[] { new ResultError(code, message, ResultErrorType.Validation) })),
            statusCode: StatusCodes.Status400BadRequest);

    public static IResult Conflict(string message, string code = "CONFLICT")
        => Results.Json(
            new ApiResponse<ErrorPayload>(false, new ErrorPayload(new[] { new ResultError(code, message, ResultErrorType.Conflict) })),
            statusCode: StatusCodes.Status409Conflict);
}

