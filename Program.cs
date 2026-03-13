
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using scheduling_management.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var cnt = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SchedulingManagementDbContext>(o
    => o.UseSqlServer(cnt));

var app = builder.Build();

app.MapControllers();

app.Run();
