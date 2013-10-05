using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Teleritter.Models
{
    public class UserAdminViewModel
    {
        public string Id { get; set; }

        [UIHint("String")]
        [Required, StringLength(100, ErrorMessage = "Username is 5 to 1000 symols long!", MinimumLength = 5)]
        public string Username { get; set; }

        internal static UserAdminViewModel FromUser(ApplicationUser applicationUser)
        {
            return new UserAdminViewModel
            {
                Id = applicationUser.Id,
                Username = applicationUser.UserName
            };
        }
    }
}