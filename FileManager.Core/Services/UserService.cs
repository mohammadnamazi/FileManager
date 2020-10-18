using FileManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FileManager.DBAccess.Context;
using FileManager.DBAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FileManager.Core.Classes;

namespace FileManager.Core.Services
{
    public class UserService : IUser
    {
        FileManagerContext _context;

        public UserService(FileManagerContext context)
        {
            _context = context;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public bool ChangePassword(string MobileNumber, string CurentPassword, string Password)
        {
            var HashCurentPassword = HashGenerator.EncodingPassWMD5(Password);
            var user = _context.Users.FirstOrDefault(u => u.username == MobileNumber && u.password == HashCurentPassword);

            if (user != null)
            {
                user.password = HashCurentPassword;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckUserRole(string State, string UserName)
        {
            throw new NotImplementedException();
        }

        public bool CheckUserState(string StateName, string UserName)
        {
            return _context.Users.Include(u => u.State).Any(u => u.username == UserName && u.State == StateName);
        }

        public User ForgetPassword(string MobileNumber)
        {
            return _context.Users.FirstOrDefault(u => u.username == MobileNumber);
        }

        public int GetUserId(string UserName)
        {
            var User = _context.Users.FirstOrDefault(u => u.username == UserName);
            return User.Id;
        }
        public string GetUserState(string UserName)
        {
            var User = _context.Users.FirstOrDefault(u => u.username == UserName);
            return User.State;
        }

        public bool IfUserNameExist(string UserName)
        {
            return _context.Users.Any(u => u.username == UserName);
        }

        public User LoginUser(string UserName, string Password)
        {
            string HashPassword = HashGenerator.EncodingPassWMD5(Password);
            return _context.Users.FirstOrDefault(u => u.username == UserName && u.password == HashPassword);
        }

        public bool RemoveUser(int id)
        {
            var ads = _context.Users.Find(id);
            _context.Remove(ads);
            _context.SaveChanges();
            return true;
        }

        public bool ResetPasswor(string UserName, string Password)
        {
            var user = _context.Users.FirstOrDefault(u => u.username == UserName && u.State != "Block");
            if (user != null)
            {
                string HashP = HashGenerator.EncodingPassWMD5(Password);
                user.password = HashP;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
