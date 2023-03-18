using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Application.Mappings;
using CleanArch.Domain.Account;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repositories;
using CleanArch.Infra.Data.Identity;

namespace CleanArch.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Infra.Data
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("CLEAN_ARCH_CONNECTIONSTRING")));

            services.AddTransient<ISeedUserRoleInitial, SeedUserRoleInitial>();

            // Interfaces
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Application
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            // AutoMapper
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            // MediatR
            services.AddMediatR(config => config.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("CleanArch.Application")));
            // services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.Load("CleanArch.Application")));

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
                options.AccessDeniedPath = "/Account/Login");

            return services;
        }
    }
}
