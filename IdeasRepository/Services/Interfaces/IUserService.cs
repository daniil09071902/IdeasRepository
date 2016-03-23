using IdeasRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeasRepository.Services
{
    public interface IUserService
    {
        bool Login(LoginData loginData);
        void Save(User user);
        User GetById(int id);
        User GetByEmail(string email);
        bool IsUserExists(User user);
    }
}
