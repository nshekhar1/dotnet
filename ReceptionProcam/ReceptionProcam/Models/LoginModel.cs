using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReceptionProcam.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter user name.")]
        [Display(Name = "User Name")]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        //[StringLength(10)]
        public string Password { get; set; }

        public string Role { get; set; }

    }
}