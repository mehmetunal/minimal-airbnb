using FluentMigrator;
using System.Data;

namespace MinimalAirbnb.Infrastructure.Migrations;

/// <summary>
/// Foreign Key'leri ekleyen migration
/// </summary>
[Migration(20240101000002)]
public class AddForeignKeys : Migration
{
    public override void Up()
    {
        // AspNetUserRoles Foreign Keys
        Create.ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId")
            .FromTable("AspNetUserRoles").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        Create.ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId")
            .FromTable("AspNetUserRoles").ForeignColumn("RoleId")
            .ToTable("AspNetRoles").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        // AspNetUserClaims Foreign Keys
        Create.ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId")
            .FromTable("AspNetUserClaims").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        // AspNetUserLogins Foreign Keys
        Create.ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId")
            .FromTable("AspNetUserLogins").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        // AspNetUserTokens Foreign Keys
        Create.ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId")
            .FromTable("AspNetUserTokens").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        // Properties Foreign Keys
        Create.ForeignKey("FK_Properties_AspNetUsers_HostId")
            .FromTable("Properties").ForeignColumn("HostId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.None);

        // Reservations Foreign Keys
        Create.ForeignKey("FK_Reservations_AspNetUsers_GuestId")
            .FromTable("Reservations").ForeignColumn("GuestId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.None);

        Create.ForeignKey("FK_Reservations_Properties_PropertyId")
            .FromTable("Reservations").ForeignColumn("PropertyId")
            .ToTable("Properties").PrimaryColumn("Id")
            .OnDelete(Rule.None);

        Create.ForeignKey("FK_Reservations_AspNetUsers_CancelledByUserId")
            .FromTable("Reservations").ForeignColumn("CancelledByUserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);

        Create.ForeignKey("FK_Reservations_AspNetUsers_ConfirmedByUserId")
            .FromTable("Reservations").ForeignColumn("ConfirmedByUserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);

        // Reviews Foreign Keys
        Create.ForeignKey("FK_Reviews_AspNetUsers_GuestId")
            .FromTable("Reviews").ForeignColumn("GuestId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.None);

        Create.ForeignKey("FK_Reviews_Properties_PropertyId")
            .FromTable("Reviews").ForeignColumn("PropertyId")
            .ToTable("Properties").PrimaryColumn("Id")
            .OnDelete(Rule.None);

        Create.ForeignKey("FK_Reviews_Reservations_ReservationId")
            .FromTable("Reviews").ForeignColumn("ReservationId")
            .ToTable("Reservations").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);

        Create.ForeignKey("FK_Reviews_AspNetUsers_HostResponseUserId")
            .FromTable("Reviews").ForeignColumn("HostResponseUserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);

        Create.ForeignKey("FK_Reviews_AspNetUsers_ModeratedByUserId")
            .FromTable("Reviews").ForeignColumn("ModeratedByUserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);

        // Payments Foreign Keys
        Create.ForeignKey("FK_Payments_AspNetUsers_UserId")
            .FromTable("Payments").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.None);

        Create.ForeignKey("FK_Payments_Reservations_ReservationId")
            .FromTable("Payments").ForeignColumn("ReservationId")
            .ToTable("Reservations").PrimaryColumn("Id")
            .OnDelete(Rule.None);

        // Messages Foreign Keys
        Create.ForeignKey("FK_Messages_AspNetUsers_SenderId")
            .FromTable("Messages").ForeignColumn("SenderId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.None);

        Create.ForeignKey("FK_Messages_AspNetUsers_ReceiverId")
            .FromTable("Messages").ForeignColumn("ReceiverId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.None);

        Create.ForeignKey("FK_Messages_Reservations_ReservationId")
            .FromTable("Messages").ForeignColumn("ReservationId")
            .ToTable("Reservations").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);

        Create.ForeignKey("FK_Messages_Properties_PropertyId")
            .FromTable("Messages").ForeignColumn("PropertyId")
            .ToTable("Properties").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);

        Create.ForeignKey("FK_Messages_Messages_ReplyToMessageId")
            .FromTable("Messages").ForeignColumn("ReplyToMessageId")
            .ToTable("Messages").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);

        Create.ForeignKey("FK_Messages_AspNetUsers_ArchivedByUserId")
            .FromTable("Messages").ForeignColumn("ArchivedByUserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);

        // Favorites Foreign Keys
        Create.ForeignKey("FK_Favorites_AspNetUsers_UserId")
            .FromTable("Favorites").ForeignColumn("UserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        Create.ForeignKey("FK_Favorites_Properties_PropertyId")
            .FromTable("Favorites").ForeignColumn("PropertyId")
            .ToTable("Properties").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        // PropertyPhotos Foreign Keys
        Create.ForeignKey("FK_PropertyPhotos_Properties_PropertyId")
            .FromTable("PropertyPhotos").ForeignColumn("PropertyId")
            .ToTable("Properties").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);

        Create.ForeignKey("FK_PropertyPhotos_AspNetUsers_ModeratedByUserId")
            .FromTable("PropertyPhotos").ForeignColumn("ModeratedByUserId")
            .ToTable("AspNetUsers").PrimaryColumn("Id")
            .OnDelete(Rule.SetNull);
    }

    public override void Down()
    {
        // Foreign Key'leri sil (ters sırada)
        Delete.ForeignKey("FK_PropertyPhotos_AspNetUsers_ModeratedByUserId").OnTable("PropertyPhotos");
        Delete.ForeignKey("FK_PropertyPhotos_Properties_PropertyId").OnTable("PropertyPhotos");
        Delete.ForeignKey("FK_Favorites_Properties_PropertyId").OnTable("Favorites");
        Delete.ForeignKey("FK_Favorites_AspNetUsers_UserId").OnTable("Favorites");
        Delete.ForeignKey("FK_Messages_AspNetUsers_ArchivedByUserId").OnTable("Messages");
        Delete.ForeignKey("FK_Messages_Messages_ReplyToMessageId").OnTable("Messages");
        Delete.ForeignKey("FK_Messages_Properties_PropertyId").OnTable("Messages");
        Delete.ForeignKey("FK_Messages_Reservations_ReservationId").OnTable("Messages");
        Delete.ForeignKey("FK_Messages_AspNetUsers_ReceiverId").OnTable("Messages");
        Delete.ForeignKey("FK_Messages_AspNetUsers_SenderId").OnTable("Messages");
        Delete.ForeignKey("FK_Payments_Reservations_ReservationId").OnTable("Payments");
        Delete.ForeignKey("FK_Payments_AspNetUsers_UserId").OnTable("Payments");
        Delete.ForeignKey("FK_Reviews_AspNetUsers_ModeratedByUserId").OnTable("Reviews");
        Delete.ForeignKey("FK_Reviews_AspNetUsers_HostResponseUserId").OnTable("Reviews");
        Delete.ForeignKey("FK_Reviews_Reservations_ReservationId").OnTable("Reviews");
        Delete.ForeignKey("FK_Reviews_Properties_PropertyId").OnTable("Reviews");
        Delete.ForeignKey("FK_Reviews_AspNetUsers_GuestId").OnTable("Reviews");
        Delete.ForeignKey("FK_Reservations_AspNetUsers_ConfirmedByUserId").OnTable("Reservations");
        Delete.ForeignKey("FK_Reservations_AspNetUsers_CancelledByUserId").OnTable("Reservations");
        Delete.ForeignKey("FK_Reservations_Properties_PropertyId").OnTable("Reservations");
        Delete.ForeignKey("FK_Reservations_AspNetUsers_GuestId").OnTable("Reservations");
        Delete.ForeignKey("FK_Properties_AspNetUsers_HostId").OnTable("Properties");
        Delete.ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId").OnTable("AspNetUserTokens");
        Delete.ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId").OnTable("AspNetUserLogins");
        Delete.ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId").OnTable("AspNetUserClaims");
        Delete.ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId").OnTable("AspNetUserRoles");
        Delete.ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId").OnTable("AspNetUserRoles");
    }
} 