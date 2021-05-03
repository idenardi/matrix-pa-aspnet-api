using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matrix.PA.Models.Api
{
    public class UserApi
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public UserModel ToModel()
        {
            return new UserModel()
            {
                Id = this.Id,
                Username = this.Username,
                Password = this.Password,
                Email = this.Email,
                IsAdmin = this.IsAdmin
            };
        }
    }
}
