using RestfulAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPI.Repositories
{
    public class UsersRepository
    {
        internal List<User> GetData()
        {
            List<User> list = new List<User>();

            User user = new User();
            user.Id = 1;
            user.Name = "Teste";
            list.Add(user);
            return list;
        }
    }
}
