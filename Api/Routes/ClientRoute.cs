using scheduling_management.Application.DTOs;
using scheduling_management.Application.UseCases.Client;
using scheduling_management.Application.Common;

namespace scheduling_management.Api.Routes;

public static class ClientRoute
{
    public static WebApplication MapClientRoute(this WebApplication app)
    {
        var g = app.MapGroup("/api/establishments/{establishmentId:guid}/clients");
        
        g.MapGet("{id:guid}", async (Guid establishmentId, Guid id, IClientUseCase clientUseCase, CancellationToken ct) =>
        {
            var result = await clientUseCase.GetByIdAsync(id, ct);
            if (result.Success && result.Value!.EstablishmentId != establishmentId)
                return Result<ClienResponsetDto>.Fail("NOT_FOUND", "Client not found.", ResultErrorType.NotFound).ToActionResult();

            return result.ToActionResult();
        });
        
        g.MapGet("", async (Guid establishmentId, IClientUseCase clientUseCase, CancellationToken ct) =>
        {
            var result = await clientUseCase.GetAllAsync(establishmentId, ct);
            return result.ToActionResult();
        });

        g.MapPost("", async (Guid establishmentId, CreateClientDto request, IClientUseCase clientUseCase, CancellationToken ct) =>
        {
            if (request.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("VALIDATION", "EstablishmentId must match route.").ToActionResult();

            var result = await clientUseCase.CreateAsync(request, ct);
            return result.ToActionResult();
        });

        g.MapPut("{id:guid}", async (Guid establishmentId, Guid id, UpdateClientDto request, IClientUseCase clientUseCase, CancellationToken ct) =>
        {
            var existing = await clientUseCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Client not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await clientUseCase.UpdateAsync(id, request, ct);
            return result.ToActionResult();
        });

        g.MapDelete("{id:guid}", async (Guid establishmentId, Guid id, IClientUseCase clientUseCase, CancellationToken ct) =>
        {
            var existing = await clientUseCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Client not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await clientUseCase.DeleteAsync(id, ct);
            return result.ToActionResult();
        });
        
        return app;
    }
}