using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Domain.Entities;

namespace MinimalAirbnb.Infrastructure.Data;

/// <summary>
/// MinimalAirbnb Veritabanı Context'i
/// </summary>
public class MinimalAirbnbDbContext : IdentityDbContext<User, Microsoft.AspNetCore.Identity.IdentityRole<Guid>, Guid>
{
    public MinimalAirbnbDbContext(DbContextOptions<MinimalAirbnbDbContext> options) : base(options)
    {
        this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    // Entity DbSet'leri
    public DbSet<Property> Properties { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<PropertyPhoto> PropertyPhotos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Entity konfigürasyonları
        ConfigureEntities(builder);
    }

    private void ConfigureEntities(ModelBuilder builder)
    {
        // Property konfigürasyonu
        builder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
            entity.Property(e => e.City).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Country).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.PricePerNight).HasPrecision(18, 2);
            entity.Property(e => e.CleaningFee).HasPrecision(18, 2);
            entity.Property(e => e.ServiceFee).HasPrecision(18, 2);
            entity.Property(e => e.AverageRating).HasPrecision(3, 2);

            // Enum konfigürasyonları
            entity.Property(e => e.PropertyType).HasConversion<int>();

            // Navigation property'ler
            entity.HasOne(e => e.Host)
                .WithMany(e => e.Properties)
                .HasForeignKey(e => e.HostId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Reservations)
                .WithOne(e => e.Property)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Reviews)
                .WithOne(e => e.Property)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Photos)
                .WithOne(e => e.Property)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Favorites)
                .WithOne(e => e.Property)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Reservation konfigürasyonu
        builder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalPrice).HasPrecision(18, 2);
            entity.Property(e => e.PricePerNight).HasPrecision(18, 2);
            entity.Property(e => e.CleaningFee).HasPrecision(18, 2);
            entity.Property(e => e.ServiceFee).HasPrecision(18, 2);
            entity.Property(e => e.SpecialRequests).HasMaxLength(1000);
            entity.Property(e => e.CancellationReason).HasMaxLength(500);
            
            // Enum konfigürasyonları
            entity.Property(e => e.Status).HasConversion<int>();

            // Navigation property'ler
            entity.HasOne(e => e.Guest)
                .WithMany(e => e.Reservations)
                .HasForeignKey(e => e.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Property)
                .WithMany(e => e.Reservations)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.CancelledByUser)
                .WithMany()
                .HasForeignKey(e => e.CancelledByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ConfirmedByUser)
                .WithMany()
                .HasForeignKey(e => e.ConfirmedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Payments)
                .WithOne(e => e.Reservation)
                .HasForeignKey(e => e.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Review konfigürasyonu
        builder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Comment).IsRequired().HasMaxLength(2000);
            entity.Property(e => e.HostResponse).HasMaxLength(2000);
            entity.Property(e => e.HostResponseDate);

            // Navigation property'ler
            entity.HasOne(e => e.Guest)
                .WithMany(e => e.Reviews)
                .HasForeignKey(e => e.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Property)
                .WithMany(e => e.Reviews)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Reservation)
                .WithMany(e => e.Reviews)
                .HasForeignKey(e => e.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.HostResponseUser)
                .WithMany()
                .HasForeignKey(e => e.HostResponseUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ModeratedByUser)
                .WithMany()
                .HasForeignKey(e => e.ModeratedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Payment konfigürasyonu
        builder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasPrecision(18, 2);
            entity.Property(e => e.TransactionId).HasMaxLength(100);
            
            // Enum konfigürasyonları
            entity.Property(e => e.PaymentMethod).HasConversion<int>();
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.ProviderReferenceId).HasMaxLength(100);
            entity.Property(e => e.RefundReason).HasMaxLength(500);
            entity.Property(e => e.ErrorMessage).HasMaxLength(1000);
            entity.Property(e => e.RefundAmount).HasPrecision(18, 2);

            // Navigation property'ler
            entity.HasOne(e => e.User)
                .WithMany(e => e.Payments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Reservation)
                .WithMany(e => e.Payments)
                .HasForeignKey(e => e.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Message konfigürasyonu
        builder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(2000);
            
            // Enum konfigürasyonları
            entity.Property(e => e.MessageType).HasConversion<int>();
            entity.Property(e => e.Priority).HasConversion<int>();
            entity.Property(e => e.Category).HasConversion<int>();

            // Navigation property'ler
            entity.HasOne(e => e.Sender)
                .WithMany(e => e.SentMessages)
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Receiver)
                .WithMany(e => e.ReceivedMessages)
                .HasForeignKey(e => e.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Property ilişkisi kaldırıldı çünkü Entity Framework otomatik kolon oluşturuyor

            // Reservation ilişkisi kaldırıldı çünkü Entity Framework otomatik kolon oluşturuyor

            // Self-referencing relationship for replies
            entity.HasOne(e => e.ReplyToMessage)
                .WithMany(e => e.Replies)
                .HasForeignKey(e => e.ReplyToMessageId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ArchivedByUser)
                .WithMany()
                .HasForeignKey(e => e.ArchivedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Favorite konfigürasyonu
        builder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Navigation property'ler
            entity.HasOne(e => e.User)
                .WithMany(e => e.Favorites)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Property)
                .WithMany(e => e.Favorites)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique constraint: Bir kullanıcı aynı evi birden fazla kez favorilere ekleyemez
            entity.HasIndex(e => new { e.UserId, e.PropertyId }).IsUnique();
        });

        // PropertyPhoto konfigürasyonu
        builder.Entity<PropertyPhoto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PhotoUrl).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Caption).HasMaxLength(200);
            entity.Property(e => e.FileType).HasMaxLength(50);

            // Navigation property'ler
            entity.HasOne(e => e.Property)
                .WithMany(e => e.Photos)
                .HasForeignKey(e => e.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
} 