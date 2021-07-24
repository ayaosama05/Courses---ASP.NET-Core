using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursatyApp.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(200,MinimumLength =2)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember Me?")]
        public bool RememberMe { get; set; }

        public string LoginInValid { get; set; }
    }
}
