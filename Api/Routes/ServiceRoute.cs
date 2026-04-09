using scheduling_management.Application.DTOs;
using scheduling_management.Application.Common;
using scheduling_management.Application.UseCases.Service;

namespace scheduling_management.Api.Routes;

public static class ServiceRoute
{
    public static WebApplication MapServiceRoute(this WebApplication app)
    {
        var g = app.MapGroup("/api/establishments/{establishmentId:guid}/services");

        g.MapPost("", async (Guid establishmentId, CreateServiceDto request, IServiceUseCase useCase, CancellationToken ct) =>
        {
            if (request.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("VALIDATION", "EstablishmentId must match route.").ToActionResult();

            var result = await useCase.CreateAsync(request, ct);
            return result.ToActionResult();
        });

        g.MapGet("", async (Guid establishmentId, IServiceUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.GetAllAsync(establishmentId, ct);
            return result.ToActionResult();
        });

        g.MapGet("{id:guid}", async (Guid establishmentId, Guid id, IServiceUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.GetByIdAsync(id, ct);
            if (result.Success && result.Value!.EstablishmentId != establishmentId)
                return Result<ServiceResponseDto>.Fail("NOT_FOUND", "Service not found.", ResultErrorType.NotFound).ToActionResult();
            return result.ToActionResult();
        });

        g.MapPut("{id:guid}", async (Guid establishmentId, Guid id, UpdateServiceDto request, IServiceUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Service not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.UpdateAsync(id, request, ct);
            return result.ToActionResult();
        });

        g.MapDelete("{id:guid}", async (Guid establishmentId, Guid id, IServiceUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Service not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.DeleteAsync(id, ct);
            return result.ToActionResult();
        });

        g.MapPost("{id:guid}/activate", async (Guid establishmentId, Guid id, IServiceUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Service not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.ActivateAsync(id, ct);
            return result.ToActionResult();
        });

        g.MapPost("{id:guid}/deactivate", async (Guid establishmentId, Guid id, IServiceUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Service not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.DeactivateAsync(id, ct);
            return result.ToActionResult();
        });

        return app;
    }
}

