using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ImageUploadAppByte.Models.DTOs;

namespace ImageUploadAppByte.ViewModel
{
    public class ImagesViewModel
    {
        [Required]
        [Display(Name = "Upload File")]
        public HttpPostedFileBase FileAttach { get; set; }

        /// <summary>
        /// Gets or sets Image file list.
        /// </summary>
        public List<ImageDTO> ImageList{ get; set; }
    }
}