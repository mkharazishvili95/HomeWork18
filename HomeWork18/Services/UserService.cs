using HomeWork18.Data;
using HomeWork18.Domain;
using HomeWork18.Models;
using HomeWork18.Validation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork18.Services
{
    public interface IUserService
    {
        User Login(UserLoginModel model);
        User Register(UserRegisterModel registrationModel);
    }
    public class UserService : IUserService
    {
        public UserService(PersonContext context) {
            _context = context;
        }
        private readonly PersonContext _context;
        private readonly List<User> _users = new()
    {
            new User
            {
                Id = 2,
                FirstName = "User123",
                LastName = "User123",
                UserName = "User123",
                Password = "User123",
                Role = Roles.User
            }
        };
        private readonly List<User> _admins = new()
        {
            new User
            {
                Id = 1,
                FirstName = "Admin123",
                LastName = "Admin123",
                UserName = "Admin123",
                Password = "Admin123",
                Role = Roles.Admin

            }
            
        };


        public User Login(UserLoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return null;
            }
            var user = _context.Users.SingleOrDefault(x => x.UserName == loginModel.UserName);
            if (user == null)
            {
                return null;
            }
            if (user.Password != loginModel.Password)
            {
                return null;
            }
            _context.SaveChanges();
            return user;
        }
        public User Register(UserRegisterModel registrationModel)
        {
            var newUser = new User
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                UserName = registrationModel.UserName,
                Password = registrationModel.Password,
                Role = Roles.User
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

    }
}