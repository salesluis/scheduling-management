using System.Threading;
using System.Threading.Tasks;
using scheduling_management.Application.Common;
using scheduling_management.Application.DTOs;
using scheduling_management.Domain.Builders;

namespace scheduling_management.Application.UseCases.Appointment;

public partial class AppointmentUseCase
{
    public async Task<Result<ResponseAppointmentDto>> CreateAsync(CreateAppointmentDto request, CancellationToken cancellationToken = default)
    {
        var entity = new AppointmentBuilder()
            .WithEstabilishmentId(request.EstablishmentId)
            .WithClientId(request.ClientId)
            .WithProfessionalId(request.ProfessionalId)
            .WithServicetId(request.ServiceId)
            .WithStartHours(request.StartHours)
            .WithEndHours(request.EndHours)
            .WithSchedulingDateOnly(request.SchedulingDateOnly)
            .Build();
        
        var created = await repository.CreateAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync();
        return Result<ResponseAppointmentDto>.Ok(MapToResponse(created));
    }
}