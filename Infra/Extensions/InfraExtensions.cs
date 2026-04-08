using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Contracts.Repositories;
using scheduling_management.Infra.Data;
using scheduling_management.Infra.Repositories;

namespace scheduling_management.Infra.Extensions;

public static class InfraExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        var cnt =  configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<SchedulingManagementDbContext>(o =>
            o.UseSqlServer(cnt));
    }
}