using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileManager.Core.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} راوارد نمایید")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [MaxLength(50, ErrorMessage = "مقدار {0}نباید بیشتر از {1} باشد ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemmberMe { get; set; }
    }
}
