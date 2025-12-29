/*
 * lufer
 * ISI
 * OAuth
 * See https://dotnetcorecentral.com/blog/asp-net-core-authorization/
 * */
using System.ComponentModel.DataAnnotations;

namespace WS_Rest1.Models
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
