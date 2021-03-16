using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Image_System.DTO
{
    public class LoginDTO
    {
        [Key]
        [Required(ErrorMessage = "Required NIK")]

        public string NIK { get; set; }

        public string Username { get; set; }

        [Required(ErrorMessage = "Required Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }

        public int RoleID { get; set; }
        public int MenuID { get; set; }

    }

    public class ChangePasswordExpiredDTO
    {
        [Key]
        public string NIK { get; set; }

        [Required(ErrorMessage = "Required Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "New password and confirm password does not match")]
        public string ConfirmPassword { get; set; }

    }

    public class LoginResult
    {
        public LoginStatus Status { get; set; }

        public int AttemptsRemaining { get; set; }
        public int RoleID { get; set; }
        public int MenuID { get; set; }

    }

    public enum LoginStatus : int
    {
        Authorized = 0,
        InvalidCredentials = 1,
        Suspended = 2

    }

}