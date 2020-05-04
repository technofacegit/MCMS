using MS.Business;
using MS.Core;
using MS.Web.Code.Attributes;
using MS.Web.Code.LIBS;
using MS.Web.Controllers;
using MS.Web.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MS.Web.Areas.Admin.Conntrollers
{
    [AdminAuthorization]
    public class ImagefileController : AdminBaseController
    {

        public ActionResult Index()
        {
            var tblimagelist = tblImage.Getimages();

            return View(tblimagelist);
        }

        [HttpGet]
        public ActionResult addedit(int? id)
        {
            tblImage tblimage = new tblImage();

            if (id.HasValue && id.Value > 0)
            {
                tblimage = tblImage.Getimage(id.Value);
            }

            return View("_addedit", tblimage);
        }
        [HttpPost]
        public ActionResult addedit(tblImage model, FormCollection FC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                    {
                        String Images = (FC["UploadedImages"]).Trim('#');
                        foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                        {
                            JsonController objjson = new JsonController();
                            string DemoImagefileDirectory = "DemoImagefile";
                            if (model.ImageDirectory == (int)ImageFolder.money)
                            {
                                DemoImagefileDirectory = "money";
                            }
                            else if (model.ImageDirectory == (int)ImageFolder.bebemoney)
                            {
                                DemoImagefileDirectory = "bebemoney";
                            }
                            else if (model.ImageDirectory == (int)ImageFolder.kampanyalar)
                            {
                                DemoImagefileDirectory = "kampanyalar";
                            }
                            else if (model.ImageDirectory == (int)ImageFolder.kategoriler)
                            {
                                DemoImagefileDirectory = "kategoriler";
                            }
                            else if (model.ImageDirectory == (int)ImageFolder.popup)
                            {
                                DemoImagefileDirectory = "popup";
                            }
                            else
                            {
                                model.ImageDirectory = 0;
                            }


                            String ix=objjson.MovePhotos("temp", DemoImagefileDirectory, item);
                            if (!ix.Equals("false"))
                            {
                                tblImage objphotos = new tblImage
                                  {
                                      Image = SiteKeys.DBImagePath+DemoImagefileDirectory+"/" + ix,
                                      ImageDirectory= model.ImageDirectory.Value,
                                      PromoNo=model.PromoNo
                                  };
                                Global.Context.tblImages.AddObject(objphotos);

                            }
                        }
                    }
                    Global.Context.SaveChanges();

                    ShowMessageBox(MessageType.Success, "Image list has been updated successfully!!", false);
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
        public ActionResult AddPhotos()
        {
            return PartialView("_AddPhotos");
        }


        [HttpGet]
        public ActionResult delete(int ID)
        {
            return PartialView("_ModalDelete", new Modal
            {
                Message = "Are you sure want to delete this Image?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Title = "Delete Image" },
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
                var tblImageEntity = tblImage.Getimage(ID);
                List<string> imageList = new List<string>();
                imageList.Add(tblImageEntity.Image);
                tblImageEntity.Delete();

                  string DemoImagefileDirectory = "DemoImagefile";
                          
                            if (tblImageEntity.ImageDirectory >0)
                            {
                                DemoImagefileDirectory = EnumHelper.GetDescription((ImageFolder)tblImageEntity.ImageDirectory);
                            }                           
           
                Common.DeleteImages(imageList.ToArray(), "~/areas/admin/content/images/uploads/" + DemoImagefileDirectory+ "/");
                ShowMessageBox(MessageType.Success, "Image has been deleted successfully!!", false);
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