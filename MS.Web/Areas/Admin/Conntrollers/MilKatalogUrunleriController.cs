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
using System.Web.UI.WebControls;

namespace MS.Web.Areas.Admin.Conntrollers
{
     [AdminAuthorization]
    public class MilKatalogUrunleriController : AdminBaseController
    {
       
        public ActionResult Index()
        {
              var milKatalogUrunleri =  MilKatalogUrunleri.GetMilKatalogUrunleries();
              GridView gv = new GridView();
              gv.DataSource = milKatalogUrunleri;
              gv.DataBind();
              Session["milKatalog"] = gv;
              return View(milKatalogUrunleri);
        }

        public ActionResult Download()
        {
            if (Session["milKatalog"] != null)
            {
                return new DownloadFileActionResult((GridView)Session["milKatalog"], "milKatalog.xls");
            }
          
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult addedit(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                MilKatalogUrunleri milKatalogUrunleri = MilKatalogUrunleri.GetMilKatalogUrunleri(id.Value);
                MilKatalogUrunleriViewModel milKatalogUrunleriViewModel = new MilKatalogUrunleriViewModel();
                milKatalogUrunleriViewModel.MilKatalogUrunID = Convert.ToInt32(id);
                milKatalogUrunleriViewModel.UrunAdi = milKatalogUrunleri.UrunAdi;
                milKatalogUrunleriViewModel.Marka = milKatalogUrunleri.Marka;
                milKatalogUrunleriViewModel.KampanyaMetin = milKatalogUrunleri.KampanyaMetin;
                milKatalogUrunleriViewModel.KampanyaID = milKatalogUrunleri.KampanyaID;
                milKatalogUrunleriViewModel.DisplayOrder = milKatalogUrunleri.DisplayOrder.Value;
                milKatalogUrunleriViewModel.UrunGorsel = milKatalogUrunleri.UrunGorsel;
                return PartialView("_addedit", milKatalogUrunleriViewModel);
            }

            return PartialView("_addedit", new MilKatalogUrunleriViewModel());

        }

        [HttpPost]
        public ActionResult addedit(MilKatalogUrunleriViewModel milKatalogUrunleriViewModel, FormCollection FC)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (milKatalogUrunleriViewModel.MilKatalogUrunID > 0)
                    {
                        MilKatalogUrunleri milKatalogUrunleri = MilKatalogUrunleri.GetMilKatalogUrunleri(milKatalogUrunleriViewModel.MilKatalogUrunID);
                        milKatalogUrunleri.DisplayOrder = milKatalogUrunleriViewModel.DisplayOrder;
                        milKatalogUrunleri.KampanyaID = milKatalogUrunleriViewModel.KampanyaID;
                        milKatalogUrunleri.KampanyaMetin = milKatalogUrunleriViewModel.KampanyaMetin;
                        milKatalogUrunleri.Marka = milKatalogUrunleriViewModel.Marka;
                        milKatalogUrunleri.UrunAdi = milKatalogUrunleriViewModel.UrunAdi;
                       

                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();

                                String ix = objjson.MovePhotos("temp", "MilKatalogImage", item);
                                if (!ix.Equals("false"))
                                {
                                //if (objjson.MovePhotos("temp", "MilKatalogImage", item))
                                    milKatalogUrunleri.UrunGorsel = ix;
                                }
                            }
                        }

                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Güncelleme işlemi gerçekleştirilmiştir!!", false);
                       
                    }
                    else
                    {
                        MilKatalogUrunleri milKatalogUrunleri = new MilKatalogUrunleri();
                        milKatalogUrunleri.DisplayOrder = milKatalogUrunleriViewModel.DisplayOrder;
                        milKatalogUrunleri.KampanyaID = milKatalogUrunleriViewModel.KampanyaID;
                        milKatalogUrunleri.KampanyaMetin = milKatalogUrunleriViewModel.KampanyaMetin;
                        milKatalogUrunleri.Marka = milKatalogUrunleriViewModel.Marka;
                        milKatalogUrunleri.UrunAdi = milKatalogUrunleriViewModel.UrunAdi;
                      
                        if (!String.IsNullOrEmpty(FC["UploadedImages"]))
                        {
                            String Images = (FC["UploadedImages"]).Trim('#');
                            foreach (var item in Images.Split(new string[] { "##" }, StringSplitOptions.None))
                            {
                                JsonController objjson = new JsonController();
                                
                                
                                //if (objjson.MovePhotos("temp", "MilKatalogImage", item))
                                //{
                                String ix = objjson.MovePhotos("temp", "MilKatalogImage", item);
                                if (!ix.Equals("false"))
                                {
                                   milKatalogUrunleri.UrunGorsel  = ix;
                                }
                            }
                        }
                        Global.Context.MilKatalogUrunleris.AddObject(milKatalogUrunleri);
                        Global.Context.SaveChanges();
                        ShowMessageBox(MessageType.Success, "Güncelleme işlemi gerçekleştirilmiştir!!", false);
                        
                    }
                    return NewtonsoftJsonResult(new RequestOutcome<string> { RedirectUrl = Url.Action("Index") });
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", "Some problem has occurred while updating the Kazanclar list!!");
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
                Message = "Silmek istediğinizden emin misiniz?",
                Size = ModalSize.Small,
                Header = new ModalHeader { Title = "Mil Kataloğunu Silme" },
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
                var MilKatalogEntity = MilKatalogUrunleri.GetMilKatalogUrunleri(ID);
                List<string> imageList = new List<string>();
                imageList.Add(MilKatalogEntity.UrunGorsel);

                MilKatalogEntity.Delete();
                Common.DeleteImages(imageList.ToArray(), "~/areas/admin/content/images/uploads/MilKatalogImage/");

                ShowMessageBox(MessageType.Success, "Silme işlemi gerçekleştirilmiştir!!", false);
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