using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data{
    public static class PrepDb{
        public static void PrePopulation(IApplicationBuilder app, bool isProd){
            using( var serviceScope= app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),isProd);
            }
        }
        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("-> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex){
                    Console.WriteLine("$--> Could not run Migrations:{ex.Message}");
                }
                
            }
            if (!context.Platforms.Any()){
                Console.WriteLine("-- Seeding Data...");
                context.Platforms.AddRange(
                    new Platform() {Name="Dot net", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="SQL server", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="Kubernetes", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="Docker", Publisher="Microsoft", Cost="Free"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}