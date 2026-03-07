
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<scheduling_management.Infra.Data.SchedulingManagementDbContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
