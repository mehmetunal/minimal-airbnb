using MinimalAirbnb.Application.Interfaces;
using MinimalAirbnb.Domain.Entities;
using MinimalAirbnb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MinimalAirbnb.Infrastructure.Repositories;

/// <summary>
/// Kullanıcı Repository Implementasyonu
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly MinimalAirbnbDbContext _context;

    public UserRepository(MinimalAirbnbDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .ToListAsync();
    }

    public async Task<User> AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        return entity;
    }

    public async Task<User> UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<IEnumerable<User>> GetByUserTypeAsync(Domain.Enums.UserType userType)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
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
            .Include(u => u.Payments)
            .Where(u => u.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetVerifiedUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .Where(u => u.IsVerified)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersByCityAsync(string city)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .Where(u => u.City == city)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> UserNameExistsAsync(string userName)
    {
        return await _context.Users.AnyAsync(u => u.UserName == userName);
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }

    public async Task<IEnumerable<User>> GetEmailConfirmedUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
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
            .Include(u => u.Payments)
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
            .Include(u => u.Payments)
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
            .Include(u => u.Payments)
            .Where(u => u.LockoutEnd.HasValue && u.LockoutEnd > DateTimeOffset.UtcNow)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetByCityAsync(string city)
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .Where(u => u.City == city)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetHostsAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .Where(u => u.UserType == Domain.Enums.UserType.Host)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetGuestsAsync()
    {
        return await _context.Users
            .Include(u => u.Properties)
            .Include(u => u.Reservations)
            .Include(u => u.Reviews)
            .Include(u => u.Favorites)
            .Include(u => u.Payments)
            .Where(u => u.UserType == Domain.Enums.UserType.Guest)
            .ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}