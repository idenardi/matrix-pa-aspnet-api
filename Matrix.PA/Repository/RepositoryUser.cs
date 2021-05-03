using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrix.PA.Models;

namespace Matrix.PA.Repository
{
    public class RepositoryUser : IRepository<UserModel>
    {
        private static List<UserModel> MockUser = new List<UserModel>()
        {
            //{ "sup", new UserModel() { Id = 2, Username="sup", Password="123", Email = "sup@matrixsaude.com", IsAdmin = false,  } }
        };

        public RepositoryUser()
        {
            MockUser.Add(new UserModel() { Id = 1, Username = "admin", Password = "123", Email = "adm@matrixsaude.com", IsAdmin = true, });
        }


        public List<UserModel> All()
        {
            return MockUser;
        }

        public UserModel ById(long id)
        {
            return MockUser.FirstOrDefault(u => u.Id == id);
        }

        public bool Save(UserModel newUser)
        {
            try
            {
                if (newUser.Id > 0)
                {
                    var oldUser = MockUser.FirstOrDefault(u => u.Id == newUser.Id);
                    oldUser.Email = newUser.Email;
                    oldUser.IsAdmin = newUser.IsAdmin;
                    oldUser.Password = newUser.Password;
                    oldUser.Updated = DateTime.Now;

                    return true;
                }
                else
                {
                    newUser.Id = MockUser.Max(u => u.Id) + 1;

                    newUser.Created = DateTime.Now;
                    newUser.Updated = DateTime.Now;

                    if (MockUser.All(u => u.Username != newUser.Username))
                    {
                        MockUser.Add(newUser);
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        public bool Delete(long id)
        {
            try
            {
                var deletedUser = MockUser.FirstOrDefault(u => u.Id == id);
                MockUser.Remove(deletedUser);

                return true;
            }
            catch { }
            return false;
        }
    }
}
