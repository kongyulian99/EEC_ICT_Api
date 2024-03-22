using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using EEC_ICT.Api.Methods;
using EEC_ICT.Data.Models;

namespace EEC_ICT.Api.Services
{
    public class FilesServices
    {
        public static FileDetail UploadFile(HttpPostedFile fileUpload)
        {
            string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
            string[] docTypes = { ".xlsx", ".xls", ".doc", ".docx", ".ppt", ".pptx", ".txt", ".pdf" };
            string[] imageTypes = { ".jpg", ".png", ".gif" };
            string path;
            // Kiểm tra loại file
            if (docTypes.Contains(fileExtension))
            {
                path = "/Uploads/Documents/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                }
            }
            else if (imageTypes.Contains(fileExtension))
            {
                path = "/Uploads/Images/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                }
            }
            else
            {
                path = "/Uploads/Others/";
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                }
            }

            // Tạo tên file duy nhất
            string uniqueFile = Path.GetFileNameWithoutExtension(fileUpload.FileName).Slugify() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + fileExtension;
            var fullPath = Path.Combine(HttpContext.Current.Server.MapPath(path), uniqueFile);
            // Lưu file
            fileUpload.SaveAs(fullPath);

            return new FileDetail { Path = path, File = uniqueFile, OriginName = fileUpload.FileName };
        }



        public static HttpResponseMessage DownloadFile(FileDetail fileDownload)
        {
            var response = new HttpResponseMessage();
            string fullPath = HttpContext.Current.Server.MapPath(Path.Combine(fileDownload.Path, fileDownload.File));

            // Kiểm tra file có tồn tại
            if (string.IsNullOrEmpty(fileDownload.File) || !File.Exists(fullPath))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", fileDownload.File);
                return response;
                //throw new HttpResponseException(response);
            }
            // Read the file into a Byte array
            byte[] bytes = File.ReadAllBytes(fullPath);
            // Set the Response Content
            response.Content = new ByteArrayContent(bytes);
            //
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileDownload.File;
            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileDownload.File));
            return response;
        }

        public static void Delete(FileDetail fileDetail)
        {
            string fullPath = HttpContext.Current.Server.MapPath(Path.Combine(fileDetail.Path, fileDetail.File));

            // Check if file exists with its full path
            if (File.Exists(fullPath))
            {
                // If file found, delete it
                File.Delete(fullPath);
            }
        }

        public static int DeleteByFileName(string fileName, string folder)
        {
            string filePath = "";
            // file types
            string[] docTypes = { ".xlsx", ".xls", ".doc", ".docx", ".ppt", ".pptx", ".txt", ".pdf" };
            string[] imageTypes = { ".jpg", ".png", ".gif" };

            if (docTypes.Contains(Path.GetExtension(fileName)))
            {
                filePath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Documents"), folder, fileName);
            }
            else if (imageTypes.Contains(Path.GetExtension(fileName)))
            {
                filePath = Path.Combine(HttpContext.Current.Server.MapPath("/Uploads/Images"), folder, fileName);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}