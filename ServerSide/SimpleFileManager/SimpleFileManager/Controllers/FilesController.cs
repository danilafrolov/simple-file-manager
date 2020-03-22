using SimpleFileManager.Helpers;
using SimpleFileManager.Models;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SimpleFileManager.Controllers
{
    public class FilesController : Controller
    {
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult GetUserFiles() {
            try {
                var fileDtos = FileHelper.GetUserFiles();
                foreach (var file in fileDtos) {
                    SetFileUrl(file);
                }
                return new JsonNetResult(fileDtos);
            }
            catch (Exception ex) {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file) {
            if (file == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Отсутствует файл для загрузки.");
            }

            try {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath($"~/{FileHelper.GetFilesStorageFolderName()}/{fileName}"));


                var fileInfo = FileHelper.GetFileInfo(fileName);
                var fileDto = new FileDto(fileInfo);
                SetFileUrl(fileDto);
                return new JsonNetResult(fileDto);
            }
            catch (Exception ex) {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        [HttpGet]
        public ActionResult DeleteFile(string filename) {
            try {
                var fileInfo = FileHelper.GetFileInfo(filename);
                fileInfo.Delete();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        private void SetFileUrl(FileDto file) {
            if (FileHelper.ImageExtensions.Contains(file.Extension.ToUpper())) {
                var url = new Uri(Request.Url, Url.Content($"~/{FileHelper.GetFilesStorageFolderName()}/{file.Name}"));
                file.ContentUrl = url.ToString();
            }
        }
    }
}