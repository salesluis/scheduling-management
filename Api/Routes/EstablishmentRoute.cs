using scheduling_management.Application.Common;
using scheduling_management.Application.UseCases.Establishment;
using scheduling_management.Application.DTOs;

namespace scheduling_management.Api.Routes;

public static class EstablishmentRoute
{
    public static WebApplication MapEstablishmentRoute(this WebApplication app)
    {
        var g = app.MapGroup("/api/establishments");

        g.MapPost("", async (CreateEstablishmentDto request, IEstablishmentUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.CreateAsync(request, ct);
            return result.ToActionResult();
        });

        g.MapGet("", async (IEstablishmentUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.GetAllAsync(Guid.Empty, ct);
            return result.ToActionResult();
        });

        g.MapGet("{id:guid}", async (Guid id, IEstablishmentUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.GetByIdAsync(id, ct);
            return result.ToActionResult();
        });

        g.MapPut("{id:guid}", async (Guid id, UpdateEstablishmentDto request, IEstablishmentUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.UpdateAsync(id, request, ct);
            return result.ToActionResult();
        });

        g.MapDelete("{id:guid}", async (Guid id, IEstablishmentUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.DeleteAsync(id, ct);
            return result.ToActionResult();
        });

        g.MapPost("{id:guid}/activate", async (Guid id, IEstablishmentUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.ActivateAsync(id, ct);
            return result.ToActionResult();
        });

        g.MapPost("{id:guid}/deactivate", async (Guid id, IEstablishmentUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.DeactivateAsync(id, ct);
            return result.ToActionResult();
        });

        return app;
    }
}

