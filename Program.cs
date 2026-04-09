using scheduling_management.Api.Routes;
using scheduling_management.Application.Extensions;
using scheduling_management.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUnitOfWork();
builder.Services.AddRepositories();
builder.Services.AddUseCase();
builder.Services.AddDb(builder.Configuration);

var app = builder.Build();

app.MapEstablishmentRoute();
app.MapClientRoute();
app.MapServiceRoute();
app.MapProfessionalRoute();
app.MapProfessionalServiceRoute();
app.MapAppointmentRoute();

app.Run();
