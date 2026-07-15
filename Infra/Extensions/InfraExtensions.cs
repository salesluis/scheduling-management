using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.UnitOfWork.Abstractions;
using scheduling_management.Domain.Repository.Abstractions;
using scheduling_management.Infra.Data;
using scheduling_management.Infra.Repositories;

namespace scheduling_management.Infra.Extensions;

public static class InfraExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
        
    }

    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        var cnt =  configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<SchedulingManagementDbContext>(o =>
            o.UseSqlServer(cnt));
        return services;
        
    }
}