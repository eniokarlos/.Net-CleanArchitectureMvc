using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Profiles;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureApi(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")
            , b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();

            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IProductService, ProductService>();

            service.AddScoped<IAuthenticate, AuthenticateService>();
            service.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            service.ConfigureApplicationCookie(options =>
            options.AccessDeniedPath = "/Account/Login");

            var handlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
            service.AddMediatR(handlers);

            service.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            service.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return service;
        }
    }
}