using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrix.PA.Models.Api;

namespace Matrix.PA.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public UserApi ToApi()
        {
            return new UserApi()
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
