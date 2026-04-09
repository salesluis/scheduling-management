namespace scheduling_management.Application.Common;

public sealed record ApiResponse<T>(bool Success, T Result);

