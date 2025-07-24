using FluentMigrator;

namespace MinimalAirbnb.Infrastructure.Migrations;

/// <summary>
/// Payment tablosuna eksik kolonları ekleyen migration
/// </summary>
[Migration(20240101000002)]
public class AddMissingPaymentColumns : Migration
{
    public override void Up()
    {
        // Payment tablosuna eksik kolonları ekle
        Alter.Table("Payments")
            .AddColumn("ProviderTransactionId").AsString(100).Nullable()
            .AddColumn("Provider").AsInt32().NotNullable().WithDefaultValue(0) // PaymentProvider.TestProvider
            .AddColumn("Description").AsString(500).Nullable();
    }

    public override void Down()
    {
        // Eklenen kolonları kaldır
        Delete.Column("ProviderTransactionId").FromTable("Payments");
        Delete.Column("Provider").FromTable("Payments");
        Delete.Column("Description").FromTable("Payments");
    }
} 