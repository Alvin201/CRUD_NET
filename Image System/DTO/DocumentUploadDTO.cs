using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Image_System.DTO
{
    public class DocumentUploadDTO 
    {
        [DisplayName("ID")]
        public string ID { get; set; }

        [DisplayName("File Name")]
        [Required(ErrorMessage = "Filename is required")]
        public string FileName { get; set; }

        [DisplayName("Status")]
        public string CurrentStatus { get; set; }



    }
}