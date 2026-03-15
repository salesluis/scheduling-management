using FluentValidation;
using Microsoft.EntityFrameworkCore;
using scheduling_management.Application.Services;
using scheduling_management.Application.Services.Appointment;
using scheduling_management.Http.Filters;
using scheduling_management.Infra.Data;
using scheduling_management.Infra.Repositories;
using scheduling_management.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(o => o.Filters.Add<FluentValidationFilter>());

var cnt = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SchedulingManagementDbContext>(o => o.UseSqlServer(cnt));

// Repositories
builder.Services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IProfessionalServiceRepository, ProfessionalServiceRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

// Services
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
builder.Services.AddScoped<IProfessionalService, ProfessionalServiceHandle>();
builder.Services.AddScoped<IServiceCatalogService, ServiceCatalogService>();
builder.Services.AddScoped<IProfessionalServiceLinkService, ProfessionalServiceLinkService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.MapControllers();

app.Run();
