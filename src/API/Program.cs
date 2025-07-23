using System.Text.Json;
using MinimalAirbnb.Infrastructure.DependencyInjection;
using MinimalAirbnb.Infrastructure.SeedData;
using MinimalAirbnb.Infrastructure.Data;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MinimalAirbnb.Application.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Maggsoft.Framework.Extensions;
using Maggsoft.Framework.Middleware;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure Services
builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("DefaultConnection")!);

// Application Services
builder.Services.AddApplicationServices();

// Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<MinimalAirbnbDbContext>()
.AddDefaultTokenProviders();

// Seed Data Service
builder.Services.AddScoped<SeedDataService>();

// Controllers
builder.Services.AddControllers();

// JSON serializer ayarları
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

// Maggsoft Global Response Middleware
builder.Services.AddGlobalResponseMiddlewareWithOptions(p => p.IgnoreAcceptHeader = ["image/", "txt"]);

builder.Services.AddExceptionHandler<ExceptionMiddleware>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Controllers
app.MapControllers();

// Migration endpoint
app.MapPost("/api/migrate", async (IMigrationService migrationService) =>
{
    try
    {
        await migrationService.MigrateAsync();
        return Results.Ok(new { message = "Migration başarıyla tamamlandı!" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = $"Migration hatası: {ex.Message}" });
    }
})
.WithName("RunMigration")
.WithOpenApi();

// Seed data endpoint
app.MapPost("/api/seed", async (SeedDataService seedDataService) =>
{
    try
    {
        await seedDataService.SeedAllAsync();
        return Results.Ok(new { message = "Seed data başarıyla eklendi!" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = $"Seed data hatası: {ex.Message}" });
    }
})
.WithName("SeedData")
.WithOpenApi();

// Database tables endpoint
app.MapGet("/api/tables", async (MinimalAirbnbDbContext context) =>
{
    try
    {
        var tables = new List<string>();

        // SQL Server'da tabloları listele
        var sql = @"
            SELECT TABLE_NAME
            FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_TYPE = 'BASE TABLE'
            AND TABLE_CATALOG = 'airbnb'
            ORDER BY TABLE_NAME";

        var tableNames = await context.Database.SqlQueryRaw<string>(sql).ToListAsync();

        return Results.Ok(new {
            message = $"Veritabanında {tableNames.Count} tablo bulundu",
            tables = tableNames,
            expectedCount = 14
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = $"Tablo listesi alınırken hata: {ex.Message}" });
    }
})
.WithName("GetTables")
.WithOpenApi();

// Database columns endpoint
app.MapGet("/api/tables/{tableName}/columns", async (MinimalAirbnbDbContext context, string tableName) =>
{
    try
    {
        // SQL Server'da tablo kolonlarını listele
        var sql = @"
            SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = @tableName
            AND TABLE_CATALOG = 'airbnb'
            ORDER BY ORDINAL_POSITION";

        var columns = await context.Database.SqlQueryRaw<dynamic>(sql, new[] { new Microsoft.Data.SqlClient.SqlParameter("@tableName", tableName) }).ToListAsync();

        return Results.Ok(new {
            message = $"{tableName} tablosunda {columns.Count} kolon bulundu",
            tableName = tableName,
            columns = columns
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = $"Kolon listesi alınırken hata: {ex.Message}" });
    }
})
.WithName("GetTableColumns")
.WithOpenApi();

// Database columns endpoint (simple)
app.MapGet("/api/columns/{tableName}", async (MinimalAirbnbDbContext context, string tableName) =>
{
    try
    {
        // SQL Server'da tablo kolonlarını listele
        var sql = $@"
            SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = '{tableName}'
            AND TABLE_CATALOG = 'airbnb'
            ORDER BY ORDINAL_POSITION";

        var columns = await context.Database.SqlQueryRaw<string>(sql).ToListAsync();

        return Results.Ok(new {
            message = $"{tableName} tablosunda {columns.Count} kolon bulundu",
            tableName = tableName,
            columns = columns
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = $"Kolon listesi alınırken hata: {ex.Message}" });
    }
})
.WithName("GetColumns")
.WithOpenApi();

// Drop all tables endpoint
app.MapPost("/api/drop-all-tables", async (MinimalAirbnbDbContext context) =>
{
    try
    {
        // Tüm tabloları sil
        var sql = @"
            DECLARE @sql NVARCHAR(MAX) = N'';

            SELECT @sql += 'ALTER TABLE [' + sch.name + '].[' + t.name + '] DROP CONSTRAINT [' + fk.name + '];' + CHAR(13)
            FROM sys.foreign_keys fk
                    JOIN sys.tables t ON fk.parent_object_id = t.object_id
                    JOIN sys.schemas sch ON t.schema_id = sch.schema_id;

            EXEC sp_executesql @sql;


            DECLARE @sql2 NVARCHAR(MAX) = N'';

            SELECT @sql2 += 'DROP TABLE [' + s.name + '].[' + t.name + '];' + CHAR(13)
            FROM sys.tables t
                    JOIN sys.schemas s ON t.schema_id = s.schema_id;

            EXEC sp_executesql @sql2;";

        await context.Database.ExecuteSqlRawAsync(sql);

        return Results.Ok(new { message = "Tüm tablolar başarıyla silindi" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = $"Tablolar silinirken hata: {ex.Message}" });
    }
})
.WithName("DropAllTables")
.WithOpenApi();

app.Run();
