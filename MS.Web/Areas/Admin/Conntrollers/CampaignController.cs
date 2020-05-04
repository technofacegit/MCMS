using FluentValidation.Results;
using MS.Business;
using MS.Core;
using MS.Web.Areas.Admin.Models;
using MS.Web.Code.Attributes;
using MS.Web.Code.LIBS;
using MS.Web.Code.Validation;
using MS.Web.Controllers;
using MS.Web.Models.Others;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;


namespace MS.Web.Areas.Admin.Conntrollers
{
    [AdminAuthorization]
    public class CampaignController : AdminBaseController
    {
        public ActionResult Index()
        {
            var campaigns = Campaign.GetCampaigns();


            //foreach (Campaign cp in campaigns)
            //{
            //    if (cp.EndDate < DateTime.Now)
            //    {
            //        cp.Status = false;
            //        cp.Save();
            //    }
            //}

            var cats = CampaignCategory.GetCampaignCategories().ToList();
            ViewBag.CampCats = new SelectList(cats, "CategoryID", "CategoryName");

            //        ViewBag.Status = new SelectList(
            //new List<SelectListItem>
            //{
            //    new SelectListItem { Text = "Aktif", Value = "true"},
            //    new SelectListItem { Text = "Pasif", Value = "false"},
            //}, "Value", "Text", 0);

            CampaignsByCategoriesViewModel camp = new CampaignsByCategoriesViewModel();
            camp.Status = true;

            int catId = cats.Select(c => c.CategoryID).First();

            if (camp.CategoryID == null || camp.CategoryID == 0)
                camp.CategoryID = cats.Select(c => c.CategoryID).First();
            camp.Campaigns = Campaign.GetCampaignByCategory(camp.CategoryID).Where(c => c.Status == true).Select(c => new CampaignViewModel
            {
                CampaignID = c.CampaignID,
                CategoryId = c.CategoryId,
                CategoryTag = c.CategoryTag,
                Discount = c.Discount,
                DiscountType = c.DiscountType,
                DisplayOrder = Convert.ToInt32(c.DisplayOrder),
                StartDate = c.StartDate.ToString(),
                Status = Convert.ToBoolean(c.Status),
                EndDate = c.EndDate.ToString(),
                ImageLink = c.ImageLink,
                ImageLink2 = c.ImageLink2,
                OfferName = c.OfferName,
                OfferDesc = c.OfferDesc,
                OptinFlag = Convert.ToBoolean(c.OptinFlag),
                EndTime = Convert.ToDateTime(c.EndDate).ToShortTimeString(),
                StartTime = Convert.ToDateTime(c.StartDate).ToShortTimeString()

            }).OrderBy(x => x.DisplayOrder).ToList();
            //campaigns = Campaign.GetCampaigns().Where(c => c.Status == true && c.EndDate >= DateTime.Now).OrderByDescending(x => x.EndDate).ToList();
            ViewBag.CategaoryName = null;

            ViewBag.CategaoryName = CampaignCategory.GetCampaignCategory(Convert.ToInt32(catId)).CategoryName;

            return View(camp);
        }

        public ActionResult getactivepassive(int catid, string campaignstatus)
        {
            bool campstatus=true;
            //if (Request.QueryString["campaignstatus"].ToString() != "true")
            //    campstatus = false;

            if (campaignstatus == "true")
                campstatus = true;
            else
                campstatus = false;

            string category=Request.Form["CategoryId"];


            var campaigns = Campaign.GetCampaigns();


            //foreach (Campaign cp in campaigns)
            //{
            //    if (cp.EndDate < DateTime.Now)
            //    {
            //        cp.Status = false;
            //        cp.Save();
            //    }
            //}

            var cats = CampaignCategory.GetCampaignCategories().ToList();
            ViewBag.CampCats = new SelectList(cats, "CategoryID", "CategoryName");

            CampaignsByCategoriesViewModel camp = new CampaignsByCategoriesViewModel();
            camp.CategoryID = catid;
            camp.Campaigns = Campaign.GetCampaignByCategory(camp.CategoryID).Where(c => c.Status == Convert.ToBoolean(campstatus)).Select(c => new CampaignViewModel
            {
                CampaignID = c.CampaignID,
                CategoryId = c.CategoryId,
                CategoryTag = c.CategoryTag,
                Discount = c.Discount,
                DiscountType = c.DiscountType,
                DisplayOrder = Convert.ToInt32(c.DisplayOrder),
                StartDate = c.StartDate.ToString(),
                Status = Convert.ToBoolean(c.Status),
                EndDate = c.EndDate.ToString(),
                ImageLink = c.ImageLink,
                ImageLink2 = c.ImageLink2,
                OfferName = c.OfferName,
                OfferDesc = c.OfferDesc,
                OptinFlag = Convert.ToBoolean(c.OptinFlag),
                EndTime = Convert.ToDateTime(c.EndDate).ToShortTimeString(),
                StartTime = Convert.ToDateTime(c.StartDate).ToShortTimeString()

            }).OrderBy(x => x.DisplayOrder).ToList();
            ViewBag.CategoryName = null;

            ViewBag.CategoryName = CampaignCategory.GetCampaignCategory(camp.CategoryID).CategoryName;

            return View("Index", camp);
        }

        [HttpGet]
        public ActionResult AddPhotos()
        {
            return PartialView("_AddPhotos");
        }

        public JsonResult Edit(CampaignsByCategoriesViewModel campaignmodel)
        {
            try
            {
                foreach (var campaign in campaignmodel.Campaigns)
                {
                    var camp = Campaign.GetCampaign(campaign.CampaignID);
                    camp.StartDate = Convert.ToDateTime(campaign.StartDate);
                    camp.EndDate = Convert.ToDateTime(campaign.EndDate);
                    camp.OfferName = campaign.OfferName;
                    camp.OfferDesc = campaign.OfferDesc;
                    camp.OptinFlag = campaign.OptinFlag;
                    camp.Status = campaign.Status;
                    camp.DisplayOrder = campaign.DisplayOrder;
                    camp.Save();

                    EventLog eventLog = new EventLog();
                    eventLog.UserID = SiteSession.TblKullanicilar.KullaniciID;
                    eventLog.EventEntityID = camp.CampaignID;
                    eventLog.EventObjectType = "CAMPAIGN";
                    eventLog.EventType = "UPDATE";
                    eventLog.EventDate = DateTime.Now;
                    eventLog.EventText = DateTime.Now.ToShortDateString() + " tarihinde " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından güncellendi...";

                    eventLog.EventData = Newtonsoft.Json.JsonConvert.SerializeObject(camp);
                    Global.Context.EventLogs.AddObject(eventLog);

                    Global.Context.SaveChanges();
                    var cacheresult = Global.Context.spUpdateCategoryCacheKey(campaignmodel.CategoryID);

                    ClearMobileCache();
                }

                ShowMessageBox(MessageType.Success, "Campaign list has been updated successfully!!", false);
            }
            catch (Exception ex)
            {
                ShowMessageBox(MessageType.Danger, "Kayıt sırasında hata oluştu. Hata: " + ex.Message, false);
            }
            return Json(new { id=campaignmodel.CategoryID, isRedirect = true, IsSuccess = true });
            //return NewtonsoftJsonResult(new { IsSuccess = true });
        }

        [HttpGet]
        public ActionResult AddCampaign(int CategoryId)
        {
            //ViewBag.CategoryList = CampaignCategory.GetCampaignCategories().Where(a => a.Status == true).ToList();
            CampaignViewModel campaignViewModel = new CampaignViewModel();
            campaignViewModel.CategoryId = CategoryId;
            campaignViewModel.CategoryTag = CampaignCategory.GetCampaignCategory(CategoryId).CategoryTag.ToString();
            return View(campaignViewModel);
        }

        [HttpPost]
        public ActionResult AddCampaign(CampaignViewModel campaignViewModel, FormCollection FC)
        {
            try
            {
                if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                {
                    String Images = (FC["UploadedImages"]).Trim('#');
                    string[] images_2 = new string[] {};
                    if (!String.IsNullOrEmpty(FC["UploadedImages2"]))
                    {
                        String Images2 = (FC["UploadedImages2"]).Trim('#');
                        images_2 = Images2.Split(new string[] { "##" }, StringSplitOptions.None);
                    }
                    int i=0;
                    foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                    {
                        var campaign = new Campaign();
                        JsonController objjson = new JsonController();
                        //if (objjson.MovePhotos("temp", "campimage", item))
                        //{
                        String ix = objjson.MovePhotos("temp", "campimage", item);
                        if (!ix.Equals("false"))
                        {
                            campaign.ImageLink = SiteKeys.DBImagePath + "/campimage/" + ix;
                        }
                        else
                        {
                            campaign.ImageLink = item;
                        }

                        if (campaignViewModel.CategoryTag=="gordugunuzeinanin")
                        {
                            //if (objjson.MovePhotos("temp", "campimage", images_2[i].ToString()))
                            //{
                            String ixx = objjson.MovePhotos("temp", "campimage", images_2[i].ToString());
                            if (!ixx.Equals("false"))
                            {
                                campaign.ImageLink2 = SiteKeys.DBImagePath + "/campimage/" + ixx;
                            }
                            else
                            {
                                campaign.ImageLink2 = images_2[i].ToString();
                            }
                            i++;
                        }
                        else
                        {
                            campaign.ImageLink2 = campaign.ImageLink;
                        }

                        campaign.CategoryId = campaignViewModel.CategoryId;
                        campaign.CategoryTag = CampaignCategory.GetCampaignCategory(Convert.ToInt32(campaign.CategoryId)).CategoryTag.ToString();
                        campaign.DisplayOrder = -1;
                        campaign.StartDate = DateTime.Now.AddHours(-4);
                        campaign.EndDate = DateTime.Now.AddDays(15);
                        campaign.Status = true;
                        campaign.OfferName = CampaignCategory.GetCampaignCategory(Convert.ToInt32(campaign.CategoryId)).CategoryName.ToString();
                        campaign.OfferDesc = CampaignCategory.GetCampaignCategory(Convert.ToInt32(campaign.CategoryId)).CategoryName.ToString();
                        campaign.Discount = "";
                        campaign.DiscountType = "";
                        campaign.ExtraData = "";
                        campaign.OfferNo = "";
                        campaign.PromoNo = "";
                        campaign.OptinFlag = false;
                        campaign.CreatedBy = SiteSession.TblKullanicilar.KullaniciID;

                        Global.Context.Campaigns.AddObject(campaign);

                        EventLog eventLog = new EventLog();
                        eventLog.UserID = SiteSession.TblKullanicilar.KullaniciID;
                        eventLog.EventEntityID = campaign.CampaignID;
                        eventLog.EventObjectType = "CAMPAIGN";
                        eventLog.EventType = "INSERT";
                        eventLog.EventDate = DateTime.Now;
                        eventLog.EventText = DateTime.Now.ToShortDateString() + " tarihinde " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından oluşturuldu...";

                        eventLog.EventData = Newtonsoft.Json.JsonConvert.SerializeObject(campaign);
                        Global.Context.EventLogs.AddObject(eventLog);
                        Global.Context.SaveChanges();
                    }
                    var cacheresult = Global.Context.spUpdateCategoryCacheKey(campaignViewModel.CategoryId);
                }


                ClearMobileCache();

                ShowMessageBox(MessageType.Success, "Campaign list has been updated successfully!!", false);
                //return RedirectToAction("Index");
                return RedirectToAction("getactivepassive", new { catid = campaignViewModel.CategoryId, campaignstatus="true" });
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Some problem has occurred while updating the Campaign list!!<br>" + ex.Message);
                return CreateModelStateErrors();
            }

        }




        public void ClearMobileCache()
        {


            HttpWebRequest req = null;
            HttpWebResponse res = null;
            try
            {

                string data = "act=clearcontentcache";

                String url = System.Configuration.ConfigurationManager.AppSettings["CacheUrl"].ToString();
                req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                //req.Timeout = 30000; 

                byte[] dataStream = Encoding.UTF8.GetBytes(data);


                req.ContentLength = dataStream.Length;
                Stream newStream = req.GetRequestStream();
                newStream.Write(dataStream, 0, dataStream.Length);
                newStream.Close();

                res = (HttpWebResponse)req.GetResponse();
                Stream responseStream = res.GetResponseStream();
                var streamReader = new StreamReader(responseStream);

                //Read the response into an xml document 
                String s = streamReader.ReadToEnd();


                ///ShowMessageBox(MessageType.Success, "İçerik devreye alındı.\r\nLütfen Mobil uygulamayı kontrol ediniz", true);
            }
            catch (Exception ex)
            {
                ///ShowMessageBox(MessageType.Warning, "Hata oluştu:" + ex.Message, true);

                // Response.Write(ex.Message);
            }

            //return RedirectToAction("Confirmation");
            //return View("Dashboard");
            // return View("Dashboard");
            // return RedirectToAction("Index");
            ////return View();
        }


        [HttpPost]
        public JsonResult OrderImages(string id, string order)
        {
            var image = Campaign.GetCampaign(Convert.ToInt32(id));
            image.DisplayOrder = Convert.ToInt32(order);
            image.Save();
            return NewtonsoftJsonResult(new { IsSuccess = true });
        }

        [HttpPost]
        public ActionResult CampaignActive(int ID)
        {
            var campaignEntity = Campaign.GetCampaign(ID);
            campaignEntity.Status = !campaignEntity.Status;
            campaignEntity.Save();
            return NewtonsoftJsonResult(new { IsSuccess = true });
        }

        [HttpGet]
        public ActionResult delete(int ID)
        {
            var camp = Campaign.GetCampaign(ID);
            camp.Status = false;
            camp.Save();
            return RedirectToAction("getactivepassive", new { catid = camp.CategoryId, campaignstatus = "true" });
        }

        [HttpGet]
        public ActionResult deletePassive(int ID)
        {
            var camp = Campaign.GetCampaign(ID);
            int categoryid = Convert.ToInt32(camp.CategoryId);
            camp.Delete();
            camp.Save();
            return RedirectToAction("getactivepassive", new { catid = categoryid, campaignstatus = "false" });
        }

        [HttpPost]
        public JsonResult delete(int ID, FormCollection FC)
        {
            try
            {
                var campaignEntity = Campaign.GetCampaign(ID);
                List<string> imageList = new List<string>();
                imageList.Add(campaignEntity.ImageLink);
                campaignEntity.Delete();
                Common.DeleteImages(imageList.ToArray(), "~/areas/admin/content/images/uploads/CampaignImage/");

                ShowMessageBox(MessageType.Success, "Campaign has been deleted successfully!!", false);


                EventLog eventLog = new EventLog();
                eventLog.UserID = SiteSession.TblKullanicilar.KullaniciID;
                eventLog.EventEntityID = ID;
                eventLog.EventObjectType = "CAMPAIGN";
                eventLog.EventType = "DELETE";
                eventLog.EventDate = DateTime.Now;
                eventLog.EventText = DateTime.Now.ToShortDateString() + " tarihinde " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından silindi...";
                eventLog.EventData = Newtonsoft.Json.JsonConvert.SerializeObject(campaignEntity);
                eventLog.Save();
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

            int catid = Convert.ToInt32(Campaign.GetCampaign(ID).CategoryId);

            return Json(new { id=catid, isRedirect = true, IsSuccess = true });
        }

        [HttpGet]
        public ActionResult addeditcampaign(int? id)
        {
            ViewBag.CategoryList = CampaignCategory.GetCampaignCategories().Where(a => a.Status == true).ToList();
            if (id.HasValue && id.Value > 0)
            {
                Campaign campaign = Campaign.GetCampaign(id.Value);
                CampaignViewModel campaignviewmodel = new CampaignViewModel();
                campaignviewmodel.CategoryId = campaign.CategoryId.HasValue ? campaign.CategoryId.Value : (int?)null;
                //campaignviewmodel.CategoryTag = campaign.CategoryTag;
                campaignviewmodel.CampaignID = campaign.CampaignID;
                campaignviewmodel.PromoNo = campaign.PromoNo;
                campaignviewmodel.OfferNo = campaign.OfferNo;
                campaignviewmodel.DisplayOrder = campaign.DisplayOrder.Value;
                campaignviewmodel.OfferDesc = campaign.OfferDesc;
                campaignviewmodel.Status = campaign.Status.Value;
                campaignviewmodel.OfferName = campaign.OfferName;
                campaignviewmodel.Discount = campaign.Discount;
                campaignviewmodel.DiscountType = campaign.DiscountType;
                campaignviewmodel.StartDate = campaign.StartDate.ToString();
                campaignviewmodel.EndDate = campaign.EndDate.ToString();
                campaignviewmodel.StartTime = Convert.ToDateTime(campaign.StartDate).ToString("HH:mm");
                campaignviewmodel.EndTime = Convert.ToDateTime(campaign.EndDate).ToString("HH:mm");
                campaignviewmodel.ImageLink = campaign.ImageLink;
                campaignviewmodel.ImageLink2 = campaign.ImageLink2;
                campaignviewmodel.ExtraData = campaign.ExtraData;
                campaignviewmodel.OptinFlag = campaign.OptinFlag.Value;

                //campaignviewmodel.UpdateLogs = "* Bu kayıt " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından oluşturuldu\r\n";
                List<EventLog> lst = EventLog.GetEventLogsForRecord(campaignviewmodel.CampaignID, "CAMPAIGN").Take(5).ToList();
                foreach (EventLog lg in lst)
                {
                    campaignviewmodel.UpdateLogs += "* " + lg.EventText + "\r\n";
                }
                return PartialView("_addeditcampaign", campaignviewmodel);
            }

            return PartialView("_addeditcampaign", new CampaignViewModel());

        }



        public string RemoveHTML(string strHTML)
        {
            return Regex.Replace(strHTML, "<(.|\n)*?>", "");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addeditcampaign(CampaignViewModel campaignViewModel, FormCollection FC)
        {
            try
            {
                //ModelState.Remove("StartDate");
                //ModelState.Remove("EndDate");

                var validator = new CampaignViewModelValidator();
                var result = validator.Validate(campaignViewModel);

                if (result.IsValid == false)
                {
                    foreach (ValidationFailure vf in result.Errors)
                    {
                        ModelState.AddModelError("", vf.ErrorMessage);
                    }
                    return CreateModelStateErrors();
                }

                if (ModelState.IsValid)
                {

                    var categaory = CampaignCategory.GetCampaignCategory(campaignViewModel.CategoryId.Value);
                    if (campaignViewModel.CampaignID > 0)
                    {
                        Campaign campaign = Campaign.GetCampaign(campaignViewModel.CampaignID);
                        campaign.CategoryId = campaignViewModel.CategoryId;
                        campaign.ImageLink = campaignViewModel.ImageLink != null ? campaignViewModel.ImageLink : FC["UploadedImages"];
                        campaign.ImageLink2 = campaignViewModel.ImageLink2 != null ? campaignViewModel.ImageLink2 : FC["UploadedImages2"];
                        campaign.CategoryTag = categaory != null ? categaory.CategoryTag : "";
                        campaign.Discount = campaignViewModel.Discount != null ? campaignViewModel.Discount : "";
                        campaign.DiscountType = campaignViewModel.DiscountType != null ? campaignViewModel.DiscountType : "";
                        campaign.DisplayOrder = campaignViewModel.DisplayOrder;

                        //string baslangic = DateTime.ParseExact(campaignViewModel.StartDate, "d.M.yyyy", CultureInfo.InvariantCulture).ToString("M.d.yyyy", CultureInfo.InvariantCulture);
                        //campaign.StartDate = DateTime.ParseExact(baslangic, "M.d.yyyy", null);
                        //string bitis = DateTime.ParseExact(campaignViewModel.EndDate, "d.M.yyyy", CultureInfo.InvariantCulture).ToString("M.d.yyyy", CultureInfo.InvariantCulture);
                        //campaign.EndDate = DateTime.ParseExact(bitis, "M.d.yyyy", null);

                        string start_date_time;
                        string end_date_time;
                        try
                        {
                            start_date_time = campaignViewModel.StartDate + " " + campaignViewModel.StartTime;
                            campaign.StartDate = DateTime.ParseExact(start_date_time, "d.M.yyyy HH:mm", CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            start_date_time = campaignViewModel.StartDate + " " + campaignViewModel.StartTime;
                            campaign.StartDate = DateTime.ParseExact(start_date_time, "M.d.yyyy HH:mm", CultureInfo.InvariantCulture);
                        }
                        try
                        {
                            end_date_time = campaignViewModel.EndDate + " " + campaignViewModel.EndTime;
                            campaign.EndDate = DateTime.ParseExact(end_date_time, "d.M.yyyy HH:mm", CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            end_date_time = campaignViewModel.EndDate + " " + campaignViewModel.EndTime;
                            campaign.EndDate = DateTime.ParseExact(end_date_time, "M.d.yyyy HH:mm", CultureInfo.InvariantCulture);
                            //campaign.EndDate = DateTime.ParseExact(campaignViewModel.EndDate, "M.d.yyyy HH:mm", CultureInfo.InvariantCulture);
                        }


                        //campaign.StartDate = campaignViewModel.StartDate.Value;
                        //campaign.EndDate = campaignViewModel.EndDate.Value;
                        campaign.ExtraData = campaignViewModel.ExtraData != null ? campaignViewModel.ExtraData : "";
                        //String s = System.Web.HttpUtility.HtmlDecode(campaignViewModel.OfferDesc);
                        //String ixx=PCLWebUtility.WebUtility.HtmlDecode(campaignViewModel.OfferDesc);
                        //String t = RemoveHTML(campaignViewModel.OfferDesc);

                        campaign.OfferDesc = campaignViewModel.OfferDesc != null ? campaignViewModel.OfferDesc : "";
                        campaign.Status = campaignViewModel.Status;
                        campaign.OfferName = campaignViewModel.OfferName;
                        campaign.OfferNo = campaignViewModel.OfferNo != null ? campaignViewModel.OfferNo : "";
                        campaign.OptinFlag = campaignViewModel.OptinFlag;
                        campaign.PromoNo = campaignViewModel.PromoNo != null ? campaignViewModel.PromoNo : "";



                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "campimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "campimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaign.ImageLink = SiteKeys.DBImagePath + "/campimage/" + ix;
                                }
                                else
                                {
                                    campaign.ImageLink = item;
                                }
                            }
                        }

                        if (!String.IsNullOrEmpty(FC["UploadedImages2"]))
                        {
                            String Images = (FC["UploadedImages2"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "campimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "campimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaign.ImageLink2 = SiteKeys.DBImagePath + "/campimage/" + ix;
                                }
                                else
                                {
                                    campaign.ImageLink2 = item;
                                }
                            }
                        }

                        EventLog eventLog = new EventLog();
                        eventLog.UserID = SiteSession.TblKullanicilar.KullaniciID;
                        eventLog.EventEntityID = campaign.CampaignID;
                        eventLog.EventObjectType = "CAMPAIGN";
                        eventLog.EventType = "UPDATE";
                        eventLog.EventDate = DateTime.Now;
                        eventLog.EventText = DateTime.Now.ToShortDateString() + " tarihinde " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından güncellendi...";

                        eventLog.EventData = Newtonsoft.Json.JsonConvert.SerializeObject(campaign);
                        Global.Context.EventLogs.AddObject(eventLog);

                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Campaign list has been updated successfully!!", false);
                        ClearMobileCache();
                    }
                    else
                    {
                        Campaign campaign = new Campaign();
                        campaign.CategoryId = campaignViewModel.CategoryId;
                        campaign.ImageLink = campaignViewModel.ImageLink != null ? campaignViewModel.ImageLink : "";
                        campaign.ImageLink2 = campaignViewModel.ImageLink2 != null ? campaignViewModel.ImageLink2 : "";
                        campaign.CategoryTag = categaory != null ? categaory.CategoryTag : "";
                        campaign.Discount = campaignViewModel.Discount != null ? campaignViewModel.Discount : "";
                        campaign.DiscountType = campaignViewModel.DiscountType != null ? campaignViewModel.DiscountType : "";
                        campaign.DisplayOrder = campaignViewModel.DisplayOrder;

                        //string baslangic_saat = campaignViewModel.StartDate;
                        //string baslangic = DateTime.ParseExact(campaignViewModel.StartDate, "d.M.yyyy", CultureInfo.InvariantCulture).ToString("M.d.yyyy", CultureInfo.InvariantCulture)+" "+baslangic_saat;

                        string start_date_time;
                        string end_date_time;
                        try
                        {
                            start_date_time = campaignViewModel.StartDate + " " + campaignViewModel.StartTime;
                            campaign.StartDate = DateTime.ParseExact(start_date_time, "d.M.yyyy HH:mm", CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            start_date_time = campaignViewModel.StartDate + " " + campaignViewModel.StartTime;
                            campaign.StartDate = DateTime.ParseExact(start_date_time, "M.d.yyyy HH:mm", CultureInfo.InvariantCulture);
                        }
                        try
                        {
                            end_date_time = campaignViewModel.EndDate + " " + campaignViewModel.EndTime;
                            campaign.EndDate = DateTime.ParseExact(end_date_time, "d.M.yyyy HH:mm", CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            end_date_time = campaignViewModel.EndDate + " " + campaignViewModel.EndTime;
                        }

                        //campaign.StartDate = campaignViewModel.StartDate.Value;
                        //campaign.EndDate = campaignViewModel.EndDate.Value;
                        campaign.ExtraData = campaignViewModel.ExtraData != null ? campaignViewModel.ExtraData : "";
                        campaign.OfferDesc = campaignViewModel.OfferDesc != null ? campaignViewModel.OfferDesc : "";
                        campaign.Status = campaignViewModel.Status;
                        campaign.OfferName = campaignViewModel.OfferName;
                        campaign.OfferNo = campaignViewModel.OfferNo != null ? campaignViewModel.OfferNo : "";
                        campaign.OptinFlag = campaignViewModel.OptinFlag;
                        campaign.PromoNo = campaignViewModel.PromoNo != null ? campaignViewModel.PromoNo : "";
                        campaign.CreatedBy = SiteSession.TblKullanicilar.KullaniciID;

                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "campimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "campimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaign.ImageLink = SiteKeys.DBImagePath + "/campimage/" + ix;
                                }
                                else
                                {
                                    campaign.ImageLink = item;
                                }
                            }
                        }

                        if (!String.IsNullOrEmpty(FC["UploadedImages2"]))
                        {
                            String Images = (FC["UploadedImages2"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "campimage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "campimage", item);
                                if (!ix.Equals("false"))
                                {
                                    campaign.ImageLink2 = SiteKeys.DBImagePath + "/campimage/" + ix;
                                }
                                else
                                {
                                    campaign.ImageLink2 = item;
                                }
                            }
                        }

                        Global.Context.Campaigns.AddObject(campaign);


                        EventLog eventLog = new EventLog();
                        eventLog.UserID = SiteSession.TblKullanicilar.KullaniciID;
                        eventLog.EventEntityID = campaign.CampaignID;
                        eventLog.EventObjectType = "CAMPAIGN";
                        eventLog.EventType = "INSERT";
                        eventLog.EventDate = DateTime.Now;
                        eventLog.EventText = DateTime.Now.ToShortDateString() + " tarihinde " + SiteSession.TblKullanicilar.KullaniciAdi + " tarafından oluşturuldu...";

                        eventLog.EventData = Newtonsoft.Json.JsonConvert.SerializeObject(campaign);
                        Global.Context.EventLogs.AddObject(eventLog);

                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Campaign list has been updated successfully!!", false);

                        ClearMobileCache();
                        // return Redirect("~/admin/CampaignCategory/Index");
                    }
                    //return RedirectToAction("Index");
                    return NewtonsoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("Index") });
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Some problem has occurred while updating the Campaign list!!<br>" + ex.Message);
                return CreateModelStateErrors();
            }
            return CreateModelStateErrors();
        }
    }
}