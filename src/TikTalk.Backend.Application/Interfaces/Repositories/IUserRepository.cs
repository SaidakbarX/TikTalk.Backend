using TikTalk.Backend.Domain.Entities;

namespace TikTalk.Backend.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> IsUsernameTakenAsync(string username);
    Task AddAsync(User user);
    void Update(User user);
    void Delete(User user);
    Task SaveChangesAsync();
}