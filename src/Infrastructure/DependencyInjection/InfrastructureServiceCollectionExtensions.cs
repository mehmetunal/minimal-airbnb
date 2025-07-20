using Microsoft.Extensions.DependencyInjection;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Infrastructure.Repositories;
using MinimalAirbnb.Infrastructure.Data;
using MinimalAirbnb.Infrastructure.Logging;
using MinimalAirbnb.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using FluentMigrator.Runner;
using System.Reflection;
using Maggsoft.Cache.MemoryCache;
using Maggsoft.Core.IoC;

namespace MinimalAirbnb.Infrastructure.DependencyInjection;

/// <summary>
/// Infrastructure katmanı için Dependency Injection ayarları
/// </summary>
public static class InfrastructureServiceCollectionExtensions
{
    /// <summary>
    /// Infrastructure servislerini ekle
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        // DbContext
        services.AddDbContext<MinimalAirbnbDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Memory Cache
        // Maggsoft Cache
        services.AddMaggsoftDistributedMemoryCache(typeof(IService));
        
        // FluentMigrator
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(lb => lb
                .AddFluentMigratorConsole());

        // Repository'ler
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        services.AddScoped<IPropertyPhotoRepository, PropertyPhotoRepository>();

        // Logging ve Migration Servisleri
        services.AddScoped<ILoggingService, LoggingService>();
        services.AddScoped<IMigrationService, MigrationService>();

        return services;
    }
} 