using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Infrastructure.SeedData;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MinimalAirbnb.Infrastructure.Services;

/// <summary>
/// Migration Servisi
/// </summary>
public class MigrationService : IMigrationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MigrationService> _logger;

    public MigrationService(IServiceProvider serviceProvider, ILogger<MigrationService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// Migration'ları çalıştır
    /// </summary>
    public async Task MigrateAsync()
    {
        try
        {
            _logger.LogInformation("Database migration'ları başlatılıyor...");

            using var scope = _serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            if (runner.HasMigrationsToApplyUp())
            {
                runner.MigrateUp();
                _logger.LogInformation("Database migration'ları başarıyla tamamlandı.");
            }
            else
            {
                _logger.LogInformation("Uygulanacak migration bulunamadı.");
            }

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database migration sırasında hata oluştu.");
            throw;
        }
    }

    /// <summary>
    /// Seed data ekle
    /// </summary>
    public async Task SeedDataAsync()
    {
        try
        {
            _logger.LogInformation("Seed data ekleniyor...");

            using var scope = _serviceProvider.CreateScope();
            var seedDataService = scope.ServiceProvider.GetRequiredService<SeedDataService>();
            
            await seedDataService.SeedAllAsync();
            _logger.LogInformation("Seed data başarıyla eklendi.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Seed data eklenirken hata oluştu.");
            throw;
        }
    }

    /// <summary>
    /// Migration durumunu kontrol et
    /// </summary>
    public async Task<bool> HasMigrationsToApplyAsync()
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            return await Task.FromResult(runner.HasMigrationsToApplyUp());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Migration durumu kontrol edilirken hata oluştu.");
            return false;
        }
    }

    /// <summary>
    /// Migration listesini getir
    /// </summary>
    public async Task<IEnumerable<string>> GetMigrationListAsync()
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            var migrations = runner.MigrationLoader.LoadMigrations();
            
            return await Task.FromResult(migrations.Select(m => m.Key.ToString()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Migration listesi alınırken hata oluştu.");
            return Enumerable.Empty<string>();
        }
    }
} 