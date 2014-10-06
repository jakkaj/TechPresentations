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
        public async Task<User> GetCurrentUser()
        {
            await Task.Delay(2000);

            return new User()
            {
                Handle = "Jakkaj",
                Id = Guid.NewGuid()
            };
        }
    }
}
