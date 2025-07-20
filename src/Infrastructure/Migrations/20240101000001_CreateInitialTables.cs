using FluentMigrator;
using MinimalAirbnb.Domain.Enums;

namespace MinimalAirbnb.Infrastructure.Migrations;

/// <summary>
/// İlk migration - Tüm tabloları oluşturur
/// </summary>
[Migration(20240101000001)]
public class CreateInitialTables : Migration
{
    public override void Up()
    {
        // AspNetUsers tablosu (Identity için)
        Create.Table("AspNetUsers")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("UserName").AsString(256).Nullable()
            .WithColumn("NormalizedUserName").AsString(256).Nullable()
            .WithColumn("Email").AsString(256).Nullable()
            .WithColumn("NormalizedEmail").AsString(256).Nullable()
            .WithColumn("EmailConfirmed").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("PasswordHash").AsString().Nullable()
            .WithColumn("SecurityStamp").AsString().Nullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable()
            .WithColumn("PhoneNumber").AsString().Nullable()
            .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("LockoutEnd").AsDateTimeOffset().Nullable()
            .WithColumn("LockoutEnabled").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("AccessFailedCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("FirstName").AsString(100).Nullable()
            .WithColumn("LastName").AsString(100).Nullable()
            .WithColumn("DateOfBirth").AsDate().Nullable()
            .WithColumn("Gender").AsString(20).Nullable()
            .WithColumn("PhoneNumber2").AsString(20).Nullable()
            .WithColumn("Address").AsString(500).Nullable()
            .WithColumn("City").AsString(100).Nullable()
            .WithColumn("Country").AsString(100).Nullable()
            .WithColumn("PostalCode").AsString(20).Nullable()
            .WithColumn("ProfilePictureUrl").AsString(500).Nullable()
            .WithColumn("Bio").AsString(1000).Nullable()
            .WithColumn("Website").AsString(200).Nullable()
            .WithColumn("SocialMediaLinks").AsString(1000).Nullable()
            .WithColumn("UserType").AsInt32().NotNullable().WithDefaultValue((int)UserType.Guest)
            .WithColumn("IsVerified").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("VerificationDate").AsDateTime().Nullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("LastLoginDate").AsDateTime().Nullable()
            .WithColumn("LoginCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
            .WithColumn("ModifiedDate").AsDateTime().Nullable()
            .WithColumn("IsPublish").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

        // AspNetRoles tablosu (Identity için)
        Create.Table("AspNetRoles")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString(256).Nullable()
            .WithColumn("NormalizedName").AsString(256).Nullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable();

        // AspNetUserRoles tablosu (Identity için)
        Create.Table("AspNetUserRoles")
            .WithColumn("UserId").AsGuid().PrimaryKey()
            .WithColumn("RoleId").AsGuid().PrimaryKey();

        // AspNetUserClaims tablosu (Identity için)
        Create.Table("AspNetUserClaims")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("ClaimType").AsString().Nullable()
            .WithColumn("ClaimValue").AsString().Nullable();

        // AspNetUserLogins tablosu (Identity için)
        Create.Table("AspNetUserLogins")
            .WithColumn("LoginProvider").AsString(450).PrimaryKey()
            .WithColumn("ProviderKey").AsString(450).PrimaryKey()
            .WithColumn("ProviderDisplayName").AsString().Nullable()
            .WithColumn("UserId").AsGuid().NotNullable();

        // AspNetUserTokens tablosu (Identity için)
        Create.Table("AspNetUserTokens")
            .WithColumn("UserId").AsGuid().PrimaryKey()
            .WithColumn("LoginProvider").AsString(450).PrimaryKey()
            .WithColumn("Name").AsString(450).PrimaryKey()
            .WithColumn("Value").AsString().Nullable();

        // Properties tablosu
        Create.Table("Properties")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("HostId").AsGuid().NotNullable()
            .WithColumn("Title").AsString(200).NotNullable()
            .WithColumn("Description").AsString(2000).Nullable()
            .WithColumn("PropertyType").AsInt32().NotNullable()
            .WithColumn("Address").AsString(500).NotNullable()
            .WithColumn("City").AsString(100).NotNullable()
            .WithColumn("Country").AsString(100).NotNullable()
            .WithColumn("PostalCode").AsString(20).Nullable()
            .WithColumn("Latitude").AsDecimal(10, 8).Nullable()
            .WithColumn("Longitude").AsDecimal(11, 8).Nullable()
            .WithColumn("PricePerNight").AsDecimal(10, 2).NotNullable()
            .WithColumn("Currency").AsString(3).NotNullable().WithDefaultValue("TRY")
            .WithColumn("MaxGuests").AsInt32().NotNullable()
            .WithColumn("Bedrooms").AsInt32().NotNullable()
            .WithColumn("Beds").AsInt32().NotNullable()
            .WithColumn("Bathrooms").AsInt32().NotNullable()
            .WithColumn("Amenities").AsString(2000).Nullable()
            .WithColumn("HouseRules").AsString(1000).Nullable()
            .WithColumn("CancellationPolicy").AsString(500).Nullable()
            .WithColumn("CheckInTime").AsString(10).Nullable()
            .WithColumn("CheckOutTime").AsString(10).Nullable()
            .WithColumn("IsInstantBookable").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("IsSuperhost").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("IsVerified").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("VerificationDate").AsDateTime().Nullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("ViewCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("FavoriteCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("AverageRating").AsDecimal(3, 2).Nullable()
            .WithColumn("TotalReviews").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
            .WithColumn("ModifiedDate").AsDateTime().Nullable()
            .WithColumn("IsPublish").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

        // Reservations tablosu
        Create.Table("Reservations")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("GuestId").AsGuid().NotNullable()
            .WithColumn("PropertyId").AsGuid().NotNullable()
            .WithColumn("CheckInDate").AsDate().NotNullable()
            .WithColumn("CheckOutDate").AsDate().NotNullable()
            .WithColumn("NumberOfGuests").AsInt32().NotNullable()
            .WithColumn("TotalPrice").AsDecimal(10, 2).NotNullable()
            .WithColumn("Currency").AsString(3).NotNullable().WithDefaultValue("TRY")
            .WithColumn("Status").AsInt32().NotNullable().WithDefaultValue((int)ReservationStatus.Pending)
            .WithColumn("CancellationReason").AsString(500).Nullable()
            .WithColumn("CancelledByUserId").AsGuid().Nullable()
            .WithColumn("CancellationDate").AsDateTime().Nullable()
            .WithColumn("RefundAmount").AsDecimal(10, 2).Nullable()
            .WithColumn("RefundDate").AsDateTime().Nullable()
            .WithColumn("SpecialRequests").AsString(1000).Nullable()
            .WithColumn("ConfirmedByUserId").AsGuid().Nullable()
            .WithColumn("ConfirmationDate").AsDateTime().Nullable()
            .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
            .WithColumn("ModifiedDate").AsDateTime().Nullable()
            .WithColumn("IsPublish").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

        // Reviews tablosu
        Create.Table("Reviews")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("GuestId").AsGuid().NotNullable()
            .WithColumn("PropertyId").AsGuid().NotNullable()
            .WithColumn("ReservationId").AsGuid().Nullable()
            .WithColumn("Rating").AsInt32().NotNullable()
            .WithColumn("Comment").AsString(2000).NotNullable()
            .WithColumn("HostResponse").AsString(2000).Nullable()
            .WithColumn("HostResponseDate").AsDateTime().Nullable()
            .WithColumn("HostResponseUserId").AsGuid().Nullable()
            .WithColumn("IsApproved").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("IsRejected").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("RejectionReason").AsString(500).Nullable()
            .WithColumn("ModeratedByUserId").AsGuid().Nullable()
            .WithColumn("ModerationDate").AsDateTime().Nullable()
            .WithColumn("LikeCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("DislikeCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
            .WithColumn("ModifiedDate").AsDateTime().Nullable()
            .WithColumn("IsPublish").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

        // Payments tablosu
        Create.Table("Payments")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("ReservationId").AsGuid().NotNullable()
            .WithColumn("Amount").AsDecimal(10, 2).NotNullable()
            .WithColumn("PaymentMethod").AsInt32().NotNullable()
            .WithColumn("Status").AsInt32().NotNullable().WithDefaultValue((int)PaymentStatus.Pending)
            .WithColumn("TransactionId").AsString(100).Nullable()
            .WithColumn("ProviderReferenceId").AsString(100).Nullable()
            .WithColumn("PaymentDate").AsDateTime().Nullable()
            .WithColumn("RefundAmount").AsDecimal(10, 2).Nullable()
            .WithColumn("RefundDate").AsDateTime().Nullable()
            .WithColumn("RefundReason").AsString(500).Nullable()
            .WithColumn("ErrorMessage").AsString(1000).Nullable()
            .WithColumn("RetryCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("LastRetryDate").AsDateTime().Nullable()
            .WithColumn("PaymentProvider").AsString(100).Nullable()
            .WithColumn("LastFourDigits").AsString(4).Nullable()
            .WithColumn("CardType").AsString(50).Nullable()
            .WithColumn("Currency").AsString(3).NotNullable().WithDefaultValue("TRY")
            .WithColumn("ExchangeRate").AsDecimal(10, 4).Nullable()
            .WithColumn("OriginalAmount").AsDecimal(10, 2).Nullable()
            .WithColumn("OriginalCurrency").AsString(3).Nullable()
            .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
            .WithColumn("ModifiedDate").AsDateTime().Nullable()
            .WithColumn("IsPublish").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

        // Messages tablosu
        Create.Table("Messages")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("SenderId").AsGuid().NotNullable()
            .WithColumn("ReceiverId").AsGuid().NotNullable()
            .WithColumn("ReservationId").AsGuid().Nullable()
            .WithColumn("PropertyId").AsGuid().Nullable()
            .WithColumn("Subject").AsString(200).NotNullable()
            .WithColumn("Content").AsString(2000).NotNullable()
            .WithColumn("IsRead").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("ReadDate").AsDateTime().Nullable()
            .WithColumn("ReplyToMessageId").AsGuid().Nullable()
            .WithColumn("MessageType").AsInt32().NotNullable().WithDefaultValue((int)MessageType.Private)
            .WithColumn("Priority").AsInt32().NotNullable().WithDefaultValue((int)MessagePriority.Normal)
            .WithColumn("Category").AsInt32().NotNullable().WithDefaultValue((int)MessageCategory.General)
            .WithColumn("AttachmentUrl").AsString(500).Nullable()
            .WithColumn("AttachmentName").AsString(200).Nullable()
            .WithColumn("AttachmentSize").AsInt64().Nullable()
            .WithColumn("AttachmentType").AsString(100).Nullable()
            .WithColumn("IsEncrypted").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("IsArchived").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("ArchivedDate").AsDateTime().Nullable()
            .WithColumn("ArchivedByUserId").AsGuid().Nullable()
            .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
            .WithColumn("ModifiedDate").AsDateTime().Nullable()
            .WithColumn("IsPublish").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

        // Favorites tablosu
        Create.Table("Favorites")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("UserId").AsGuid().NotNullable()
            .WithColumn("PropertyId").AsGuid().NotNullable()
            .WithColumn("Note").AsString(500).Nullable()
            .WithColumn("Category").AsString(100).Nullable()
            .WithColumn("Tags").AsString(500).Nullable()
            .WithColumn("SortOrder").AsInt32().Nullable()
            .WithColumn("IsPrivate").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("IsShared").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("SharedDate").AsDateTime().Nullable()
            .WithColumn("ShareLink").AsString(500).Nullable()
            .WithColumn("VisitCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("LastVisitDate").AsDateTime().Nullable()
            .WithColumn("ReminderDate").AsDateTime().Nullable()
            .WithColumn("ReminderNote").AsString(200).Nullable()
            .WithColumn("ReminderSent").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
            .WithColumn("ModifiedDate").AsDateTime().Nullable()
            .WithColumn("IsPublish").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

        // PropertyPhotos tablosu
        Create.Table("PropertyPhotos")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("PropertyId").AsGuid().NotNullable()
            .WithColumn("PhotoUrl").AsString(500).NotNullable()
            .WithColumn("Caption").AsString(200).Nullable()
            .WithColumn("Description").AsString(1000).Nullable()
            .WithColumn("IsMainPhoto").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("SortOrder").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("FileName").AsString(200).Nullable()
            .WithColumn("FileType").AsString(50).Nullable()
            .WithColumn("FileSize").AsInt64().Nullable()
            .WithColumn("Width").AsInt32().Nullable()
            .WithColumn("Height").AsInt32().Nullable()
            .WithColumn("Resolution").AsInt32().Nullable()
            .WithColumn("Quality").AsInt32().Nullable()
            .WithColumn("Tags").AsString(500).Nullable()
            .WithColumn("Category").AsString(100).Nullable()
            .WithColumn("IsApproved").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("IsRejected").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("RejectionReason").AsString(500).Nullable()
            .WithColumn("ModeratedByUserId").AsGuid().Nullable()
            .WithColumn("ModerationDate").AsDateTime().Nullable()
            .WithColumn("ViewCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("LikeCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("CommentCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("ShareCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("DownloadCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("CreatedDate").AsDateTime().NotNullable().WithDefaultValue(SystemMethods.CurrentDateTime)
            .WithColumn("ModifiedDate").AsDateTime().Nullable()
            .WithColumn("IsPublish").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

        // Indexes oluştur
        CreateIndexes();
    }

    public override void Down()
    {
        // Tabloları sil (ters sırada)
        Delete.Table("PropertyPhotos");
        Delete.Table("Favorites");
        Delete.Table("Messages");
        Delete.Table("Payments");
        Delete.Table("Reviews");
        Delete.Table("Reservations");
        Delete.Table("Properties");
        Delete.Table("AspNetUserTokens");
        Delete.Table("AspNetUserLogins");
        Delete.Table("AspNetUserClaims");
        Delete.Table("AspNetUserRoles");
        Delete.Table("AspNetRoles");
        Delete.Table("AspNetUsers");
    }

    private void CreateIndexes()
    {
        // AspNetUsers indexes
        Create.Index("IX_AspNetUsers_Email").OnTable("AspNetUsers").OnColumn("Email");
        Create.Index("IX_AspNetUsers_NormalizedEmail").OnTable("AspNetUsers").OnColumn("NormalizedEmail");
        Create.Index("IX_AspNetUsers_UserName").OnTable("AspNetUsers").OnColumn("UserName");
        Create.Index("IX_AspNetUsers_NormalizedUserName").OnTable("AspNetUsers").OnColumn("NormalizedUserName");

        // AspNetRoles indexes
        Create.Index("IX_AspNetRoles_NormalizedName").OnTable("AspNetRoles").OnColumn("NormalizedName");

        // Properties indexes
        Create.Index("IX_Properties_HostId").OnTable("Properties").OnColumn("HostId");
        Create.Index("IX_Properties_City").OnTable("Properties").OnColumn("City");
        Create.Index("IX_Properties_Country").OnTable("Properties").OnColumn("Country");
        Create.Index("IX_Properties_PropertyType").OnTable("Properties").OnColumn("PropertyType");
        Create.Index("IX_Properties_PricePerNight").OnTable("Properties").OnColumn("PricePerNight");
        Create.Index("IX_Properties_IsActive").OnTable("Properties").OnColumn("IsActive");

        // Reservations indexes
        Create.Index("IX_Reservations_GuestId").OnTable("Reservations").OnColumn("GuestId");
        Create.Index("IX_Reservations_PropertyId").OnTable("Reservations").OnColumn("PropertyId");
        Create.Index("IX_Reservations_CheckInDate").OnTable("Reservations").OnColumn("CheckInDate");
        Create.Index("IX_Reservations_CheckOutDate").OnTable("Reservations").OnColumn("CheckOutDate");
        Create.Index("IX_Reservations_Status").OnTable("Reservations").OnColumn("Status");

        // Reviews indexes
        Create.Index("IX_Reviews_GuestId").OnTable("Reviews").OnColumn("GuestId");
        Create.Index("IX_Reviews_PropertyId").OnTable("Reviews").OnColumn("PropertyId");
        Create.Index("IX_Reviews_ReservationId").OnTable("Reviews").OnColumn("ReservationId");
        Create.Index("IX_Reviews_Rating").OnTable("Reviews").OnColumn("Rating");
        Create.Index("IX_Reviews_IsApproved").OnTable("Reviews").OnColumn("IsApproved");

        // Payments indexes
        Create.Index("IX_Payments_UserId").OnTable("Payments").OnColumn("UserId");
        Create.Index("IX_Payments_ReservationId").OnTable("Payments").OnColumn("ReservationId");
        Create.Index("IX_Payments_Status").OnTable("Payments").OnColumn("Status");
        Create.Index("IX_Payments_TransactionId").OnTable("Payments").OnColumn("TransactionId");

        // Messages indexes
        Create.Index("IX_Messages_SenderId").OnTable("Messages").OnColumn("SenderId");
        Create.Index("IX_Messages_ReceiverId").OnTable("Messages").OnColumn("ReceiverId");
        Create.Index("IX_Messages_ReservationId").OnTable("Messages").OnColumn("ReservationId");
        Create.Index("IX_Messages_PropertyId").OnTable("Messages").OnColumn("PropertyId");
        Create.Index("IX_Messages_IsRead").OnTable("Messages").OnColumn("IsRead");

        // Favorites indexes
        Create.Index("IX_Favorites_UserId").OnTable("Favorites").OnColumn("UserId");
        Create.Index("IX_Favorites_PropertyId").OnTable("Favorites").OnColumn("PropertyId");

        // PropertyPhotos indexes
        Create.Index("IX_PropertyPhotos_PropertyId").OnTable("PropertyPhotos").OnColumn("PropertyId");
        Create.Index("IX_PropertyPhotos_IsMainPhoto").OnTable("PropertyPhotos").OnColumn("IsMainPhoto");
        Create.Index("IX_PropertyPhotos_SortOrder").OnTable("PropertyPhotos").OnColumn("SortOrder");
    }
} 