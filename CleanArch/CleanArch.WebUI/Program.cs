using CleanArch.Domain.Account;
using CleanArch.Infra.Data.Identity;
using CleanArch.Infra.IoC;

namespace CleanArch.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddInfrastructure();

            ISeedUserRoleInitial? seeder = builder.Services.BuildServiceProvider().GetService<ISeedUserRoleInitial>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            seeder.SeedUsersAsync();
            seeder.SeedRolesAsync();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
