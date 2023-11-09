using DockerComposeSample.Data;
using Microsoft.EntityFrameworkCore;

namespace DockerComposeSample.Services
{
    public static class DatabaseManagementService
    {
        public static void ApplyMigrations(IApplicationBuilder app) 
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>()?.Database.Migrate();
            }
        }
    }
}
