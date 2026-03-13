using Microsoft.EntityFrameworkCore;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Infra.Data;

public class SchedulingManagementDbContext : DbContext
{
    public SchedulingManagementDbContext(DbContextOptions<SchedulingManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<Establishment> Establishments { get; set; }    
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Professional> Professionals { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ProfessionalService> ProfessionalServices { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulingManagementDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

