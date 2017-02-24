using MS.Web.Code.LIBS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Controllers
{
    public class JsonController : BaseController
    {
        #region "Upload Photos"

        public ActionResult UploadPhotos(string folder, bool? isfront = false)
        {
            string filenameToSave = String.Empty;

            #region uploadPhotos
            string currentpath = (isfront.Value == true ? "~/content/images/uploads/" : "~/areas/admin/content/images/uploads/");
            string allfiles = "";
            for (int i = 0; i < HttpContext.Request.Files.Count; i++)
            {
                HttpPostedFileBase file = HttpContext.Request.Files[i];
                if (file.ContentLength == 0 || file.FileName.Equals(""))
                {
                    continue;
                }
                string filename = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                string ext = Path.GetExtension(filename);
                ///filenameToSave = Guid.NewGuid().ToString() + "_" + Common.CleanFileName(filename);
                filenameToSave = Common.CleanFileName(filename); 
                if (allfiles != "")
                {
                    allfiles = allfiles + "";
                }
                else
                {
                    allfiles = allfiles + "";
                }
                if (filenameToSave != "")
                {
                    string path = Server.MapPath(currentpath + folder + "/" + filenameToSave);

                    if (System.IO.File.Exists(path))
                    {
                        filenameToSave = DateTime.Now.Ticks.ToString() + "_" + filenameToSave;
                        path = Server.MapPath(currentpath + folder + "/" + filenameToSave);
                    }

                    //if (System.IO.File.Exists(path))
                    //{
                    //    System.IO.File.Delete(path);
                    //}



                    if (!System.IO.File.Exists(path))
                    {
                        file.SaveAs(path);
                        string[] details = new string[2] { Url.Content(currentpath + folder + "/"), filenameToSave };
                        return Json(details, "text/html", JsonRequestBehavior.AllowGet);
                    }
                }
            }

            #endregion

            return Json(String.Empty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletePhotos(string folder, string filename, bool? isfront = false)
        {
            string currentpath = (isfront.Value == true ? "~/content/images/uploads/" : "~/areas/admin/content/images/uploads/");
            try
            {
                FileInfo file = new FileInfo(Server.MapPath(currentpath + folder + "/") + filename);
                if (file.Exists)
                {
                    file.Delete();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public String MovePhotos(string containsfolder, string movefolder, string filename, bool? isfront = false)
        {
            string currentpath = (isfront.Value == true ? "~/uploads/" : "~/areas/admin/content/images/uploads/");
            try
            {
                FileInfo file = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath(currentpath + containsfolder + "/") + filename);
                FileInfo file2 = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/" + movefolder + "/") + filename);

                if (file2.Exists)
                {
                    filename = DateTime.Now.Ticks.ToString() +"_"+ filename;
                    file2 = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/" + movefolder + "/") + filename);
                }

                if (file2.Exists)
                {
                    file2.Delete();
                }
                if (file.Exists)
                {
                    file.MoveTo(System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/" + movefolder + "/") + filename);
                }
                filename = filename;
                return filename;
            }
            catch (Exception ex)
            {
                filename = filename;
                return "false";
            }
        }

        #endregion

        #region "Upload Pdf"
        public ActionResult UploadPdf(string folder, bool? isfront = false)
        {
            string filenameToSave = String.Empty;
           
            #region uploadPhotos
            string currentpath = (isfront.Value == true ? "~/content/images/uploads/" : "~/areas/admin/content/images/uploads/");
            string allfiles = "";
            for (int i = 0; i < HttpContext.Request.Files.Count; i++)
            {
                HttpPostedFileBase file = HttpContext.Request.Files[i];
                if (file.ContentLength == 0 || file.FileName.Equals(""))
                {
                    continue;
                }
                string filename = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                string ext = Path.GetExtension(filename);
                filenameToSave = Guid.NewGuid().ToString() + "_" + Common.CleanFileName(filename);
                if (allfiles != "")
                {
                    allfiles = allfiles + "";
                }
                else
                {
                    allfiles = allfiles + "";
                }
                if (filenameToSave != "")
                {
                    string path = Server.MapPath(currentpath + folder + "/" + filenameToSave);
                    if (!System.IO.File.Exists(path))
                    {
                        file.SaveAs(path);
                        string[] details = new string[2] { Url.Content(currentpath + folder + "/"), filenameToSave };
                        return Json(details, "text/html", JsonRequestBehavior.AllowGet);
                    }
                }
            }

            #endregion

            return Json(String.Empty, JsonRequestBehavior.AllowGet);
        }

        #endregion


        public ActionResult DeleteLivePhotos(string type, string folder, string photoId, bool? isfront = false)
        {
            string currentpath = (isfront.Value == true ? "~/content/images/uploads/" : "~/areas/admin/content/images/uploads/");
            try
            {
                string filename = string.Empty;
               if (type == "Category")
                {
                    //TrainLine postphoto = TrainLine.GetTrainLineLogo(photoId);
                    //if (postphoto != null)
                    //{
                    //    filename = postphoto.LogoImage;
                    //    postphoto.LogoImage = "";
                    //    postphoto.Save();
                    //}
                }
              FileInfo file = new FileInfo(Server.MapPath(currentpath + folder + "/") + filename);
                if (file.Exists)
                {
                    file.Delete();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}