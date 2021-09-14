using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageUploadAppByte.Models.DTOs
{
    public class ImageDTO
    {
        /// <summary>
        /// dto koji predstavlja sliku iz baze podataka
        /// </summary>
        public int ImageID { get; set; }

        public string ImageName { get; set; }

        public string ImageContentType { get; set; }

    }
}