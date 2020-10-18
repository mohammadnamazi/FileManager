using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Controllers
{
    public class FileManagerController : Controller
    {
        [Authorize]
        public IActionResult FileManager()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
