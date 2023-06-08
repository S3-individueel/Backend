using Microsoft.AspNetCore.Identity;

namespace VolksmondAPI.Models
{
    public class Account : IdentityUser
    {
        public int Id { get; set; }
        public Citizen? citizen { get; set; }
    }
}
