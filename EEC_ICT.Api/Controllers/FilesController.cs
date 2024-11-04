using EEC_ICT.Api.Providers;
using EEC_ICT.Api.Services;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace EEC_ICT.Api.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : BaseController
    {
        [HttpPost]
        [Route("ckeditor-images")]
        //[Permission]
        public async Task<object> ImageUpload()
        {
            try
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                WebImage img = new WebImage(file.InputStream);
                //if (img.Width > 600)
                //    img.Resize(600, 600);
                Regex regex = new Regex(@"api/files/ckeditor-images");
                string serverIp = regex.Replace(HttpContext.Current.Request.Url.ToString(), "");
                Logger.Info(HttpContext.Current.Request.JSONSerializer());

                string folderPath = "/Uploads/CkEditor/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(folderPath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderPath));
                }
                var baseUrl = AppDomain.CurrentDomain.BaseDirectory + folderPath;
                int total;
                try
                {
                    total = HttpContext.Current.Request.Files.Count;
                }
                catch (Exception ex)
                {
                    return await Task.FromResult(new { error = new { message = "Lỗi upload!" } });
                }
                if (total == 0)
                {
                    return await Task.FromResult(new { error = new { message = "Không tồn tại file tải lên!" } });
                }
                string fileName = file.FileName;
                if (fileName == "")
                {
                    return await Task.FromResult(new { error = new { message = "Không tồn tại file tải lên!" } });
                }
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                string newPath = baseUrl + newFileName;
                img.Save(newPath);
                //return await Task.FromResult(new { url = serverIp + folderPath + newFileName });
                return await Task.FromResult(new { url = folderPath + newFileName });
            }
            catch (Exception exeption)
            {
                return await Task.FromResult(new { error = new { message = exeption.Message } });
            }
        }

        [HttpPost]
        [Route("uploadvideo")]
        [Permission]
        public object UploadVideo()
        {
            Logger.Info("[UploadVideo]");
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = null;
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Video/")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Video/"));
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Images/VideoAvatar")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Images/VideoAvatar"));
            }

            // check file from client
            if (HttpContext.Current.Request.Files.Count < 2)
            {
                retval.Status.Message = "Số file nhận được không đúng";
                retval.Status.Code = -1;
                return retval;
            }

            try
            {
                List<string> uniqueFileNames = new List<string>();
                string now = DateTime.Now.ToString("yyyyMMddhhmmss");
                // video
                var receivedFile = HttpContext.Current.Request.Files[0];
                // create unique file name
                var uniqueFileName = Path.GetFileNameWithoutExtension(receivedFile.FileName) + "_" + now + Path.GetExtension(receivedFile.FileName);
                var fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Video"), uniqueFileName);
                receivedFile.SaveAs(fullPath);

                uniqueFileNames.Add(uniqueFileName);

                // image
                receivedFile = HttpContext.Current.Request.Files[1];
                // create unique file name
                uniqueFileName = Path.GetFileNameWithoutExtension(receivedFile.FileName) + "_" + now + Path.GetExtension(receivedFile.FileName);
                fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Images/VideoAvatar"), uniqueFileName);
                receivedFile.SaveAs(fullPath);

                uniqueFileNames.Add(uniqueFileName);

                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = uniqueFileNames;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }
        [HttpPost]
        [Route("uploadimageswthumbnail")]
        [Permission]
        public object UploadWThumbnail([FromUri] string folder)
        {
            Logger.Info("[UploadImagesWThumbnail]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = null;

            // create document and images folders in Upload folder

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Images/" + folder)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Images/" + folder));
            }

            // check file from client
            if (HttpContext.Current.Request.Files.Count < 1)
            {
                retval.Status.Message = "Lỗi";
                retval.Status.Code = -1;
                return retval;
            }
            // check file type
            string[] imageTypes = { ".jpg", ".png", ".gif", ".jpeg" };
            List<string> uniqueFileNames = new List<string>();
            for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                var receivedFile = HttpContext.Current.Request.Files[i];
                if (!imageTypes.Contains(Path.GetExtension(receivedFile.FileName)))
                {
                    retval.Status.Message = "Không hỗ trợ định dạng file";
                    retval.Status.Code = -1;
                    return retval;
                }
            }

            try
            {
                for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var receivedFile = HttpContext.Current.Request.Files[i];
                    // create unique file name
                    //var uniqueFileName = Path.GetFileNameWithoutExtension(receivedFile.FileName) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(receivedFile.FileName);
                    var uniqueFileName = Path.GetFileNameWithoutExtension(receivedFile.FileName) + Path.GetExtension(receivedFile.FileName);
                    var fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads"), folder, uniqueFileName); ;
                    // save file
                    fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Images"), folder, uniqueFileName);
                    receivedFile.SaveAs(fullPath);
                    string fileThumb = Path.GetFileNameWithoutExtension(uniqueFileName) + "_thumbnail" + Path.GetExtension(uniqueFileName);
                    string filePathThumb = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Images"), folder, fileThumb);
                    Image image = Image.FromFile(fullPath);
                    Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                    thumb.Save(filePathThumb);
                    uniqueFileNames.Add(uniqueFileName);
                }
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = uniqueFileNames;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpPost]
        [Route("upload")]
        [Permission]
        public object Upload([FromUri] string fileType, [FromUri] string folder)
        {
            Logger.Info("[UploadFile]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = null;

            // create document and images folders in Upload folder

            if (fileType == "document")
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Documents/" + folder)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Documents/" + folder));
                }
            }
            else if (fileType == "image")
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Images/" + folder)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Images/" + folder));
                }
            }

            // check file from client
            if (HttpContext.Current.Request.Files.Count < 1)
            {
                retval.Status.Message = "Lỗi";
                retval.Status.Code = -1;
                return retval;
            }
            // check file type
            string[] docTypes = { ".xlsx", ".xls", ".doc", ".docx", ".ppt", ".pptx", ".txt", ".pdf" };
            string[] imageTypes = { ".jpg", ".png", ".gif", ".jpeg" };
            
            List<string> uniqueFileNames = new List<string>();
            for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                var receivedFile = HttpContext.Current.Request.Files[i];
                if (fileType == "document")
                {
                    if (!docTypes.Contains(Path.GetExtension(receivedFile.FileName)))
                    {
                        retval.Status.Message = "Không hỗ trợ định dạng file";
                        retval.Status.Code = -1;
                        return retval;
                    }
                }
                else if (fileType == "image")
                {
                    if (!imageTypes.Contains(Path.GetExtension(receivedFile.FileName)))
                    {
                        retval.Status.Message = "Không hỗ trợ định dạng file";
                        retval.Status.Code = -1;
                        return retval;
                    }
                }
            }

            try
            {
                for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var receivedFile = HttpContext.Current.Request.Files[i];
                    // create unique file name
                    var uniqueFileName = Path.GetFileNameWithoutExtension(receivedFile.FileName) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(receivedFile.FileName);
                    //var uniqueFileName = Path.GetFileNameWithoutExtension(receivedFile.FileName) + Path.GetExtension(receivedFile.FileName);
                    var fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads"), folder, uniqueFileName); ;
                    // save file
                    if (fileType == "document" || fileType == "any")
                    {
                        fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Documents"), folder, uniqueFileName);
                        //receivedFile.SaveAs(fullPath);
                    }
                    else if (fileType == "image")
                    {
                        fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Images"), folder, uniqueFileName);
                    }
                    receivedFile.SaveAs(fullPath);
                    uniqueFileNames.Add(uniqueFileName);
                }
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = uniqueFileNames;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpPost]
        [Route("uploadchunk")]
        [Permission]
        public object UploadChunk([FromUri] string folder)
        {
            Logger.Info("[UploadFile]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = null;

            // create document and images folders in Upload folder

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Documents/" + folder)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Documents/" + folder));
            }

            //if (fileType == "document" || fileType == "any")
            //{
            //    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Documents/" + folder)))
            //    {
            //        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Documents/" + folder));
            //    }
            //}
            //else if (fileType == "image")
            //{
            //    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Images/" + folder)))
            //    {
            //        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Images/" + folder));
            //    }
            //}

            //// check file from client
            //if (HttpContext.Current.Request.Files.Count < 1)
            //{
            //    retval.Status.Message = "Lỗi";
            //    retval.Status.Code = -1;
            //    return retval;
            //}
            // check file type
            //string[] docTypes = { ".xlsx", ".xls", ".doc", ".docx", ".ppt", ".pptx", ".txt", ".pdf" };
            //string[] imageTypes = { ".jpg", ".png", ".gif", ".jpeg" };

            //List<string> uniqueFileNames = new List<string>();
            //for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            //{
            //    var receivedFile = HttpContext.Current.Request.Files[i];
            //    if (fileType == "document")
            //    {
            //        if (!docTypes.Contains(Path.GetExtension(receivedFile.FileName)))
            //        {
            //            retval.Status.Message = "Không hỗ trợ định dạng file";
            //            retval.Status.Code = -1;
            //            return retval;
            //        }
            //    }
            //    else if (fileType == "image")
            //    {
            //        if (!imageTypes.Contains(Path.GetExtension(receivedFile.FileName)))
            //        {
            //            retval.Status.Message = "Không hỗ trợ định dạng file";
            //            retval.Status.Code = -1;
            //            return retval;
            //        }
            //    }
            //}

            try
                {

                string fileId = HttpContext.Current.Request.Form["fileId"];

                string nameFile = Path.GetFileNameWithoutExtension(fileId);
                string extensionFile = Path.GetExtension(fileId);

                int chunkNumber = Convert.ToInt32(HttpContext.Current.Request.Form["chunkNumber"]);
                int totalChunks = Convert.ToInt32(HttpContext.Current.Request.Form["totalChunks"]);
                HttpPostedFile chunk = HttpContext.Current.Request.Files["chunk"];

                string tempPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Documents"), folder, nameFile);
                Directory.CreateDirectory(tempPath);
                string chunkFilePath = Path.Combine(tempPath, chunkNumber.ToString());              
                chunk.SaveAs(chunkFilePath);

                string finalFilePath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Documents"), folder, nameFile + "_final" + extensionFile);
                if (chunkNumber == totalChunks - 1)
                {
                    
                    using (var finalFile = File.OpenWrite(finalFilePath))
                    {
                        for (int i = 0; i < totalChunks; i++)
                        {
                            chunkFilePath = Path.Combine(tempPath, i.ToString());
                            byte[] chunkBytes = File.ReadAllBytes(chunkFilePath);
                            finalFile.Write(chunkBytes, 0, chunkBytes.Length);
                        }
                    }
                    Directory.Delete(tempPath, true);
                }

                //for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                //{
                //    var receivedFile = HttpContext.Current.Request.Files[i];
                //    // create unique file name
                //    //var uniqueFileName = Path.GetFileNameWithoutExtension(receivedFile.FileName) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(receivedFile.FileName);
                //    var uniqueFileName = Path.GetFileNameWithoutExtension(receivedFile.FileName) + Path.GetExtension(receivedFile.FileName);
                //    var fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads"), folder, uniqueFileName); ;
                //    // save file
                //    if (fileType == "document" || fileType == "any")
                //    {
                //        fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Documents"), folder, uniqueFileName);
                //        //receivedFile.SaveAs(fullPath);
                //    }
                //    else if (fileType == "image")
                //    {
                //        fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Images"), folder, uniqueFileName);
                //    }
                //    receivedFile.SaveAs(fullPath);
                //    uniqueFileNames.Add(uniqueFileName);
                //}
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = nameFile + "_final" + extensionFile;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpDelete]
        [Route("delete")]
        [Permission]
        public object Delete([FromUri] string fileName, string folder)
        {
            // uploaded files to server saved in Images (.png, .jpg ...), Documents(.docx, .pdf ...) folders
            // get fileName extension
            // after that, find correct file path and delete

            Logger.Info("[DeleteFile]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn() { Code = 0, Message = "Không thành công" };
            retval.Pagination = null;
            retval.Data = null;

            try
            {
                var data = FilesServices.DeleteByFileName(fileName, folder);
                if (data == 1)
                {
                    retval.Status.Message = "Thành công";
                    retval.Status.Code = 1;
                }
                else
                {
                    retval.Status.Message = "File không tồn tại";
                    retval.Status.Code = -1;
                }
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            return retval;
        }

        [HttpGet]
        [Route("downloadreport")]
        public HttpResponseMessage DownloadReport(string fileName)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string[] docTypes = { ".xlsx", ".xls", ".doc", ".docx", ".ppt", ".pptx", ".txt", ".pdf" };

            string filePath = "";
            if (docTypes.Contains(Path.GetExtension(fileName)))
            {
                filePath = Path.Combine(HttpContext.Current.Server.MapPath("/Reports"), fileName);
                File.SetAttributes(filePath, FileAttributes.Normal);
            }

            // Check whether File exists
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", fileName);
                throw new HttpResponseException(response);
            }
            // Read the file into a Byte array
            byte[] bytes = File.ReadAllBytes(filePath);
            // Set the Response Content
            response.Content = new ByteArrayContent(bytes);
            // 
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;
            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
            return response;
        }


        [HttpGet]
        [Route("download")]
        public HttpResponseMessage Download([FromUri] string file, [FromUri] string path)
        {
            FileDetail fileDetail = new FileDetail() { File = file, Path = path };
            return FilesServices.DownloadFile(fileDetail);
        }

        [HttpGet]
        [Route("downloaddocument")]
        [Permission]
        public HttpResponseMessage DownloadDocument(string fileName, string folder)
        {
            // Create HTTP Response
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            // file types
            string[] docTypes = { ".xlsx", ".xls", ".doc", ".docx", ".ppt", ".pptx", ".txt", ".pdf" };
            string[] imageTypes = { ".jpg", ".png", ".gif" };

            string filePath = "";

            if (docTypes.Contains(Path.GetExtension(fileName)))
            {
                filePath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Documents"), folder, fileName);
            }
            else if (imageTypes.Contains(Path.GetExtension(fileName)))
            {
                filePath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Images"), folder, fileName);
            }

            // Check whether File exists
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", fileName);
                return response;
                //throw new HttpResponseException(response);
            }
            // Read the file into a Byte array
            byte[] bytes = File.ReadAllBytes(filePath);
            // Set the Response Content
            response.Content = new ByteArrayContent(bytes);
            //
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;
            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
            return response;
        }

        [HttpPost]
        [Route("upload-tcsp")]
        [Permission]
        public object UploadTCSP([FromUri] string fileType, [FromUri] string folder, [FromUri] string fileName)
        {
            Logger.Info("[UploadFile]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = null;

            // create document and images folders in Upload folder

            if (fileType == "document")
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Documents/" + folder)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Documents/" + folder));
                }
            }
            else if (fileType == "image")
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("/Uploads/Images/" + folder)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Uploads/Images/" + folder));
                }
            }

            // check file from client
            if (HttpContext.Current.Request.Files.Count < 1)
            {
                retval.Status.Message = "Lỗi";
                retval.Status.Code = -1;
                return retval;
            }
            // check file type
            string[] docTypes = { ".xlsx", ".xls", ".doc", ".docx", ".ppt", ".pptx", ".txt", ".pdf" };
            string[] imageTypes = { ".jpg", ".png", ".gif", ".jpeg" };
            List<string> uniqueFileNames = new List<string>();
            for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                var receivedFile = HttpContext.Current.Request.Files[i];
                if (fileType == "document")
                {
                    if (!docTypes.Contains(Path.GetExtension(receivedFile.FileName)))
                    {
                        retval.Status.Message = "Không hỗ trợ định dạng file";
                        retval.Status.Code = -1;
                        return retval;
                    }
                }
                else
                {
                    if (!imageTypes.Contains(Path.GetExtension(receivedFile.FileName)))
                    {
                        retval.Status.Message = "Không hỗ trợ định dạng file";
                        retval.Status.Code = -1;
                        return retval;
                    }
                }
            }

            try
            {
                for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var receivedFile = HttpContext.Current.Request.Files[i];
                    // create unique file name
                    //var uniqueFileName = Path.GetFileNameWithoutExtension(receivedFile.FileName) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(receivedFile.FileName);
                    var uniqueFileName = fileName;
                    var fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads"), folder, uniqueFileName); ;
                    // save file
                    if (fileType == "document")
                    {
                        fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Documents"), folder, uniqueFileName);
                        //receivedFile.SaveAs(fullPath);
                    }
                    else if (fileType == "image")
                    {
                        fullPath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Images"), folder, uniqueFileName);
                    }
                    receivedFile.SaveAs(fullPath);
                    uniqueFileNames.Add(uniqueFileName);
                }
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = uniqueFileNames;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

    }
}