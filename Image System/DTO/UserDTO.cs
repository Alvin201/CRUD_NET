using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Image_System.DTO
{
    public class UserDTO
    {
        public int NIK { get; set; }

        [Required(ErrorMessage = "Required Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string CurrentStatus { get; set; }

        //[DataType(DataType.Text)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string LastLoginDate { get; set; }
    }

}