using MS.Business;
using MS.Core;
using MS.Web.Areas.Admin.Models;
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
    public class CampaignCategoryController : AdminBaseController
    {
        public ActionResult Index()
        {
            var campaigncategories = CampaignCategory.GetCampaignCategories();
            return View(campaigncategories);
        }



        [HttpPost]
        public ActionResult CategoryActive(int ID)
        {
            var campaignEntity = CampaignCategory.GetCampaignCategory(ID);
            campaignEntity.Status = !campaignEntity.Status;
            campaignEntity.Save();

            EventLog eventLog = new EventLog();
            eventLog.UserID = SiteSession.TblKullanicilar.KullaniciID;
            eventLog.EventEntityID = ID;
            eventLog.EventObjectType = "CAMPAIGNCATEGORY";
            eventLog.EventType = "UPDATE";
            eventLog.EventDate = DateTime.Now;
            eventLog.EventText = DateTime.Now.ToShortDateString() + " tarihinde " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından güncellendi...";
                
            eventLog.EventData = Newtonsoft.Json.JsonConvert.SerializeObject(campaignEntity);
            eventLog.Save();

            return NewtonsoftJsonResult(new { IsSuccess = true });
        }


    
        [HttpGet]
        public ActionResult addeditcategory(int? id)
        {
            ViewBag.CategoryTypeList = Kategoritipi.GetCategoryType();
            //ViewBag.DiscountTypeList = new List<SelectListItem>()
            //    {
            //            new SelectListItem() {Text="Money", Value="Money"},
            //            new SelectListItem() {Text="TL", Value="TL"},
            //            new SelectListItem() {Text="Yüzde", Value="%"},

 
            //    };


            if (id.HasValue && id.Value > 0)
            {
                
                CampaignCategory campaigncategory = CampaignCategory.GetCampaignCategory(id.Value);
                CampaignCategoryViewModel campaigncategoryviewmodel = new CampaignCategoryViewModel();
                campaigncategoryviewmodel.CategoryName = campaigncategory.CategoryName;
                campaigncategoryviewmodel.CategoryTag = campaigncategory.CategoryTag;
                campaigncategoryviewmodel.CategoryType = campaigncategory.CategoryType.Value;
                campaigncategoryviewmodel.CategoryBackground = campaigncategory.CategoryBackground;
                campaigncategoryviewmodel.CategoryImage = campaigncategory.CategoryImage;
                campaigncategoryviewmodel.DisplayOrder = campaigncategory.DisplayOrder.Value;
                campaigncategoryviewmodel.ShowLoginPanel = campaigncategory.ShowLoginPanel;
                campaigncategoryviewmodel.Status = campaigncategory.Status;
                campaigncategoryviewmodel.VisibleOnCampPage = campaigncategory.VisibleOnCampPage;
                campaigncategoryviewmodel.VisibleOnMainPage = campaigncategory.VisibleOnMainPage;
                campaigncategoryviewmodel.VisibleOnNotLogin = campaigncategory.VisibleOnNotLogin;
                campaigncategoryviewmodel.CategoryID = campaigncategory.CategoryID;
                campaigncategoryviewmodel.CacheKey = campaigncategory.CacheKey;
                campaigncategoryviewmodel.NewCategoryBackground = campaigncategory.NewCategoryBackground;
                campaigncategoryviewmodel.NewCategoryBoardImage = campaigncategory.NewCategoryBoardImage;
                campaigncategoryviewmodel.OptinText = campaigncategory.OptinText;
                campaigncategoryviewmodel.OptinHoverText = campaigncategory.OptinHoverText;


                //campaigncategoryviewmodel.UpdateLogs = "* Bu kayıt " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından oluşturuldu\r\n";
                List<EventLog> lst = EventLog.GetEventLogsForRecord(campaigncategoryviewmodel.CategoryID, "CAMPAIGNCATEGORY").Take(5).ToList();
                foreach (EventLog lg in lst)
                {
                    campaigncategoryviewmodel.UpdateLogs += "* " + lg.EventText + "\r\n";
                }

                return PartialView("_addeditcategory", campaigncategoryviewmodel);
            }

            return PartialView("_addeditcategory", new CampaignCategoryViewModel());

        }

        [HttpPost]
        public ActionResult addeditcategory(CampaignCategoryViewModel CampaignCategoryViewModel, FormCollection FC)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    if (CampaignCategoryViewModel.CategoryID > 0)
                    {

                        CampaignCategory campaigncategory =  CampaignCategory.GetCampaignCategory(CampaignCategoryViewModel.CategoryID);
                        
                        
                        campaigncategory.CategoryBackground = CampaignCategoryViewModel.CategoryBackground;
                        campaigncategory.CategoryImage = CampaignCategoryViewModel.CategoryImage;
                        campaigncategory.CategoryName = CampaignCategoryViewModel.CategoryName;
                        campaigncategory.CategoryTag = CampaignCategoryViewModel.CategoryTag;
                        campaigncategory.CategoryType = CampaignCategoryViewModel.CategoryType;
                        campaigncategory.DisplayOrder = CampaignCategoryViewModel.DisplayOrder;
                        campaigncategory.ShowLoginPanel = CampaignCategoryViewModel.ShowLoginPanel;
                        campaigncategory.VisibleOnCampPage = CampaignCategoryViewModel.VisibleOnCampPage;
                        campaigncategory.VisibleOnMainPage = CampaignCategoryViewModel.VisibleOnMainPage;
                        campaigncategory.VisibleOnNotLogin = CampaignCategoryViewModel.VisibleOnNotLogin;
                        campaigncategory.Status = CampaignCategoryViewModel.Status;
                        campaigncategory.CategoryID = CampaignCategoryViewModel.CategoryID;
                        campaigncategory.CacheKey = CampaignCategoryViewModel.CacheKey;
                        campaigncategory.NewCategoryBackground = CampaignCategoryViewModel.NewCategoryBackground;
                        campaigncategory.NewCategoryBoardImage = CampaignCategoryViewModel.NewCategoryBoardImage;
                        campaigncategory.OptinText = CampaignCategoryViewModel.OptinText;
                        campaigncategory.OptinHoverText = CampaignCategoryViewModel.OptinHoverText;
                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "categoryimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "categoryimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaigncategory.CategoryImage = SiteKeys.DBImagePath + "/categoryimage/" + ix;
                                }
                                else {
                                    campaigncategory.CategoryImage = item;
                                
                                }
                            }
                        }
                       
                        if (!String.IsNullOrEmpty(FC["UploadedBackImages"]))
                        {
                            String Images = (FC["UploadedBackImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "categorybgimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "categorybgimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaigncategory.CategoryBackground = SiteKeys.DBImagePath + "/categorybgimage/" + ix;
                                }
                                else
                                {
                                    campaigncategory.CategoryBackground = item;

                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(FC["UploadedNewBackImages"]))
                        {
                            String Images = (FC["UploadedNewBackImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "newcategorybgimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "newcategorybgimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaigncategory.NewCategoryBackground = SiteKeys.DBImagePath + "/newcategorybgimage/" + ix;
                                }
                                else
                                {
                                    campaigncategory.NewCategoryBackground = item;

                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(FC["UploadedNewBoardImages"]))
                        {
                            String Images = (FC["UploadedNewBoardImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "newcategoryimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "newcategoryimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaigncategory.NewCategoryBoardImage = SiteKeys.DBImagePath + "/newcategoryimage/" + ix;
                                }
                                else
                                {
                                    campaigncategory.NewCategoryBoardImage = item;

                                }
                            }
                        }
                        Global.Context.SaveChanges();

                        EventLog eventLog = new EventLog();
                        eventLog.UserID = SiteSession.TblKullanicilar.KullaniciID;
                        eventLog.EventEntityID = campaigncategory.CategoryID;
                        eventLog.EventObjectType = "CAMPAIGNCATEGORY";
                        eventLog.EventType = "UPDATE";
                        eventLog.EventDate = DateTime.Now;
                        eventLog.EventText = DateTime.Now.ToShortDateString() + " tarihinde " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından güncellendi...";
                
                        eventLog.EventData = Newtonsoft.Json.JsonConvert.SerializeObject(campaigncategory);
                        eventLog.Save();

                        ShowMessageBox(MessageType.Success, "Category list has been updated successfully!!", false);
                       
                    }
                    else
                    {
                        CampaignCategory campaigncategory = new CampaignCategory();
                        campaigncategory.CategoryBackground = CampaignCategoryViewModel.CategoryBackground;
                      
                        campaigncategory.CategoryName = CampaignCategoryViewModel.CategoryName;
                        campaigncategory.CategoryTag = CampaignCategoryViewModel.CategoryTag;
                        campaigncategory.CategoryType = CampaignCategoryViewModel.CategoryType;
                        campaigncategory.DisplayOrder = CampaignCategoryViewModel.DisplayOrder;
                        campaigncategory.ShowLoginPanel = CampaignCategoryViewModel.ShowLoginPanel;
                        campaigncategory.VisibleOnCampPage = CampaignCategoryViewModel.VisibleOnCampPage;
                        campaigncategory.VisibleOnMainPage = CampaignCategoryViewModel.VisibleOnMainPage;
                        campaigncategory.VisibleOnNotLogin = CampaignCategoryViewModel.VisibleOnNotLogin;
                        campaigncategory.Status = CampaignCategoryViewModel.Status;
                        campaigncategory.CreatedBy = SiteSession.TblKullanicilar.KullaniciID;
                        campaigncategory.NewCategoryBackground = CampaignCategoryViewModel.NewCategoryBackground;
                        campaigncategory.NewCategoryBoardImage = CampaignCategoryViewModel.NewCategoryBoardImage;
                        campaigncategory.OptinText = CampaignCategoryViewModel.OptinText;
                        campaigncategory.OptinHoverText = CampaignCategoryViewModel.OptinHoverText;

                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "categoryimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "categoryimage",item);
                                if (!ix.Equals("false"))
                                {
                                    campaigncategory.CategoryImage = SiteKeys.DBImagePath + "/categoryimage/" + ix;
                                }
                            }
                        }

                        if (!String.IsNullOrEmpty(FC["UploadedBackImages"]))
                        {
                            String Images = (FC["UploadedBackImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "categorybgimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "categorybgimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaigncategory.CategoryBackground = SiteKeys.DBImagePath + "/categorybgimage/" + ix;
                                }
                            }
                        }
                        if (!String.IsNullOrEmpty(FC["UploadedNewBoardImages"]))
                        {
                            String Images = (FC["UploadedNewBoardImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "newcategoryimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "newcategoryimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaigncategory.NewCategoryBoardImage = SiteKeys.DBImagePath + "/newcategoryimage/" + ix;
                                }
                            }
                        }

                        if (!String.IsNullOrEmpty(FC["UploadedNewBackImages"]))
                        {
                            String Images = (FC["UploadedNewBackImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "newcategorybgimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "newcategorybgimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaigncategory.NewCategoryBoardImage =  SiteKeys.DBImagePath + "/newcategorybgimage/" + ix;
                                }
                            }
                        }
                        Global.Context.CampaignCategories.AddObject(campaigncategory);
                        Global.Context.SaveChanges();

                        EventLog eventLog = new EventLog();
                        eventLog.UserID = SiteSession.TblKullanicilar.KullaniciID;
                        eventLog.EventEntityID = campaigncategory.CategoryID;
                        eventLog.EventObjectType = "CAMPAIGNCATEGORY";
                        eventLog.EventType = "INSERT";
                        eventLog.EventDate = DateTime.Now;
                        eventLog.EventText = DateTime.Now.ToShortDateString() + " tarihinde " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından olusturuldu...";
                
                        eventLog.EventData = Newtonsoft.Json.JsonConvert.SerializeObject(campaigncategory);
                        eventLog.Save();

                        ShowMessageBox(MessageType.Success, "Category list has been updated successfully!!", false);



                    }

                   // return NewtonsoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("Index") });
                }
                //return RedirectToAction("Index");

                return NewtonsoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("Index") });
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Some problem has occurred while updating the Category list!!");
                return CreateModelStateErrors();
            }
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
                Message = "Are you sure want to delete this category ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Title = "Delete category" },
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
                var categoryEntity = CampaignCategory.GetCampaignCategory(ID);
                List<string> imageList = new List<string>();
                imageList.Add(categoryEntity.CategoryImage);
                imageList.Add(categoryEntity.CategoryBackground);
                categoryEntity.Delete();
                Common.DeleteImages(imageList.ToArray(), "~/areas/admin/content/images/uploads/CategoryImage/");

                EventLog eventLog = new EventLog();
                eventLog.UserID = SiteSession.TblKullanicilar.KullaniciID;
                eventLog.EventEntityID = categoryEntity.CategoryID;
                eventLog.EventObjectType = "CAMPAIGNCATEGORY";
                eventLog.EventType = "DELETE";
                eventLog.EventDate = DateTime.Now;
                eventLog.EventText = DateTime.Now.ToShortDateString() + " tarihinde " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından silindi...";
                
                eventLog.EventData = Newtonsoft.Json.JsonConvert.SerializeObject(categoryEntity);
                eventLog.Save();

                ShowMessageBox(MessageType.Success, "Category has been deleted successfully!!", false);
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

           // return NewtonsoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("Index") });

            return RedirectToAction("Index");
        }



    }
}