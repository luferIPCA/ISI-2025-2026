
using AuthRest.Entities;

namespace AuthRest.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAndPassword(string email, string pass);
        Task<User?> GetByEmailAsync(string email);
        public List<User> GetAll();
    }
}
