using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeasRepository.Models;
using System.Data.Entity;

namespace IdeasRepository.Services
{
    public class UserService : IUserService 
    {
        public bool Login(LoginData loginData)
        {
            User user = GetByEmail(loginData.Email);
            if (user.Password.Equals(loginData.Password))
            {
                return true;
            }
            return false;
        }

        public void Save(User user) 
        {
            using (IdeasContext context = new IdeasContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public User GetById(int id)
        {
            using (IdeasContext context = new IdeasContext())
            {
                return context.Users.First(u => u.Id == id);
            }
        }

        public User GetByEmail(string email)
        {
            using (IdeasContext context = new IdeasContext())
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        public bool IsUserExists(User user) 
        {
            User oldUser = GetByEmail(user.Email);
            if (oldUser == null)
            {
                return false; 
            }
            return true;
        }

    }
}