/**
 * DTO
 * */

namespace AuthRest.Entities
{
    public class UserDetailsResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
    }
}
