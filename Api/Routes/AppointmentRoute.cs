using System.Linq;
using scheduling_management.Application.DTOs;
using scheduling_management.Application.Common;
using scheduling_management.Application.UseCases.Appointment;

namespace scheduling_management.Api.Routes;

public static class AppointmentRoute
{
    public static WebApplication MapAppointmentRoute(this WebApplication app)
    {
        var g = app.MapGroup("/api/establishments/{establishmentId:guid}/appointments");

        g.MapPost("", async (Guid establishmentId, CreateAppointmentDto request, IAppointmentUseCase useCase, CancellationToken ct) =>
        {
            if (request.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("VALIDATION", "EstablishmentId must match route.").ToActionResult();

            var result = await useCase.CreateAsync(request, ct);
            return result.ToActionResult();
        });

        g.MapGet("", async (Guid establishmentId, IAppointmentUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.GetAllAsync(establishmentId, ct);
            return result.ToActionResult();
        });

        g.MapGet("{id:guid}", async (Guid establishmentId, Guid id, IAppointmentUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.GetByIdAsync(id, ct);
            if (result.Success && result.Value!.EstablishmentId != establishmentId)
                return Result<ResponseAppointmentDto>.Fail("NOT_FOUND", "Appointment not found.", ResultErrorType.NotFound).ToActionResult();
            return result.ToActionResult();
        });

        g.MapPut("{id:guid}", async (Guid establishmentId, Guid id, UpdateAppointmentDto request, IAppointmentUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Appointment not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.UpdateAsync(id, request, ct);
            return result.ToActionResult();
        });

        g.MapPost("{id:guid}/cancel", async (Guid establishmentId, Guid id, IAppointmentUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Appointment not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.CancelAsync(id, ct);
            return result.ToActionResult();
        });

        g.MapPost("{id:guid}/complete", async (Guid establishmentId, Guid id, IAppointmentUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Appointment not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.CompleteAsync(id, ct);
            return result.ToActionResult();
        });

        g.MapDelete("{id:guid}", async (Guid establishmentId, Guid id, IAppointmentUseCase useCase, CancellationToken ct) =>
        {
            var existing = await useCase.GetByIdAsync(id, ct);
            if (existing.Success && existing.Value!.EstablishmentId != establishmentId)
                return Result<Unit>.Fail("NOT_FOUND", "Appointment not found.", ResultErrorType.NotFound).ToActionResult();
            if (existing.Failure)
                return existing.ToActionResult();

            var result = await useCase.DeleteAsync(id, ct);
            return result.ToActionResult();
        });

        // Convenience nested routes (tenant isolation enforced by filtering response DTOs).
        app.MapGroup("/api/establishments/{establishmentId:guid}/clients/{clientId:guid}/appointments")
            .MapGet("", async (Guid establishmentId, Guid clientId, DateOnly? fromDate, DateOnly? toDate, IAppointmentUseCase useCase, CancellationToken ct) =>
            {
                if (fromDate.HasValue && toDate.HasValue && fromDate.Value > toDate.Value)
                    return Result<Unit>.Fail("VALIDATION", "fromDate must be less than or equal to toDate.").ToActionResult();

                var result = await useCase.GetByClientAsync(clientId, fromDate, toDate, ct);
                if (result.Success)
                {
                    var filtered = result.Value!.Where(a => a.EstablishmentId == establishmentId).ToList();
                    return Result<List<ResponseAppointmentDto>>.Ok(filtered).ToActionResult();
                }

                return result.ToActionResult();
            });

        app.MapGroup("/api/establishments/{establishmentId:guid}/professionals/{professionalId:guid}/appointments")
            .MapGet("", async (Guid establishmentId, Guid professionalId, DateOnly? fromDate, DateOnly? toDate, IAppointmentUseCase useCase, CancellationToken ct) =>
            {
                if (fromDate.HasValue && toDate.HasValue && fromDate.Value > toDate.Value)
                    return Result<Unit>.Fail("VALIDATION", "fromDate must be less than or equal to toDate.").ToActionResult();

                var result = await useCase.GetByProfessionalAsync(professionalId, fromDate, toDate, ct);
                if (result.Success)
                {
                    var filtered = result.Value!.Where(a => a.EstablishmentId == establishmentId).ToList();
                    return Result<List<ResponseAppointmentDto>>.Ok(filtered).ToActionResult();
                }

                return result.ToActionResult();
            });

        return app;
    }
}

