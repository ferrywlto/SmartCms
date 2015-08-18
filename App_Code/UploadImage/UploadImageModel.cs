using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartCms.App_Code
{
    public class UploadImageModel
    {
        [Required]
        public DateTime UploadDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public HttpPostedFileBase UploadFile { get; set; }
    }
}