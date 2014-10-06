using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniModel.Entity.Auth;

namespace MiniModel.Contract.Service
{
    public interface IUserService
    {
        Task<User> GetCurrentUser();
        Task SetCurrentUser(User u);
    }
}
