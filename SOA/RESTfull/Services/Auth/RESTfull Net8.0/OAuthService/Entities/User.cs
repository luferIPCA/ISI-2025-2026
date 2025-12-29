

using Microsoft.AspNetCore.Identity;

/**
 * Entidade: User
 * 
 * Cifrar passwords (Hashed Passwords)
 * https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-8.0
 * Instalar o package "BCrypt.Net-Next"
 * 
 * Usar GUID
 * https://learn.microsoft.com/en-us/dotnet/api/system.guid.newguid?view=net-10.0
 *
 * lufer
 * **/
namespace AuthRest.Entities
{
    public class User
    {
        private readonly Guid id;
        public Guid Id { 
            get { return id; }
        }
        public string Email { set; get; }
        public string PasswordHash { private set; get; }
        public string Role { get; private set; }
        public int Age { get; set; }


        public User(string email, string passwordHash, int age, string role = "User" )
        {
            id = Guid.NewGuid();
            Email = email;
            //password é gravada cifrada
            PasswordHash = passwordHash;
            Role = role;
            Age = age;
        }
     
    }
}
