using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileManager.DBAccess.Entities
{
    public class User
    {
        //[Key]
        public int Id { get; set; }
        [Display(Name = "رده")]
        [Required(ErrorMessage = "{0} راوارد نمایید")]
        public string State { get; set; }
        [Display(Name = "نام ونام خانوادگی")]
        [Required(ErrorMessage = "{0} راوارد نمایید")]
        public string nameandfamily { get; set; }
        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "{0} راوارد نمایید")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "رمز ورود")]
        [Required(ErrorMessage = "{0} راوارد نمایید")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} راوارد نمایید")]
        public string username { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} راوارد نمایید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
