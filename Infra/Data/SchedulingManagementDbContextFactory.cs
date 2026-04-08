using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace scheduling_management.Infra.Data;

// public class SchedulingManagementDbContextFactory : IDesignTimeDbContextFactory<SchedulingManagementDbContext>
// {
//     public SchedulingManagementDbContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<SchedulingManagementDbContext>();
//         optionsBuilder
//             .UseSqlServer(
//                 "Server=localhost,1433;Database=scheduling-management;User Id=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;");
//         return new SchedulingManagementDbContext(optionsBuilder.Options);
//     }
// }