using LoginSignupApi.Models;
using System.Threading.Tasks;

namespace LoginSignupApi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
        Task AddUserAsync(User user);
        Task<bool> UserExistsAsync(string username, string email);
        Task SaveChangesAsync();
    }
}
