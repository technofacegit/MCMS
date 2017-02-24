using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MS.Web.Code.LIBS
{
    public class Common
    {

        public static string SaveImage(HttpPostedFileBase file, string savepath, out bool IsError)
        {
            try
            {
                string filenameToSave = String.Empty;
                if (file.ContentLength > 0 && !file.FileName.Equals(""))
                {
                    string filename = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                    string ext = Path.GetExtension(filename);
                    filenameToSave = Guid.NewGuid().ToString() + "_" + CleanFileName(filename);
                    if (filenameToSave != "")
                    {

                        string path = System.IO.Path.Combine(ConfigurationManager.AppSettings["PhysicalPath"] + savepath);
                        if (!Directory.Exists(path))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(path);
                        }

                        file.SaveAs(path + "\\" + filenameToSave);
                    }
                    IsError = false;
                    return filenameToSave;
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                return ex.Message;
            }
            IsError = false;
            return String.Empty;
        }

        public static string MoveImage(string copyimage, string savepath)
        {
            try
            {
                if (File.Exists(ConfigurationManager.AppSettings["PhysicalPath"] + savepath + "\\" + copyimage))
                {
                    string filenameToSave = copyimage.Replace(".", "1.");
                    File.Copy(ConfigurationManager.AppSettings["PhysicalPath"] + savepath + "\\" + copyimage, ConfigurationManager.AppSettings["PhysicalPath"] + savepath + "\\" + filenameToSave);
                    return filenameToSave;
                }
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
            return String.Empty;
        }

        public static string DeleteImages(string[] imageList, string physicalPath)
        {
            try
            {
                foreach (var imageName in imageList)
                {
                    FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(physicalPath + "/") + imageName);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        public static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty)).Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", "_").Replace(":", string.Empty).Replace(";", string.Empty).Replace("'", string.Empty);
        }

        /// <summary>
        /// Set default no image 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="defultpath"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string ImageOrDefault(string filepath, string defultpath, int? height = 0, int? width = 0)
        {
            var imageSrc = filepath;
            if (!string.IsNullOrEmpty(filepath))
            {
                imageSrc = File.Exists(HttpContext.Current.Server.MapPath(filepath))
                                   ? filepath : defultpath;
            }
            else
            {
                imageSrc = defultpath;
            }

            if (height > 0 && width > 0)
            {
                imageSrc = imageSrc + "?height=" + height + "&width=" + width;
            }
            else if (height > 0 && width == 0)
            {
                imageSrc = imageSrc + "?height=" + height;
            }
            else if (height == 0 && width > 0)
            {
                imageSrc = imageSrc + "?width=" + width;
            }

            return imageSrc;
        }


        public static bool HtmlToPdfbyContent(string URL, string fileName, string headerUrl = null, string footerUrl = null, params string[] metaInfo)
        {
            // assemble destination PDF file name

            var exePath = HttpContext.Current.Server.MapPath("~/WKHTML/wkhtmltopdf.exe"); //Path to the WKHTMLTOPDF executable.
            var workingDir = HttpContext.Current.Server.MapPath("~/WKHTML");

            var p = new System.Diagnostics.Process
            {
                StartInfo = { FileName = @"""" + exePath + @"""", UseShellExecute = false }
            };

            // String switches = String.Join(" ", metaInfo);
            String switches = URL; //String.Join(" ", URL + " \"" + fileName + "\"");

            if (!String.IsNullOrEmpty(headerUrl)) { switches += " -T 27mm -B 2mm --header-spacing 10 --header-html \"" + Uri.EscapeUriString(headerUrl) + "\""; }
            if (!String.IsNullOrEmpty(footerUrl)) { switches += " -B 9mm --footer-html \"" + Uri.EscapeUriString(footerUrl) + "\""; }
            switches += " --page-size A4";
            switches += " \"" + fileName + "\"";

            p.StartInfo.Arguments = switches; //+ String.Join(" ", URL +" " + fileName);

            p.Start();
            p.WaitForExit();
            int returnCode = p.ExitCode;
            p.Close();

            // if 0 or 2, it worked (not sure about other values, I want a better way to confirm this)
            return (returnCode <= 2);
        }

   
    


    }
}