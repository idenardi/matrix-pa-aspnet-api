using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Matrix.PA.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
