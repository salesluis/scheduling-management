using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Infra.Data;

public class SchedulingManagementDbContext : DbContext
{
    public SchedulingManagementDbContext(DbContextOptions<SchedulingManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<Establishment> Establishments => Set<Establishment>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Professional> Professionals => Set<Professional>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<ProfessionalService> ProfessionalServices => Set<ProfessionalService>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulingManagementDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

