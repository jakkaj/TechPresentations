using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniModel.Contract.Service;
using MiniModel.Entity.Auth;

namespace MiniModel.Service.Services
{
    public class UserService : IUserService
    {
        private static User _user = null;
        public async Task SetCurrentUser(User u)
        {
            _user = u;
        }
        public async Task<User> GetCurrentUser()
        {
            await Task.Delay(1000);

            return _user;
        }
    }
}
