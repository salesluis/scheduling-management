using scheduling_management.Api.Routes;
using scheduling_management.Application.Extensions;
using scheduling_management.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUseCase();

builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();
builder.Services.AddDb(builder.Configuration);
builder.Services.AddUseCase();

var app = builder.Build();

app.MapClientRoute();

app.Run();
