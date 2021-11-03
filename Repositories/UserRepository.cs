using System.Collections.Generic;
using System.Linq;
using AnimalsApi.Models;

namespace AnimalsApi.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { username = "admin", password = "admin@123"});

            return users.Where(x => x.username.ToLower() == username.ToLower() && x.password == x.password).FirstOrDefault();
        }
    }
}