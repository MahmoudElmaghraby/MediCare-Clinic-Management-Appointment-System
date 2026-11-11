using MediCare.Data.Entities;
using MediCare.Data.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediCare.Data;

/// <summary>
/// Registers the database context and the unit of work.
/// MediCare.Services calls this. MediCare.Web does not reference this project.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddMediCareData(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'DefaultConnection' was not found.");
        }

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
