/**
 * Repository de Utilizadores
 * Simular DAL
 * 
 * Instalar package "BCrypt.Net-Next" para cifrar passwords
 **/
using AuthRest.Entities;

namespace AuthRest.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> users = new List<User>();

        public UserRepository()
        {
            users.Add(new User("email1", BCrypt.Net.BCrypt.HashPassword("pass1"),19,"User"));
            users.Add(new User("email2", BCrypt.Net.BCrypt.HashPassword("pass2"),20,"Guest"));
            users.Add(new User("email3", BCrypt.Net.BCrypt.HashPassword("pass3"),17,"Admin"));
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<User?> GetByEmailAsync(string email)
        {
            var user = users.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }

        /// <summary>
        /// Get user by email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public Task<User?> GetUserByEmailAndPassword(string email, string pass)
        {
            User? u = users.FirstOrDefault(x => (x.Email == email && BCrypt.Net.BCrypt.Verify(pass, x.PasswordHash)));
            return Task.FromResult(u);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            List<User> aux = new List<User>(users); //MELHORAR
            return aux;
        }
    }
}
