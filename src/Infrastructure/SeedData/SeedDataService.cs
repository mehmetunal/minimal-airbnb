using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;
using MinimalAirbnb.Infrastructure.Data;

namespace MinimalAirbnb.Infrastructure.SeedData;

/// <summary>
/// Seed Data servisi
/// </summary>
public class SeedDataService
{
    private readonly MinimalAirbnbDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public SeedDataService(
        MinimalAirbnbDbContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Tüm seed data'yı oluşturur
    /// </summary>
    public async Task SeedAllAsync()
    {
        try
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedPropertiesAsync();
            await SeedReservationsAsync();
            await SeedReviewsAsync();
            await SeedPaymentsAsync();
            await SeedMessagesAsync();
            await SeedFavoritesAsync();
            await SeedPropertyPhotosAsync(); //
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException;
            var errorMessage = $"Seed data hatası: {ex.Message}";

            if (innerException != null)
            {
                errorMessage += $" | Inner Exception: {innerException.Message}";
            }

            throw new Exception(errorMessage, ex);
        }
    }

    /// <summary>
    /// Rolleri oluşturur
    /// </summary>
    private async Task SeedRolesAsync()
    {
        var roles = new[] { "Admin", "Host", "Guest" };

        foreach (var roleName in roles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }
    }

    /// <summary>
    /// Kullanıcıları oluşturur
    /// </summary>
    private async Task SeedUsersAsync()
    {
        // Admin kullanıcısı
        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User",
            UserType = UserType.Admin,
            IsVerified = true,
            IsActive = true
        };

        if (await _userManager.FindByEmailAsync(adminUser.Email) == null)
        {
            var result = await _userManager.CreateAsync(adminUser, "Super123!");
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Host kullanıcıları
        var hostFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.UserName, f => f.Internet.Email())
            .RuleFor(u => u.Email, (f, u) => u.UserName)
            .RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper())
            .RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper())
            .RuleFor(u => u.EmailConfirmed, f => true)
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.DateOfBirth, f => f.Date.Past(50, DateTime.Now.AddYears(-18)))
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.Address, f => f.Address.StreetAddress())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.Country, f => f.Address.Country())
            .RuleFor(u => u.PostalCode, f => f.Address.ZipCode())
            .RuleFor(u => u.UserType, UserType.Host)
            .RuleFor(u => u.IsVerified, f => f.Random.Bool(0.8f))
            .RuleFor(u => u.IsActive, f => f.Random.Bool(0.9f));

        var hosts = hostFaker.Generate(20);
        foreach (var host in hosts)
        {
            if (await _userManager.FindByEmailAsync(host.Email) == null)
            {
                var result = await _userManager.CreateAsync(host, "Password123!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(host, "Host");
                }
            }
        }

        // Guest kullanıcıları
        var guestFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.UserName, f => f.Internet.Email())
            .RuleFor(u => u.Email, (f, u) => u.UserName)
            .RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper())
            .RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper())
            .RuleFor(u => u.EmailConfirmed, f => true)
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.DateOfBirth, f => f.Date.Past(50, DateTime.Now.AddYears(-18)))
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.Address, f => f.Address.StreetAddress())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.Country, f => f.Address.Country())
            .RuleFor(u => u.PostalCode, f => f.Address.ZipCode())
            .RuleFor(u => u.UserType, UserType.Guest)
            .RuleFor(u => u.IsVerified, f => f.Random.Bool(0.7f))
            .RuleFor(u => u.IsActive, f => f.Random.Bool(0.9f));

        var guests = guestFaker.Generate(50);
        foreach (var guest in guests)
        {
            if (await _userManager.FindByEmailAsync(guest.Email) == null)
            {
                var result = await _userManager.CreateAsync(guest, "Password123!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(guest, "Guest");
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Evleri oluşturur
    /// </summary>
    private async Task SeedPropertiesAsync()
    {
        var hosts = await _context.Users
            .Where(u => u.UserType == UserType.Host)
            .ToListAsync();

        if (!hosts.Any()) return;

        var propertyFaker = new Faker<Property>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.HostId, f => f.PickRandom(hosts).Id)
            .RuleFor(p => p.Title, f => f.Lorem.Sentence(3, 8))
            .RuleFor(p => p.Description, f => f.Lorem.Paragraphs(2, 4))
            .RuleFor(p => p.PropertyType, f => f.PickRandom<PropertyType>())
            .RuleFor(p => p.Address, f => f.Address.StreetAddress())
            .RuleFor(p => p.City, f => f.PickRandom("İstanbul", "Ankara", "İzmir", "Antalya", "Bursa", "Adana", "Konya", "Gaziantep"))
            .RuleFor(p => p.Country, "Türkiye")
            .RuleFor(p => p.PostalCode, f => f.Address.ZipCode())
            .RuleFor(p => p.Latitude, f => f.Random.Double(36.0, 42.0))
            .RuleFor(p => p.Longitude, f => f.Random.Double(26.0, 45.0))
            .RuleFor(p => p.PricePerNight, f => f.Random.Decimal(100, 2000))
            .RuleFor(p => p.MaxGuestCount, f => f.Random.Int(1, 10))
            .RuleFor(p => p.BedroomCount, f => f.Random.Int(1, 5))
            .RuleFor(p => p.BedCount, f => f.Random.Int(1, 8))
            .RuleFor(p => p.BathroomCount, f => f.Random.Int(1, 4))
            .RuleFor(p => p.HasWifi, f => f.Random.Bool(0.9f))
            .RuleFor(p => p.HasAirConditioning, f => f.Random.Bool(0.8f))
            .RuleFor(p => p.HasKitchen, f => f.Random.Bool(0.9f))
            .RuleFor(p => p.HasParking, f => f.Random.Bool(0.7f))
            .RuleFor(p => p.HasPool, f => f.Random.Bool(0.3f))
            .RuleFor(p => p.AllowsPets, f => f.Random.Bool(0.4f))
            .RuleFor(p => p.AllowsSmoking, f => f.Random.Bool(0.2f))
            .RuleFor(p => p.ViewCount, f => f.Random.Int(0, 1000))
            .RuleFor(p => p.FavoriteCount, f => f.Random.Int(0, 100))
            .RuleFor(p => p.AverageRating, f => f.Random.Decimal(3.0m, 5.0m))
            .RuleFor(p => p.TotalReviews, f => f.Random.Int(0, 50))
            .RuleFor(p => p.IsPublish, true)
            .RuleFor(p => p.IsDeleted, false);

        var properties = propertyFaker.Generate(100);
        await _context.Properties.AddRangeAsync(properties);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Rezervasyonları oluşturur
    /// </summary>
    private async Task SeedReservationsAsync()
    {
        var properties = await _context.Properties.ToListAsync();
        var guests = await _context.Users
            .Where(u => u.UserType == UserType.Guest)
            .ToListAsync();

        if (!properties.Any() || !guests.Any()) return;

        var reservationFaker = new Faker<Reservation>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.GuestId, f => f.PickRandom(guests).Id)
            .RuleFor(r => r.PropertyId, f => f.PickRandom(properties).Id)
            .RuleFor(r => r.CheckInDate, f => f.Date.Future(1))
            .RuleFor(r => r.CheckOutDate, (f, r) => r.CheckInDate.AddDays(f.Random.Int(1, 14)))
            .RuleFor(r => r.GuestCount, f => f.Random.Int(1, 6))
            .RuleFor(r => r.TotalDays, (f, r) => (r.CheckOutDate - r.CheckInDate).Days)
            .RuleFor(r => r.PricePerNight, f => f.Random.Decimal(100, 500))
            .RuleFor(r => r.CleaningFee, f => f.Random.Decimal(50, 200))
            .RuleFor(r => r.ServiceFee, f => f.Random.Decimal(20, 100))
            .RuleFor(r => r.TotalPrice, (f, r) => (r.PricePerNight * r.TotalDays) + r.CleaningFee + r.ServiceFee)

            .RuleFor(r => r.Status, f => f.PickRandom<ReservationStatus>())
            .RuleFor(r => r.SpecialRequests, f => f.Random.Bool(0.3f) ? f.Lorem.Sentence() : null)
            .RuleFor(r => r.IsPublish, true)
            .RuleFor(r => r.IsDeleted, false);

        var reservations = reservationFaker.Generate(200);
        await _context.Reservations.AddRangeAsync(reservations);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Yorumları oluşturur
    /// </summary>
    private async Task SeedReviewsAsync()
    {
        var properties = await _context.Properties.ToListAsync();
        var guests = await _context.Users
            .Where(u => u.UserType == UserType.Guest)
            .ToListAsync();
        var reservations = await _context.Reservations.ToListAsync();

        if (!properties.Any() || !guests.Any()) return;

        var reviewFaker = new Faker<Review>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.GuestId, f => f.PickRandom(guests).Id)
            .RuleFor(r => r.PropertyId, f => f.PickRandom(properties).Id)
            .RuleFor(r => r.ReservationId, f => f.Random.Bool(0.7f) ? f.PickRandom(reservations).Id : null)
            .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
            .RuleFor(r => r.Comment, f => f.Lorem.Paragraph())
            .RuleFor(r => r.HostResponse, f => f.Random.Bool(0.3f) ? f.Lorem.Paragraph() : null)
            .RuleFor(r => r.IsApproved, f => f.Random.Bool(0.9f))
            .RuleFor(r => r.IsRejected, f => f.Random.Bool(0.05f))
            .RuleFor(r => r.LikeCount, f => f.Random.Int(0, 20))
            .RuleFor(r => r.DislikeCount, f => f.Random.Int(0, 5))
            .RuleFor(r => r.CreatedDate, f => f.Date.Past(6))
            .RuleFor(r => r.IsPublish, true)
            .RuleFor(r => r.IsDeleted, false);

        var reviews = reviewFaker.Generate(300);
        await _context.Reviews.AddRangeAsync(reviews);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Ödemeleri oluşturur
    /// </summary>
    private async Task SeedPaymentsAsync()
    {
        var reservations = await _context.Reservations.ToListAsync();
        var users = await _context.Users.ToListAsync();

        if (!reservations.Any() || !users.Any()) return;

        var paymentFaker = new Faker<Payment>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.UserId, f => f.PickRandom(users).Id)
            .RuleFor(p => p.ReservationId, f => f.PickRandom(reservations).Id)
            .RuleFor(p => p.Amount, f => f.Random.Decimal(500, 5000))
            .RuleFor(p => p.PaymentMethod, f => f.PickRandom<PaymentMethod>())
            .RuleFor(p => p.Status, f => f.PickRandom<PaymentStatus>())
            .RuleFor(p => p.TransactionId, f => f.Random.AlphaNumeric(20))
            .RuleFor(p => p.PaymentDate, f => f.Date.Past(6))
            .RuleFor(p => p.Currency, "TRY")
            .RuleFor(p => p.CreatedDate, f => f.Date.Past(6))
            .RuleFor(p => p.IsPublish, true)
            .RuleFor(p => p.IsDeleted, false);

        var payments = paymentFaker.Generate(150);
        await _context.Payments.AddRangeAsync(payments);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Mesajları oluşturur
    /// </summary>
    private async Task SeedMessagesAsync()
    {
        var users = await _context.Users.ToListAsync();
        var properties = await _context.Properties.ToListAsync();
        var reservations = await _context.Reservations.ToListAsync();

        if (!users.Any()) return;

        var messageFaker = new Faker<Message>()
            .RuleFor(m => m.Id, f => Guid.NewGuid())
            .RuleFor(m => m.SenderId, f => f.PickRandom(users).Id)
            .RuleFor(m => m.ReceiverId, (f, m) => f.PickRandom(users.Where(u => u.Id != m.SenderId)).Id)
            .RuleFor(m => m.PropertyId, f => f.Random.Bool(0.3f) ? f.PickRandom(properties).Id : null)
            .RuleFor(m => m.ReservationId, f => f.Random.Bool(0.2f) ? f.PickRandom(reservations).Id : null)
            .RuleFor(m => m.Subject, f => f.Lorem.Sentence(3, 8))
            .RuleFor(m => m.Content, f => f.Lorem.Paragraph())
            .RuleFor(m => m.IsRead, f => f.Random.Bool(0.7f))
            .RuleFor(m => m.MessageType, f => f.PickRandom<MessageType>())
            .RuleFor(m => m.Priority, f => f.PickRandom<MessagePriority>())
            .RuleFor(m => m.Category, f => f.PickRandom<MessageCategory>())
            .RuleFor(m => m.IsPublish, true)
            .RuleFor(m => m.IsDeleted, false);

        var messages = messageFaker.Generate(500);
        await _context.Messages.AddRangeAsync(messages);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Favorileri oluşturur
    /// </summary>
    private async Task SeedFavoritesAsync()
    {
        var users = await _context.Users
            .Where(u => u.UserType == UserType.Guest)
            .ToListAsync();
        var properties = await _context.Properties.ToListAsync();

        if (!users.Any() || !properties.Any()) return;

        var favoriteFaker = new Faker<Favorite>()
            .RuleFor(f => f.Id, f => Guid.NewGuid())
            .RuleFor(f => f.UserId, f => f.PickRandom(users).Id)
            .RuleFor(f => f.PropertyId, f => f.PickRandom(properties).Id)
            .RuleFor(f => f.Note, f => f.Random.Bool(0.3f) ? f.Lorem.Sentence() : null)
            .RuleFor(f => f.Category, f => f.PickRandom("Ev", "Villa", "Apartman", "Stüdyo"))
            .RuleFor(f => f.Tags, f => f.Random.Bool(0.4f) ? string.Join(",", f.PickRandom(new[] { "Deniz Manzarası", "Şehir Merkezi", "Sessiz", "Lüks", "Ekonomik" }, f.Random.Int(1, 3))) : null)
            .RuleFor(f => f.IsPrivate, f => f.Random.Bool(0.2f))
            .RuleFor(f => f.VisitCount, f => f.Random.Int(0, 50))
            .RuleFor(f => f.CreatedDate, f => f.Date.Past(6))
            .RuleFor(f => f.IsPublish, true)
            .RuleFor(f => f.IsDeleted, false);

        var favorites = favoriteFaker.Generate(300);
        await _context.Favorites.AddRangeAsync(favorites);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Ev fotoğraflarını oluşturur
    /// </summary>
    private async Task SeedPropertyPhotosAsync()
    {
        var properties = await _context.Properties.ToListAsync();

        if (!properties.Any()) return;

        var photoFaker = new Faker<PropertyPhoto>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.PropertyId, f => f.PickRandom(properties).Id)
            .RuleFor(p => p.PhotoUrl, f => f.Image.PicsumUrl(800, 600))
            .RuleFor(p => p.Caption, f => f.Random.Bool(0.5f) ? f.Lorem.Sentence() : null)
            .RuleFor(p => p.Description, f => f.Random.Bool(0.3f) ? f.Lorem.Paragraph() : null)
            .RuleFor(p => p.IsMainPhoto, f => f.Random.Bool(0.1f))
            .RuleFor(p => p.SortOrder, f => f.Random.Int(0, 10))
            .RuleFor(p => p.FileName, f => f.System.FileName("jpg"))
            .RuleFor(p => p.FileType, "image/jpeg")
            .RuleFor(p => p.FileSize, f => f.Random.Long(100000, 5000000))
            .RuleFor(p => p.Width, 800)
            .RuleFor(p => p.Height, 600)
            .RuleFor(p => p.Resolution, 72)
            .RuleFor(p => p.Quality, f => f.Random.Int(70, 95))
            .RuleFor(p => p.IsApproved, f => f.Random.Bool(0.9f))
            .RuleFor(p => p.IsRejected, f => f.Random.Bool(0.05f))
            .RuleFor(p => p.ViewCount, f => f.Random.Int(0, 1000))
            .RuleFor(p => p.LikeCount, f => f.Random.Int(0, 100))
            .RuleFor(p => p.CreatedDate, f => f.Date.Past(6))
            .RuleFor(p => p.IsPublish, true)
            .RuleFor(p => p.IsDeleted, false);

        var photos = photoFaker.Generate(500);
        await _context.PropertyPhotos.AddRangeAsync(photos);
        await _context.SaveChangesAsync();
    }
}
