using scheduling_management.Application.UseCases.Appointment;
using scheduling_management.Application.UseCases.Client;
using scheduling_management.Application.UseCases.Establishment;
using scheduling_management.Application.UseCases.Professional;
using scheduling_management.Application.UseCases.ProfessionalServiceLink;
using scheduling_management.Application.UseCases.Service;
using scheduling_management.Application.UseCases.User;

namespace scheduling_management.Application.Extensions;

public static class ApplicationExtensions
{
    public static void AddUseCase(this IServiceCollection services) 
    {
        services.AddScoped<IEstablishmentUseCase, EstablishmentUseCase>();
        services.AddScoped<IAppointmentUseCase, AppointmentUseCase>();
        services.AddScoped<IClientUseCase, ClientUseCase>();
        services.AddScoped<IProfessionalUseCase, ProfessionalUseCase>();
        services.AddScoped<IServiceUseCase, ServiceUseCase>();
        services.AddScoped<IProfessionalLinkUseCase, ProfessionalLinkUseCase>();
        services.AddScoped<IUserUseCase, UserUseCase>();
    }
}