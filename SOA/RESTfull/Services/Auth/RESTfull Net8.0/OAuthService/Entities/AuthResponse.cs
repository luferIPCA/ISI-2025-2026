namespace AuthRest.Entities
{
    public class AuthResponse
    {
            public string Name { get; set; }
            public string Token { get; set; }

            public DateTime Expiration { get; set; }

            public AuthResponse() { }
            public AuthResponse(string user, string token)
            {
                Name = user;
                Token = token;
                Expiration = DateTime.Now.AddMinutes(120);
            }

            public AuthResponse(User user, string token, DateTime expires)
            {
                Name = "";
                Token = token;
                Expiration = expires;
            }
    }
}
