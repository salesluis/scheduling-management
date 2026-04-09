using scheduling_management.Application.DTOs;
using scheduling_management.Application.Common;
using scheduling_management.Application.UseCases.ProfessionalServiceLink;

namespace scheduling_management.Api.Routes;

public static class ProfessionalServiceRoute
{
    public static WebApplication MapProfessionalServiceRoute(this WebApplication app)
    {
        var g = app.MapGroup("/api/establishments/{establishmentId:guid}/professional-services");

        g.MapPost("", async (Guid establishmentId, CreateProfessionalServiceDto request, IProfessionalLinkUseCase useCase, CancellationToken ct) =>
        {
            if (request.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("VALIDATION", "EstablishmentId must match route.").ToActionResult();

            var result = await useCase.CreateAsync(request, ct);
            return result.ToActionResult();
        });

        g.MapGet("", async (
            Guid establishmentId,
            int page,
            int pageSize,
            Guid? professionalId,
            Guid? serviceId,
            IProfessionalLinkUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.ListAsync(page, pageSize, establishmentId, professionalId, serviceId, ct);
            return result.ToActionResult();
        });

        g.MapGet("search", async (
            Guid establishmentId,
            Guid? professionalId,
            Guid? serviceId,
            IProfessionalLinkUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.SearchAsync(establishmentId, professionalId, serviceId, ct);
            return result.ToActionResult();
        });

        g.MapGet("{id:guid}", async (Guid establishmentId, Guid id, IProfessionalLinkUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.GetByIdAsync(id, ct);
            if (result.Success && result.Value!.EstablishmentId != establishmentId)
                return Result<ResponseProfessionalServiceDto>.Fail("NOT_FOUND", "ProfessionalService link not found.", ResultErrorType.NotFound).ToActionResult();
            return result.ToActionResult();
        });

        g.MapPut("{id:guid}", async (Guid establishmentId, Guid id, UpdateProfessionalServiceDto request, IProfessionalLinkUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "ProfessionalService link not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.UpdateAsync(id, request, ct);
            return result.ToActionResult();
        });

        g.MapDelete("{id:guid}", async (Guid establishmentId, Guid id, IProfessionalLinkUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "ProfessionalService link not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.DeleteAsync(id, ct);
            return result.ToActionResult();
        });

        return app;
    }
}

