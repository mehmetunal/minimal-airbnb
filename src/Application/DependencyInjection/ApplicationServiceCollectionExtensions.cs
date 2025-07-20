using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation;
using AutoMapper;

namespace MinimalAirbnb.Application.DependencyInjection;

/// <summary>
/// Application katmanı için Dependency Injection ayarları
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    /// <summary>
    /// Application servislerini ekle
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // AutoMapper
        services.AddAutoMapper(assembly);

        // FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
} 