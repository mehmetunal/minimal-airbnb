using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MinimalAirbnb.Infrastructure.SeedData;

/// <summary>
/// Seed Data için extension method'lar
/// </summary>
public static class SeedDataExtensions
{
    /// <summary>
    /// Uygulama başlangıcında seed data'yı çalıştırır
    /// </summary>
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var seedDataService = scope.ServiceProvider.GetRequiredService<SeedDataService>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedDataService>>();

        try
        {
            logger.LogInformation("Seed data başlatılıyor...");
            await seedDataService.SeedAllAsync();
            logger.LogInformation("Seed data başarıyla tamamlandı.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Seed data sırasında hata oluştu.");
            throw;
        }
    }

    /// <summary>
    /// Seed Data servisini DI container'a ekler
    /// </summary>
    public static IServiceCollection AddSeedData(this IServiceCollection services)
    {
        services.AddScoped<SeedDataService>();
        return services;
    }
} 