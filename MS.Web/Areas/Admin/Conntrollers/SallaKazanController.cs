using MS.Business;
using MS.Core;
using MS.Web.Areas.Admin.Models;
using MS.Web.Code.Attributes;
using MS.Web.Code.LIBS;
using MS.Web.Controllers;
using MS.Web.Models.Others;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MS.Web.Areas.Admin.Conntrollers
{
    [AdminAuthorization]
    public class SallaKazanController : AdminBaseController
    {
       
        public ActionResult Index()
        {
             var sallaKazan = SallaKazan.GetSallakazans();
             return View(sallaKazan);
            
        }

        public ActionResult getactive()
        {
            var sallaKazan = SallaKazan.GetSallakazans().Where(s => s.Durum == true && s.BitisTarih>=DateTime.Now).OrderByDescending(x => x.id).ToList();
            return View("Index", sallaKazan);
        }

        public ActionResult getpassive()
        {
            var sallaKazan = SallaKazan.GetSallakazans().Where(s => s.Durum == false).OrderByDescending(x => x.id).ToList();
            return View("Index",sallaKazan);
        }

        [HttpPost]
        public ActionResult SallaKazanActive(int ID)
        {
            var SallaKazanEntity = SallaKazan.GetSallaKazan(ID);
            SallaKazanEntity.Durum = !SallaKazanEntity.Durum;
            SallaKazanEntity.Save();
            return NewtonsoftJsonResult(new { IsSuccess = true });
        }

        [HttpGet]
        public ActionResult addedit(int? id)
        {
            ViewBag.MetinList = SallaKazanController.GetMetinList();

            if (id.HasValue && id.Value > 0)
            {
                SallaKazan SallaKazanDataById = SallaKazan.GetSallaKazan(id.Value);
                SallaKazanViewModel sallaKazanviewmodel = new SallaKazanViewModel();
                sallaKazanviewmodel.Durum = SallaKazanDataById.Durum.Value;
                sallaKazanviewmodel.id = SallaKazanDataById.id;
                sallaKazanviewmodel.IndirimOran = SallaKazanDataById.IndirimOran.Value;
                sallaKazanviewmodel.KampanyaID = SallaKazanDataById.KampanyaID;
                sallaKazanviewmodel.Kota = SallaKazanDataById.Kota.Value;
                sallaKazanviewmodel.Metin = SallaKazanDataById.Metin;
                sallaKazanviewmodel.UrunAdi = SallaKazanDataById.UrunAdi;
                sallaKazanviewmodel.UrunGorsel = SallaKazanDataById.UrunGorsel;
                ///sallaKazanviewmodel.UrunKodu = Convert.ToDecimal(SallaKazanDataById.UrunKodu.Value);
                sallaKazanviewmodel.BaslangicTarih = SallaKazanDataById.BaslangicTarih.Value;
                sallaKazanviewmodel.BitisTarih = SallaKazanDataById.BitisTarih.Value;
                return PartialView("_addedit", sallaKazanviewmodel);
            }
            SallaKazanViewModel sallaKazanViewModel = new SallaKazanViewModel();
            sallaKazanViewModel.BaslangicTarih = DateTime.Now.Date;
            sallaKazanViewModel.BitisTarih = DateTime.Now.Date;

            return PartialView("_addedit", sallaKazanViewModel);

        }

        [HttpPost]
        public ActionResult addedit(SallaKazanViewModel sallaKazanViewModel, FormCollection FC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sallaKazanViewModel.id > 0)
                    {                      

                        SallaKazan SallaKazanDataById = SallaKazan.GetSallaKazan(sallaKazanViewModel.id);
                        string baslangic = DateTime.ParseExact(sallaKazanViewModel.BaslangicTarih.ToShortDateString(), "d.M.yyyy", CultureInfo.InvariantCulture).ToString("M.d.yyyy", CultureInfo.InvariantCulture);
                        SallaKazanDataById.BaslangicTarih = DateTime.ParseExact(baslangic, "M.d.yyyy", null);
                        string bitis = DateTime.ParseExact(sallaKazanViewModel.BitisTarih.ToShortDateString(), "d.M.yyyy", CultureInfo.InvariantCulture).ToString("M.d.yyyy", CultureInfo.InvariantCulture);
                        SallaKazanDataById.BitisTarih = DateTime.ParseExact(bitis, "M.d.yyyy", null);
                        SallaKazanDataById.Durum = sallaKazanViewModel.Durum;
                        SallaKazanDataById.IndirimOran = sallaKazanViewModel.IndirimOran;
                        SallaKazanDataById.KampanyaID = sallaKazanViewModel.KampanyaID;
                        SallaKazanDataById.Kota = sallaKazanViewModel.Kota;
                        SallaKazanDataById.Metin = sallaKazanViewModel.Metin == null ? "" : sallaKazanViewModel.Metin;
                        SallaKazanDataById.UrunAdi = sallaKazanViewModel.UrunAdi;
                        SallaKazanDataById.UrunKodu = 0;// Convert.ToDouble(sallaKazanViewModel.UrunKodu);
                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "sallakazan", item))
                                //{

                                String ix = objjson.MovePhotos("temp", "sallakazan", item);
                                if (!ix.Equals("false"))
                                {
                                    SallaKazanDataById.UrunGorsel = SiteKeys.DBImagePath + "/sallakazan/" + ix;

                                    string targetPath = System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/kampanyalar/");
                                    string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/sallakazan/" + ix);
                                    string destFile = System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/kampanyalar/" + ix);
                                    if (!Directory.Exists(targetPath))
                                    {
                                        Directory.CreateDirectory(targetPath);
                                    }
                                    System.IO.File.Copy(sourceFile, destFile, true);

                                }

                                else
                                {
                                    SallaKazanDataById.UrunGorsel = item;
                                }

                                
                            }
                        }

                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Güncelleme işlemi gerçekleştirilmiştir!!", false);
                        
                    }
                    else
                    {
                        SallaKazan SallaKazan = new SallaKazan();

                        string baslangic = DateTime.ParseExact(sallaKazanViewModel.BaslangicTarih.ToShortDateString(), "d.M.yyyy", CultureInfo.InvariantCulture).ToString("M.d.yyyy", CultureInfo.InvariantCulture);
                        SallaKazan.BaslangicTarih = DateTime.ParseExact(baslangic, "M.d.yyyy", null);
                        string bitis = DateTime.ParseExact(sallaKazanViewModel.BitisTarih.ToShortDateString(), "d.M.yyyy", CultureInfo.InvariantCulture).ToString("M.d.yyyy", CultureInfo.InvariantCulture);
                        SallaKazan.BitisTarih = DateTime.ParseExact(bitis, "M.d.yyyy", null);
                        //SallaKazan.BaslangicTarih = sallaKazanViewModel.BaslangicTarih;
                        //SallaKazan.BitisTarih = sallaKazanViewModel.BitisTarih;
                        SallaKazan.Durum = sallaKazanViewModel.Durum;
                        SallaKazan.IndirimOran = sallaKazanViewModel.IndirimOran;
                        SallaKazan.KampanyaID = sallaKazanViewModel.KampanyaID;
                        SallaKazan.Kota = sallaKazanViewModel.Kota;
                        SallaKazan.Metin = sallaKazanViewModel.Metin == null ? "" : sallaKazanViewModel.Metin;
                        SallaKazan.UrunAdi = sallaKazanViewModel.UrunAdi;
                        SallaKazan.UrunKodu = 0;// Convert.ToDouble(sallaKazanViewModel.UrunKodu);
                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                //if (objjson.MovePhotos("temp", "sallakazan", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "sallakazan", item);
                                if (!ix.Equals("false"))
                                {
                                    SallaKazan.UrunGorsel = SiteKeys.DBImagePath + "/sallakazan/" + ix;


                                    string targetPath = System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/kampanyalar/");
                                    string sourceFile = System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/sallakazan/" + ix);
                                    string destFile = System.Web.Hosting.HostingEnvironment.MapPath("~/uploads/kampanyalar/" + ix);
                                    if (!Directory.Exists(targetPath))
                                    {
                                        Directory.CreateDirectory(targetPath);
                                    }
                                    System.IO.File.Copy(sourceFile, destFile, true);

                                }
                                else
                                {
                                    SallaKazan.UrunGorsel = item;
                                }
                               
                            }
                        }
                        Global.Context.SallaKazans.AddObject(SallaKazan);
                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Güncelleme işlemi gerçekleştirilmiştir!!", false);

                    }
                    //return RedirectToAction("Index");
                    return NewtonsoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("Index") });
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Some problem has occurred while updating the SallaKazan list!!<br>"+ex.Message);
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
                Message = "Are you sure want to delete this SallaKazan ?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Title = "Delete SallaKazan" },
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
                var SallaKazanEntity = SallaKazan.GetSallaKazan(ID);
                List<string> imageList = new List<string>();
                imageList.Add(SallaKazanEntity.UrunGorsel);

                SallaKazanEntity.Delete();
                Common.DeleteImages(imageList.ToArray(), "~/areas/admin/content/images/uploads/SallaKazanImage/");

                ShowMessageBox(MessageType.Success, "SallaKazan has been deleted successfully!!", false);
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

        public static List<ListItem> GetMetinList()
        {
            var listItems = new List<ListItem>();
            listItems.Add(new ListItem { Text = "% İndirim", Value = "" });
            listItems.Add(new ListItem { Text = "TL", Value = "TL" });
            listItems.Add(new ListItem { Text = "Money", Value = "Money" });
            return listItems;

        }

	}
}