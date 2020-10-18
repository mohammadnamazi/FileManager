using FileManager.DBAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Core.Interfaces
{
    public interface IUser
    {
        bool IfUserNameExist(string UserName);
        int AddUser(User user);

        User LoginUser(string UserName, string Password);

        User ForgetPassword(string UserName);

        bool ResetPasswor(string UserName, string Password);
        bool ChangePassword(string UserName, string CurentPassword, string Password);
        int GetUserId(string UserName);
        string GetUserState(string UserName);
        bool CheckUserState(string State, string UserName);
        bool RemoveUser(int id);
    }
}
