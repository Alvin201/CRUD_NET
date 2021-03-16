using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Image_System.DTO
{
    public class MenuDTO
    {
        public int IDMenu { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }
        public string Fa_Awesome { get; set; }
    }
}