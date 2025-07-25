using Microsoft.EntityFrameworkCore;
using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Domain.Enums;
using MinimalAirbnb.Infrastructure.Data;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// User Repository Implementation
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public UserRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tüm kullanıcıları getir (IQueryable)
    /// </summary>
    public IQueryable<User> GetAll()
    {
        return _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Select(u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                DateOfBirth = u.DateOfBirth,
                UserName = u.UserName,
                ProfilePicture = u.ProfilePicture,
                ProfilePictureUrl = u.ProfilePictureUrl,
                Bio = u.Bio,
                Address = u.Address,
                City = u.City,
                Country = u.Country,
                PostalCode = u.PostalCode,
                UserType = u.UserType,
                IsVerified = u.IsVerified,
                IsActive = u.IsActive,
                LastLoginDate = u.LastLoginDate,
                LoginCount = u.LoginCount
            })
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <summary>
    /// Kullanıcıyı tüm navigation property'ler ile getir
    /// </summary>
    public async Task<User?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .FirstOrDefaultAsync(u => u.Id == id);
    }



    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Select(u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                DateOfBirth = u.DateOfBirth,
                UserName = u.UserName,
                ProfilePicture = u.ProfilePicture,
                ProfilePictureUrl = u.ProfilePictureUrl,
                Bio = u.Bio,
                Address = u.Address,
                City = u.City,
                Country = u.Country,
                PostalCode = u.PostalCode,
                UserType = u.UserType,
                IsVerified = u.IsVerified,
                IsActive = u.IsActive,
                LastLoginDate = u.LastLoginDate,
                LoginCount = u.LoginCount
            })
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }

    public async Task<IEnumerable<User>> GetByUserTypeAsync(UserType userType)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .Where(u => u.UserType == userType)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .Where(u => u.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetEmailConfirmedUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .Where(u => u.EmailConfirmed)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetPhoneConfirmedUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .Where(u => u.PhoneNumberConfirmed)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetTwoFactorEnabledUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .Where(u => u.TwoFactorEnabled)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetLockedOutUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .Where(u => u.LockoutEnd > DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetByCityAsync(string city)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .Where(u => u.City.Contains(city))
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetHostsAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .Where(u => u.UserType == UserType.Host)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetGuestsAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.SentMessages)
            .Include(u => u.ReceivedMessages)
            .Where(u => u.UserType == UserType.Guest)
            .ToListAsync();
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return user;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}