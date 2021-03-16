using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Image_System.DTO
{
    public class ChangePasswordDTO
    {
        [Key]
        public int NIK { get; set; }

        [Required(ErrorMessage = "Required Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "New password and confirm password does not match")]
        public string ConfirmPassword { get; set; }

    }
}