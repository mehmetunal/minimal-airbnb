using FluentMigrator;

namespace MinimalAirbnb.Infrastructure.Migrations;

/// <summary>
/// Review tablosuna detaylı rating kolonlarını ekle
/// </summary>
[Migration(20240101000004)]
public class AddReviewRatingColumns : Migration
{
    public override void Up()
    {
        Alter.Table("Reviews")
            .AddColumn("CleanlinessRating").AsInt32().Nullable()
            .AddColumn("CommunicationRating").AsInt32().Nullable()
            .AddColumn("CheckInRating").AsInt32().Nullable()
            .AddColumn("AccuracyRating").AsInt32().Nullable()
            .AddColumn("LocationRating").AsInt32().Nullable()
            .AddColumn("ValueRating").AsInt32().Nullable();
    }

    public override void Down()
    {
        Delete.Column("CleanlinessRating").FromTable("Reviews");
        Delete.Column("CommunicationRating").FromTable("Reviews");
        Delete.Column("CheckInRating").FromTable("Reviews");
        Delete.Column("AccuracyRating").FromTable("Reviews");
        Delete.Column("LocationRating").FromTable("Reviews");
        Delete.Column("ValueRating").FromTable("Reviews");
    }
} 