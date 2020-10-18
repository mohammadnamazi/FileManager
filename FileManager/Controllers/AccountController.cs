using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileManager.Core.ViewModel;
using FileManager.Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace FileManager.Controllers
{
    
    public class AccountController : Controller
    {
        private IUser _iuser;

        public AccountController(IUser iuser)
        {
            _iuser = iuser;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = _iuser.LoginUser(login.UserName, login.Password);
               

                if (user != null)
                {

                    var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                new Claim(ClaimTypes.Name, user.username)
                            };

                    var identity = new ClaimsIdentity(claims, "UserCookie");
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RemmberMe
                    };

                    HttpContext.SignInAsync(principal, properties);

                    if (_iuser.GetUserState(user.username) == "Admin")
                    {
                        return RedirectToAction("Index", "Users");
                    }
                    else if (_iuser.GetUserState(user.username) == "User")
                    {
                        return RedirectToAction("FileManager", "FileManager");
                    }
                    else
                    {
                        ModelState.AddModelError("Mobile", "حساب شما در سایت مسدود شده است برای رفع انسداد با مدیر تماس بگیرید");
                        return View(login);
                    }

                    
                }
                else
                {
                    ModelState.AddModelError("Password", "مشخصات کاربری صحیح نیست");

                    return View(login);
                }
            }
            else
            {
                return View(login);
            }

        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("UserCookie");
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult Forgetpassword()
        {
            return View();
        }
    }
}
