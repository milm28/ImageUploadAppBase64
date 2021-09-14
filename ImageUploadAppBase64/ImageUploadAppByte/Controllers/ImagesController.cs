using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageUploadAppByte.ViewModel;
using ImageUploadAppByte.Models.DTOs;
using ImageUploadAppByte.Models;

namespace ImageUploadAppByte.Controllers
{
    public class ImagesController : Controller
    {
        private db_imgEntities db = new db_imgEntities();
        // GET: Images
        public ActionResult Index()
        {
            // Initialization.
            //sada nam ovaj model stize na View
            ImagesViewModel model = new ImagesViewModel 
            { 
                FileAttach = null, 
                ImageList= GetAllImages()
            };

            return View(model);
        }

        #region POST: /Img/Index

        /// <summary>
        /// POST: /Img/Index
        /// </summary>
        /// <param name="model">Model parameter</param>
        /// <returns>Return - Response information</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ImagesViewModel model)
        {
            // Initialization.
            string fileContent = string.Empty;
            string fileContentType = string.Empty;

            try
            {
                // Verification
                if (ModelState.IsValid)
                {
                    // Converting to bytes.
                    byte[] uploadedFile = new byte[model.FileAttach.InputStream.Length];
                    model.FileAttach.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                    // Initialization.
                    fileContent = Convert.ToBase64String(uploadedFile);
                    fileContentType = model.FileAttach.ContentType;

                    // Saving info.
                    tbl_file tbl_File = new tbl_file();

                    tbl_File.file_name = model.FileAttach.FileName;
                    tbl_File.file_ext = fileContentType;
                    tbl_File.file_base6 = fileContent;
                    db.tbl_file.Add(tbl_File);
                    db.SaveChanges();

                    //List<tbl_file> tbl_Files=db.tbl_file.ToList();
                    //List<ImageDTO> dtos = new List<ImageDTO>(tbl_Files.Count);

                    //foreach(var item in tbl_Files)
                    //{
                    //    dtos.Add(new ImageDTO
                    //    {
                    //        ImageID = item.file_id,
                    //        ImageName = item.file_name,
                    //        ImageContentType = item.file_base6
                    //    });
                    //}

                    //vracaju se slike na view sa modelom
                    model.ImageList = GetAllImages();
                }
                
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info
            return this.View(model);
        }

        #endregion

        public ActionResult DownloadFile(int fileId)
        {
            // Model binding.
            ImagesViewModel model = new ImagesViewModel
            {
                FileAttach = null,
                ImageList = new List<ImageDTO>()
            };

            try
            {
                // Loading dile info.
                tbl_file tbl_File = db.tbl_file.FirstOrDefault(x => x.file_id == fileId);
                return GetFile(tbl_File.file_base6,tbl_File.file_ext);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info.
            return this.View(model);
        }

        private FileResult GetFile(string fileContent, string fileContentType)
        {
            // Initialization.
            FileResult file = null;

            try
            {
                // Get file.
                byte[] byteContent = Convert.FromBase64String(fileContent);
                file = this.File(byteContent, fileContentType);
            }
            catch (Exception ex)
            {
                // Info.
                throw ex;
            }

            // info.
            return file;
        }

        private List<ImageDTO> GetAllImages()
        {
            List<tbl_file> tbl_Files = db.tbl_file.ToList();
            List<ImageDTO> dtos = new List<ImageDTO>(tbl_Files.Count);

            foreach (var item in tbl_Files)
            {
                dtos.Add(new ImageDTO
                {
                    ImageID = item.file_id,
                    ImageName = item.file_name,
                    ImageContentType = item.file_base6
                });
            }

            return dtos;
        }
    }
}