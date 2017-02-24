using Cyotek.GhostScript.PdfConversion;
using ICSharpCode.SharpZipLib.Zip;
using MS.Business;
using MS.Core;
using MS.Web.Areas.Admin.Models;
using MS.Web.Code.Attributes;
using MS.Web.Code.LIBS;
using MS.Web.Controllers;
using MS.Web.Models.Others;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Areas.Admin.Conntrollers
{
     [AdminAuthorization]
    public class MigroskopController : AdminBaseController
    {
        //
        // GET: /Admin/Migroskop/

        public ActionResult Index()
        {
            var tblimagelist = Campaign.GetMigroskop();
            return View(tblimagelist);
        }



        public static String Decompress(FileInfo fileToDecompress)
        {

            var zipFileName =fileToDecompress.FullName;
            string currentFileName = fileToDecompress.FullName;
            string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

            var targetDir = newFileName;

            FastZip fastZip = new FastZip();
            string fileFilter = null;



            // Will always overwrite if target filenames already exist
            fastZip.ExtractZip(zipFileName, targetDir, fileFilter);

            //object loadedObject = null;

            //FileStream stream = new FileStream(fileToDecompress.FullName, FileMode.Open);

            //BinaryFormatter formatter = new BinaryFormatter();

            //if (stream.Length > 4)
            //{
            //    byte[] data = new byte[4];
            //    stream.Read(data, 0, 4);

            //    if (BitConverter.ToUInt16(data, 0) == 0x8b1f) //GZIP_LEAD_BYTES == 0x8b1f
            //    {
            //        GZipStream decompressor = new GZipStream(stream, CompressionMode.Decompress);
            //        loadedObject = formatter.Deserialize(decompressor); //Exception
            //        decompressor.Close();
            //    }
            //    else { loadedObject = formatter.Deserialize(stream); }
            //}
            //stream.Close();

            //using (FileStream originalFileStream = fileToDecompress.OpenRead())
            //{
            //    string currentFileName = fileToDecompress.FullName;
            //    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

            //    using (FileStream decompressedFileStream = System.IO.File.Create(newFileName))
            //    {
            //        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
            //        {
            //            decompressionStream.Seek(0, SeekOrigin.Begin);
            //            decompressionStream.CopyTo(decompressedFileStream);
            //            Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
            //            return true;
            //        }
            //    }
            //}

            return targetDir;
        }


        [HttpGet]
        public ActionResult addedit(int? id)
        {
            Campaign tblimage = new Campaign();

            if (id.HasValue && id.Value > 0)
            {
                tblimage = Campaign.GetCampaign(id.Value);
            }

            return View("_addedit", tblimage);
        }


        [HttpPost]
        public ActionResult addedit(CampaignViewModel model, FormCollection FC)
        {
            try
            {
                if (model.Discount.Equals("")==false)
                {
                    if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                    {
                        String guid = Guid.NewGuid().ToString();
                        int ix = 1;
                            
                        String Images = (FC["UploadedImages"]).Trim('#');
                        foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                        {
                            JsonController objjson = new JsonController();
                            string DemoImagefileDirectory = "migroskop";
                            String filepath = this.Server.MapPath("~/areas/admin/content/images/uploads/temp/" + item);

                            FileInfo fi=new FileInfo(filepath);

                            String newDirectory = Decompress(fi);




                            FileInfo[] imgs = new DirectoryInfo(newDirectory).GetFiles();

                                System.IO.Directory.CreateDirectory(this.Server.MapPath("~/uploads/migroskop/" + guid));
                                foreach (System.IO.FileInfo bmp in imgs)
                                {
                                    bmp.CopyTo(this.Server.MapPath("~/uploads/migroskop/" + guid + "/" + ix + ".jpg"));
                                    ix++;
                                }

                            

                            //Pdf2Image converter = new Pdf2Image(filepath);
                            //System.Drawing.Bitmap[] imgs=converter.GetImages();

                           
                            //System.IO.Directory.CreateDirectory(this.Server.MapPath("~/uploads/migroskop/"+guid));

                            //foreach (System.Drawing.Bitmap bmp in imgs)
                            //{
                            //    bmp.Save(this.Server.MapPath("~/uploads/migroskop/" + guid+"/"+ ix + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                            //    ix++;
                            //}
                        }

                            Campaign camp = new Campaign();
                            camp.CategoryId = 3;
                            camp.CategoryTag = "migroskop";
                            camp.CreatedBy = SiteSession.TblKullanicilar.KullaniciID;
                            camp.ExtraData = (ix-1).ToString();
                            camp.ImageLink = SiteKeys.DBImagePath + "/migroskop/" + guid + "/pgnumber.jpg";
                            camp.ImageLink2 = camp.ImageLink;
                            camp.OfferName = "Migroskop";
                            camp.OfferDesc = "Migroskop";
                            camp.Discount = model.Discount;
                            camp.PromoNo = "";
                            camp.OfferNo = "";
                            camp.DiscountType = "";
                            camp.DisplayOrder = 0;
                            camp.OptinFlag = false;
                            camp.StartDate = DateTime.Now;
                            camp.EndDate = DateTime.Now.AddYears(1);
                        //fixmc
                            ///camp.EndDate = model.EndDate != null ? model.EndDate : DateTime.Now.AddYears(1);
                        
                            camp.Status = true;

                            foreach (Campaign campx in Campaign.GetCampaigns())
                            {
                                campx.Status = false;
                                
                            }

                            Global.Context.Campaigns.AddObject(camp);
                            
                        
                    }
                    Global.Context.SaveChanges();

                    ShowMessageBox(MessageType.Success, "Migroskop listesi güncellendi!!", false);
                    return NewtonsoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("Index") });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Some problem has occurred while updating the Campaign list!!");
                return CreateModelStateErrors();
            }

            return CreateModelStateErrors();

        }

        [HttpGet]
        public ActionResult AddPdf()
        {
            return PartialView("_AddPdf");
        }


        [HttpGet]
        public ActionResult delete(int ID)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "İlgili migroskop kaydını silmek istiyor musunuz?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Title = "Delete PDF" },
                Footer = new ModalFooter
                {
                    SubmitButtonText = "Yes",
                    CancelButtonText = "No"
                }
            });
        }

        [HttpPost]
        public ActionResult delete(int ID, FormCollection FC)
        {
            try
            {
                Campaign camp=Campaign.GetCampaign(ID);
                Global.Context.Campaigns.DeleteObject(camp);
                Global.Context.SaveChanges();
               
                ShowMessageBox(MessageType.Success, "Migroskop silindi!!", false);
            }
            catch (Exception ex)
            {
                string message = ex.GetBaseException().Message;
                if (message.Contains("DELETE statement conflicted"))
                {
                    message = "You can't delete this because it has some child records.";
                }
                ShowMessageBox(MessageType.Danger, message, false);
            }

            return RedirectToAction("Index");

        }
    }
}
