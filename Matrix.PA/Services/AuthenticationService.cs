using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrix.PA.Models;
using Matrix.PA.Repository;

namespace Matrix.PA.Services
{
    public interface IAuthenticationService
    {
        bool TrySignIn(string login, string password, out UserModel usermodel);
    }



    public class AuthenticationService : IAuthenticationService
    {
        public static string SecurityKey = "matrix-webapi-authentication-valid";
        public readonly IRepository<UserModel> _repositoryUser;

        public AuthenticationService(IRepository<UserModel> repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public bool TrySignIn(string login, string password, out UserModel usermodel)
        {
            usermodel = _repositoryUser.All().FirstOrDefault(u => u.Username == login.ToLower() && u.Password == password);
            return usermodel != null;
        }
    }
}
